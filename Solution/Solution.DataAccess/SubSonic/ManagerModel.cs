 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// Manager表实体类
    /// </summary>
	[Serializable]
    public partial class Manager
    {

		int _Id = 0;
		/// <summary>
		/// 主键Id
		/// </summary>
		public int Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		string _LoginName = "";
		/// <summary>
		/// 登陆账号
		/// </summary>
		public string LoginName
		{
			get { return _LoginName; }
			set { _LoginName = value; }
		}

		string _LoginPass = "";
		/// <summary>
		/// 登陆密码
		/// </summary>
		public string LoginPass
		{
			get { return _LoginPass; }
			set { _LoginPass = value; }
		}

		DateTime _LoginTime = new DateTime(1900,1,1);
		/// <summary>
		/// 最后登陆时间
		/// </summary>
		public DateTime LoginTime
		{
			get { return _LoginTime; }
			set { _LoginTime = value; }
		}

		string _LoginIp = "";
		/// <summary>
		/// 最后登陆IP
		/// </summary>
		public string LoginIp
		{
			get { return _LoginIp; }
			set { _LoginIp = value; }
		}

		int _LoginCount = 0;
		/// <summary>
		/// 登陆次数
		/// </summary>
		public int LoginCount
		{
			get { return _LoginCount; }
			set { _LoginCount = value; }
		}

		DateTime _CreateTime = new DateTime(1900,1,1);
		/// <summary>
		/// 注册时间
		/// </summary>
		public DateTime CreateTime
		{
			get { return _CreateTime; }
			set { _CreateTime = value; }
		}

		DateTime _UpdateTime = new DateTime(1900,1,1);
		/// <summary>
		/// 资料最后修改日期
		/// </summary>
		public DateTime UpdateTime
		{
			get { return _UpdateTime; }
			set { _UpdateTime = value; }
		}

		byte _IsMultiUser = 0;
		/// <summary>
		/// 是否允许同一帐号多人使用，0=只能单个在线，1=可以多人同时在线
		/// </summary>
		public byte IsMultiUser
		{
			get { return _IsMultiUser; }
			set { _IsMultiUser = value; }
		}

		int _Branch_Id = 0;
		/// <summary>
		/// 所属部门ID
		/// </summary>
		public int Branch_Id
		{
			get { return _Branch_Id; }
			set { _Branch_Id = value; }
		}

		string _Branch_Code = "";
		/// <summary>
		/// 所属部门编号，用户只能正式归属于一个部门
		/// </summary>
		public string Branch_Code
		{
			get { return _Branch_Code; }
			set { _Branch_Code = value; }
		}

		string _Branch_Name = "";
		/// <summary>
		/// 部门名称
		/// </summary>
		public string Branch_Name
		{
			get { return _Branch_Name; }
			set { _Branch_Name = value; }
		}

		string _Position_Id = "";
		/// <summary>
		/// 用户职位ID
		/// </summary>
		public string Position_Id
		{
			get { return _Position_Id; }
			set { _Position_Id = value; }
		}

		string _Position_Name = "";
		/// <summary>
		/// 职位名称
		/// </summary>
		public string Position_Name
		{
			get { return _Position_Name; }
			set { _Position_Name = value; }
		}

		byte _IsWork = 0;
		/// <summary>
		/// 0=离职，1=就职
		/// </summary>
		public byte IsWork
		{
			get { return _IsWork; }
			set { _IsWork = value; }
		}

		byte _IsEnable = 0;
		/// <summary>
		/// 账号是否启用，1=true(启用)，0=false（禁用）
		/// </summary>
		public byte IsEnable
		{
			get { return _IsEnable; }
			set { _IsEnable = value; }
		}

		string _CName = "";
		/// <summary>
		/// 用户中文名称
		/// </summary>
		public string CName
		{
			get { return _CName; }
			set { _CName = value; }
		}

		string _EName = "";
		/// <summary>
		/// 用户英文名称
		/// </summary>
		public string EName
		{
			get { return _EName; }
			set { _EName = value; }
		}

		string _PhotoImg = "";
		/// <summary>
		/// 头像图片路径
		/// </summary>
		public string PhotoImg
		{
			get { return _PhotoImg; }
			set { _PhotoImg = value; }
		}

		string _Sex = "";
		/// <summary>
		/// 性别（0=未知，1=男，2=女）
		/// </summary>
		public string Sex
		{
			get { return _Sex; }
			set { _Sex = value; }
		}

		string _Birthday = "";
		/// <summary>
		/// 出生日期
		/// </summary>
		public string Birthday
		{
			get { return _Birthday; }
			set { _Birthday = value; }
		}

		string _NativePlace = "";
		/// <summary>
		/// 籍贯
		/// </summary>
		public string NativePlace
		{
			get { return _NativePlace; }
			set { _NativePlace = value; }
		}

		string _NationalName = "";
		/// <summary>
		/// 民族
		/// </summary>
		public string NationalName
		{
			get { return _NationalName; }
			set { _NationalName = value; }
		}

		string _Record = "";
		/// <summary>
		/// 个人--学历
		/// </summary>
		public string Record
		{
			get { return _Record; }
			set { _Record = value; }
		}

		string _GraduateCollege = "";
		/// <summary>
		/// 毕业学校
		/// </summary>
		public string GraduateCollege
		{
			get { return _GraduateCollege; }
			set { _GraduateCollege = value; }
		}

		string _GraduateSpecialty = "";
		/// <summary>
		/// 毕业专业
		/// </summary>
		public string GraduateSpecialty
		{
			get { return _GraduateSpecialty; }
			set { _GraduateSpecialty = value; }
		}

		string _Tel = "";
		/// <summary>
		/// 个人--联系电话
		/// </summary>
		public string Tel
		{
			get { return _Tel; }
			set { _Tel = value; }
		}

		string _Mobile = "";
		/// <summary>
		/// 个人--移动电话
		/// </summary>
		public string Mobile
		{
			get { return _Mobile; }
			set { _Mobile = value; }
		}

		string _Email = "";
		/// <summary>
		/// 个人--联系邮箱
		/// </summary>
		public string Email
		{
			get { return _Email; }
			set { _Email = value; }
		}

		string _Qq = "";
		/// <summary>
		/// 个人--QQ
		/// </summary>
		public string Qq
		{
			get { return _Qq; }
			set { _Qq = value; }
		}

		string _Msn = "";
		/// <summary>
		/// 个人--Msn
		/// </summary>
		public string Msn
		{
			get { return _Msn; }
			set { _Msn = value; }
		}

		string _Address = "";
		/// <summary>
		/// 个人--通讯地址
		/// </summary>
		public string Address
		{
			get { return _Address; }
			set { _Address = value; }
		}

		string _Content = "";
		/// <summary>
		/// 备注
		/// </summary>
		public string Content
		{
			get { return _Content; }
			set { _Content = value; }
		}

		int _Manager_Id = 0;
		/// <summary>
		/// 修改人员id
		/// </summary>
		public int Manager_Id
		{
			get { return _Manager_Id; }
			set { _Manager_Id = value; }
		}

		string _Manager_CName = "";
		/// <summary>
		/// 修改人中文名称
		/// </summary>
		public string Manager_CName
		{
			get { return _Manager_CName; }
			set { _Manager_CName = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("LoginName=" +　LoginName + "; ");
			sb.Append("LoginPass=" +　LoginPass + "; ");
			sb.Append("LoginTime=" +　LoginTime + "; ");
			sb.Append("LoginIp=" +　LoginIp + "; ");
			sb.Append("LoginCount=" +　LoginCount + "; ");
			sb.Append("CreateTime=" +　CreateTime + "; ");
			sb.Append("UpdateTime=" +　UpdateTime + "; ");
			sb.Append("IsMultiUser=" +　IsMultiUser + "; ");
			sb.Append("Branch_Id=" +　Branch_Id + "; ");
			sb.Append("Branch_Code=" +　Branch_Code + "; ");
			sb.Append("Branch_Name=" +　Branch_Name + "; ");
			sb.Append("Position_Id=" +　Position_Id + "; ");
			sb.Append("Position_Name=" +　Position_Name + "; ");
			sb.Append("IsWork=" +　IsWork + "; ");
			sb.Append("IsEnable=" +　IsEnable + "; ");
			sb.Append("CName=" +　CName + "; ");
			sb.Append("EName=" +　EName + "; ");
			sb.Append("PhotoImg=" +　PhotoImg + "; ");
			sb.Append("Sex=" +　Sex + "; ");
			sb.Append("Birthday=" +　Birthday + "; ");
			sb.Append("NativePlace=" +　NativePlace + "; ");
			sb.Append("NationalName=" +　NationalName + "; ");
			sb.Append("Record=" +　Record + "; ");
			sb.Append("GraduateCollege=" +　GraduateCollege + "; ");
			sb.Append("GraduateSpecialty=" +　GraduateSpecialty + "; ");
			sb.Append("Tel=" +　Tel + "; ");
			sb.Append("Mobile=" +　Mobile + "; ");
			sb.Append("Email=" +　Email + "; ");
			sb.Append("Qq=" +　Qq + "; ");
			sb.Append("Msn=" +　Msn + "; ");
			sb.Append("Address=" +　Address + "; ");
			sb.Append("Content=" +　Content + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			return sb.ToString();
        }

    } 

}


