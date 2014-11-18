 
using System;
using System.Text;

namespace Solution.DataAccess.Model
{
    /// <summary>
    /// UploadFile表实体类
    /// </summary>
	[Serializable]
    public partial class UploadFile
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
		/// 新文件名（包括扩展名）
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		string _Path = "";
		/// <summary>
		/// 新路径（包括文件名）
		/// </summary>
		public string Path
		{
			get { return _Path; }
			set { _Path = value; }
		}

		string _Ext = "";
		/// <summary>
		/// 扩展名
		/// </summary>
		public string Ext
		{
			get { return _Ext; }
			set { _Ext = value; }
		}

		string _Src = "";
		/// <summary>
		/// 原文件名（包括扩展名）
		/// </summary>
		public string Src
		{
			get { return _Src; }
			set { _Src = value; }
		}

		int _Size = 0;
		/// <summary>
		/// 文件大小
		/// </summary>
		public int Size
		{
			get { return _Size; }
			set { _Size = value; }
		}

		int _PicWidth = 0;
		/// <summary>
		/// 图片的宽
		/// </summary>
		public int PicWidth
		{
			get { return _PicWidth; }
			set { _PicWidth = value; }
		}

		int _PicHeight = 0;
		/// <summary>
		/// 图片的高
		/// </summary>
		public int PicHeight
		{
			get { return _PicHeight; }
			set { _PicHeight = value; }
		}

		int _UploadConfig_Id = 0;
		/// <summary>
		/// 系统ID:---UploadConfig_Id
			/// 1=后台--新闻封面/新闻编辑器
		/// </summary>
		public int UploadConfig_Id
		{
			get { return _UploadConfig_Id; }
			set { _UploadConfig_Id = value; }
		}

		string _JoinName = "";
		/// <summary>
		/// 关联表ID--1=NewsInfo,2=PrdInfo,
		/// </summary>
		public string JoinName
		{
			get { return _JoinName; }
			set { _JoinName = value; }
		}

		int _JoinId = 0;
		/// <summary>
		/// 关联ID--所属的文章ID,产品ID，头像等
		/// </summary>
		public int JoinId
		{
			get { return _JoinId; }
			set { _JoinId = value; }
		}

		byte _UserType = 0;
		/// <summary>
		/// 用户类别:1=管理员上传，2=会员上传
		/// </summary>
		public byte UserType
		{
			get { return _UserType; }
			set { _UserType = value; }
		}

		int _UserId = 0;
		/// <summary>
		/// 上传者ID
		/// </summary>
		public int UserId
		{
			get { return _UserId; }
			set { _UserId = value; }
		}

		string _UserName = "";
		/// <summary>
		/// 上传者Name
		/// </summary>
		public string UserName
		{
			get { return _UserName; }
			set { _UserName = value; }
		}

		string _UserIp = "";
		/// <summary>
		/// 上传者Ip
		/// </summary>
		public string UserIp
		{
			get { return _UserIp; }
			set { _UserIp = value; }
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

		string _InfoText = "";
		/// <summary>
		/// 备注
		/// </summary>
		public string InfoText
		{
			get { return _InfoText; }
			set { _InfoText = value; }
		}

		string _RndKey = "";
		/// <summary>
		/// 随机Key
		/// </summary>
		public string RndKey
		{
			get { return _RndKey; }
			set { _RndKey = value; }
		}

		/// <summary>
        /// 输出实体所有值
        /// </summary>
        /// <returns></returns>
		public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Path=" +　Path + "; ");
			sb.Append("Ext=" +　Ext + "; ");
			sb.Append("Src=" +　Src + "; ");
			sb.Append("Size=" +　Size + "; ");
			sb.Append("PicWidth=" +　PicWidth + "; ");
			sb.Append("PicHeight=" +　PicHeight + "; ");
			sb.Append("UploadConfig_Id=" +　UploadConfig_Id + "; ");
			sb.Append("JoinName=" +　JoinName + "; ");
			sb.Append("JoinId=" +　JoinId + "; ");
			sb.Append("UserType=" +　UserType + "; ");
			sb.Append("UserId=" +　UserId + "; ");
			sb.Append("UserName=" +　UserName + "; ");
			sb.Append("UserIp=" +　UserIp + "; ");
			sb.Append("AddDate=" +　AddDate + "; ");
			sb.Append("InfoText=" +　InfoText + "; ");
			sb.Append("RndKey=" +　RndKey + "; ");
			return sb.ToString();
        }

    } 

}


