 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Branch
        /// Primary Key: Id
        /// </summary>
        public class BranchTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "Branch";
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
			/// 部门ID，内容为001001001，即每低一级部门，编码增加三位小数
			/// </summary>
   			public static string Code{
			      get{
        			return "Code";
      			}
		    }
			/// <summary>
			/// 部门名称
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 备注
			/// </summary>
   			public static string Notes{
			      get{
        			return "Notes";
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
