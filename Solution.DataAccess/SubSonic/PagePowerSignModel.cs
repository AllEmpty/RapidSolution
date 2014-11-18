 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// PagePowerSign表实体类
    /// </summary>
	[Serializable]
    public partial class PagePowerSign
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

		int _PagePowerSignPublic_Id = 0;
		/// <summary>
		/// 公用页面权限ID
		/// </summary>
		public int PagePowerSignPublic_Id
		{
			get { return _PagePowerSignPublic_Id; }
			set { _PagePowerSignPublic_Id = value; }
		}

		string _CName = "";
		/// <summary>
		/// 权限名称，如：浏览、添加、修改、删除、报表、查询、调动/分配、设置等(名称可以自由定，但建议取有意义的名称)
		/// </summary>
		public string CName
		{
			get { return _CName; }
			set { _CName = value; }
		}

		string _EName = "";
		/// <summary>
		/// 权限英文名称，除了在英文版权限设置时显示对应菜单外，还用来在页面程序中区分页面不同位置所调用的权限(在检测页面权限时使用)
		/// </summary>
		public string EName
		{
			get { return _EName; }
			set { _EName = value; }
		}

		int _MenuInfo_Id = 0;
		/// <summary>
		/// 菜单ID
		/// </summary>
		public int MenuInfo_Id
		{
			get { return _MenuInfo_Id; }
			set { _MenuInfo_Id = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("PagePowerSignPublic_Id=" +　PagePowerSignPublic_Id + "; ");
			sb.Append("CName=" +　CName + "; ");
			sb.Append("EName=" +　EName + "; ");
			sb.Append("MenuInfo_Id=" +　MenuInfo_Id + "; ");
			return sb.ToString();
        }

    } 

}


