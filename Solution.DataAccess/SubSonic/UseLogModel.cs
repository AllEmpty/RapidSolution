 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// UseLog表实体类
    /// </summary>
	[Serializable]
    public partial class UseLog
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

		DateTime _AddDate = new DateTime(1900,1,1);
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime AddDate
		{
			get { return _AddDate; }
			set { _AddDate = value; }
		}

		int _Manager_Id = 0;
		/// <summary>
		/// 登陆用户ID
		/// </summary>
		public int Manager_Id
		{
			get { return _Manager_Id; }
			set { _Manager_Id = value; }
		}

		string _Manager_CName = "";
		/// <summary>
		/// 用户中文名称
		/// </summary>
		public string Manager_CName
		{
			get { return _Manager_CName; }
			set { _Manager_CName = value; }
		}

		string _Ip = "";
		/// <summary>
		/// 登陆IP
		/// </summary>
		public string Ip
		{
			get { return _Ip; }
			set { _Ip = value; }
		}

		int _MenuInfo_Id = 0;
		/// <summary>
		/// 菜单ID（用户操作页面）
		/// </summary>
		public int MenuInfo_Id
		{
			get { return _MenuInfo_Id; }
			set { _MenuInfo_Id = value; }
		}

		string _MenuInfo_Name = "";
		/// <summary>
		/// 菜单名称或各个页面功能名称
		/// </summary>
		public string MenuInfo_Name
		{
			get { return _MenuInfo_Name; }
			set { _MenuInfo_Name = value; }
		}

		string _Notes = "";
		/// <summary>
		/// 操作内容
		/// </summary>
		public string Notes
		{
			get { return _Notes; }
			set { _Notes = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("AddDate=" +　AddDate + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("Ip=" +　Ip + "; ");
			sb.Append("MenuInfo_Id=" +　MenuInfo_Id + "; ");
			sb.Append("MenuInfo_Name=" +　MenuInfo_Name + "; ");
			sb.Append("Notes=" +　Notes + "; ");
			return sb.ToString();
        }

    } 

}


