 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: ErrorLog
        /// Primary Key: Id
        /// </summary>
        public class ErrorLogTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "ErrorLog";
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
			/// 出错时间
			/// </summary>
   			public static string ErrTime{
			      get{
        			return "ErrTime";
      			}
		    }
			/// <summary>
			/// 浏览器版本
			/// </summary>
   			public static string BrowserVersion{
			      get{
        			return "BrowserVersion";
      			}
		    }
			/// <summary>
			/// 浏览器
			/// </summary>
   			public static string BrowserType{
			      get{
        			return "BrowserType";
      			}
		    }
			/// <summary>
			/// IP
			/// </summary>
   			public static string Ip{
			      get{
        			return "Ip";
      			}
		    }
			/// <summary>
			/// 异常页面
			/// </summary>
   			public static string PageUrl{
			      get{
        			return "PageUrl";
      			}
		    }
			/// <summary>
			/// 异常消息
			/// </summary>
   			public static string ErrMessage{
			      get{
        			return "ErrMessage";
      			}
		    }
			/// <summary>
			/// 异常源
			/// </summary>
   			public static string ErrSource{
			      get{
        			return "ErrSource";
      			}
		    }
			/// <summary>
			/// 堆栈轨迹
			/// </summary>
   			public static string StackTrace{
			      get{
        			return "StackTrace";
      			}
		    }
			/// <summary>
			/// 帮助连接
			/// </summary>
   			public static string HelpLink{
			      get{
        			return "HelpLink";
      			}
		    }
			/// <summary>
			/// 错误类型，0=后台，1=前台，......
			/// </summary>
   			public static string Type{
			      get{
        			return "Type";
      			}
		    }
                    
        }
}
