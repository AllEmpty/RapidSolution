 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: MenuInfo
        /// Primary Key: Id
        /// </summary>
        public class MenuInfoTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "MenuInfo";
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
			/// 菜单名称或各个页面功能名称
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 各页面URL（主菜单与分类菜单没有URL）
			/// </summary>
   			public static string Url{
			      get{
        			return "Url";
      			}
		    }
			/// <summary>
			/// 父ID
			/// </summary>
   			public static string ParentId{
			      get{
        			return "ParentId";
      			}
		    }
			/// <summary>
			/// 排序
			/// </summary>
   			public static string Sort{
			      get{
        			return "Sort";
      			}
		    }
			/// <summary>
			/// 深度
			/// </summary>
   			public static string Depth{
			      get{
        			return "Depth";
      			}
		    }
			/// <summary>
			/// 该菜单是否在菜单栏显示，0=不显示，1=显示
			/// </summary>
   			public static string IsDisplay{
			      get{
        			return "IsDisplay";
      			}
		    }
			/// <summary>
			/// 是否是菜单还是页面
			/// </summary>
   			public static string IsMenu{
			      get{
        			return "IsMenu";
      			}
		    }
                    
        }
}
