 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// ErrorLog表实体类
    /// </summary>
	[Serializable]
    public partial class ErrorLog
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

		DateTime _ErrTime = new DateTime(1900,1,1);
		/// <summary>
		/// 出错时间
		/// </summary>
		public DateTime ErrTime
		{
			get { return _ErrTime; }
			set { _ErrTime = value; }
		}

		string _BrowserVersion = "";
		/// <summary>
		/// 浏览器版本
		/// </summary>
		public string BrowserVersion
		{
			get { return _BrowserVersion; }
			set { _BrowserVersion = value; }
		}

		string _BrowserType = "";
		/// <summary>
		/// 浏览器
		/// </summary>
		public string BrowserType
		{
			get { return _BrowserType; }
			set { _BrowserType = value; }
		}

		string _Ip = "";
		/// <summary>
		/// IP
		/// </summary>
		public string Ip
		{
			get { return _Ip; }
			set { _Ip = value; }
		}

		string _PageUrl = "";
		/// <summary>
		/// 异常页面
		/// </summary>
		public string PageUrl
		{
			get { return _PageUrl; }
			set { _PageUrl = value; }
		}

		string _ErrMessage = "";
		/// <summary>
		/// 异常消息
		/// </summary>
		public string ErrMessage
		{
			get { return _ErrMessage; }
			set { _ErrMessage = value; }
		}

		string _ErrSource = "";
		/// <summary>
		/// 异常源
		/// </summary>
		public string ErrSource
		{
			get { return _ErrSource; }
			set { _ErrSource = value; }
		}

		string _StackTrace = "";
		/// <summary>
		/// 堆栈轨迹
		/// </summary>
		public string StackTrace
		{
			get { return _StackTrace; }
			set { _StackTrace = value; }
		}

		string _HelpLink = "";
		/// <summary>
		/// 帮助连接
		/// </summary>
		public string HelpLink
		{
			get { return _HelpLink; }
			set { _HelpLink = value; }
		}

		byte _Type = 0;
		/// <summary>
		/// 错误类型，0=后台，1=前台，......
		/// </summary>
		public byte Type
		{
			get { return _Type; }
			set { _Type = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("ErrTime=" +　ErrTime + "; ");
			sb.Append("BrowserVersion=" +　BrowserVersion + "; ");
			sb.Append("BrowserType=" +　BrowserType + "; ");
			sb.Append("Ip=" +　Ip + "; ");
			sb.Append("PageUrl=" +　PageUrl + "; ");
			sb.Append("ErrMessage=" +　ErrMessage + "; ");
			sb.Append("ErrSource=" +　ErrSource + "; ");
			sb.Append("StackTrace=" +　StackTrace + "; ");
			sb.Append("HelpLink=" +　HelpLink + "; ");
			sb.Append("Type=" +　Type + "; ");
			return sb.ToString();
        }

    } 

}


