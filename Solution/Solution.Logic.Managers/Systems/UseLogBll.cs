using System;
using System.Web.UI;
/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：UseLogBll.cs
 *   描    述：用户操作日志管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

namespace Solution.Logic.Managers
{
    /// <summary>
    /// UseLog（用户操作日志）逻辑类
    /// </summary>
    public partial class UseLogBll : LogicBase
    {
        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        #region 添加用户操作日志
        /// <summary>
        /// 添加用户操作日志
        /// </summary>
        /// <param name="page">页面指针</param>
        /// <param name="useLogOccurrence">用户操作内容备注，{0}=用户名称，{1}=当前页面名称</param>
        public void Save(Page page, string useLogOccurrence)
        {
            try
            {
                //创建用户操作日志对象
                var uselog = new UseLog();
                //记录登录时间
                uselog.AddDate = DateTime.Now;
                //获取用户在线实体
                var model = OnlineUsersBll.GetInstence().GetOnlineUsersModelForLog();
                if (model != null)
                {
                    //当前用户ID
                    uselog.Manager_Id = model.Manager_Id;
                    //当前用户名称
                    uselog.Manager_CName = model.Manager_CName;
                }
                else
                {
                    //当前用户ID
                    uselog.Manager_Id = 0;
                    //当前用户名称
                    uselog.Manager_CName = "";
                }
                if (page != null)
                {
                    //当前页面ID
                    var menu = MenuInfoBll.GetInstence().GetMenuInfo(page.Request.Url.AbsolutePath);
                    if (menu == null)
                    {
                        uselog.MenuInfo_Id = 0;
                    }
                    else
                    {
                        uselog.MenuInfo_Id = menu.Id;
                        //当前页面名称
                        uselog.MenuInfo_Name = menu.Name;
                    }

                    //判断是否为首页
                    if (uselog.MenuInfo_Id == 0)
                    {
                        if (page.Request.Url.AbsolutePath.Equals("/WebManage/Main.aspx"))
                        {
                            //当前页面名称
                            uselog.MenuInfo_Name = "首页";
                        }
                    }
                }

                //当前用户IP
                uselog.Ip = IpHelper.GetUserIp();

                //操作内容
                uselog.Notes = StringHelper.FilterSql(String.Format(useLogOccurrence, uselog.Manager_CName, uselog.MenuInfo_Name));
                //插入记录
                uselog.Save();
            }
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("添加用户操作日志时出现异常", e);
            }
        }
        #endregion

        #endregion 自定义函数

    }
}
