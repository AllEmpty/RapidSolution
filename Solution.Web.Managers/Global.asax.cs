using System;
using System.Web.Configuration;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;

namespace Solution.Web.Managers
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            //主要是对SubSonic ORM框架所有生成类进行初始化，以避免在某些特殊状态下运行过程出现null异常的发生
            new MenuInfo();

            //初始化日志文件 
            string state = WebConfigurationManager.AppSettings["IsWriteLog"];
            //判断是否开启日志记录
            if (state == "1")
            {
                var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
                           WebConfigurationManager.AppSettings["log4net"];
                var fi = new System.IO.FileInfo(path);
                log4net.Config.XmlConfigurator.Configure(fi);
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                //收集服务器端页面发生的异常，并添加到数据库中
                ErrorLogBll.GetInstence().Save(this.Context);

                this.Context.Response.Clear();
            }
            catch { }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}