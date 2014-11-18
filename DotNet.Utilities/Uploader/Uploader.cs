/*******************************************
 *  Desc:       上传类 
 *  Author:     July 
 *  Date:       2013-11-18  
 *  Log:        
 *              2013-11-18，因前台美工有第五size的图片，根据前台新增第五size图片的代码
********************************************/
using System;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;

namespace DotNet.Utilities
{
    /// <summary>上传文件,针对图片文件加水印(图片水印)<para/>
    /// 原始图(不作任何操作，不水印，不放缩)，返回文件名：2008090201o.jpg<para/>
    /// 大图(终页放大)，返回文件名：2008090201b.jpg<para/>
    /// 中图(终页图)，返回文件名：2008090201m.jpg<para/>
    /// 小图(微型图)，返回文件名：2008090201s.jpg<para/>
    /// 第五size(推荐页图)，返回文件名：2008090201h.jpg<para/>
    /// </summary>
    public class Uploader
    {
        #region 变量定义
        /// <summary>是否允许上传文件</summary>
        public bool IsEnabled = false;

        /// <summary>是否检查提交来源</summary>
        public bool IsChkSrcPost = true;

        /// <summary>图片：0=按比例生成宽高，1=固定图片宽高，2=固定背景宽高，图片按比例生成</summary>
        public int CutType = 0;

        /// <summary>出错信息</summary>
        private string _errMsg = "";

        /// <summary>充许上传的文件类型,不要随意增加内容!(注:一定要使用,分隔)</summary>
        public string AllowedExt = "jpg,gif,png";

        /// <summary>禁止上传的文件类型,不要随意删减内容!(注:一定要使用,分隔)</summary>
        private const string _DeniedExt = "html,htm,php,php2,php3,php4,php5,phtml,pwml,inc,asp,aspx,ascx,jsp,cfm,cfc,pl,bat,exe,com,dll,vbs,js,reg,cgi,htaccess,asis,sh,shtml,shtm,phtm,config";

        /// <summary>充许上传的文件大小(单位:KB)，默认:500KB </summary>
        private int _maxSize = 500;

        //上传后,新文件大小（单位KB）
        private long _fileSize = 0;

        /// <summary>form file控件的名称</summary>
        public string FilePostName = "imgFile";

        /// <summary>保存路径</summary>
        private string _savePath = "/UploadFile/";

        /// <summary>是否限制上传的图片大小(列表图)，默认：flase</summary>
        private bool _isFixPic = false;
        private int _picWidth = 700;
        private int _picHeight = 1500;
        private int _picQuality = 0;      //大于0，为控制质量

        /// <summary>生成大图()，默认：flase</summary>
        private bool _isBigPic = false;
        private int _bigWidth = 0;     //0=不限制宽高
        private int _bigHeight = 0;    //0=不限制宽高
        private int _bigQuality = 0;   //大于0，为控制质量

        /// <summary>生成中型(终页显示)，默认：flase</summary>
        private bool _isMidPic = false;
        private int _midWidth = 320;
        private int _midHeight = 320;
        private int _midQuality = 0;   //大于0，为控制质量

        /// <summary>生成微型(推荐显示)，默认：flase</summary>
        private bool _isMinPic = false;
        private int _minWidth = 50;
        private int _minHeight = 50;
        private int _minQuality = 0;   //大于0，为控制质量

        /// <summary>生成微型(首页显示)，默认：flase</summary>
        private bool _isHotPic = false;
        private int _hotWidth = 50;
        private int _hotHeight = 50;
        private int _hotQuality = 0;   //大于0，为控制质量

        /// <summary>生成图片水印</summary>
        public bool IsWaterPic = false;

        /// <summary>图片水印的Logo地址:使用相对根目录的方式("/images/Water.png")</summary>
        public string WaterPicPath = "/images/Water.png";

        /// <summary>上传图片的扩展名（全部会转为小写）</summary>
        public string FileExt = "";

