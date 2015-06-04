 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// V_Position_Branch表实体类
    /// </summary>
	[Serializable]
    public partial class V_Position_Branch
    {

		string _Code = "";
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			get { return _Code; }
			set { _Code = value; }
		}

		int _Id = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		string _Name = "";
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		int _Branch_Id = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Branch_Id
		{
			get { return _Branch_Id; }
			set { _Branch_Id = value; }
		}

		string _Branch_Code = "";
		/// <summary>
		/// 
		/// </summary>
		public string Branch_Code
		{
			get { return _Branch_Code; }
			set { _Branch_Code = value; }
		}

		string _Branch_Name = "";
		/// <summary>
		/// 
		/// </summary>
		public string Branch_Name
		{
			get { return _Branch_Name; }
			set { _Branch_Name = value; }
		}

		string _PagePower = "";
		/// <summary>
		/// 
		/// </summary>
		public string PagePower
		{
			get { return _PagePower; }
			set { _PagePower = value; }
		}

		string _ControlPower = "";
		/// <summary>
		/// 
		/// </summary>
		public string ControlPower
		{
			get { return _ControlPower; }
			set { _ControlPower = value; }
		}

		byte _IsSetBranchPower = 0;
		/// <summary>
		/// 
		/// </summary>
		public byte IsSetBranchPower
		{
			get { return _IsSetBranchPower; }
			set { _IsSetBranchPower = value; }
		}

		string _SetBranchCode = "";
		/// <summary>
		/// 
		/// </summary>
		public string SetBranchCode
		{
			get { return _SetBranchCode; }
			set { _SetBranchCode = value; }
		}

		int _Manager_Id = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Manager_Id
		{
			get { return _Manager_Id; }
			set { _Manager_Id = value; }
		}

		string _Manager_CName = "";
		/// <summary>
		/// 
		/// </summary>
		public string Manager_CName
		{
			get { return _Manager_CName; }
			set { _Manager_CName = value; }
		}

		DateTime _UpdateDate = new DateTime(1900,1,1);
		/// <summary>
		/// 
		/// </summary>
		public DateTime UpdateDate
		{
			get { return _UpdateDate; }
			set { _UpdateDate = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Code=" +　Code + "; ");
			sb.Append("Id=" +　Id + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Branch_Id=" +　Branch_Id + "; ");
			sb.Append("Branch_Code=" +　Branch_Code + "; ");
			sb.Append("Branch_Name=" +　Branch_Name + "; ");
			sb.Append("PagePower=" +　PagePower + "; ");
			sb.Append("ControlPower=" +　ControlPower + "; ");
			sb.Append("IsSetBranchPower=" +　IsSetBranchPower + "; ");
			sb.Append("SetBranchCode=" +　SetBranchCode + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

    } 

}


