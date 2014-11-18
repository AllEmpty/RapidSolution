using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using DotNet.Utilities;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using SubSonic.Query;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：OnlineUsersBll.cs
 *   描    述：用户在线列表缓存管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// OnlineUsersBll逻辑类
    /// </summary>
    public partial class OnlineUsersBll : LogicBase
    {
        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        #region 获取在线用户表实体
        /// <summary>
        /// 根据字段名称，获取当前用户在线表中的内容
        /// </summary>
        /// <returns></returns>
        public DataAccess.Model.OnlineUsers GetOnlineUsersModel(string userHashKey = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userHashKey))
                {
                    userHashKey = GetUserHashKey();
                }

                //如果不存在在线表则退出
                if (string.IsNullOrEmpty(userHashKey))
                    return null;

                //返回指定字段的内容
                var model = GetModelForCache(x => x.UserHashKey == userHashKey);

                return model;
            }
            catch (Exception e)
            {
                //记录出错日志
                CommonBll.WriteLog("", e);
            }

            return null;
        }
        #endregion

        #region 获取在线用户表内容
        /// <summary>
        /// 根据字段名称，获取指定用户在线表中的内容
        /// </summary>
        /// <param name="userHashKey">用户在线列表的Key</param>
        /// <param name="colName">字段名<para/>
        /// userId : 当前用户的ID编号<para/>
        /// LoginDate : 登录时间<para/>
        /// OnlineTime : 在线时长<para/>
        /// LoginIp : 当前用户IP<para/>
        /// LoginName : 当前用户登陆名<para/>
        /// CName : 当前用户中文名<para/>
        /// Sex : 当前用户的性别<para/>
        /// BranchId : 部门自动ID<para/>
        /// BranchCode : 部门编码<para/>
        /// BranchName : 部门名称<para/>
        /// PositionInfoId : 职位ID<para/>
        /// PositionInfoName : 职位名称<para/>
        /// </param>
        /// <returns></returns>
        public object GetUserOnlineInfo(string userHashKey, string colName)
        {
            try
            {
                if (colName == "")
                {
                    return null;
                }

                //返回指定字段的内容
                var model = GetOnlineUsersModel(userHashKey);
                if (model == null)
                    return null;

                return GetFieldValue(model, colName);
            }
            catch (Exception e)
            {
                //记录出错日志
                CommonBll.WriteLog("", e);
            }

            return null;
        }
        #endregion

        #region 更新在线用户信息
        /// <summary>
        /// 更新在线用户信息
        /// </summary>
        /// <param name="userHashKey">用户在线Hashtable Key</param>
        /// <param name="colName">要更新的字段名称</param>
        /// <param name="value">将要赋的值</param>
        public void UpdateUserOnlineInfo(string userHashKey, string colName, object value)
        {
            try
            {
                SetModelValue(GetOnlineUsersModel(userHashKey), colName, value);
            }
            catch (Exception e)
            { //记录出错日志
                CommonBll.WriteLog("", e);
            }
        }
        #endregion

        #region 更新在线用户缓存表中最后在线时间
        /// <summary>
        /// 更新在线用户缓存表中最后在线时间
        /// </summary>
        public void UpdateTime()
        {
            try
            {
                //更新数据库与缓存中的最后在线时间
                UpdateValue(null, GetOnlineUsersId(), OnlineUsersTable.UpdateTime, DateTime.Now, "", true, false);

                //修改在线缓存表中的用户最后在线时间
                //UpdateUserOnlineInfo(GetUserHashKey(), OnlineUsersTable.UpdateTime, DateTime.Now);
            }
            catch (Exception e)
            {
                //记录出错日志
                CommonBll.WriteLog("", e);
            }
        }
        #endregion

        #region 获取在线人数
        /// <summary>
        /// 获取在线人数
        /// </summary>
        /// <returns></returns>
        public int GetUserOnlineCount()
        {
            var onlineUsers = GetList();
            return onlineUsers == null ? 0 : onlineUsers.Count;
        }
        #endregion

        #region 检查在线列表
        /// <summary>
        /// 检查在线列表，将不在线人员删除
        /// </summary>
        public void CheckOnline()
        {
            //获取在线列表
            var onlineUsers = GetList();

            //如果不存在在线表则退出
            if (onlineUsers == null || onlineUsers.Count == 0)
                return;
            
            //循环读取在线信息
            foreach (var model in onlineUsers)
            {
                //判断该用户最后更新时间是否已经有10分钟未更新，是的话则不将其添加到缓存中
                if (TimeHelper.DateDiff("n", model.UpdateTime, DateTime.Now) > 10)
                {
                    //添加用户下线记录
                    LoginLogBll.GetInstence().Save(model.UserHashKey, "用户【{0}】退出系统！在线时间【{1}】");
                    //移除在线数据
                    Delete(null, model.Id);
                }
            }
        }
        #endregion

        #region 判断用户是否被强迫离线
        /// <summary>
        /// 判断用户是否被强迫离线[true是；false否]
        /// </summary>
        public bool IsOffline(Page page)
        {
            try
            {
                //获取当前用户Id
                var userinfoId = GetManagerId();

                //判断当前用户是否已经被系统清除
                if (userinfoId == 0)
                {
                    //通知用户
                    FineUI.Alert.Show("您太久没有操作已退出系统，请重新登录！", "检测通知", MessageBoxIcon.Information, "window.location.href='/WebManage/Login.aspx';");
                    return true;
                }
                else
                {
                    //判断在线用户的Md5与当前用户存储的Md5是否一致
                    if (GenerateMd5() != CookieHelper.GetCookieValue(OnlineUsersTable.Md5))
                    {
                        //添加用户下线记录
                        LoginLogBll.GetInstence().Save(userinfoId, "用户【{0}】的账号已经在另一处登录，本次登陆下线！在线时间【{1}】");

                        //清除在线表里与当前用户同名的记录
                        Delete(null, x => x.Id == userinfoId);

                        //清空Session
                        SessionHelper.RemoveSession(OnlineUsersTable.UserHashKey);
                        SessionHelper.RemoveSession(OnlineUsersTable.Md5);
                        SessionHelper.RemoveSession(PositionTable.PagePower);
                        SessionHelper.RemoveSession(PositionTable.ControlPower);
                        //删除Cookies
                        CookieHelper.ClearCookie(OnlineUsersTable.UserHashKey);
                        CookieHelper.ClearCookie(OnlineUsersTable.Md5);

                        CommonBll.WriteLog("当前帐号已经下线，用户Id【" + userinfoId + "】");

                        //通知用户
                        FineUI.Alert.Show("您的账号已经在另一处登录，当前账号已经下线！", "检测通知", MessageBoxIcon.Information, "window.location.href='/WebManage/Login.aspx';");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CommonBll.WriteLog("Logic.Systems.Manager.CheckIsOffline出现异常", ex);

                FineUI.Alert.Show("系统已经开始更新维护，请稍后重新登录！", "检测通知", MessageBoxIcon.Information, "window.location.href='/WebManage/Login.aspx';");
                return true;
            }

        }
        #endregion

        #region 判断用户是否超时退出

        /// <summary>
        /// 判断用户是否超时退出（退出情况：1.系统更新，2.用户自动退出）
        /// </summary>
        public void IsTimeOut()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session[OnlineUsersTable.UserHashKey] == null)
            {
                try
                {
                    //不存在则表示Session失效了，重新从Cookies中加载
                    var userHashKey = CookieHelper.GetCookieValue(OnlineUsersTable.UserHashKey);
                    var md5 = CookieHelper.GetCookieValue(OnlineUsersTable.Md5);

                    //判断Cookies是否存在，存在则查询在线列表，重新获取用户信息
                    if (userHashKey.Length > 0 && md5.Length == 32)
                    {
                        //读取当前用户在线实体
                        var model = GetOnlineUsersModel(userHashKey);
                        //当前用户存在在线列表中
                        if (model != null)
                        {
                            //计算用户md5值
                            var key = GenerateMd5(model);

                            //判断用户的md5值是否正确
                            if (md5 == key)
                            {
                                //将UserHashKey存储到缓存中
                                HttpContext.Current.Session[OnlineUsersTable.UserHashKey] = userHashKey;
                                //获取用户权限并存储到用户Session里
                                PositionBll.GetInstence().SetUserPower(model.Position_Id);
                                //更新用户当前SessionId到在线表中
                                //UpdateUserOnlineInfo(model.Id + "", OnlineUsersTable.SessionId, HttpContext.Current.Session.SessionID);

                                return;
                            }
                            //添加用户下线记录
                            LoginLogBll.GetInstence().Save(model.Id, "用户【{0}】的账号已经在另一处登录，本次登陆下线！在线时间【{1}】");

                            //清除在线表里与当前用户同名的记录
                            Delete(null, x => x.Id == model.Id);

                            //清空Session
                            SessionHelper.RemoveSession(OnlineUsersTable.UserHashKey);
                            SessionHelper.RemoveSession(OnlineUsersTable.Md5);
                            SessionHelper.RemoveSession(PositionTable.PagePower);
                            SessionHelper.RemoveSession(PositionTable.ControlPower);
                            //删除Cookies
                            CookieHelper.ClearCookie(OnlineUsersTable.UserHashKey);
                            CookieHelper.ClearCookie(OnlineUsersTable.Md5);
                        }
                        else
                        {
                            //删除数据库记录与IIS缓存
                            Delete(null, x => x.UserHashKey == userHashKey);
                            //清空Session
                            SessionHelper.RemoveSession(OnlineUsersTable.UserHashKey);
                            SessionHelper.RemoveSession(OnlineUsersTable.Md5);
                            SessionHelper.RemoveSession(PositionTable.PagePower);
                            SessionHelper.RemoveSession(PositionTable.ControlPower);
                            //删除Cookies
                            CookieHelper.ClearCookie(OnlineUsersTable.UserHashKey);
                            CookieHelper.ClearCookie(OnlineUsersTable.Md5);
                        }
                    }
                }
                catch (Exception e)
                {
                    //出现异常，保存出错日志信息
                    CommonBll.WriteLog("", e);
                }
                
                //用户不存在，直接退出
                //FineUI.Alert.Show("当前用户登录已经过时或系统已更新,请重新登录！", "检测通知", MessageBoxIcon.Information, "top.location='Login.aspx'");
                //DotNet.Utilities.JsHelper.AlertAndParentUrl("当前用户登录已经过时或系统已更新,请重新登录！", "Login.aspx");
                HttpContext.Current.Response.Redirect("/WebManage/Login.aspx");
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #region 管理员退出系统
        /// <summary>
        /// 用户点击退出系统时，调用本函数，本函数将在在线用户表中删除当前用户，并添加用户退出日志
        /// </summary>
        public void UserExit(Page page)
        {
            try
            {
                //获取用户Hashtable Key
                var userHashKey = GetUserHashKey();
                //判断用户的Session["UserHashKey"]是否存在，即用户是否TimeOut退出了
                if (userHashKey != null)
                {
                    //添加用户退出日志
                    LoginLogBll.GetInstence().Save(userHashKey + "", "用户【{0}】退出系统！在线时间【{1}】");

                    //删除数据库记录与IIS缓存
                    Delete(page, x => x.UserHashKey == userHashKey);
                    //清空Session
                    SessionHelper.RemoveSession(OnlineUsersTable.UserHashKey);
                    SessionHelper.RemoveSession(OnlineUsersTable.Md5);
                    SessionHelper.RemoveSession(PositionTable.PagePower);
                    SessionHelper.RemoveSession(PositionTable.ControlPower);
                    //删除Cookies
                    CookieHelper.ClearCookie(OnlineUsersTable.UserHashKey);
                    CookieHelper.ClearCookie(OnlineUsersTable.Md5);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 将指定用户踢下线，并添加用户退出日志
        /// </summary>
        public void UserExit(Page page, string userHashKey)
        {
            var model = GetOnlineUsersModel(userHashKey);
            if (model != null)
            {
                //添加用户退出日志
                LoginLogBll.GetInstence().Save(userHashKey, "用户【" + model.Manager_CName + "】给管理员【" + GetManagerCName() + "】踢出系统！在线时间【{1}】");

                //删除数据库记录与IIS缓存
                Delete(page, model.Id);
            }
        }

        #endregion

        #region 生成加密串——用户加密密钥计算
        /// <summary>
        /// 生成加密串——用户加密密钥计算
        /// </summary>
        /// <param name="model">在线实体</param>
        /// <returns></returns>
        public string GenerateMd5(DataAccess.Model.OnlineUsers model)
        {
            if (model == null)
            {
                return RandomHelper.GetRndKey();
            }
            else
            {
                //Md5(密钥+登陆帐号+密码+IP+密钥.Substring(6,8))
                //return Encrypt.Md5(model.UserKey + model.Manager_LoginName + model.Manager_LoginPass +
                //            IpHelper.GetUserIp() + model.UserKey.Substring(6, 8));
                return Encrypt.Md5(model.UserKey + model.Manager_LoginName + model.Manager_LoginPass + model.UserKey.Substring(6, 8));
            }
        }

        /// <summary>
        /// 生成加密串——用户加密密钥计算
        /// </summary>
        /// <param name="model">在线实体</param>
        /// <returns></returns>
        public string GenerateMd5(OnlineUsers model)
        {
            if (model == null)
            {
                return RandomHelper.GetRndKey();
            }
            else
            {
                return Encrypt.Md5(model.UserKey + model.Manager_LoginName + model.Manager_LoginPass + model.UserKey.Substring(6, 8));
            }
        }

        /// <summary>
        /// 生成加密串——用户加密密钥计算，直接读取当前用户实体
        /// </summary>
        /// <returns></returns>
        public string GenerateMd5()
        {
            //读取当前用户实体
            var model = GetOnlineUsersModel();
            return GenerateMd5(model);
        }
        #endregion

        #region 获取当前在线用户Id
        /// <summary>
        /// 获取当前在线用户Id
        /// </summary>
        /// <returns></returns>
        public int GetOnlineUsersId()
        {
            var id = SessionHelper.GetSession("OnlineUsersId");
            if (id == null)
            {
                id = GetUserOnlineInfo(GetUserHashKey(), "OnlineUsersId") + "";

                SessionHelper.SetSession(OnlineUsersTable.Id, id);
            }

            return ConvertHelper.Cint0(id);
        }
        #endregion

        #region 获取当前管理员Id
        /// <summary>
        /// 从缓存中读取当前管理员Id
        /// </summary>
        /// <returns></returns>
        public int GetManagerId()
        {
            var id = SessionHelper.GetSession(OnlineUsersTable.Manager_Id);
            if (id == null)
            {
                id = GetUserOnlineInfo(GetUserHashKey(), OnlineUsersTable.Manager_Id) + "";

                SessionHelper.SetSession(OnlineUsersTable.Manager_Id, id);
            }

            return ConvertHelper.Cint0(id);
        }
        #endregion

        #region 获取用户中文名称
        /// <summary>
        /// 从Session中读取用户中文名称,如果Session为Null时,返回""
        /// </summary>
        /// <returns></returns>
        public string GetManagerCName()
        {
            var name = SessionHelper.GetSessionString(OnlineUsersTable.Manager_CName);
            if (string.IsNullOrEmpty(name))
            {
                name = GetUserOnlineInfo(GetUserHashKey(), OnlineUsersTable.Manager_CName) + "";

                SessionHelper.SetSession(OnlineUsersTable.Manager_CName, name);
            }

            return name;
        }

        /// <summary>
        /// 从Session中读取用户中文名称,如果Session为Null时,返回""
        /// </summary>
        /// <returns></returns>
        public string GetManagerCName(int userId)
        {
            if (userId == 0)
            {
                return "";
            }

            var model = GetModelForCache(x => x.Manager_Id == userId);
            if (model == null)
            {
                return "";
            }
            else
            {
                return model.Manager_CName;
            }
        }
        #endregion

        #region 获取用户UserHashKey
        /// <summary>
        /// 获取用户UserHashKey
        /// </summary>
        /// <returns></returns>
        public string GetUserHashKey()
        {
            //读取Session中存储的UserHashKey值
            var userHashKey = SessionHelper.GetSession(OnlineUsersTable.UserHashKey);
            //如果为null
            if (userHashKey == null)
            {
                //为null则表示用户Session过期了，所以要检查用户登陆，避免用户权限问题
                IsTimeOut();
            }
            return SessionHelper.GetSessionString(OnlineUsersTable.UserHashKey);
        }
        #endregion

        #region 获取用户加密串——用户加密密钥Md5值
        /// <summary>
        /// 获取用户加密串——用户加密密钥Md5值
        /// </summary>
        /// <returns></returns>
        public string GetMd5()
        {
            //读取Session中存储的Md5值
            var md5 = SessionHelper.GetSessionString(OnlineUsersTable.Md5);
            //如果为null
            if (string.IsNullOrEmpty(md5))
            {
                //由于GenerateMd5()获取在线实体时，会执行IsTimeOut()函数，所以这里注释掉
                //为null则表示用户Session过期了，所以要检查用户登陆，避免用户权限问题
                //IsTimeOut();
                md5 = GenerateMd5();

                SessionHelper.SetSession(OnlineUsersTable.Md5, md5);
            }

            return md5;
        }

        /// <summary>
        /// 获取用户加密串——用户加密密钥Md5值
        /// </summary>
        /// <returns></returns>
        public string GetMd5(int userId)
        {
            if (userId == 0)
            {
                return "";
            }

            var model = GetModelForCache(x => x.Manager_Id == userId);
            if (model == null)
            {
                return "";
            }
            else
            {
                return model.Md5;
            }
        }
        #endregion

        #region 获取用户页面操作权限
        /// <summary>
        /// 获取用户页面操作权限
        /// </summary>
        /// <returns></returns>
        public string GetPagePower()
        {
            //读取Session中存储的PagePower值
            var pagePower = SessionHelper.GetSession(PositionTable.PagePower);
            //如果为null
            if (pagePower == null)
            {
                //获取用户权限并存储到用户Session里
                PositionBll.GetInstence().SetUserPower(GetUserOnlineInfo(GetUserHashKey(), OnlineUsersTable.Position_Id) + "");
            }
            pagePower = SessionHelper.GetSession(PositionTable.PagePower);
            return pagePower + "";
        }
        #endregion

        #region 获取用户页面控件（按键）操作权限
        /// <summary>
        /// 获取用户页面控件（按键）操作权限
        /// </summary>
        /// <returns></returns>
        public string GetControlPower()
        {
            //读取Session中存储的ControlPower值
            var controlPower = SessionHelper.GetSession(PositionTable.ControlPower);
            //如果为null
            if (controlPower == null)
            {
                //获取用户权限并存储到用户Session里
                PositionBll.GetInstence().SetUserPower(GetUserOnlineInfo(GetUserHashKey(), OnlineUsersTable.Position_Id) + "");
            }
            controlPower = SessionHelper.GetSession(PositionTable.ControlPower);
            return controlPower + "";
        }
        #endregion

        #endregion 自定义函数
    }
}
