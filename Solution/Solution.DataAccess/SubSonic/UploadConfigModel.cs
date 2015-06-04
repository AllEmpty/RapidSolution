 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// UploadConfig表实体类
    /// </summary>
	[Serializable]
    public partial class UploadConfig
    {

		int _Id = 0;
		/// <summary>
		/// 唯一索引，但不自增，
		/// </summary>
		public int Id
		{
			get { return _Id; }
			set { _Id = value; }
		}

		string _Name = "";
		/// <summary>
		/// 模块名称
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		string _JoinName = "";
		/// <summary>
		/// 关联表名
		/// </summary>
		public string JoinName
		{
			get { return _JoinName; }
			set { _JoinName = value; }
		}

		byte _UserType = 0;
		/// <summary>
		/// 用户类别：1=管理员上传，2=会员上传
		/// </summary>
		public byte UserType
		{
			get { return _UserType; }
			set { _UserType = value; }
		}

		int _UploadType_Id = 0;
		/// <summary>
		/// 上传类型表主键
		/// </summary>
		public int UploadType_Id
		{
			get { return _UploadType_Id; }
			set { _UploadType_Id = value; }
		}

		string _UploadType_Name = "";
		/// <summary>
		/// 上传类型名称
		/// </summary>
		public string UploadType_Name
		{
			get { return _UploadType_Name; }
			set { _UploadType_Name = value; }
		}

		string _UploadType_TypeKey = "";
		/// <summary>
		/// 上传类型：image(默认),flash,media,file,editor，绑定UploadType表对应字段
		/// </summary>
		public string UploadType_TypeKey
		{
			get { return _UploadType_TypeKey; }
			set { _UploadType_TypeKey = value; }
		}

		int _PicSize = 0;
		/// <summary>
		/// 图片类,允许最大上传Size（单位：KB），默认:200 =200 KB，"上传类别"为image专用
		/// </summary>
		public int PicSize
		{
			get { return _PicSize; }
			set { _PicSize = value; }
		}

		int _FileSize = 0;
		/// <summary>
		/// 附件类,允许最大上传Size（单位：KB），默认:20000 = 20 M，当使用"上传类别"非image的情况下使用
		/// </summary>
		public int FileSize
		{
			get { return _FileSize; }
			set { _FileSize = value; }
		}

		string _SaveDir = "";
		/// <summary>
		/// 保存的目录"/UploadFile/n/","/UploadFile/p/"
		/// </summary>
		public string SaveDir
		{
			get { return _SaveDir; }
			set { _SaveDir = value; }
		}

		byte _IsPost = 0;
		/// <summary>
		/// 1=使用中,0=停止使用
		/// </summary>
		public byte IsPost
		{
			get { return _IsPost; }
			set { _IsPost = value; }
		}

		byte _IsSwf = 0;
		/// <summary>
		/// 1=flash上传，0=web上传
		/// </summary>
		public byte IsSwf
		{
			get { return _IsSwf; }
			set { _IsSwf = value; }
		}

		byte _IsChkSrcPost = 0;
		/// <summary>
		/// 是否检查提交输入口是否为本服务器（Flash提交的必须设置为false，不用检查）
		/// </summary>
		public byte IsChkSrcPost
		{
			get { return _IsChkSrcPost; }
			set { _IsChkSrcPost = value; }
		}

		byte _IsFixPic = 0;
		/// <summary>
		/// 是否按比例生成
		/// </summary>
		public byte IsFixPic
		{
			get { return _IsFixPic; }
			set { _IsFixPic = value; }
		}

		int _CutType = 0;
		/// <summary>
		/// 0=按比例生成宽高，1=固定图片宽高，2=固定背景宽高，图片按比例生成
		/// </summary>
		public int CutType
		{
			get { return _CutType; }
			set { _CutType = value; }
		}

		int _PicWidth = 0;
		/// <summary>
		/// 最大宽度，超过将按比例进行缩放
		/// </summary>
		public int PicWidth
		{
			get { return _PicWidth; }
			set { _PicWidth = value; }
		}

		int _PicHeight = 0;
		/// <summary>
		/// 最大高度，超过将按比例进行缩放
		/// </summary>
		public int PicHeight
		{
			get { return _PicHeight; }
			set { _PicHeight = value; }
		}

		int _PicQuality = 0;
		/// <summary>
		/// 图片质量，0=使用默认值，>0指定质量值（指定值的情况下，范围：50-100）
		/// </summary>
		public int PicQuality
		{
			get { return _PicQuality; }
			set { _PicQuality = value; }
		}

		byte _IsEditor = 0;
		/// <summary>
		/// 1=编辑器专用,0=web
		/// </summary>
		public byte IsEditor
		{
			get { return _IsEditor; }
			set { _IsEditor = value; }
		}

		byte _IsBigPic = 0;
		/// <summary>
		/// 是否创建大图(原始图片)，1=是，0=否
		/// </summary>
		public byte IsBigPic
		{
			get { return _IsBigPic; }
			set { _IsBigPic = value; }
		}

		int _BigWidth = 0;
		/// <summary>
		/// 大图宽度
		/// </summary>
		public int BigWidth
		{
			get { return _BigWidth; }
			set { _BigWidth = value; }
		}

		int _BigHeight = 0;
		/// <summary>
		/// 大图高度
		/// </summary>
		public int BigHeight
		{
			get { return _BigHeight; }
			set { _BigHeight = value; }
		}

		int _BigQuality = 0;
		/// <summary>
		/// 大图压缩质量
		/// </summary>
		public int BigQuality
		{
			get { return _BigQuality; }
			set { _BigQuality = value; }
		}

		byte _IsMidPic = 0;
		/// <summary>
		/// 是否创建中图，1=是，0=否
		/// </summary>
		public byte IsMidPic
		{
			get { return _IsMidPic; }
			set { _IsMidPic = value; }
		}

		int _MidWidth = 0;
		/// <summary>
		/// 中图宽度
		/// </summary>
		public int MidWidth
		{
			get { return _MidWidth; }
			set { _MidWidth = value; }
		}

		int _MidHeight = 0;
		/// <summary>
		/// 中图高度
		/// </summary>
		public int MidHeight
		{
			get { return _MidHeight; }
			set { _MidHeight = value; }
		}

		int _MidQuality = 0;
		/// <summary>
		/// 中图压缩质量
		/// </summary>
		public int MidQuality
		{
			get { return _MidQuality; }
			set { _MidQuality = value; }
		}

		byte _IsMinPic = 0;
		/// <summary>
		/// 是否创建小图，1=是，0=否
		/// </summary>
		public byte IsMinPic
		{
			get { return _IsMinPic; }
			set { _IsMinPic = value; }
		}

		int _MinWidth = 0;
		/// <summary>
		/// 小图宽度
		/// </summary>
		public int MinWidth
		{
			get { return _MinWidth; }
			set { _MinWidth = value; }
		}

		int _MinHeight = 0;
		/// <summary>
		/// 小图高度
		/// </summary>
		public int MinHeight
		{
			get { return _MinHeight; }
			set { _MinHeight = value; }
		}

		int _MinQuality = 0;
		/// <summary>
		/// 小图压缩质量
		/// </summary>
		public int MinQuality
		{
			get { return _MinQuality; }
			set { _MinQuality = value; }
		}

		byte _IsHotPic = 0;
		/// <summary>
		/// 是否创建推荐图，1=是，0=否
		/// </summary>
		public byte IsHotPic
		{
			get { return _IsHotPic; }
			set { _IsHotPic = value; }
		}

		int _HotWidth = 0;
		/// <summary>
		/// 推荐图宽度
		/// </summary>
		public int HotWidth
		{
			get { return _HotWidth; }
			set { _HotWidth = value; }
		}

		int _HotHeight = 0;
		/// <summary>
		/// 推荐图高度
		/// </summary>
		public int HotHeight
		{
			get { return _HotHeight; }
			set { _HotHeight = value; }
		}

		int _HotQuality = 0;
		/// <summary>
		/// 推荐图压缩质量
		/// </summary>
		public int HotQuality
		{
			get { return _HotQuality; }
			set { _HotQuality = value; }
		}

		byte _IsWaterPic = 0;
		/// <summary>
		/// 是否加水印，1=是，0=否
		/// </summary>
		public byte IsWaterPic
		{
			get { return _IsWaterPic; }
			set { _IsWaterPic = value; }
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
			sb.Append("JoinName=" +　JoinName + "; ");
			sb.Append("UserType=" +　UserType + "; ");
			sb.Append("UploadType_Id=" +　UploadType_Id + "; ");
			sb.Append("UploadType_Name=" +　UploadType_Name + "; ");
			sb.Append("UploadType_TypeKey=" +　UploadType_TypeKey + "; ");
			sb.Append("PicSize=" +　PicSize + "; ");
			sb.Append("FileSize=" +　FileSize + "; ");
			sb.Append("SaveDir=" +　SaveDir + "; ");
			sb.Append("IsPost=" +　IsPost + "; ");
			sb.Append("IsSwf=" +　IsSwf + "; ");
			sb.Append("IsChkSrcPost=" +　IsChkSrcPost + "; ");
			sb.Append("IsFixPic=" +　IsFixPic + "; ");
			sb.Append("CutType=" +　CutType + "; ");
			sb.Append("PicWidth=" +　PicWidth + "; ");
			sb.Append("PicHeight=" +　PicHeight + "; ");
			sb.Append("PicQuality=" +　PicQuality + "; ");
			sb.Append("IsEditor=" +　IsEditor + "; ");
			sb.Append("IsBigPic=" +　IsBigPic + "; ");
			sb.Append("BigWidth=" +　BigWidth + "; ");
			sb.Append("BigHeight=" +　BigHeight + "; ");
			sb.Append("BigQuality=" +　BigQuality + "; ");
			sb.Append("IsMidPic=" +　IsMidPic + "; ");
			sb.Append("MidWidth=" +　MidWidth + "; ");
			sb.Append("MidHeight=" +　MidHeight + "; ");
			sb.Append("MidQuality=" +　MidQuality + "; ");
			sb.Append("IsMinPic=" +　IsMinPic + "; ");
			sb.Append("MinWidth=" +　MinWidth + "; ");
			sb.Append("MinHeight=" +　MinHeight + "; ");
			sb.Append("MinQuality=" +　MinQuality + "; ");
			sb.Append("IsHotPic=" +　IsHotPic + "; ");
			sb.Append("HotWidth=" +　HotWidth + "; ");
			sb.Append("HotHeight=" +　HotHeight + "; ");
			sb.Append("HotQuality=" +　HotQuality + "; ");
			sb.Append("IsWaterPic=" +　IsWaterPic + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

    } 

}


