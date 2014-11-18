 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// OnlineUsers表实体类
    /// </summary>
	[Serializable]
    public partial class OnlineUsers
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

		string _UserHashKey = "";
		/// <summary>
		/// 在线用户列表中的HashTable Key值
		/// </summary>
		public string UserHashKey
		{
			get { return _UserHashKey; }
			set { _UserHashKey = value; }
		}

		int _Manager_Id = 0;
		/// <summary>
		/// 用户Id
		/// </summary>
		public int Manager_Id
		{
			get { return _Manager_Id; }
			set { _Manager_Id = value; }
		}

		string _Manager_LoginName = "";
		/// <summary>
		/// 登陆账号
		/// </summary>
		public string Manager_LoginName
		{
			get { return _Manager_LoginName; }
			set { _Manager_LoginName = value; }
		}

		string _Manager_LoginPass = "";
		/// <summary>
		/// 登陆密码
		/// </summary>
		public string Manager_LoginPass
		{
			get { return _Manager_LoginPass; }
			set { _Manager_LoginPass = value; }
		}

		string _Manager_CName = "";
		/// <summary>
		/// 用户中文名称
		/// </summary>
		public string Manager_CName
		{
			get { return _Manager_CName; }
			set { _Manager_CName = value; }
		}

		DateTime _LoginTime = new DateTime(1900,1,1);
		/// <summary>
		/// 登陆时间
		/// </summary>
		public DateTime LoginTime
		{
			get { return _LoginTime; }
			set { _LoginTime = value; }
		}

		string _LoginIp = "";
		/// <summary>
		/// 登陆IP
		/// </summary>
		public string LoginIp
		{
			get { return _LoginIp; }
			set { _LoginIp = value; }
		}

		string _UserKey = "";
		/// <summary>
		/// 用户密钥
		/// </summary>
		public string UserKey
		{
			get { return _UserKey; }
			set { _UserKey = value; }
		}

		string _Md5 = "";
		/// <summary>
		/// Md5(密钥+登陆帐号+密码+IP+密钥.Substring(6,8))
		/// </summary>
		public string Md5
		{
			get { return _Md5; }
			set { _Md5 = value; }
		}

		DateTime _UpdateTime = new DateTime(1900,1,1);
		/// <summary>
		/// 最后在线时间
		/// </summary>
		public DateTime UpdateTime
		{
			get { return _UpdateTime; }
			set { _UpdateTime = value; }
		}

		string _Sex = "";
		/// <summary>
		/// 性别（0=未知，1=男，2=女）
		/// </summary>
		public string Sex
		{
			get { return _Sex; }
			set { _Sex = value; }
		}

		int _Branch_Id = 0;
		/// <summary>
		/// 所属部门ID
		/// </summary>
		public int Branch_Id
		{
			get { return _Branch_Id; }
			set { _Branch_Id = value; }
		}

		string _Branch_Code = "";
		/// <summary>
		/// 所属部门编号，用户只能正式归属于一个部门
		/// </summary>
		public string Branch_Code
		{
			get { return _Branch_Code; }
			set { _Branch_Code = value; }
		}

		string _Branch_Name = "";
		/// <summary>
		/// 部门名称
		/// </summary>
		public string Branch_Name
		{
			get { return _Branch_Name; }
			set { _Branch_Name = value; }
		}

		string _Position_Id = "";
		/// <summary>
		/// 用户职位ID
		/// </summary>
		public string Position_Id
		{
			get { return _Position_Id; }
			set { _Position_Id = value; }
		}

		string _Position_Name = "";
		/// <summary>
		/// 职位名称
		/// </summary>
		public string Position_Name
		{
			get { return _Position_Name; }
			set { _Position_Name = value; }
		}

		string _CurrentPage = "";
		/// <summary>
		/// 用户当前所在页面Url
		/// </summary>
		public string CurrentPage
		{
			get { return _CurrentPage; }
			set { _CurrentPage = value; }
		}

		string _CurrentPageTitle = "";
		/// <summary>
		/// 用户当前所在页面名称
		/// </summary>
		public string CurrentPageTitle
		{
			get { return _CurrentPageTitle; }
			set { _CurrentPageTitle = value; }
		}

		string _SessionId = "";
		/// <summary>
		/// 用户SessionId
		/// </summary>
		public string SessionId
		{
			get { return _SessionId; }
			set { _SessionId = value; }
		}

		string _UserAgent = "";
		/// <summary>
		/// 客户端UA
		/// </summary>
		public string UserAgent
		{
			get { return _UserAgent; }
			set { _UserAgent = value; }
		}

		string _OperatingSystem = "";
		/// <summary>
		/// 操作系统
		/// </summary>
		public string OperatingSystem
		{
			get { return _OperatingSystem; }
			set { _OperatingSystem = value; }
		}

		int _TerminalType = 0;
		/// <summary>
		/// 终端类型（0=非移动设备，1=移动设备）
		/// </summary>
		public int TerminalType
		{
			get { return _TerminalType; }
			set { _TerminalType = value; }
		}

		string _BrowserName = "";
		/// <summary>
		/// 浏览器名称
		/// </summary>
		public string BrowserName
		{
			get { return _BrowserName; }
			set { _BrowserName = value; }
		}

		string _BrowserVersion = "";
		/// <summary>
		/// 浏览器的版本
		/// </summary>
		public string BrowserVersion
		{
			get { return _BrowserVersion; }
			set { _BrowserVersion = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("UserHashKey=" +　UserHashKey + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_LoginName=" +　Manager_LoginName + "; ");
			sb.Append("Manager_LoginPass=" +　Manager_LoginPass + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("LoginTime=" +　LoginTime + "; ");
			sb.Append("LoginIp=" +　LoginIp + "; ");
			sb.Append("UserKey=" +　UserKey + "; ");
			sb.Append("Md5=" +　Md5 + "; ");
			sb.Append("UpdateTime=" +　UpdateTime + "; ");
			sb.Append("Sex=" +　Sex + "; ");
			sb.Append("Branch_Id=" +　Branch_Id + "; ");
			sb.Append("Branch_Code=" +　Branch_Code + "; ");
			sb.Append("Branch_Name=" +　Branch_Name + "; ");
			sb.Append("Position_Id=" +　Position_Id + "; ");
			sb.Append("Position_Name=" +　Position_Name + "; ");
			sb.Append("CurrentPage=" +　CurrentPage + "; ");
			sb.Append("CurrentPageTitle=" +　CurrentPageTitle + "; ");
			sb.Append("SessionId=" +　SessionId + "; ");
			sb.Append("UserAgent=" +　UserAgent + "; ");
			sb.Append("OperatingSystem=" +　OperatingSystem + "; ");
			sb.Append("TerminalType=" +　TerminalType + "; ");
			sb.Append("BrowserName=" +　BrowserName + "; ");
			sb.Append("BrowserVersion=" +　BrowserVersion + "; ");
			return sb.ToString();
        }

    } 

}