        /// <summary>上传图片的原始文件名称（包含扩展名） </summary>
        public string SrcName = "";

        /// <summary>图片文件的原始宽</summary>
        public int SrcWidth = 0;

        /// <summary>图片文件的原始高</summary>
        public int SrcHeight = 0;

        /// <summary>上传后，生图片的宽（列表的宽）</summary>
        public int NewWidth = 0;

        /// <summary>上传后，生图片的高（列表的高）</summary>
        public int NewHeight = 0;

        /// <summary>上传后,随机生成的新文件名:"120514064733805.jpg"</summary>
        public string NewFile = "";

        /// <summary>上传后,新文件路径（包括文件名和扩展名）"/upload/n/0908/120514064733805.jpg"</summary>
        public string NewPath = "";
        #endregion

        #region 属性

        /// <summary>取得DeniedExt</summary>
        public string DeniedExt
        {
            get
            {
                return _DeniedExt;
            }
        }

        /// <summary>上传文件的保存路径</summary>
        public string SavePath
        {
            get
            {
                return _savePath;
            }
            set
            {
                _savePath = DirFileHelper.FixDirPath("/UploadFile/" + value);
            }
        }

        /// <summary>充许上传的文件大小(单位:KB)<para/>
        /// 默认值:500, 范围:( 1＜value＜512000 ),512000=500Mb
        /// </summary>
        public int MaxSize
        {
            get
            {
                return _maxSize;
            }
            set
            {
                int k = ConvertHelper.Cint(value);
                if (k > 1 && k < 512000)
                {
                    _maxSize = k;
                }
            }
        }

        ///<summary>大型图(原始图)，设置参数</summary>
        /// <param name="isFix">ture=生成，false=不处理</param>
        /// <param name="ww">最大宽度, 默认:700</param>
        /// <param name="hh">最大高度, 默认:1500</param>
        /// <param name="qty">质量:50-100 or 0（大于0，为控制质量）</param>
        /// <returns></returns>
        public void SetPic(bool isFix = false, int ww = 0, int hh = 0, int qty = 0)
        {
            this._isFixPic = isFix;
            if (isFix)
            {
                if (ww > 0) { this._picWidth = ww; }
                if (hh > 0) { this._picHeight = hh; }
                if (qty > 0) { this._picQuality = qty; }
            }
        }

        ///<summary>大型图(原始图)，设置参数</summary>
        /// <param name="isFix">ture=生成，false=不处理</param>
        /// <param name="ww">最大宽度</param>
        /// <param name="hh">最大高度</param>
        /// <param name="qty">质量:50-100 or 0（大于0，为控制质量）</param>
        /// <returns></returns>
        public void SetBig(bool isFix = false, int ww = 0, int hh = 0, int qty = 0)
        {
            this._isBigPic = isFix;
            if (isFix)
            {
                if (ww > 0) { this._bigWidth = ww; }
                if (hh > 0) { this._bigHeight = hh; }
                if (qty > 0) { this._bigQuality = qty; }
            }
        }

        ///<summary>中型图，设置参数</summary>
        /// <param name="isFix">ture=生成，false=不处理</param>
        /// <param name="ww">最大宽度, 默认:320</param>
        /// <param name="hh">最大高度, 默认:320</param>
        /// <param name="qty">质量:50-100 or 0（大于0，为控制质量）</param>
        /// <returns></returns>
        public void SetMid(bool isFix = false, int ww = 0, int hh = 0, int qty = 0)
        {
            this._isMidPic = isFix;
            if (isFix)
            {
                if (ww > 0) { this._midWidth = ww; }
                if (hh > 0) { this._midHeight = hh; }
                if (qty > 0) { this._midQuality = qty; }
            }
        }

