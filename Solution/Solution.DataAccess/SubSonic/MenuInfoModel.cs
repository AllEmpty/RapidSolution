 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// MenuInfo表实体类
    /// </summary>
	[Serializable]
    public partial class MenuInfo
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

		string _Name = "";
		/// <summary>
		/// 菜单名称或各个页面功能名称
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		string _Url = "";
		/// <summary>
		/// 各页面URL（主菜单与分类菜单没有URL）
		/// </summary>
		public string Url
		{
			get { return _Url; }
			set { _Url = value; }
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

		byte _IsDisplay = 0;
		/// <summary>
		/// 该菜单是否在菜单栏显示，0=不显示，1=显示
		/// </summary>
		public byte IsDisplay
		{
			get { return _IsDisplay; }
			set { _IsDisplay = value; }
		}

		byte _IsMenu = 0;
		/// <summary>
		/// 是否是菜单还是页面
		/// </summary>
		public byte IsMenu
		{
			get { return _IsMenu; }
			set { _IsMenu = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Url=" +　Url + "; ");
			sb.Append("ParentId=" +　ParentId + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("Depth=" +　Depth + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("IsMenu=" +　IsMenu + "; ");
			return sb.ToString();
        }

    } 

}


