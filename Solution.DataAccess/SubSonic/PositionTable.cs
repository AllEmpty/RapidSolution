 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Position
        /// Primary Key: Id
        /// </summary>
        public class PositionTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "Position";
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
			/// 职位名称
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 部门自编号ID
			/// </summary>
   			public static string Branch_Id{
			      get{
        			return "Branch_Id";
      			}
		    }
			/// <summary>
			/// 部门编号
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
			/// 菜单操作权限，有操作权限的菜单ID列表：|1|2|3|4|5|
			/// </summary>
   			public static string PagePower{
			      get{
        			return "PagePower";
      			}
		    }
			/// <summary>
			/// 页面功能操作权限，各个页面有操作权限的菜单ID和页面权限标志ID列表：|1,1|2,1|2,2|2,4|
			/// </summary>
   			public static string ControlPower{
			      get{
        			return "ControlPower";
      			}
		    }
			/// <summary>
			/// 是否有操作绑定指定部门的权限，0=无，1=有
			/// </summary>
   			public static string IsSetBranchPower{
			      get{
        			return "IsSetBranchPower";
      			}
		    }
			/// <summary>
			/// 绑定部门的编号
			/// </summary>
   			public static string SetBranchCode{
			      get{
        			return "SetBranchCode";
      			}
		    }
			/// <summary>
			/// 修改人员id
			/// </summary>
   			public static string Manager_Id{
			      get{
        			return "Manager_Id";
      			}
		    }
			/// <summary>
			/// 修改人员姓名
			/// </summary>
   			public static string Manager_CName{
			      get{
        			return "Manager_CName";
      			}
		    }
			/// <summary>
			/// 修改时间
			/// </summary>
   			public static string UpdateDate{
			      get{
        			return "UpdateDate";
      			}
		    }
                    
        }
}
