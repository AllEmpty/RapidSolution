 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// AdvertisingPosition表实体类
    /// </summary>
	[Serializable]
    public partial class AdvertisingPosition
    {

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

		int _ParentId = 0;
		/// <summary>
		/// 
		/// </summary>
		public int ParentId
		{
			get { return _ParentId; }
			set { _ParentId = value; }
		}

		int _Depth = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Depth
		{
			get { return _Depth; }
			set { _Depth = value; }
		}

		int _Sort = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			get { return _Sort; }
			set { _Sort = value; }
		}

		string _Keyword = "";
		/// <summary>
		/// 
		/// </summary>
		public string Keyword
		{
			get { return _Keyword; }
			set { _Keyword = value; }
		}

		string _MapImg = "";
		/// <summary>
		/// 
		/// </summary>
		public string MapImg
		{
			get { return _MapImg; }
			set { _MapImg = value; }
		}

		byte _IsDisplay = 0;
		/// <summary>
		/// 
		/// </summary>
		public byte IsDisplay
		{
			get { return _IsDisplay; }
			set { _IsDisplay = value; }
		}

		int _Width = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Width
		{
			get { return _Width; }
			set { _Width = value; }
		}

		int _Height = 0;
		/// <summary>
		/// 
		/// </summary>
		public int Height
		{
			get { return _Height; }
			set { _Height = value; }
		}

		string _PicImg = "";
		/// <summary>
		/// 
		/// </summary>
		public string PicImg
		{
			get { return _PicImg; }
			set { _PicImg = value; }
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

		DateTime _AddDate = new DateTime(1900,1,1);
		/// <summary>
		/// 
		/// </summary>
		public DateTime AddDate
		{
			get { return _AddDate; }
			set { _AddDate = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("ParentId=" +　ParentId + "; ");
			sb.Append("Depth=" +　Depth + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("Keyword=" +　Keyword + "; ");
			sb.Append("MapImg=" +　MapImg + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("Width=" +　Width + "; ");
			sb.Append("Height=" +　Height + "; ");
			sb.Append("PicImg=" +　PicImg + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("AddDate=" +　AddDate + "; ");
			return sb.ToString();
        }

    } 

}


