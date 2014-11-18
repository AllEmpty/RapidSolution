using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using DotNet.Utilities;
using DotNet.Utilities.Mail;
using Solution.DataAccess.DataModel;

namespace Solution.Logic.Managers
{
    /// <summary>E-Mail封装函数</summary>
    public class MailBll
    {
        //定义单例实体
        private static MailBll _MailBll = null;

        /// <summary>构造函数</summary>
        public static MailBll GetInstence()
        {
            if (_MailBll == null)
            {
                _MailBll = new MailBll();
            }
            return _MailBll;
        }
        
        #region test
        /// <summary>发送测试邮件</summary>
        /// <returns></returns>
        public string TestMail(string sTo = "")
        {
            string tit = "test Mail == " + DateTime.Now.ToString("yyyy-M-d");
            string msg = "test Mail == " + DateTime.Now.ToString("yyyy-M-d");

            //---------------------------------
            var model = WebConfigBll.GetInstence().GetModelForCache(x => x.Id > 0);

            if (sTo.Length < 2) { sTo = model.EmailUserName; }
            //---------------------------------

            return SendMail(model.EmailSmtp, model.EmailUserName, model.EmailPassWord, sTo, tit, msg, false);
        }
        #endregion

        #region SendMail

        /// <summary>发送E-mail</summary>
        /// <param name="domain">smtp域名,例如:smtp.qq.com</param>
        /// <param name="user">smtp账号</param>
        /// <param name="pass">smtp密码</param>
        /// <param name="sTo">发送对象</param>
        /// <param name="tit">smtp账号</param>
        /// <param name="msg">smtp密码</param>
        /// <param name="isHtml"></param>
        /// <returns></returns>
        private string SendMail(string domain, string user, string pass, string sTo, string tit, string msg, bool isHtml)
        {
            var oMail = new SendMailHelper();
            oMail.From = user;
            oMail.AddRecipient(sTo);
            oMail.MailDomain = domain;
            oMail.MailServerUserName = user;
            oMail.MailServerPassWord = pass;
            oMail.Html = isHtml;
            oMail.Priority = "High";
            oMail.Subject = tit;
            oMail.Body = msg;

            oMail.Dispose();
            if (oMail.Send())
            {
                return "";
            }
            else
            {
                return oMail.ErrorMessage;
            }
        }

        #endregion
    }
}
