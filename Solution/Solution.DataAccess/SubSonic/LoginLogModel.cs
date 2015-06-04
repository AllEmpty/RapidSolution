 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// LoginLog表实体类
    /// </summary>
	[Serializable]
    public partial class LoginLog
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
		/// 登陆日期
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
			sb.Append("Notes=" +　Notes + "; ");
			return sb.ToString();
        }

    } 

}


