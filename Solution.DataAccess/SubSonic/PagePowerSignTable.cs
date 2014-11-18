 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: PagePowerSign
        /// Primary Key: Id
        /// </summary>
        public class PagePowerSignTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "PagePowerSign";
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
			/// 公用页面权限ID
			/// </summary>
   			public static string PagePowerSignPublic_Id{
			      get{
        			return "PagePowerSignPublic_Id";
      			}
		    }
			/// <summary>
			/// 权限名称，如：浏览、添加、修改、删除、报表、查询、调动/分配、设置等(名称可以自由定，但建议取有意义的名称)
			/// </summary>
   			public static string CName{
			      get{
        			return "CName";
      			}
		    }
			/// <summary>
			/// 权限英文名称，除了在英文版权限设置时显示对应菜单外，还用来在页面程序中区分页面不同位置所调用的权限(在检测页面权限时使用)
			/// </summary>
   			public static string EName{
			      get{
        			return "EName";
      			}
		    }
			/// <summary>
			/// 菜单ID
			/// </summary>
   			public static string MenuInfo_Id{
			      get{
        			return "MenuInfo_Id";
      			}
		    }
                    
        }
}
