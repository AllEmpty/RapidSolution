 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// Branch表实体类
    /// </summary>
	[Serializable]
    public partial class Branch
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

		string _Code = "";
		/// <summary>
		/// 部门ID，内容为001001001，即每低一级部门，编码增加三位小数
		/// </summary>
		public string Code
		{
			get { return _Code; }
			set { _Code = value; }
		}

		string _Name = "";
		/// <summary>
		/// 部门名称
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		string _Notes = "";
		/// <summary>
		/// 备注
		/// </summary>
		public string Notes
		{
			get { return _Notes; }
			set { _Notes = value; }
		}

		int _ParentId = 0;
		/// <summary>
		/// 父ID
		/// </summary>
		public int ParentId
		{
			get { return _ParentId; }
			set { _ParentId = value; }
		}

		int _Sort = 0;
		/// <summary>
		/// 排序
		/// </summary>
		public int Sort
		{
			get { return _Sort; }
			set { _Sort = value; }
		}

		int _Depth = 0;
		/// <summary>
		/// 深度
		/// </summary>
		public int Depth
		{
			get { return _Depth; }
			set { _Depth = value; }
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
		/// 修改人员姓名
		/// </summary>
		public string Manager_CName
		{
			get { return _Manager_CName; }
			set { _Manager_CName = value; }
		}

		DateTime _UpdateDate = new DateTime(1900,1,1);
		/// <summary>
		/// 修改时间
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
			sb.Append("Id=" +　Id + "; ");
			sb.Append("Code=" +　Code + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Notes=" +　Notes + "; ");
			sb.Append("ParentId=" +　ParentId + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("Depth=" +　Depth + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

    } 

}


