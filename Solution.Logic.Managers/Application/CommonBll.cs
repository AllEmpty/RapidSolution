using System;
using System.Collections.Generic;
using System.Net;
using System.Web.UI;
using DotNet.Utilities;
using DotNet.Utilities.Log;
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
 *   文件名称：CommonBll.cs
 *   描    述：公共逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// CommonBll公共逻辑类
    /// </summary>
    public class CommonBll
    {
        #region 写log日志

        /// <summary>
        /// 将内容写入日志里
        /// </summary>
        /// <param name="content">要写入日志文件的内容</param>
        /// <param name="ex">异常</param>
        public static void WriteLog(string content, Exception ex = null)
        {
            try
            {
                if (ConfigHelper.GetConfigBool("IsWriteLog"))
                {
                    if (ex == null)
                    {
                        LogHelper.WriteLog(content);
                    }
                    else
                    {
                        LogHelper.WriteLog(content, ex);
                    }
                }
            }
            catch (Exception) { }
        }

        #endregion

        #region 添加用户访问页面记录

        /// <summary>
        /// 添加用户访问页面记录
        /// </summary>
        public static void UserRecord(Page page)
        {
            //获取当前用户的编号
            var userHashKey = OnlineUsersBll.GetInstence().GetUserHashKey();

            //获取当前页面名称
            var menuName = "";
            //取得当前页面实体
            var menu = MenuInfoBll.GetInstence().GetMenuInfo(page.Request.Url.AbsolutePath);
            if (menu != null)
            {
                menuName = menu.Name;
            }
            //判断是否为首页
            if (string.IsNullOrEmpty(menuName))
            {
                if (page.Request.Url.AbsolutePath.Equals("/WebManage/Main.aspx"))
                {
                    //当前页面名称
                    menuName = "首页";
                }
            }

            //更新当前用户所在页面路径
            OnlineUsersBll.GetInstence()
                .UpdateUserOnlineInfo(userHashKey, OnlineUsersTable.CurrentPage, page.Request.Url.AbsolutePath);
            //更新当前用户所在页面名称
            OnlineUsersBll.GetInstence().UpdateUserOnlineInfo(userHashKey, OnlineUsersTable.CurrentPageTitle, menuName);

            //同步更新数据库与缓存
            //获取在线用户Id
            var id = OnlineUsersBll.GetInstence().GetOnlineUsersId();
            if (id > 0)
            {
                //更新
                OnlineUsersBll.GetInstence()
                    .UpdateValue(page, id, OnlineUsersTable.CurrentPage, page.Request.Url.AbsolutePath,
                        OnlineUsersTable.CurrentPageTitle, menuName, "", true, false);
            }

            //添加用户访问记录
            UseLogBll.GetInstence().Save(page, "{0}进入了【{1}】页面");

        }
        #endregion

        #region 清除前端缓存
        /// <summary>
        /// 清除前台缓存——通过与前端指定的接口提交约定的字串，来执行缓存清理程序，如果前后端在一个站点里，则直接注释本函数即可
        /// </summary>
        /// <param name="cacheName">将要清除的缓存名称，值为AllCache时，表示清除所有缓存</param>
        public static void RemoveCache(string cacheName)
        {
            try
            {
                //获取参数
                var time = DateTime.Now.Ticks;
                var setKey = ConfigHelper.GetConfigString("SetKey");
                //对加密后的验证码与提交的key进行匹配，如果不正确则直接退出
                var checkKey = Encrypt.Md5(cacheName + time + setKey + setKey.Substring(2, 8));

                var url = ConfigHelper.GetConfigString("Site") + "Handles/RemoveCache.ashx?cacheName=" + cacheName + "&time=" + time + "&key=" + checkKey;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
            }
            catch
            {

            }
        }
        #endregion

        #region 计算在线时间
        /// <summary>
        /// 计算当前用户在线时长
        /// </summary>
        /// <param name="startTime">用户登陆时间</param>
        /// <param name="endTime">用户离线时间</param>
        /// <returns></returns>
        public static string LoginDuration(object startTime, object endTime)
        {
            try
            {
                double minu = TimeHelper.DateDiff("n", TimeHelper.CDate(startTime), TimeHelper.CDate(endTime));
                if (minu < 1.0)
                {
                    return "小于1分钟";
                }
                return minu.ToString("0") + "分钟";
            }
            catch (Exception)
            {
                return "计算异常";
            }
        }
        #endregion

        #region 检查指定缓存是否过期
        /// <summary>
        /// 检查指定缓存是否过期——缓存当天有效，第二天自动清空
        /// </summary>
        /// <param name="constCacheKeyDate">当前缓存日期名称</param>
        /// <returns></returns>
        public static bool CheckCacheIsExpired(string constCacheKeyDate)
        {
            //判断缓存日期是否存在
            if (CacheHelper.GetCache(constCacheKeyDate) == null)
            {
                return false;
            }

            //判断当前日期是否是第二天（即是否过期），如果是的话，则返回true
            if (TimeHelper.DateDiff("d", TimeHelper.CDate(CacheHelper.GetCache(constCacheKeyDate)), DateTime.Now) != 0)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region 是否启用缓存
        /// <summary>
        /// 是否启用缓存
        /// </summary>
        /// <returns></returns>
        public static bool IsUseCache()
        {
            return ConfigHelper.GetConfigBool("IsUseCache");
        }
        #endregion

        #region 更新排序值
        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="Grid1">页面表格</param>
        /// <param name="tbxSortId">表格中绑定排序的表单名</param>
        /// <param name="tableName">表名</param>
        /// <param name="sortIdName">排序字段名</param>
        /// <param name="pkName">主键字段名</param>
        /// <returns>更新成功返回true，失败返回false</returns>
        public static bool UpdateSort(Page page, FineUI.Grid Grid1, string tbxSortId, string tableName, string sortIdName, string pkName)
        {
            try
            {
                //如果不存在记录则不操作
                if (Grid1.Rows.Count <= 0)
                    return false;

                //获取记录数量
                int count = Grid1.Rows.Count;
                var strArray = new string[Grid1.Rows.Count];
                int j = 0;

                //遍历所有记录
                for (int i = 0; i < count; i++)
                {
                    //读取一行
                    GridRow row = Grid1.Rows[i];
                    //获取主键Id值
                    int k = ConvertHelper.Cint0(row.DataKeys[0].ToString());

                    if (k > 0)
                    {
                        //读取当前行排序值
                        var tbx = (System.Web.UI.WebControls.TextBox)row.FindControl(tbxSortId);
                        //如果排序值不为空
                        if (tbx.Text.Trim() != "")
                        {
                            //将排序值转为数值
                            int sortId = ConvertHelper.Cint1(tbx.Text);
                            //拼接SQL语句
                            strArray[j] = string.Format("update {0} set {1}={2} where {3}={4};", tableName, sortIdName, sortId, pkName, k);
                            j++;
                        }
                    }
                }

                //------------------------------------------------
                if (j >= 1)
                {
                    UpdateHelper update = new UpdateHelper();
                    update.Update(strArray);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                //出现异常，保存出错日志信息
                WriteLog("更新排序值失败", ex);
                return false;
            }
        }
        #endregion

        #region 自动排序
        /// <summary>排序</summary>
        /// <param name="nowId"></param>
        /// <param name="m">"up" or "down"</param>
        /// <param name="fieldId">id字段名:"ClassId"</param>
        /// <param name="tableName">表名</param>
        /// <param name="strWhere">附加Where : " sid=1 "</param>
        /// <param name="isExistsMoreLv">是否存在多级分类,一级时,请使用false,多级使用true(一级不包括ParentId字段)</param>
        /// <param name="fieldName">字段名:"SortId"</param>
        /// <param name="fieldParentId">字段名:"ParentId"</param>
        public static void AutoSort(int nowId, string m, string fieldId, string tableName, string strWhere = "", bool isExistsMoreLv = false, string fieldName = "SortId", string fieldParentId = "ParentId")
        {
            string ss = "";
            string iOldId = nowId.ToString(), iOldNo = "", iNewId = "", iNewNo = "";
            string[] aRs1, aRs2;
            var select = new SelectHelper();

            if (string.IsNullOrEmpty(strWhere) == false)
            {
                ss = " and ";
            }

            if (isExistsMoreLv)
            {

                var dt =
                    select.ExcuSQLDataTable("select top 1 " + fieldName + "," + fieldParentId + " from " + tableName +
                                            " where " + strWhere + " " + ss + " " + fieldId + "=" + iOldId);
                aRs1 = DataTableHelper.GetColumnsString(dt);
                if (aRs1.Length > 0)
                {
                    iOldNo = aRs1[0];

                    if (string.IsNullOrEmpty(strWhere) == false)
                    {
                        ss = " and " + fieldParentId + "=" + aRs1[1] + " and ";
                    }
                    else
                    {
                        ss = " " + fieldParentId + "=" + aRs1[1] + " and ";
                    }
                }
                else
                {
                    return;
                }
                aRs1 = null;
            }
            else
            {
                iOldNo = select.ExecuteScalar("select top 1 " + fieldName + " from " + tableName + " where " + strWhere + " " + ss + " " + fieldId + "=" + iOldId) + "";
                if (iOldNo.Length == 0) { return; }
            }


            if (m == "up")
            {//上移
                var dt =
                    select.ExcuSQLDataTable("select top 1 " + fieldId + "," + fieldName + " from " + tableName + " where " + strWhere + " " + ss + " " + fieldName + "<" + iOldNo + " order by " + fieldName + " desc");
                aRs2 = DataTableHelper.GetColumnsString(dt);
                if (aRs2.Length > 0)
                {
                    iNewId = aRs2[0];
                    iNewNo = aRs2[1];
                }
                else
                {
                    return;
                }
            }
            else
            {//下移
                var dt =
                    select.ExcuSQLDataTable("select top 1 " + fieldId + "," + fieldName + " from " + tableName + " where " + strWhere + " " + ss + " " + fieldName + ">" + iOldNo + " order by " + fieldName + " asc");
                aRs2 = DataTableHelper.GetColumnsString(dt);
                if (aRs2.Length > 0)
                {
                    iNewId = aRs2[0];
                    iNewNo = aRs2[1];
                }
                else
                {
                    return;
                }
            }
            var update = new UpdateHelper();
            update.Update("update " + tableName + " set " + fieldName + "=" + iNewNo + " where " + strWhere + " " + ss + " " + fieldId + "=" + iOldId);
            update.Update("update " + tableName + " set " + fieldName + "=" + iOldNo + " where " + strWhere + " " + ss + " " + fieldId + "=" + iNewId);
        }

        /// <summary>自动排序</summary>
        /// <param name="fieldId">id字段名:"Id"</param>
        /// <param name="tableName">表名:"NewsClass"</param>
        /// <param name="strWhere">附加Where : " sid=1 "</param>
        /// <param name="isExistsMoreLv">是否存在多级分类,一级时,请使用false,多级使用true，(一级不包括ParentId字段)</param>
        /// <param name="pid">父级分类的ParentId</param>
        /// <param name="fieldName">字段名:"OrderId"</param>
        /// <param name="fieldParentId">字段名:"ParentId"</param>
        public static bool AutoSort(string fieldId, string tableName, string strWhere = "", bool isExistsMoreLv = false, int pid = 0, string fieldName = "Sort", string fieldParentId = "ParentId")
        {
            try
            {
                string sw = "";
                var select = new SelectHelper();
                var update = new UpdateHelper();

                if (string.IsNullOrEmpty(strWhere) == false)
                {
                    sw = " where " + strWhere;
                }

                if (isExistsMoreLv)
                {
                    pid = ConvertHelper.Cint0(pid);

                    if (string.IsNullOrEmpty(sw))
                    {
                        sw = " where " + fieldParentId + "=" + pid;
                    }
                    else
                    {
                        sw += " and " + fieldParentId + "=" + pid;
                    }
                }

                var dt =
                    select.ExcuSQLDataTable("select " + fieldId + " from " + tableName + " " + sw + " order by " + fieldName + " asc," + fieldId);
                string[] pRs = DataTableHelper.GetArrayString(dt, fieldId);

                if (pRs.Length > 0)
                {
                    int ti = pRs.Length;

                    for (int i = 0; i < ti; i++)
                    {
                        if (pRs[i].Length > 0)
                        {
                            update.Update("update " + tableName + " set " + fieldName + "=" + (i + 1).ToString() + " where " + fieldId + "=" + pRs[i]);

                            if (isExistsMoreLv)
                            {
                                AutoSort(fieldId, tableName, strWhere, isExistsMoreLv, ConvertHelper.Cint0(pRs[i]), fieldName, fieldParentId);
                            }
                        }
                    }
                }
                pRs = null;

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
