 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: LoginLog
        /// Primary Key: Id
        /// </summary>
        public class LoginLogTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "LoginLog";
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
			/// 登陆日期
			/// </summary>
   			public static string AddDate{
			      get{
        			return "AddDate";
      			}
		    }
			/// <summary>
			/// 登陆用户ID
			/// </summary>
   			public static string Manager_Id{
			      get{
        			return "Manager_Id";
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
			/// 登陆IP
			/// </summary>
   			public static string Ip{
			      get{
        			return "Ip";
      			}
		    }
			/// <summary>
			/// 操作内容
			/// </summary>
   			public static string Notes{
			      get{
        			return "Notes";
      			}
		    }
                    
        }
}
