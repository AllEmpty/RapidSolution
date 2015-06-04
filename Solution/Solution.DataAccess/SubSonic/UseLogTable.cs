 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UseLog
        /// Primary Key: Id
        /// </summary>
        public class UseLogTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "UseLog";
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
			/// 操作时间
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
			/// 菜单ID（用户操作页面）
			/// </summary>
   			public static string MenuInfo_Id{
			      get{
        			return "MenuInfo_Id";
      			}
		    }
			/// <summary>
			/// 菜单名称或各个页面功能名称
			/// </summary>
   			public static string MenuInfo_Name{
			      get{
        			return "MenuInfo_Name";
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