        ///<summary>微型图，设置参数</summary>
        /// <param name="isFix">ture=生成，false=不处理</param>
        /// <param name="ww">最大宽度, 默认:50</param>
        /// <param name="hh">最大高度, 默认:50</param>
        /// <param name="qty">质量:50-100 or 0（大于0，为控制质量）</param>
        /// <returns></returns>
        public void SetMin(bool isFix = false, int ww = 0, int hh = 0, int qty = 0)
        {
            this._isMinPic = isFix;
            if (isFix)
            {
                if (ww > 0) { this._minWidth = ww; }
                if (hh > 0) { this._minHeight = hh; }
                if (qty > 0) { this._minQuality = qty; }
            }
        }


        ///<summary>微型图，设置参数</summary>
        /// <param name="isFix">ture=生成，false=不处理</param>
        /// <param name="ww">最大宽度, 默认:50</param>
        /// <param name="hh">最大高度, 默认:50</param>
        /// <param name="qty">质量:50-100 or 0（大于0，为控制质量）</param>
        /// <returns></returns>
        public void SetHot(bool isFix = false, int ww = 0, int hh = 0, int qty = 0)
        {
            this._isHotPic = isFix;
            if (isFix)
            {
                if (ww > 0) { this._hotWidth = ww; }
                if (hh > 0) { this._hotHeight = hh; }
                if (qty > 0) { this._hotQuality = qty; }
            }
        }
        #endregion

        #region 返回方法
        ///<summary>上传后,取得新文件大小（单位KB）</summary>
        public int GetFileSize()
        {
            if (this._fileSize > 0)
            {
                return System.Convert.ToInt32(Math.Ceiling((double)this._fileSize / (double)1024));
            }
            return 0;
        }

        ///<summary>上传失败后,取得出错信息</summary>
        public string GetErrMsg()
        {
            return _errMsg;
        }
        #endregion

        #region 构造函数
        /// <summary>构造函数</summary>
        public Uploader() { }
        #endregion

