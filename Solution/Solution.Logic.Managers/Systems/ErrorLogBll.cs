using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-22
 *   文件名称：ErrorLogBll.cs
 *   描    述：错误日志逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// ErrorLogBll逻辑类
    /// </summary>
    public partial class ErrorLogBll : LogicBase
    {
        /***********************************************************************
		 * 自定义函数                                                          *
		 ***********************************************************************/

        #region 自定义函数
        
        #region 添加系统错误日志
        /// <summary>
        /// 添加系统错误日志
        /// </summary>
        /// <param name="context">页面的context</param>
        public void Save(System.Web.HttpContext context)
        {
            try
            {
                //收集服务器端页面发生的异常，并添加到数据库中
                var ex = context.Server.GetLastError().GetBaseException();

                var errorLog = new ErrorLog();
                //异常产生时间
                errorLog.ErrTime = DateTime.Now;

                var ch = new ClientHelper(context.Request);
                //客户端浏览器版本
                errorLog.BrowserVersion = ch.GetBrowserVersion();
                //客户端浏览器名称
                errorLog.BrowserType = ch.GetBrowserInfo();
                //获取用户IP
                errorLog.Ip = IpHelper.GetUserIp();
                // 异常页面
                errorLog.PageUrl = context.Request.Url.ToString();
                //异常消息
                errorLog.ErrMessage = ex.Message;
                //异常源
                errorLog.ErrSource = ex.Source;
                //堆栈轨迹
                errorLog.StackTrace = ex.StackTrace;
                //帮助连接
                errorLog.HelpLink = ex.HelpLink;

                //添加进数据库
                errorLog.Save();
            }
            catch (Exception) { }
        }
        #endregion
        
        #endregion 自定义函数
    }
}
