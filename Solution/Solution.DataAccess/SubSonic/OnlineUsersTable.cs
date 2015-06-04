 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: OnlineUsers
        /// Primary Key: Id
        /// </summary>
        public class OnlineUsersTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "OnlineUsers";
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
			/// 在线用户列表中的HashTable Key值
			/// </summary>
   			public static string UserHashKey{
			      get{
        			return "UserHashKey";
      			}
		    }
			/// <summary>
			/// 用户Id
			/// </summary>
   			public static string Manager_Id{
			      get{
        			return "Manager_Id";
      			}
		    }
			/// <summary>
			/// 登陆账号
			/// </summary>
   			public static string Manager_LoginName{
			      get{
        			return "Manager_LoginName";
      			}
		    }
			/// <summary>
			/// 登陆密码
			/// </summary>
   			public static string Manager_LoginPass{
			      get{
        			return "Manager_LoginPass";
      			}
		    }
			/// <summary>
			/// 用户中文名称
			/// </summary>
   			public static string Manager_CName{
			      get{
        			return "Manager_CName";
      			}
		    }
			/// <summary>
			/// 登陆时间
			/// </summary>
   			public static string LoginTime{
			      get{
        			return "LoginTime";
      			}
		    }
			/// <summary>
			/// 登陆IP
			/// </summary>
   			public static string LoginIp{
			      get{
        			return "LoginIp";
      			}
		    }
			/// <summary>
			/// 用户密钥
			/// </summary>
   			public static string UserKey{
			      get{
        			return "UserKey";
      			}
		    }
			/// <summary>
			/// Md5(密钥+登陆帐号+密码+IP+密钥.Substring(6,8))
			/// </summary>
   			public static string Md5{
			      get{
        			return "Md5";
      			}
		    }
			/// <summary>
			/// 最后在线时间
			/// </summary>
   			public static string UpdateTime{
			      get{
        			return "UpdateTime";
      			}
		    }
			/// <summary>
			/// 性别（0=未知，1=男，2=女）
			/// </summary>
   			public static string Sex{
			      get{
        			return "Sex";
      			}
		    }
			/// <summary>
			/// 所属部门ID
			/// </summary>
   			public static string Branch_Id{
			      get{
        			return "Branch_Id";
      			}
		    }
			/// <summary>
			/// 所属部门编号，用户只能正式归属于一个部门
			/// </summary>
   			public static string Branch_Code{
			      get{
        			return "Branch_Code";
      			}
		    }
			/// <summary>
			/// 部门名称
			/// </summary>
   			public static string Branch_Name{
			      get{
        			return "Branch_Name";
      			}
		    }
			/// <summary>
			/// 用户职位ID
			/// </summary>
   			public static string Position_Id{
			      get{
        			return "Position_Id";
      			}
		    }
			/// <summary>
			/// 职位名称
			/// </summary>
   			public static string Position_Name{
			      get{
        			return "Position_Name";
      			}
		    }
			/// <summary>
			/// 用户当前所在页面Url
			/// </summary>
   			public static string CurrentPage{
			      get{
        			return "CurrentPage";
      			}
		    }
			/// <summary>
			/// 用户当前所在页面名称
			/// </summary>
   			public static string CurrentPageTitle{
			      get{
        			return "CurrentPageTitle";
      			}
		    }
			/// <summary>
			/// 用户SessionId
			/// </summary>
   			public static string SessionId{
			      get{
        			return "SessionId";
      			}
		    }
			/// <summary>
			/// 客户端UA
			/// </summary>
   			public static string UserAgent{
			      get{
        			return "UserAgent";
      			}
		    }
			/// <summary>
			/// 操作系统
			/// </summary>
   			public static string OperatingSystem{
			      get{
        			return "OperatingSystem";
      			}
		    }
			/// <summary>
			/// 终端类型（0=非移动设备，1=移动设备）
			/// </summary>
   			public static string TerminalType{
			      get{
        			return "TerminalType";
      			}
		    }
			/// <summary>
			/// 浏览器名称
			/// </summary>
   			public static string BrowserName{
			      get{
        			return "BrowserName";
      			}
		    }
			/// <summary>
			/// 浏览器的版本
			/// </summary>
   			public static string BrowserVersion{
			      get{
        			return "BrowserVersion";
      			}
		    }
                    
        }
}
