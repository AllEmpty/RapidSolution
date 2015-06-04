 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Information
        /// Primary Key: Id
        /// </summary>
        public class InformationTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "Information";
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
			/// 根类id
			/// </summary>
   			public static string InformationClass_Root_Id{
			      get{
        			return "InformationClass_Root_Id";
      			}
		    }
			/// <summary>
			/// 根类名称
			/// </summary>
   			public static string InformationClass_Root_Name{
			      get{
        			return "InformationClass_Root_Name";
      			}
		    }
			/// <summary>
			/// 分类id
			/// </summary>
   			public static string InformationClass_Id{
			      get{
        			return "InformationClass_Id";
      			}
		    }
			/// <summary>
			/// 分类名称
			/// </summary>
   			public static string InformationClass_Name{
			      get{
        			return "InformationClass_Name";
      			}
		    }
			/// <summary>
			/// 标题
			/// </summary>
   			public static string Title{
			      get{
        			return "Title";
      			}
		    }
			/// <summary>
			/// 重定向页面（跳转页面），不为空时直接跳转到指定页面
			/// </summary>
   			public static string RedirectUrl{
			      get{
        			return "RedirectUrl";
      			}
		    }
			/// <summary>
			/// 文章内容（Html图文编辑）
			/// </summary>
   			public static string Content{
			      get{
        			return "Content";
      			}
		    }
			/// <summary>
			/// 上传文件的名字列表: 20040413081811.gif|20040413081811.jpg|
			/// </summary>
   			public static string Upload{
			      get{
        			return "Upload";
      			}
		    }
			/// <summary>
			/// 文章封面图片
			/// </summary>
   			public static string FrontCoverImg{
			      get{
        			return "FrontCoverImg";
      			}
		    }
			/// <summary>
			/// 简介
			/// </summary>
   			public static string Notes{
			      get{
        			return "Notes";
      			}
		    }
			/// <summary>
			/// 新闻时间(可以修改)
			/// </summary>
   			public static string NewsTime{
			      get{
        			return "NewsTime";
      			}
		    }
			/// <summary>
			/// 文章关键字
			/// </summary>
   			public static string Keywords{
			      get{
        			return "Keywords";
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
			/// 作者姓名
			/// </summary>
   			public static string Author{
			      get{
        			return "Author";
      			}
		    }
			/// <summary>
			/// 转贴自(名)/出处(名)
			/// </summary>
   			public static string FromName{
			      get{
        			return "FromName";
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
			/// 是否显示, 0=False,1=True,
			/// </summary>
   			public static string IsDisplay{
			      get{
        			return "IsDisplay";
      			}
		    }
			/// <summary>
			/// 是否要推荐,0=不要,1=要
			/// </summary>
   			public static string IsHot{
			      get{
        			return "IsHot";
      			}
		    }
			/// <summary>
			/// 是否置顶,0=false,1=true
			/// </summary>
   			public static string IsTop{
			      get{
        			return "IsTop";
      			}
		    }
			/// <summary>
			/// 是否为单页
			/// </summary>
   			public static string IsPage{
			      get{
        			return "IsPage";
      			}
		    }
			/// <summary>
			/// 回收站标志,0=false,1=true
			/// </summary>
   			public static string IsDel{
			      get{
        			return "IsDel";
      			}
		    }
			/// <summary>
			/// 评论数
			/// </summary>
   			public static string CommentCount{
			      get{
        			return "CommentCount";
      			}
		    }
			/// <summary>
			/// 浏览量
			/// </summary>
   			public static string ViewCount{
			      get{
        			return "ViewCount";
      			}
		    }
			/// <summary>
			/// 年（用于查询）
			/// </summary>
   			public static string AddYear{
			      get{
        			return "AddYear";
      			}
		    }
			/// <summary>
			/// 月（用于查询）
			/// </summary>
   			public static string AddMonth{
			      get{
        			return "AddMonth";
      			}
		    }
			/// <summary>
			/// 日（用于查询）
			/// </summary>
   			public static string AddDay{
			      get{
        			return "AddDay";
      			}
		    }
			/// <summary>
			/// 添加时间
			/// </summary>
   			public static string AddDate{
			      get{
        			return "AddDate";
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
