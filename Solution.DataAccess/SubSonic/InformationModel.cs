 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// Information表实体类
    /// </summary>
	[Serializable]
    public partial class Information
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

		int _InformationClass_Root_Id = 0;
		/// <summary>
		/// 根类id
		/// </summary>
		public int InformationClass_Root_Id
		{
			get { return _InformationClass_Root_Id; }
			set { _InformationClass_Root_Id = value; }
		}

		string _InformationClass_Root_Name = "";
		/// <summary>
		/// 根类名称
		/// </summary>
		public string InformationClass_Root_Name
		{
			get { return _InformationClass_Root_Name; }
			set { _InformationClass_Root_Name = value; }
		}

		int _InformationClass_Id = 0;
		/// <summary>
		/// 分类id
		/// </summary>
		public int InformationClass_Id
		{
			get { return _InformationClass_Id; }
			set { _InformationClass_Id = value; }
		}

		string _InformationClass_Name = "";
		/// <summary>
		/// 分类名称
		/// </summary>
		public string InformationClass_Name
		{
			get { return _InformationClass_Name; }
			set { _InformationClass_Name = value; }
		}

		string _Title = "";
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}

		string _RedirectUrl = "";
		/// <summary>
		/// 重定向页面（跳转页面），不为空时直接跳转到指定页面
		/// </summary>
		public string RedirectUrl
		{
			get { return _RedirectUrl; }
			set { _RedirectUrl = value; }
		}

		string _Content = "";
		/// <summary>
		/// 文章内容（Html图文编辑）
		/// </summary>
		public string Content
		{
			get { return _Content; }
			set { _Content = value; }
		}

		string _Upload = "";
		/// <summary>
		/// 上传文件的名字列表: 20040413081811.gif|20040413081811.jpg|
		/// </summary>
		public string Upload
		{
			get { return _Upload; }
			set { _Upload = value; }
		}

		string _FrontCoverImg = "";
		/// <summary>
		/// 文章封面图片
		/// </summary>
		public string FrontCoverImg
		{
			get { return _FrontCoverImg; }
			set { _FrontCoverImg = value; }
		}

		string _Notes = "";
		/// <summary>
		/// 简介
		/// </summary>
		public string Notes
		{
			get { return _Notes; }
			set { _Notes = value; }
		}

		DateTime _NewsTime = new DateTime(1900,1,1);
		/// <summary>
		/// 新闻时间(可以修改)
		/// </summary>
		public DateTime NewsTime
		{
			get { return _NewsTime; }
			set { _NewsTime = value; }
		}

		string _Keywords = "";
		/// <summary>
		/// 文章关键字
		/// </summary>
		public string Keywords
		{
			get { return _Keywords; }
			set { _Keywords = value; }
		}

		string _SeoTitle = "";
		/// <summary>
		/// Seo标题
		/// </summary>
		public string SeoTitle
		{
			get { return _SeoTitle; }
			set { _SeoTitle = value; }
		}

		string _SeoKey = "";
		/// <summary>
		/// Seo关键字(搜索文章)
		/// </summary>
		public string SeoKey
		{
			get { return _SeoKey; }
			set { _SeoKey = value; }
		}

		string _SeoDesc = "";
		/// <summary>
		/// Seo描述
		/// </summary>
		public string SeoDesc
		{
			get { return _SeoDesc; }
			set { _SeoDesc = value; }
		}

		string _Author = "";
		/// <summary>
		/// 作者姓名
		/// </summary>
		public string Author
		{
			get { return _Author; }
			set { _Author = value; }
		}

		string _FromName = "";
		/// <summary>
		/// 转贴自(名)/出处(名)
		/// </summary>
		public string FromName
		{
			get { return _FromName; }
			set { _FromName = value; }
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

		byte _IsDisplay = 0;
		/// <summary>
		/// 是否显示, 0=False,1=True,
		/// </summary>
		public byte IsDisplay
		{
			get { return _IsDisplay; }
			set { _IsDisplay = value; }
		}

		byte _IsHot = 0;
		/// <summary>
		/// 是否要推荐,0=不要,1=要
		/// </summary>
		public byte IsHot
		{
			get { return _IsHot; }
			set { _IsHot = value; }
		}

		byte _IsTop = 0;
		/// <summary>
		/// 是否置顶,0=false,1=true
		/// </summary>
		public byte IsTop
		{
			get { return _IsTop; }
			set { _IsTop = value; }
		}

		byte _IsPage = 0;
		/// <summary>
		/// 是否为单页
		/// </summary>
		public byte IsPage
		{
			get { return _IsPage; }
			set { _IsPage = value; }
		}

		byte _IsDel = 0;
		/// <summary>
		/// 回收站标志,0=false,1=true
		/// </summary>
		public byte IsDel
		{
			get { return _IsDel; }
			set { _IsDel = value; }
		}

		int _CommentCount = 0;
		/// <summary>
		/// 评论数
		/// </summary>
		public int CommentCount
		{
			get { return _CommentCount; }
			set { _CommentCount = value; }
		}

		int _ViewCount = 0;
		/// <summary>
		/// 浏览量
		/// </summary>
		public int ViewCount
		{
			get { return _ViewCount; }
			set { _ViewCount = value; }
		}

		int _AddYear = 0;
		/// <summary>
		/// 年（用于查询）
		/// </summary>
		public int AddYear
		{
			get { return _AddYear; }
			set { _AddYear = value; }
		}

		int _AddMonth = 0;
		/// <summary>
		/// 月（用于查询）
		/// </summary>
		public int AddMonth
		{
			get { return _AddMonth; }
			set { _AddMonth = value; }
		}

		int _AddDay = 0;
		/// <summary>
		/// 日（用于查询）
		/// </summary>
		public int AddDay
		{
			get { return _AddDay; }
			set { _AddDay = value; }
		}

		DateTime _AddDate = new DateTime(1900,1,1);
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime AddDate
		{
			get { return _AddDate; }
			set { _AddDate = value; }
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
			sb.Append("InformationClass_Root_Id=" +　InformationClass_Root_Id + "; ");
			sb.Append("InformationClass_Root_Name=" +　InformationClass_Root_Name + "; ");
			sb.Append("InformationClass_Id=" +　InformationClass_Id + "; ");
			sb.Append("InformationClass_Name=" +　InformationClass_Name + "; ");
			sb.Append("Title=" +　Title + "; ");
			sb.Append("RedirectUrl=" +　RedirectUrl + "; ");
			sb.Append("Content=" +　Content + "; ");
			sb.Append("Upload=" +　Upload + "; ");
			sb.Append("FrontCoverImg=" +　FrontCoverImg + "; ");
			sb.Append("Notes=" +　Notes + "; ");
			sb.Append("NewsTime=" +　NewsTime + "; ");
			sb.Append("Keywords=" +　Keywords + "; ");
			sb.Append("SeoTitle=" +　SeoTitle + "; ");
			sb.Append("SeoKey=" +　SeoKey + "; ");
			sb.Append("SeoDesc=" +　SeoDesc + "; ");
			sb.Append("Author=" +　Author + "; ");
			sb.Append("FromName=" +　FromName + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("IsHot=" +　IsHot + "; ");
			sb.Append("IsTop=" +　IsTop + "; ");
			sb.Append("IsPage=" +　IsPage + "; ");
			sb.Append("IsDel=" +　IsDel + "; ");
			sb.Append("CommentCount=" +　CommentCount + "; ");
			sb.Append("ViewCount=" +　ViewCount + "; ");
			sb.Append("AddYear=" +　AddYear + "; ");
			sb.Append("AddMonth=" +　AddMonth + "; ");
			sb.Append("AddDay=" +　AddDay + "; ");
			sb.Append("AddDate=" +　AddDate + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

    } 

}