        #region 上传文件，并按设置生成缩略图，水印
        /// <summary>上传文件，并按设置生成缩略图，水印</summary>
        /// <returns></returns>
        public bool UploadFile(HttpPostedFile oFile = null)
        {
            #region 检查设置
            if (!this.IsEnabled)
            {
                SendResponse(500, "");
                return false;
            }

            if (this.IsChkSrcPost)
            {
                if (!RequestHelper.ChkSrcPost())
                {
                    SendResponse(501, "");
                    return false;
                }
            }

            if (this._savePath.Length < 1)
            {
                SendResponse(101, "SavePath未设置");
                return false;
            }

            if (oFile == null)
            {
                if (this.FilePostName.Length < 1)
                {
                    SendResponse(101, "filePostName未设置");
                    return false;
                }
            }

            this._savePath = DirFileHelper.FixDirPath(_savePath) + DateTime.Now.ToString("yyMM") + "/";
            bool isOk = DirFileHelper.CheckSaveDir(this._savePath);

            if (!isOk)
            {
                SendResponse(101, "SavePath设置不当:" + this._savePath + ", 或权限不足！");
                return false;
            }
            #endregion

            //------------------------------------------------
            #region 获取文件对象
            //获取文件对象
            if (oFile == null)
            {
                oFile = HttpContext.Current.Request.Files[FilePostName];
            }
            if (oFile == null)
            {
                SendResponse(201, "");
                return false;
            }
            //------------------------------------------------
            //原始文件名;
            this.SrcName = Path.GetFileName(oFile.FileName);
            #endregion

            //------------------------------------------------
            #region 检查文件大小
            this._fileSize = oFile.ContentLength;

            //不能上传小于10字节的内容
            if (this.SrcName.Length < 3 || this._fileSize < 10)
            {
                SendResponse(201, "");
                return false;
            }

            //检测文件大小是否超过限制
            if (this._fileSize > (this._maxSize * 1024))
            {
                SendResponse(301, "");
                return false;
            }

            #endregion

            //------------------------------------------------
            #region 检查文件类型
            this.FileExt = Path.GetExtension(this.SrcName).ToLower().TrimStart('.');

            if (!checkAllowedExt(this.FileExt))
            {
                SendResponse(202, "");
                return false;
            }
            #endregion


            #region 上传文件
            //上传
            string sServerDir = _savePath;
            if (sServerDir.IndexOf(":") < 0)
            {
                sServerDir = DirFileHelper.FixDirPath(DirFileHelper.GetMapPath(sServerDir));
            }

            string sNewFile = "";   //新文件名(系统生成)
            string sNewRoot = "";   //新文件路径(绝对路径)

            while (true)
            {
                sNewFile = DirFileHelper.GetRndFileName("." + this.FileExt);
                sNewRoot = System.IO.Path.Combine(sServerDir, sNewFile);

                if (!DirFileHelper.IsExistFile(sNewRoot))
                {
                    try
                    {
                        oFile.SaveAs(sNewRoot);
                    }
                    catch
                    {
                        SendResponse(204, "");
                        return false;
                    }
                    break;
                }
            }

            this.NewFile = sNewFile;
            this.NewPath = _savePath + sNewFile;
            #endregion

            //------------------------------------------------
            #region 生成缩略图 + 水印
            if (this.FileExt == "jpg" || this.FileExt == "png" || this.FileExt == "gif" || this.FileExt == "jpeg" || this.FileExt == "bmp")
            {
                #region 取得原始尺寸
                try//能取得图片宽高，是真实的图片
                {
                    Image testImage1 = Image.FromFile(sNewRoot);
                    this.SrcWidth = testImage1.Width;
                    this.SrcHeight = testImage1.Height;
                    testImage1.Dispose();

                    this.NewWidth = this.SrcWidth;
                    this.NewHeight = this.SrcHeight;
                }
                catch
                {
                    //非法格式，不是真正的图片
                    DirFileHelper.DeleteFile(sNewRoot);
                    SendResponse(202, "");
                    return false;
                }

                //------------------------------------------------
                //先备份原始图
                var tmpPath = "";
                tmpPath = System.IO.Path.Combine(sServerDir, DirFileHelper.GetFileNamePostfix(sNewFile, "o"));
                DirFileHelper.CopyFile(sNewRoot, tmpPath);
                #endregion


                if (this._isFixPic || this._isBigPic || this._isMidPic || this._isMinPic)
                {
                    #region 创建大图
                    if (this._isBigPic)
                    {
                        tmpPath = System.IO.Path.Combine(sServerDir, DirFileHelper.GetFileNamePostfix(sNewFile, "b"));

                        if (this._bigWidth > 0 && this._bigHeight > 0)
                        {
                            //缩略
                            MakeThumbImage(sNewRoot, tmpPath, this._bigWidth, this._bigHeight, this._bigQuality, this.CutType);
                        }
                        else//因为不限制宽高，所以直接复制出来就行了
                        {
                            DirFileHelper.CopyFile(sNewRoot, tmpPath);
                        }

                        //添加水印
                        if (this.IsWaterPic)
                        {
                            MakeWaterPic(tmpPath);
                        }
                    }
                    #endregion

                    //------------------------------------------------
                    #region 创建中图
                    if (this._isMidPic)
                    {
                        tmpPath = System.IO.Path.Combine(sServerDir, DirFileHelper.GetFileNamePostfix(sNewFile, "m"));

                        if (this._midWidth > 0 && this._midHeight > 0)
                        {
                            //缩略图
                            MakeThumbImage(sNewRoot, tmpPath, this._midWidth, this._midHeight, this._midQuality, this.CutType);
                        }
                        else//因为不限制宽高，所以直接复制出来就行了
                        {
                            DirFileHelper.CopyFile(sNewRoot, tmpPath);
                        }

                        //添加水印
                        if (this.IsWaterPic)
                        {
                            MakeWaterPic(tmpPath);
                        }
                    }
                    #endregion

                    //------------------------------------------------
                    #region 创建小图
                    if (this._isMinPic)
                    {
                        tmpPath = System.IO.Path.Combine(sServerDir, DirFileHelper.GetFileNamePostfix(sNewFile, "s"));

                        if (this._minWidth > 0 && this._minHeight > 0)
                        {
                            //缩略图
                            MakeThumbImage(sNewRoot, tmpPath, this._minWidth, this._minHeight, this._minQuality, this.CutType);
                        }
                        else//因为不限制宽高，所以直接复制出来就行了
                        {
                            DirFileHelper.CopyFile(sNewRoot, tmpPath);
                        }

                        //微型图不用加水印
                    }
                    #endregion

                    //------------------------------------------------
                    #region 创建推荐图
                    if (this._isHotPic)
                    {
                        tmpPath = System.IO.Path.Combine(sServerDir, DirFileHelper.GetFileNamePostfix(sNewFile, "h"));

                        if (this._hotWidth > 0 && this._hotHeight > 0)
                        {
                            //缩略图
                            MakeThumbImage(sNewRoot, tmpPath, this._hotWidth, this._hotHeight, this._hotQuality, this.CutType);
                        }
                        else//因为不限制宽高，所以直接复制出来就行了
                        {
                            DirFileHelper.CopyFile(sNewRoot, tmpPath);
                        }


                        //添加水印
                        if (this.IsWaterPic)
                        {
                            MakeWaterPic(tmpPath);
                        }
                    }
                    #endregion

                    //------------------------------------------------
                    #region 限制列表图
                    if (this._isFixPic && this._picWidth > 0 && this._picHeight > 0)
                    {
                        MakeThumbImage(sNewRoot, sNewRoot, this._picWidth, this._picHeight, this._picQuality, this.CutType);
                    }
                    #endregion


                    #region 取得缩放后的图片宽高
                    try
                    {
                        Image testImage2 = Image.FromFile(sNewRoot);
                        this.NewWidth = testImage2.Width;
                        this.NewHeight = testImage2.Height;
                        testImage2.Dispose();
                    }
                    catch
                    {
                        DirFileHelper.DelPicFile(this.NewPath);
                        SendResponse(202, "");
                        return false;
                    }
                    #endregion
                }

                //列表图是否加水印
                if (DirFileHelper.IsExistFile(sNewRoot) && this.IsWaterPic)
                {
                    MakeWaterPic(sNewRoot);
                }
            }
            #endregion

            //上传成功!!
            return true;
        }
        #endregion

