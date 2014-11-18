 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UploadType
        /// Primary Key: Id
        /// </summary>
        public class UploadTypeTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "UploadType";
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
			/// 关键字
			/// </summary>
   			public static string TypeKey{
			      get{
        			return "TypeKey";
      			}
		    }
			/// <summary>
			/// 类别名称
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 允许的扩展名使用,分隔，例如:"jpg,png,gif"
			/// </summary>
   			public static string Ext{
			      get{
        			return "Ext";
      			}
		    }
			/// <summary>
			/// 1=系统默认，不能删除，不能修改 TypeKey
			/// </summary>
   			public static string IsSys{
			      get{
        			return "IsSys";
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
