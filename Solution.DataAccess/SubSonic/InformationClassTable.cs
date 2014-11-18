 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: InformationClass
        /// Primary Key: Id
        /// </summary>
        public class InformationClassTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "InformationClass";
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
			/// 信息名称
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 描述
			/// </summary>
   			public static string Notes{
			      get{
        			return "Notes";
      			}
		    }
			/// <summary>
			/// 1=系统分类（不能删除，不能添加文章，但可以添加子分类）
			/// </summary>
   			public static string IsSys{
			      get{
        			return "IsSys";
      			}
		    }
			/// <summary>
			/// 分类图
			/// </summary>
   			public static string ClassImg{
			      get{
        			return "ClassImg";
      			}
		    }
			/// <summary>
			/// 是否显示, 0=False,1=True,
			/// </summary>
   			public static string IsShow{
			      get{
        			return "IsShow";
      			}
		    }
			/// <summary>
			/// 是否为单页,单页，没有文章封面，没有发表者，也不能评论
			/// </summary>
   			public static string IsPage{
			      get{
        			return "IsPage";
      			}
		    }
			/// <summary>
			/// 分类顶层id
			/// </summary>
   			public static string RootId{
			      get{
        			return "RootId";
      			}
		    }
			/// <summary>
			/// 父id
			/// </summary>
   			public static string ParentId{
			      get{
        			return "ParentId";
      			}
		    }
			/// <summary>
			/// 层数
			/// </summary>
   			public static string Depth{
			      get{
        			return "Depth";
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
			/// Seo标题
			/// </summary>
   			public static string SeoTitle{
			      get{
        			return "SeoTitle";
      			}
		    }
			/// <summary>
			/// Seo关键字(搜索文章)
			/// </summary>
   			public static string SeoKey{
			      get{
        			return "SeoKey";
      			}
		    }
			/// <summary>
			/// Seo描述
			/// </summary>
   			public static string SeoDesc{
			      get{
        			return "SeoDesc";
      			}
		    }
			/// <summary>
			/// 
			/// </summary>
   			public static string Manager_Id{
			      get{
        			return "Manager_Id";
      			}
		    }
			/// <summary>
			/// 修改人员id
			/// </summary>
   			public static string Manager_CName{
			      get{
        			return "Manager_CName";
      			}
		    }
			/// <summary>
			/// 修改人员姓名
			/// </summary>
   			public static string UpdateDate{
			      get{
        			return "UpdateDate";
      			}
		    }
                    
        }
}
