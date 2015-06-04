using System;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：LoginLogBll.cs
 *   描    述：用户登陆日志逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// LoginLog（用户登陆日志）逻辑类
    /// </summary>
    public partial class LoginLogBll : LogicBase
    {

        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        #region 添加用户登陆日志
        /// <summary>
        /// 添加用户登陆日志
        /// </summary>
        /// <param name="userHashKey">登录用户在线列表的HashTable Key</param>
        /// <param name="notes">用户登录内容备注，{0}=用户名称，{1}=用户在线时间</param>
        public void Save(string userHashKey, string notes)
        {
            try
            {
                //创建登录日志对象，便于登录日志的添加
                var loginlog = new LoginLog();
                //记录登录时间
                loginlog.AddDate = DateTime.Now;
                //在线时长
                var loginDuration = "未知";
                
                //获取用户在线实体
                var model = OnlineUsersBll.GetInstence().GetOnlineUsersModel(userHashKey);
                if (model != null)
                {
                    //当前用户ID
                    loginlog.Manager_Id = model.Manager_Id;
                    //当前用户名称
                    loginlog.Manager_CName = model.Manager_CName;
                    //当前用户IP
                    loginlog.Ip = model.LoginIp;
                    //计算在线时长
                    loginDuration = CommonBll.LoginDuration(model.LoginTime, model.UpdateTime);
                }
                else
                {
                    //当前用户ID
                    loginlog.Manager_Id = 0;
                    //当前用户名称
                    loginlog.Manager_CName = "";
                    //当前用户IP
                    loginlog.Ip = IpHelper.GetUserIp();
                }

                //日志记录说明
                loginlog.Notes = String.Format(notes, loginlog.Manager_CName, loginDuration);

                loginlog.Save();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 添加用户登陆日志
        /// </summary>
        /// <param name="managerId">登录用户ID</param>
        /// <param name="notes">用户登录内容备注</param>
        public void Save(int managerId, string notes)
        {
            try
            {
                //创建登录日志对象，便于登录日志的添加
                var loginlog = new LoginLog();
                //记录登录时间
                loginlog.AddDate = DateTime.Now;
                //当前用户ID
                loginlog.Manager_Id = managerId;

                //当前用户名称
                loginlog.Manager_CName = ManagerBll.GetInstence().GetCName(null, managerId);
                //当前用户IP
                loginlog.Ip = IpHelper.GetUserIp();
                //日志记录说明
                loginlog.Notes = notes;

                loginlog.Save();
            }
            catch (Exception) { }
        }
        #endregion

        #endregion 自定义函数

    }
}