        #region 向页面输出js提示
        /// <summary>向页面输出js提示</summary>
        /// <param name="errorNumber"></param>
        /// <param name="customMsg"></param>
        private void SendResponse(int errorNumber, string customMsg)
        {
            _errMsg = customMsg;

            switch (errorNumber)
            {
                case 101:
                    return;

                case 201:
                    _errMsg = "请选择要上传的文件!";
                    return;

                case 202:
                    _errMsg = "不支持该文件格式,只支持以下格式:(" + this.AllowedExt + ").";
                    return;

                case 203:
                    _errMsg = "系统禁止上传格式:(" + this.DeniedExt + ").";
                    return;

                case 204:
                    _errMsg = "权限出错. 您可能没有权限上传文件. 请检查服务器.";
                    return;

                case 301:
                    _errMsg = "上传文件大小超过了限制.最多上传(" + DirFileHelper.FmtFileSize2(_maxSize) + ").";
                    return;

                case 500:
                    _errMsg = "系统暂时禁止上传文件.";
                    return;

                case 501:
                    _errMsg = "非法提交!!";
                    return;

                case 502:
                    _errMsg = "参数错误!!key";
                    return;

                case 503:
                    _errMsg = "参数错误!!subid";
                    return;

                default:
                    _errMsg = "不知名的错误!.";
                    return;
            }
        }
        #endregion

