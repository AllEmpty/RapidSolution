 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UploadConfig
        /// Primary Key: Id
        /// </summary>
        public class UploadConfigTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "UploadConfig";
      			}
			}

			/// <summary>
			/// 唯一索引，但不自增，
			/// </summary>
   			public static string Id{
			      get{
        			return "Id";
      			}
		    }
			/// <summary>
			/// 模块名称
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 关联表名
			/// </summary>
   			public static string JoinName{
			      get{
        			return "JoinName";
      			}
		    }
			/// <summary>
			/// 用户类别：1=管理员上传，2=会员上传
			/// </summary>
   			public static string UserType{
			      get{
        			return "UserType";
      			}
		    }
			/// <summary>
			/// 上传类型表主键
			/// </summary>
   			public static string UploadType_Id{
			      get{
        			return "UploadType_Id";
      			}
		    }
			/// <summary>
			/// 上传类型名称
			/// </summary>
   			public static string UploadType_Name{
			      get{
        			return "UploadType_Name";
      			}
		    }
			/// <summary>
			/// 上传类型：image(默认),flash,media,file,editor，绑定UploadType表对应字段
			/// </summary>
   			public static string UploadType_TypeKey{
			      get{
        			return "UploadType_TypeKey";
      			}
		    }
			/// <summary>
			/// 图片类,允许最大上传Size（单位：KB），默认:200 =200 KB，"上传类别"为image专用
			/// </summary>
   			public static string PicSize{
			      get{
        			return "PicSize";
      			}
		    }
			/// <summary>
			/// 附件类,允许最大上传Size（单位：KB），默认:20000 = 20 M，当使用"上传类别"非image的情况下使用
			/// </summary>
   			public static string FileSize{
			      get{
        			return "FileSize";
      			}
		    }
			/// <summary>
			/// 保存的目录"/UploadFile/n/","/UploadFile/p/"
			/// </summary>
   			public static string SaveDir{
			      get{
        			return "SaveDir";
      			}
		    }
			/// <summary>
			/// 1=使用中,0=停止使用
			/// </summary>
   			public static string IsPost{
			      get{
        			return "IsPost";
      			}
		    }
			/// <summary>
			/// 1=flash上传，0=web上传
			/// </summary>
   			public static string IsSwf{
			      get{
        			return "IsSwf";
      			}
		    }
			/// <summary>
			/// 是否检查提交输入口是否为本服务器（Flash提交的必须设置为false，不用检查）
			/// </summary>
   			public static string IsChkSrcPost{
			      get{
        			return "IsChkSrcPost";
      			}
		    }
			/// <summary>
			/// 是否按比例生成
			/// </summary>
   			public static string IsFixPic{
			      get{
        			return "IsFixPic";
      			}
		    }
			/// <summary>
			/// 0=按比例生成宽高，1=固定图片宽高，2=固定背景宽高，图片按比例生成
			/// </summary>
   			public static string CutType{
			      get{
        			return "CutType";
      			}
		    }
			/// <summary>
			/// 最大宽度，超过将按比例进行缩放
			/// </summary>
   			public static string PicWidth{
			      get{
        			return "PicWidth";
      			}
		    }
			/// <summary>
			/// 最大高度，超过将按比例进行缩放
			/// </summary>
   			public static string PicHeight{
			      get{
        			return "PicHeight";
      			}
		    }
			/// <summary>
			/// 图片质量，0=使用默认值，>0指定质量值（指定值的情况下，范围：50-100）
			/// </summary>
   			public static string PicQuality{
			      get{
        			return "PicQuality";
      			}
		    }
			/// <summary>
			/// 1=编辑器专用,0=web
			/// </summary>
   			public static string IsEditor{
			      get{
        			return "IsEditor";
      			}
		    }
			/// <summary>
			/// 是否创建大图(原始图片)，1=是，0=否
			/// </summary>
   			public static string IsBigPic{
			      get{
        			return "IsBigPic";
      			}
		    }
			/// <summary>
			/// 大图宽度
			/// </summary>
   			public static string BigWidth{
			      get{
        			return "BigWidth";
      			}
		    }
			/// <summary>
			/// 大图高度
			/// </summary>
   			public static string BigHeight{
			      get{
        			return "BigHeight";
      			}
		    }
			/// <summary>
			/// 大图压缩质量
			/// </summary>
   			public static string BigQuality{
			      get{
        			return "BigQuality";
      			}
		    }
			/// <summary>
			/// 是否创建中图，1=是，0=否
			/// </summary>
   			public static string IsMidPic{
			      get{
        			return "IsMidPic";
      			}
		    }
			/// <summary>
			/// 中图宽度
			/// </summary>
   			public static string MidWidth{
			      get{
        			return "MidWidth";
      			}
		    }
			/// <summary>
			/// 中图高度
			/// </summary>
   			public static string MidHeight{
			      get{
        			return "MidHeight";
      			}
		    }
			/// <summary>
			/// 中图压缩质量
			/// </summary>
   			public static string MidQuality{
			      get{
        			return "MidQuality";
      			}
		    }
			/// <summary>
			/// 是否创建小图，1=是，0=否
			/// </summary>
   			public static string IsMinPic{
			      get{
        			return "IsMinPic";
      			}
		    }
			/// <summary>
			/// 小图宽度
			/// </summary>
   			public static string MinWidth{
			      get{
        			return "MinWidth";
      			}
		    }
			/// <summary>
			/// 小图高度
			/// </summary>
   			public static string MinHeight{
			      get{
        			return "MinHeight";
      			}
		    }
			/// <summary>
			/// 小图压缩质量
			/// </summary>
   			public static string MinQuality{
			      get{
        			return "MinQuality";
      			}
		    }
			/// <summary>
			/// 是否创建推荐图，1=是，0=否
			/// </summary>
   			public static string IsHotPic{
			      get{
        			return "IsHotPic";
      			}
		    }
			/// <summary>
			/// 推荐图宽度
			/// </summary>
   			public static string HotWidth{
			      get{
        			return "HotWidth";
      			}
		    }
			/// <summary>
			/// 推荐图高度
			/// </summary>
   			public static string HotHeight{
			      get{
        			return "HotHeight";
      			}
		    }
			/// <summary>
			/// 推荐图压缩质量
			/// </summary>
   			public static string HotQuality{
			      get{
        			return "HotQuality";
      			}
		    }
			/// <summary>
			/// 是否加水印，1=是，0=否
			/// </summary>
   			public static string IsWaterPic{
			      get{
        			return "IsWaterPic";
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
