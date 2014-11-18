 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Advertisement
        /// Primary Key: Id
        /// </summary>
        public class AdvertisementTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "Advertisement";
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
			/// 标题
			/// </summary>
   			public static string Name{
			      get{
        			return "Name";
      			}
		    }
			/// <summary>
			/// 备注
			/// </summary>
   			public static string Content{
			      get{
        			return "Content";
      			}
		    }
			/// <summary>
			/// 链接Url
			/// </summary>
   			public static string Url{
			      get{
        			return "Url";
      			}
		    }
			/// <summary>
			/// 关键字，只能由字母数字组成，主要用于模板标签 {%ad-InfoKey%}
			/// </summary>
   			public static string Keyword{
			      get{
        			return "Keyword";
      			}
		    }
			/// <summary>
			/// 广告位置Id
			/// </summary>
   			public static string AdvertisingPosition_Id{
			      get{
        			return "AdvertisingPosition_Id";
      			}
		    }
			/// <summary>
			/// 广告位置名称
			/// </summary>
   			public static string AdvertisingPosition_Name{
			      get{
        			return "AdvertisingPosition_Name";
      			}
		    }
			/// <summary>
			/// 图片
			/// </summary>
   			public static string AdImg{
			      get{
        			return "AdImg";
      			}
		    }
			/// <summary>
			/// 显示频率（同一个位置有多个广告时，这里用来计算它随机出现的频率）
			/// </summary>
   			public static string ShowRate{
			      get{
        			return "ShowRate";
      			}
		    }
			/// <summary>
			/// 开始时间
			/// </summary>
   			public static string StartTime{
			      get{
        			return "StartTime";
      			}
		    }
			/// <summary>
			/// 结束时间
			/// </summary>
   			public static string EndTime{
			      get{
        			return "EndTime";
      			}
		    }
			/// <summary>
			/// 审核, 0=False,1=True,
			/// </summary>
   			public static string IsDisplay{
			      get{
        			return "IsDisplay";
      			}
		    }
			/// <summary>
			/// 点击数
			/// </summary>
   			public static string HitCount{
			      get{
        			return "HitCount";
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