        #region 检查扩展名
        /// <summary>检查是否充许该文件类型上传</summary>
        /// <param name="sFileExt"></param>
        private bool checkAllowedExt(string sFileExt)
        {
            string strAllowed = "," + this.AllowedExt.ToLower() + ",";
            string strDenied = "," + this.DeniedExt.ToLower() + ",";
            string sExt = "," + sFileExt + ",";

            if (strDenied.Length > 0 && (strDenied.IndexOf(sExt)) > -1)
            {
                SendResponse(203, "");
                return false;
            }


            if (strAllowed.Length > 4)
            {
                if (strAllowed.IndexOf(sExt) < 0)
                {
                    SendResponse(202, "");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 缩略图
        /// <summary>生成缩略图</summary>
        /// <param name="srcFile">原图片路径(服务器路径d:\web\upload\100.jpg)</param>
        /// <param name="decFile">缩略图路径(服务器路径d:\web\upload\100.jpg)</param>
        /// <param name="iMaxWidth">限制的宽度</param>
        /// <param name="iMaxHeight">限制的高度</param>
        /// <param name="highQuality">如果大于0，使用质量控制(50-100)</param>   
        /// <param name="CutType">0=按比例生成宽高，1=固定图片宽高，2=固定背景宽高，图片按比例生成</param>
        public static void MakeThumbImage(string srcFile, string decFile, int iMaxWidth, int iMaxHeight, int highQuality = 0, int CutType = 0)
        {
            #region 取得路径
            if (srcFile.IndexOf(":") < 0) { srcFile = DirFileHelper.GetMapPath(srcFile); }
            if (decFile.IndexOf(":") < 0) { decFile = DirFileHelper.GetMapPath(decFile); }
            if (!DirFileHelper.IsExistFile(srcFile)) { return; }
            #endregion

            //---------------------------------------
            #region 原始图片宽高
            Image srcImage = Image.FromFile(srcFile);
            int iSrcWidth = srcImage.Width;
            int iSrcHeight = srcImage.Height;
            #endregion

            //---------------------------------------
            #region 读取新图片宽高
            int toWidth = 0, toHeight = 0, x = 0, y = 0;

            if (CutType == 1)//固定宽高
            {
                toWidth = iMaxWidth;
                toHeight = iMaxHeight;
            }
            else
            {
                //如果原始图,高度大于宽度,按高度缩放,如果宽度大高度按宽度缩放（不变形）
                //如果图片缩放后,还是比限制大,再进行缩放,直接宽同度都不超过限制
                if ((iSrcWidth < iMaxWidth) && (iSrcHeight < iMaxHeight))
                {
                    toWidth = iSrcWidth;
                    toHeight = iSrcHeight;
                }
                else
                {
                    if (iSrcHeight > iSrcWidth)
                    {
                        toHeight = iMaxHeight;
                        toWidth = iSrcWidth * iMaxHeight / iSrcHeight;

                        if (toWidth > iMaxWidth)
                        {
                            //toHeight 必须在 toWidth 前
                            toHeight = toHeight * iMaxWidth / toWidth;
                            toWidth = iMaxWidth;
                        }
                    }
                    else
                    {
                        toHeight = iSrcHeight * iMaxWidth / iSrcWidth;
                        toWidth = iMaxWidth;

                        if (toHeight > iMaxHeight)
                        {
                            //toWidth 必须在 toHeight 前
                            toWidth = toWidth * iMaxHeight / toHeight;
                            toHeight = iMaxHeight;
                        }
                    }
                }
            }
            #endregion

            #region 输出
            Bitmap bitmap;
            if (CutType == 2)
            {
                //2=固定背景宽高，图片按比例生成
                bitmap = new Bitmap(iMaxWidth, iMaxHeight);
                if (toWidth <= iMaxWidth && toHeight <= iMaxHeight)
                {
                    x = Convert.ToInt16((iMaxWidth - toWidth) / 2);
                    y = Convert.ToInt16((iMaxHeight - toHeight) / 2);
                }
                else
                {
                    if (toWidth < toHeight)
                    {
                        x = Convert.ToInt16((iMaxWidth - toWidth) / 2);
                    }
                    else
                    {
                        y = Convert.ToInt16((iMaxHeight - toHeight) / 2);
                    }
                }
            }
            else
            {
                bitmap = new Bitmap(toWidth, toHeight);
            }
            Graphics g = Graphics.FromImage(bitmap);

            //高质量
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //设置高质量插值法            
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //设置高质量,低速度呈现平滑程度            
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充            
            //g.Clear(System.Drawing.Color.Transparent);
            g.Clear(System.Drawing.Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分         
            g.DrawImage(srcImage, new Rectangle(x, y, toWidth, toHeight), new Rectangle(0, 0, iSrcWidth, iSrcHeight), GraphicsUnit.Pixel);

            srcImage.Dispose();
            g.Dispose();

            try
            {
                if (DirFileHelper.GetFileExtension(srcFile) == "png")
                {
                    bitmap.Save(decFile, System.Drawing.Imaging.ImageFormat.Png);
                }
                else//以jpg格式保存缩略图
                {
                    //---------------------------------------
                    //高质量--使用分级图片质量
                    if (highQuality > 0)
                    {
                        // 以下代码为保存图片时,设置压缩质量
                        var encoderParams = new EncoderParameters();
                        var quality = new long[1];
                        quality[0] = highQuality;   //50-100内
                        var encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                        encoderParams.Param[0] = encoderParam;

                        //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
                        ImageCodecInfo[] arrayIci = ImageCodecInfo.GetImageEncoders();
                        ImageCodecInfo jpegIci = null;
                        for (int i = 0; i < arrayIci.Length; i++)
                        {
                            if (arrayIci[i].FormatDescription.Equals("JPEG"))
                            {
                                jpegIci = arrayIci[i];
                                //设置JPEG编码
                                break;
                            }
                        }

                        //---------------------------------------
                        bitmap.Save(decFile, jpegIci, encoderParams);

                    }
                    else
                    {
                        bitmap.Save(decFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
            }
            #endregion
        }

        #endregion

        #region 水印
        /// <summary>在图片上生成图片水印</summary>        
        /// <param name="srcFile">要添加水印的图片</param>  
        /// <param name="top">上边距 为0时下边距生效</param>
        /// <param name="bottom">下边距 上边距为0时生效</param>
        /// <param name="left">左边距 为0时右边距生效</param>
        /// <param name="right">右边距 左边距为0时生效</param>
        /// <param name="limitWidth">原图小于该宽度,将不添加水印</param>
        /// <param name="HighQuality">如果大于0，使用质量控制(50-100)</param>
        /// <param name="_WaterPicPath">水印图片所在地址,默认:"/images/Water.png"</param>
        public static void MakeWaterPic(string srcFile, int top = 0, int bottom = 10, int left = 0, int right = 10, int limitWidth = 300, int HighQuality = 0, string _WaterPicPath = "/images/Water.png")
        {
            #region 取得图片绝对地址
            if (srcFile.IndexOf(":") < 0) { srcFile = DirFileHelper.GetMapPath(srcFile); }
            if (!DirFileHelper.IsExistFile(srcFile)) { return; }
            #endregion

            #region 取得水印图片
            //如果是默认水印图片
            if (_WaterPicPath == "/images/Water.png")
            {
                //则从配置信息里读取水印图片路径
                var waterPicPath = ConfigHelper.GetConfigString("WaterPicPath");
                if (!string.IsNullOrEmpty(waterPicPath))
                {
                    _WaterPicPath = waterPicPath;
                }
            }

            if (_WaterPicPath.IndexOf(":") < 0) { _WaterPicPath = DirFileHelper.GetMapPath(_WaterPicPath); }
            if (!DirFileHelper.IsExistFile(_WaterPicPath)) { return; }
            #endregion

            //---------------------------------------
            #region 判断
            Image srcImage = Image.FromFile(srcFile);
            Image watImage = Image.FromFile(_WaterPicPath);

            //水印图大于原图或原图小于300,不加水印
            if (watImage.Width > srcImage.Width || srcImage.Width < limitWidth)
            {
                srcImage.Dispose();
                watImage.Dispose();
                return;
            }
            #endregion

            //---------------------------------------
            #region 位置
            int x = 0, y = 0;
            if (top != 0)
            {
                y = top;
                if (left != 0)
                {
                    x = left;
                }
                else
                {
                    x = srcImage.Width - watImage.Width - right;
                }

            }
            else if (bottom != 0)
            {
                y = srcImage.Height - watImage.Height - bottom;
                if (left != 0)
                {
                    x = left;
                }
                else
                {
                    x = srcImage.Width - watImage.Width - right;
                }
            }
            else
            {
                //居中
                x = srcImage.Width / 2 - watImage.Width / 2;
                y = srcImage.Height / 2 - watImage.Height / 2;
            }
            #endregion

            //---------------------------------------
            #region 保存
            Bitmap bitmap = new Bitmap(srcImage);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(watImage, new Rectangle(x, y, watImage.Width, watImage.Height), 0, 0, watImage.Width, watImage.Height, GraphicsUnit.Pixel);
            srcImage.Dispose();
            watImage.Dispose();
            g.Dispose();

            try
            {
                if (DirFileHelper.GetFileExtension(srcFile) == "png")
                {
                    bitmap.Save(srcFile, System.Drawing.Imaging.ImageFormat.Png);
                }
                else//以jpg格式保存缩略图
                {
                    //---------------------------------------
                    //高质量--使用分级图片质量
                    if (HighQuality > 0)
                    {
                        // 以下代码为保存图片时,设置压缩质量
                        EncoderParameters encoderParams = new EncoderParameters();
                        long[] quality = new long[1];
                        quality[0] = HighQuality;   //50-100内
                        EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                        encoderParams.Param[0] = encoderParam;

                        //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象.
                        ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                        ImageCodecInfo jpegICI = null;
                        for (int i = 0; i < arrayICI.Length; i++)
                        {
                            if (arrayICI[i].FormatDescription.Equals("JPEG"))
                            {
                                jpegICI = arrayICI[i];//设置JPEG编码
                                break;
                            }
                        }

                        //---------------------------------------
                        bitmap.Save(srcFile, jpegICI, encoderParams);

                    }
                    else
                    {
                        bitmap.Save(srcFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
            }
            #endregion
        }

        #endregion

        #region 取得图片文件的宽高
        /// <summary>取得图片文件的宽高</summary>
        /// <param name="srcFile"></param>
        /// <param name="ww">取得图片的宽度</param>
        /// <param name="hh">取得图片的高度</param>
        /// <returns></returns>
        public static bool Get_Pic_WW_HH(string srcFile, out int ww, out int hh)
        {
            string sExt = DirFileHelper.GetFileExtension(srcFile).ToLower();
            if (sExt == "gif" || sExt == "jpg" || sExt == "jpeg" || sExt == "bmp" || sExt == "png")
            {
                if (srcFile.IndexOf(":") < 0) { srcFile = DirFileHelper.GetMapPath(srcFile); }
                if (DirFileHelper.IsExistFile(srcFile))
                {
                    try
                    {
                        Image testImage = Image.FromFile(srcFile);
                        ww = testImage.Width;
                        hh = testImage.Height;
                        testImage.Dispose();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            ww = 0;
            hh = 0;
            return false;
        }
        #endregion


    }
}
