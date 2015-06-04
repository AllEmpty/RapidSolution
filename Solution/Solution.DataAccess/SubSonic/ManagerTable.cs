 
using System;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Manager
        /// Primary Key: Id
        /// </summary>
        public class ManagerTable {
			/// <summary>
			/// 表名
			/// </summary>
			public static string TableName {
				get{
        			return "Manager";
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
			/// 登陆账号
			/// </summary>
   			public static string LoginName{
			      get{
        			return "LoginName";
      			}
		    }
			/// <summary>
			/// 登陆密码
			/// </summary>
   			public static string LoginPass{
			      get{
        			return "LoginPass";
      			}
		    }
			/// <summary>
			/// 最后登陆时间
			/// </summary>
   			public static string LoginTime{
			      get{
        			return "LoginTime";
      			}
		    }
			/// <summary>
			/// 最后登陆IP
			/// </summary>
   			public static string LoginIp{
			      get{
        			return "LoginIp";
      			}
		    }
			/// <summary>
			/// 登陆次数
			/// </summary>
   			public static string LoginCount{
			      get{
        			return "LoginCount";
      			}
		    }
			/// <summary>
			/// 注册时间
			/// </summary>
   			public static string CreateTime{
			      get{
        			return "CreateTime";
      			}
		    }
			/// <summary>
			/// 资料最后修改日期
			/// </summary>
   			public static string UpdateTime{
			      get{
        			return "UpdateTime";
      			}
		    }
			/// <summary>
			/// 是否允许同一帐号多人使用，0=只能单个在线，1=可以多人同时在线
			/// </summary>
   			public static string IsMultiUser{
			      get{
        			return "IsMultiUser";
      			}
		    }
			/// <summary>
			/// 所属部门ID
			/// </summary>
   			public static string Branch_Id{
			      get{
        			return "Branch_Id";
      			}
		    }
			/// <summary>
			/// 所属部门编号，用户只能正式归属于一个部门
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
			/// 用户职位ID
			/// </summary>
   			public static string Position_Id{
			      get{
        			return "Position_Id";
      			}
		    }
			/// <summary>
			/// 职位名称
			/// </summary>
   			public static string Position_Name{
			      get{
        			return "Position_Name";
      			}
		    }
			/// <summary>
			/// 0=离职，1=就职
			/// </summary>
   			public static string IsWork{
			      get{
        			return "IsWork";
      			}
		    }
			/// <summary>
			/// 账号是否启用，1=true(启用)，0=false（禁用）
			/// </summary>
   			public static string IsEnable{
			      get{
        			return "IsEnable";
      			}
		    }
			/// <summary>
			/// 用户中文名称
			/// </summary>
   			public static string CName{
			      get{
        			return "CName";
      			}
		    }
			/// <summary>
			/// 用户英文名称
			/// </summary>
   			public static string EName{
			      get{
        			return "EName";
      			}
		    }
			/// <summary>
			/// 头像图片路径
			/// </summary>
   			public static string PhotoImg{
			      get{
        			return "PhotoImg";
      			}
		    }
			/// <summary>
			/// 性别（0=未知，1=男，2=女）
			/// </summary>
   			public static string Sex{
			      get{
        			return "Sex";
      			}
		    }
			/// <summary>
			/// 出生日期
			/// </summary>
   			public static string Birthday{
			      get{
        			return "Birthday";
      			}
		    }
			/// <summary>
			/// 籍贯
			/// </summary>
   			public static string NativePlace{
			      get{
        			return "NativePlace";
      			}
		    }
			/// <summary>
			/// 民族
			/// </summary>
   			public static string NationalName{
			      get{
        			return "NationalName";
      			}
		    }
			/// <summary>
			/// 个人--学历
			/// </summary>
   			public static string Record{
			      get{
        			return "Record";
      			}
		    }
			/// <summary>
			/// 毕业学校
			/// </summary>
   			public static string GraduateCollege{
			      get{
        			return "GraduateCollege";
      			}
		    }
			/// <summary>
			/// 毕业专业
			/// </summary>
   			public static string GraduateSpecialty{
			      get{
        			return "GraduateSpecialty";
      			}
		    }
			/// <summary>
			/// 个人--联系电话
			/// </summary>
   			public static string Tel{
			      get{
        			return "Tel";
      			}
		    }
			/// <summary>
			/// 个人--移动电话
			/// </summary>
   			public static string Mobile{
			      get{
        			return "Mobile";
      			}
		    }
			/// <summary>
			/// 个人--联系邮箱
			/// </summary>
   			public static string Email{
			      get{
        			return "Email";
      			}
		    }
			/// <summary>
			/// 个人--QQ
			/// </summary>
   			public static string Qq{
			      get{
        			return "Qq";
      			}
		    }
			/// <summary>
			/// 个人--Msn
			/// </summary>
   			public static string Msn{
			      get{
        			return "Msn";
      			}
		    }
			/// <summary>
			/// 个人--通讯地址
			/// </summary>
   			public static string Address{
			      get{
        			return "Address";
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
			/// 修改人员id
			/// </summary>
   			public static string Manager_Id{
			      get{
        			return "Manager_Id";
      			}
		    }
			/// <summary>
			/// 修改人中文名称
			/// </summary>
   			public static string Manager_CName{
			      get{
        			return "Manager_CName";
      			}
		    }
                    
        }
}
