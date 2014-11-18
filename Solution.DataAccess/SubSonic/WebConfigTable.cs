 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: WebConfig
        /// Primary Key: Id
        /// </summary>
        public class WebConfigTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "WebConfig";
      			}
			}

			/// <summary>
			/// 主键Id
			/// </summary>
   			public static string Id{
			      get{
        			return "Id";
      			}
		    }
			/// <summary>
			/// 基本信息--网站名称
			/// </summary>
   			public static string WebName{
			      get{
        			return "WebName";
      			}
		    }
			/// <summary>
			/// 基本信息--网站地址
			/// </summary>
   			public static string WebDomain{
			      get{
        			return "WebDomain";
      			}
		    }
			/// <summary>
			/// 基本信息--管理员邮箱
			/// </summary>
   			public static string WebEmail{
			      get{
        			return "WebEmail";
      			}
		    }
			/// <summary>
			/// 日志--系统登陆日志保留时间，0=无限制，N（数字）= X月
			/// </summary>
   			public static string LoginLogReserveTime{
			      get{
        			return "LoginLogReserveTime";
      			}
		    }
			/// <summary>
			/// 日志--系统操作日志保留时间，0=无限制，N（数字）= X月
			/// </summary>
   			public static string UseLogReserveTime{
			      get{
        			return "UseLogReserveTime";
      			}
		    }
			/// <summary>
			/// 邮件设置--SMTP服务器
			/// </summary>
   			public static string EmailSmtp{
			      get{
        			return "EmailSmtp";
      			}
		    }
			/// <summary>
			/// 邮件设置--登录用户名
			/// </summary>
   			public static string EmailUserName{
			      get{
        			return "EmailUserName";
      			}
		    }
			/// <summary>
			/// 邮件设置--登录密码
			/// </summary>
   			public static string EmailPassWord{
			      get{
        			return "EmailPassWord";
      			}
		    }
			/// <summary>
			/// 邮件设置--邮件域名
			/// </summary>
   			public static string EmailDomain{
			      get{
        			return "EmailDomain";
      			}
		    }
                    
        }
}
