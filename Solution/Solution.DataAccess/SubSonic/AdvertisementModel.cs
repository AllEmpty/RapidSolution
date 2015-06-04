 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// Advertisement表实体类
    /// </summary>
	[Serializable]
    public partial class Advertisement
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
		/// 标题
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
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

		string _Url = "";
		/// <summary>
		/// 链接Url
		/// </summary>
		public string Url
		{
			get { return _Url; }
			set { _Url = value; }
		}

		string _Keyword = "";
		/// <summary>
		/// 关键字，只能由字母数字组成，主要用于模板标签 {%ad-InfoKey%}
		/// </summary>
		public string Keyword
		{
			get { return _Keyword; }
			set { _Keyword = value; }
		}

		int _AdvertisingPosition_Id = 0;
		/// <summary>
		/// 广告位置Id
		/// </summary>
		public int AdvertisingPosition_Id
		{
			get { return _AdvertisingPosition_Id; }
			set { _AdvertisingPosition_Id = value; }
		}

		string _AdvertisingPosition_Name = "";
		/// <summary>
		/// 广告位置名称
		/// </summary>
		public string AdvertisingPosition_Name
		{
			get { return _AdvertisingPosition_Name; }
			set { _AdvertisingPosition_Name = value; }
		}

		string _AdImg = "";
		/// <summary>
		/// 图片
		/// </summary>
		public string AdImg
		{
			get { return _AdImg; }
			set { _AdImg = value; }
		}

		int _ShowRate = 0;
		/// <summary>
		/// 显示频率（同一个位置有多个广告时，这里用来计算它随机出现的频率）
		/// </summary>
		public int ShowRate
		{
			get { return _ShowRate; }
			set { _ShowRate = value; }
		}

		DateTime _StartTime = new DateTime(1900,1,1);
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime StartTime
		{
			get { return _StartTime; }
			set { _StartTime = value; }
		}

		DateTime _EndTime = new DateTime(1900,1,1);
		/// <summary>
		/// 结束时间
		/// </summary>
		public DateTime EndTime
		{
			get { return _EndTime; }
			set { _EndTime = value; }
		}

		byte _IsDisplay = 0;
		/// <summary>
		/// 审核, 0=False,1=True,
		/// </summary>
		public byte IsDisplay
		{
			get { return _IsDisplay; }
			set { _IsDisplay = value; }
		}

		int _HitCount = 0;
		/// <summary>
		/// 点击数
		/// </summary>
		public int HitCount
		{
			get { return _HitCount; }
			set { _HitCount = value; }
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
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Content=" +　Content + "; ");
			sb.Append("Url=" +　Url + "; ");
			sb.Append("Keyword=" +　Keyword + "; ");
			sb.Append("AdvertisingPosition_Id=" +　AdvertisingPosition_Id + "; ");
			sb.Append("AdvertisingPosition_Name=" +　AdvertisingPosition_Name + "; ");
			sb.Append("AdImg=" +　AdImg + "; ");
			sb.Append("ShowRate=" +　ShowRate + "; ");
			sb.Append("StartTime=" +　StartTime + "; ");
			sb.Append("EndTime=" +　EndTime + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("HitCount=" +　HitCount + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

    } 

}


