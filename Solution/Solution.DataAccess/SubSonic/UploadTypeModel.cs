 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// UploadType表实体类
    /// </summary>
	[Serializable]
    public partial class UploadType
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

		string _TypeKey = "";
		/// <summary>
		/// 关键字
		/// </summary>
		public string TypeKey
		{
			get { return _TypeKey; }
			set { _TypeKey = value; }
		}

		string _Name = "";
		/// <summary>
		/// 类别名称
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		string _Ext = "";
		/// <summary>
		/// 允许的扩展名使用,分隔，例如:"jpg,png,gif"
		/// </summary>
		public string Ext
		{
			get { return _Ext; }
			set { _Ext = value; }
		}

		byte _IsSys = 0;
		/// <summary>
		/// 1=系统默认，不能删除，不能修改 TypeKey
		/// </summary>
		public byte IsSys
		{
			get { return _IsSys; }
			set { _IsSys = value; }
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
			sb.Append("TypeKey=" +　TypeKey + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Ext=" +　Ext + "; ");
			sb.Append("IsSys=" +　IsSys + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

    } 

}


