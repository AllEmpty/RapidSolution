 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// WebConfig表实体类
    /// </summary>
	[Serializable]
    public partial class WebConfig
    {

		int _Id = 0;
		/// <summary>
		/// 主键Id
		/// </summary>
		public int Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		string _WebName = "";
		/// <summary>
		/// 基本信息--网站名称
		/// </summary>
		public string WebName
		{
			get { return _WebName; }
			set { _WebName = value; }
		}

		string _WebDomain = "";
		/// <summary>
		/// 基本信息--网站地址
		/// </summary>
		public string WebDomain
		{
			get { return _WebDomain; }
			set { _WebDomain = value; }
		}

		string _WebEmail = "";
		/// <summary>
		/// 基本信息--管理员邮箱
		/// </summary>
		public string WebEmail
		{
			get { return _WebEmail; }
			set { _WebEmail = value; }
		}

		int _LoginLogReserveTime = 0;
		/// <summary>
		/// 日志--系统登陆日志保留时间，0=无限制，N（数字）= X月
		/// </summary>
		public int LoginLogReserveTime
		{
			get { return _LoginLogReserveTime; }
			set { _LoginLogReserveTime = value; }
		}

		int _UseLogReserveTime = 0;
		/// <summary>
		/// 日志--系统操作日志保留时间，0=无限制，N（数字）= X月
		/// </summary>
		public int UseLogReserveTime
		{
			get { return _UseLogReserveTime; }
			set { _UseLogReserveTime = value; }
		}

		string _EmailSmtp = "";
		/// <summary>
		/// 邮件设置--SMTP服务器
		/// </summary>
		public string EmailSmtp
		{
			get { return _EmailSmtp; }
			set { _EmailSmtp = value; }
		}

		string _EmailUserName = "";
		/// <summary>
		/// 邮件设置--登录用户名
		/// </summary>
		public string EmailUserName
		{
			get { return _EmailUserName; }
			set { _EmailUserName = value; }
		}

		string _EmailPassWord = "";
		/// <summary>
		/// 邮件设置--登录密码
		/// </summary>
		public string EmailPassWord
		{
			get { return _EmailPassWord; }
			set { _EmailPassWord = value; }
		}

		string _EmailDomain = "";
		/// <summary>
		/// 邮件设置--邮件域名
		/// </summary>
		public string EmailDomain
		{
			get { return _EmailDomain; }
			set { _EmailDomain = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("WebName=" +　WebName + "; ");
			sb.Append("WebDomain=" +　WebDomain + "; ");
			sb.Append("WebEmail=" +　WebEmail + "; ");
			sb.Append("LoginLogReserveTime=" +　LoginLogReserveTime + "; ");
			sb.Append("UseLogReserveTime=" +　UseLogReserveTime + "; ");
			sb.Append("EmailSmtp=" +　EmailSmtp + "; ");
			sb.Append("EmailUserName=" +　EmailUserName + "; ");
			sb.Append("EmailPassWord=" +　EmailPassWord + "; ");
			sb.Append("EmailDomain=" +　EmailDomain + "; ");
			return sb.ToString();
        }

    } 

}


