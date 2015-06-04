 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UploadFile
        /// Primary Key: Id
        /// </summary>
        public class UploadFileTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "UploadFile";
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
			/// 新文件名（包括扩展名）
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 新路径（包括文件名）
			/// </summary>
   			public static string Path{
			      get{
        			return "Path";
      			}
		    }
			/// <summary>
			/// 扩展名
			/// </summary>
   			public static string Ext{
			      get{
        			return "Ext";
      			}
		    }
			/// <summary>
			/// 原文件名（包括扩展名）
			/// </summary>
   			public static string Src{
			      get{
        			return "Src";
      			}
		    }
			/// <summary>
			/// 文件大小
			/// </summary>
   			public static string Size{
			      get{
        			return "Size";
      			}
		    }
			/// <summary>
			/// 图片的宽
			/// </summary>
   			public static string PicWidth{
			      get{
        			return "PicWidth";
      			}
		    }
			/// <summary>
			/// 图片的高
			/// </summary>
   			public static string PicHeight{
			      get{
        			return "PicHeight";
      			}
		    }
			/// <summary>
			/// 系统ID:---UploadConfig_Id
			/// 1=后台--新闻封面/新闻编辑器
			/// </summary>
   			public static string UploadConfig_Id{
			      get{
        			return "UploadConfig_Id";
      			}
		    }
			/// <summary>
			/// 关联表ID--1=NewsInfo,2=PrdInfo,
			/// </summary>
   			public static string JoinName{
			      get{
        			return "JoinName";
      			}
		    }
			/// <summary>
			/// 关联ID--所属的文章ID,产品ID，头像等
			/// </summary>
   			public static string JoinId{
			      get{
        			return "JoinId";
      			}
		    }
			/// <summary>
			/// 用户类别:1=管理员上传，2=会员上传
			/// </summary>
   			public static string UserType{
			      get{
        			return "UserType";
      			}
		    }
			/// <summary>
			/// 上传者ID
			/// </summary>
   			public static string UserId{
			      get{
        			return "UserId";
      			}
		    }
			/// <summary>
			/// 上传者Name
			/// </summary>
   			public static string UserName{
			      get{
        			return "UserName";
      			}
		    }
			/// <summary>
			/// 上传者Ip
			/// </summary>
   			public static string UserIp{
			      get{
        			return "UserIp";
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
			/// 备注
			/// </summary>
   			public static string InfoText{
			      get{
        			return "InfoText";
      			}
		    }
			/// <summary>
			/// 随机Key
			/// </summary>
   			public static string RndKey{
			      get{
        			return "RndKey";
      			}
		    }
                    
        }
}
