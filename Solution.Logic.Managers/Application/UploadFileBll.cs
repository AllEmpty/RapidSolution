using System;
using System.Collections.Generic;
using System.Text;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using SubSonic.Query;
using System.Web.UI;


 /***********************************************************************
  *   作    者：July
  *   博    客：
  *   技 术 群：327360708
  *  
  *   创建日期：很多很多年前
  *   文件名称：UploadFileBll.cs
  *   描    述：文件上传逻辑类
  *             
  *   修 改 人：
  *   修改日期：
  *   修改原因：
  ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>UploadFileBll逻辑类</summary>
    public partial class UploadFileBll : LogicBase
    {

        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        #region 取得属性
        /// <summary>取得 Ext</summary>
        /// <param name="typeKey"></param>
        /// <returns></returns>
        private string Get_Ext(string typeKey)
        {
            if (typeKey.Length == 0) { return ""; }

            var model = UploadType.SingleOrDefault(x => x.TypeKey == typeKey);
            return model.Ext;
            //return SqlHelper.ExecuteScalarToStr("select top 1 Ext from UploadType where Key='" + typeKey.ToString() + "'");
        }

        /// <summary>读取--UploadConfig</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private UploadConfig Read_UploadConfig(int id)
        {
            return UploadConfig.SingleOrDefault(x => x.Id == id);
        }

        /// <summary>添加--UploadFile</summary>
        /// <param name="model">UploadFile实体</param>
        /// <returns></returns>
        public void Add_UploadFile(UploadFile model)
        {
            model.Save();
        }
        #endregion

        #region 检查权限
        /// <summary>检查权限,是否有权上传图片 ( Web 上传专用)</summary>
        /// <param name="userType">用户类别：1=管理员上传，2=会员上传</param>
        /// <returns></returns>
        public bool CheckPower(string userType)
        {
            bool ret = false;

            if (userType == "1")
            {
                var uinfo = (Manager)SessionHelper.GetSession("User");
                if (uinfo != null && uinfo.Id > 0)
                {
                    ret = true;
                }
            }
            else
            {
                /*
                BLL.wUser oWa = new wUser();
                if (oWa.ReadUser())
                {
                    ret = true;
                }
                */
            }
            return ret;
        }
        #endregion

        #region 上传文件
        /// <returns>上传成功返回"",并填充 UploadFile</returns>
        /// <param name="vid">上传配置模块id，即Id</param>
        /// <param name="key">随机key</param>
        /// <param name="userId">上传者id</param>
        /// <param name="userName">上传者UserName</param>
        /// <param name="m_r">UploadFile</param>
        /// <param name="filePostName">上传文件框控件的名称，默认"imgFile"，uploadify 默认 "Filedata"</param>
        /// <param name="userType">0=未知，1＝后台管理员上传，2=前台会员上传</param>
        /// <param name="isEditor">从GetAction返回判断是否为编辑器</param>
        /// <param name="isSwf">是否通过flash上传</param>
        /// <returns>上传成功返回"",并填充 UploadFile</returns>
        public string Upload_Web(int vid, string key, int userId, string userName,
            UploadFile m_r, string filePostName = "imgFile",
            int userType = 1, bool isEditor = false, bool isSwf = false)
        {
            #region 检查参数
            //---------------------------------------------------
            if (vid < 1 || key.Length < 10)
            {
                return "缺少参数:key或sid";
            }

            string dir = RequestHelper.GetKeyChar("dir");//编辑器专用:image,flash,media,file
            if (dir.Length > 0)
            {
                if (Array.IndexOf("image,flash,media,file".Split(','), dir) == -1)
                {
                    return "缺少参数:dir";
                }
            }
            //---------------------------------------------------
            UploadConfig mC = Read_UploadConfig(vid);
            if (mC == null || mC.Id != vid)
            {
                return "缺少参数:上传配置Id设置不正确！";
            }

            if (mC.IsPost != 1)
            {
                return "系统暂时禁止上传文件2！";
            }

            if (mC.IsEditor == 1 && isEditor == false)
            {
                return "非编辑器类别！";
            }


            if (mC.IsSwf == 0 && isSwf == true)
            {
                return "不能从flash中上传！";
            }
            #endregion

            //---------------------------------------------------
            #region 检查登陆
            m_r.UserId = 0;
            if (mC.UserType == 1)//管理员
            {
                if (userType == 1)
                {
                    m_r.UserId = userId;
                    m_r.UserName = userName;
                }
            }
            else
            {
                if (userType == 2)//一般会员
                {
                    m_r.UserId = userId;
                    m_r.UserName = userName;
                }
            }

            if (m_r.UserId == 0)
            {
                return "您的权限不足！";
            }
            #endregion

            //------------------------------------------------
            #region 设置上传参数
            var oUp = new Uploader();

            oUp.IsEnabled = true;
            if (isSwf)
            {
                oUp.IsChkSrcPost = false;  //如果swf提交，必须设置为 o_up.isChkSrcPost = false;
            }
            else
            {
                //o_up.isChkSrcPost = (m_c.isChkSrcPost == "1");  //如果swf提交，必须设置为 o_up.isChkSrcPost = false;
                oUp.IsChkSrcPost = true;
            }

            oUp.CutType = ConvertHelper.Cint0(mC.CutType);

            oUp.FilePostName = filePostName;

            if (isEditor && mC.UploadType_TypeKey == "editor")
            {
                mC.UploadType_TypeKey = dir;
            }
            oUp.AllowedExt = Get_Ext(mC.UploadType_TypeKey);
            oUp.MaxSize = (mC.UploadType_TypeKey == "image") ? ConvertHelper.Cint0(mC.PicSize) : ConvertHelper.Cint0(mC.FileSize);
            oUp.SavePath = mC.SaveDir;


            oUp.SetPic((mC.IsFixPic == 1), ConvertHelper.Cint0(mC.PicWidth), ConvertHelper.Cint0(mC.PicHeight), ConvertHelper.Cint0(mC.PicQuality));
            oUp.SetBig((mC.IsBigPic == 1), ConvertHelper.Cint0(mC.BigWidth), ConvertHelper.Cint0(mC.BigHeight), ConvertHelper.Cint0(mC.BigQuality));
            oUp.SetMid((mC.IsMidPic == 1), ConvertHelper.Cint0(mC.MidWidth), ConvertHelper.Cint0(mC.MidHeight), ConvertHelper.Cint0(mC.MidQuality));
            oUp.SetMin((mC.IsMinPic == 1), ConvertHelper.Cint0(mC.MinWidth), ConvertHelper.Cint0(mC.MinHeight), ConvertHelper.Cint0(mC.MinQuality));
            oUp.SetHot((mC.IsHotPic == 1), ConvertHelper.Cint0(mC.HotWidth), ConvertHelper.Cint0(mC.HotHeight), ConvertHelper.Cint0(mC.HotQuality));

            oUp.IsWaterPic = (mC.IsWaterPic == 1);
            #endregion

            #region 上传
            //------------------------------------------------
            bool isOk = oUp.UploadFile();
            if (!isOk)
            {
                //上传出错
                return StringHelper.XssTextClear(oUp.GetErrMsg() + mC.Id);
            }
            #endregion

            //----------------------------------------------------------------
            #region 保存入数据库
            m_r.UploadConfig_Id = mC.Id;
            m_r.JoinName = mC.JoinName;
            m_r.JoinId = 0;

            m_r.UserType = mC.UserType;
            m_r.UserIp = IpHelper.GetUserIp();
            m_r.AddDate = DateTime.Now;
            m_r.InfoText = "";
            m_r.RndKey = key;

            m_r.Name = oUp.NewFile;
            m_r.Path = oUp.NewPath;
            m_r.Src = StringHelper.Left(oUp.SrcName, 90);
            m_r.Ext = oUp.FileExt;

            m_r.Size = oUp.GetFileSize();
            m_r.PicWidth = oUp.NewWidth;
            m_r.PicHeight = oUp.NewHeight;

            //保存入数据库
            Add_UploadFile(m_r);
            #endregion

            //------------------------------------
            //上传成功，输出结果
            return "";
        }

        /// <returns>上传成功返回"",并填充 UploadFile(AspNet上传控件专用)</returns>
        /// <param name="oFile">System.Web.HttpPostedFile</param>
        /// <param name="vid">上传配置模块id，即Id</param>
        /// <param name="key">随机key</param>
        /// <param name="userId">上传者id</param>
        /// <param name="userName">上传者UserName</param>
        /// <param name="m_r">UploadFile</param>
        /// <param name="userType">0=未知，1＝后台管理员上传，2=前台会员上传</param>
        /// <returns>上传成功返回"",并填充 UploadFile</returns>
        public string Upload_AspNet(System.Web.HttpPostedFile oFile, int vid, string key, int userId, string userName,
            UploadFile m_r, int userType = 1)
        {
            #region 检查参数
            //---------------------------------------------------
            if (vid < 1 || key.Length < 10)
            {
                return "缺少参数:key或sid";
            }


            //---------------------------------------------------
            UploadConfig mC = Read_UploadConfig(vid);
            if (mC.Id != vid)
            {
                return "缺少参数:Id！";
            }

            if (mC.IsPost != 1)
            {
                return "系统暂时禁止上传文件2！";
            }

            if (mC.IsEditor == 1)
            {
                return "非编辑器类别！";
            }
            #endregion

            //---------------------------------------------------
            #region 检查登陆
            m_r.UserId = 0;
            if (mC.UserType == 1)//管理员
            {
                if (userType == 1)
                {
                    m_r.UserId = userId;
                    m_r.UserName = userName;
                }
            }
            else
            {
                if (userType == 2)//一般会员
                {
                    m_r.UserId = userId;
                    m_r.UserName = userName;
                }
            }

            if (m_r.UserId == 0)
            {
                return "您的权限不足！";
            }
            #endregion

            //------------------------------------------------
            #region 设置上传参数
            var oUp = new Uploader();

            oUp.IsEnabled = true;
            oUp.IsChkSrcPost = true;
            oUp.CutType = ConvertHelper.Cint0(mC.CutType);
            oUp.AllowedExt = Get_Ext(mC.UploadType_TypeKey);
            oUp.MaxSize = (mC.UploadType_TypeKey == "image") ? ConvertHelper.Cint0(mC.PicSize) : ConvertHelper.Cint0(mC.FileSize);
            oUp.SavePath = mC.SaveDir;


            oUp.SetPic((mC.IsFixPic == 1), ConvertHelper.Cint0(mC.PicWidth), ConvertHelper.Cint0(mC.PicHeight), ConvertHelper.Cint0(mC.PicQuality));
            oUp.SetBig((mC.IsBigPic == 1), ConvertHelper.Cint0(mC.BigWidth), ConvertHelper.Cint0(mC.BigHeight), ConvertHelper.Cint0(mC.BigQuality));
            oUp.SetMid((mC.IsMidPic == 1), ConvertHelper.Cint0(mC.MidWidth), ConvertHelper.Cint0(mC.MidHeight), ConvertHelper.Cint0(mC.MidQuality));
            oUp.SetMin((mC.IsMinPic == 1), ConvertHelper.Cint0(mC.MinWidth), ConvertHelper.Cint0(mC.MinHeight), ConvertHelper.Cint0(mC.MinQuality));
            oUp.SetHot((mC.IsHotPic == 1), ConvertHelper.Cint0(mC.HotWidth), ConvertHelper.Cint0(mC.HotHeight), ConvertHelper.Cint0(mC.HotQuality));

            oUp.IsWaterPic = (mC.IsWaterPic == 1);
            #endregion

            #region 上传
            //------------------------------------------------
            bool isOk = oUp.UploadFile(oFile);
            if (!isOk)
            {
                //上传出错
                return StringHelper.XssTextClear(oUp.GetErrMsg() + mC.Id);
            }
            #endregion

            //----------------------------------------------------------------
            #region 保存入数据库
            m_r.UploadConfig_Id = mC.Id;
            m_r.JoinName = mC.JoinName;
            m_r.JoinId = 0;

            m_r.UserType = mC.UserType;
            m_r.UserIp = IpHelper.GetUserIp();
            m_r.AddDate = DateTime.Now;
            m_r.InfoText = "";
            m_r.RndKey = key;

            m_r.Name = oUp.NewFile;
            m_r.Path = oUp.NewPath;
            m_r.Src = StringHelper.Left(oUp.SrcName, 90, false);
            m_r.Ext = oUp.FileExt;

            m_r.Size = oUp.GetFileSize();
            m_r.PicWidth = oUp.NewWidth;
            m_r.PicHeight = oUp.NewHeight;

            //保存入数据库
            Add_UploadFile(m_r);
            #endregion

            //------------------------------------
            //上传成功，输出结果
            return "";
        }


        /// <returns>上传成功返回"",并填充 Model.UploadFile</returns>
        /// <param name="vid">上传配置模块id，即UploadConfig_Id</param>
        /// <param name="key">随机key</param>
        /// <param name="userId">上传者id</param>
        /// <param name="userName">上传者UserName</param>
        /// <param name="remotePicUrl">远程图片的url地址</param>
        /// <param name="m_r">Model.UploadFile</param>
        /// <returns>上传成功返回"",并填充 Model.UploadFile</returns>
        public string Upload_RemotePic(int vid, string key, int userId, string userName, string remotePicUrl, UploadFile m_r)
        {
            #region 检查参数
            //---------------------------------------------------
            if (vid < 1 || key.Length < 10)
            {
                return "缺少参数:key或sid";
            }
            //---------------------------------------------------

            #region 检查登陆
            m_r.UserId = userId;
            m_r.UserName = userName;

            if (m_r.UserId == 0)
            {
                return "您的权限不足！";
            }
            #endregion

            //---------------------------------------------------
            UploadConfig mC = Read_UploadConfig(vid);
            if (mC.Id != vid)
            {
                return "缺少参数:UploadConfig_Id！";
            }

            if (mC.IsPost != 1)
            {
                return "系统暂时禁止上传文件2！";
            }

            if (mC.IsEditor != 1)
            {
                return "非编辑器类别！";
            }

            mC.UploadType_TypeKey = "image";
            #endregion


            //----------------------------------------------
            #region 生成暂时目录
            string sCfgSavePath = new Uploader().SavePath;
            string sSavePath = DirFileHelper.FixDirPath(sCfgSavePath + mC.SaveDir) + DateTime.Now.ToString("yyMM") + "/";
            if (!DirFileHelper.CheckSaveDir(sSavePath))
            {
                return "SavePath设置不当:" + sSavePath + ", 或权限不足！";
            }

            string sServerDir = sCfgSavePath + "remote/";
            if (!DirFileHelper.CheckSaveDir(sServerDir))
            {
                return "ServerDir设置不当:" + sServerDir + ", 或权限不足！";
            }
            //----------------------------------------------
            string sSrcName = StringHelper.Left(DirFileHelper.GetFileName(remotePicUrl), 90);
            string sFileExt = DirFileHelper.GetFileExtension(sSrcName);

            //因部部分网站不是标准的jpg、gif扩展名，所以修改下面代码
            if (sFileExt.Length > 0)
            {
                string sAllowed = ",jpg,gif,png,bmp,";
                string sExt = "," + sFileExt.ToLower() + ",";
                if (sAllowed.IndexOf(sExt) == -1)
                {
                    sFileExt = "jpg";
                }
            }
            else
            {
                sFileExt = "jpg";
            }
            //----------------------------------------------

            string sNewFile = DirFileHelper.GetRndFileName("." + sFileExt);

            if (sServerDir.IndexOf(":") < 0)
            {
                sServerDir = DirFileHelper.FixDirPath(DirFileHelper.GetMapPath(sServerDir));
            }
            string sNewRoot = System.IO.Path.Combine(sServerDir, sNewFile);
            #endregion

            //----------------------------------------------
            #region 上传到暂时目录
            try
            {
                var wc = new System.Net.WebClient();
                wc.DownloadFile(remotePicUrl, sNewRoot);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message.ToLower();
            }

            if (!DirFileHelper.IsExistFile(sNewRoot))
            {
                return "上传失败";
            }
            #endregion

            //----------------------------------------------
            #region 判断是否真实图片格式，并取得图片宽高
            int ww = 0, hh = 0;
            if (!Uploader.Get_Pic_WW_HH(sNewRoot, out ww, out hh))
            {
                DirFileHelper.DeleteFile(sNewRoot);
                return "非法格式！不是图片文件。";
            }

            int iMaxSize = mC.PicSize;
            long iFileSize = DirFileHelper.GetFileSize(sNewRoot);
            /*
            if (iFileSize > iMaxSize)
            {
                return "上传文件大小超过了限制.最多上传(" + DirFileHelper.FmtFileSize2(iMaxSize) + ").";
            }
            */
            #endregion


            #region 把上传的暂时文件复制到相关模块目录中
            string sNewPath = sSavePath + sNewFile;
            string orgImg = DirFileHelper.GetFilePathPostfix(sNewPath, "o");

            //复制到原始图
            DirFileHelper.CopyFile(sNewRoot, orgImg);

            //删除暂时上传的图片
            DirFileHelper.DeleteFile(sNewRoot);

            //生成相关缩略图
            OneMakeThumbImage(sNewPath, mC);

            #endregion


            //----------------------------------------------
            #region 保存入数据库
            m_r.UploadConfig_Id = mC.Id;
            m_r.JoinName = mC.JoinName;
            m_r.JoinId = 0;

            m_r.UserType = mC.UserType;
            m_r.UserIp = IpHelper.GetUserIp();
            m_r.AddDate = DateTime.Now;
            m_r.InfoText = "";
            m_r.RndKey = key;

            m_r.Name = sNewFile;
            m_r.Path = sNewPath;
            m_r.Src = sSrcName;
            m_r.Ext = sFileExt;

            m_r.Size = ConvertHelper.Cint0(iFileSize);
            m_r.PicWidth = ww;
            m_r.PicHeight = hh;

            //保存入数据库
            Add_UploadFile(m_r);
            #endregion

            //------------------------------------
            //上传成功，输出结果
            return "";
        }

        #endregion


        #region FCK
        /// <summary>FCKEditor 使用,整理_UpList,把不存在的文件删除</summary>
        /// <param name="sHtml">Html代码</param>
        /// <param name="upList">UpFileList列表</param>
        /// <returns></returns>
        public string FCK_BatchDelPic(string sHtml, string upList)
        {
            if (upList.Length == 0) { return ""; }
            const string basePath = "/UploadFile/";
            string ss = "";
            int tj = basePath.Length + 3;

            upList = upList.Replace("|", ",");
            upList = upList.Replace(",,", ",");

            string[] aFiles = StringHelper.SplitMulti(upList, ",");

            var sb = new StringBuilder();
            foreach (string c in aFiles)
            {
                if (c.Length > tj && StringHelper.IsRndFileName(c))
                {
                    if (sHtml.IndexOf(c) > 0)
                    {
                        sb.Append(c);
                        sb.Append(",");
                    }
                    else
                    {
                        ss = c.Substring(0, basePath.Length);

                        if (string.Compare(ss, basePath, true) == 0)
                        {
                            DirFileHelper.DelPicFile(c);
                            Upload_DelRs(c);
                        }
                    }
                }
            }

            ss = sb.ToString();
            if (ss.Length > 0)
            {
                ss = StringHelper.DelStrSign(ss, ",");
            }
            return ss;
        }
        #endregion

        #region 上传后关联表格
        /// <summary>根据 FilePath 更新 UploadFile的记录</summary>
        /// <param name="rndKey">RndKey</param>
        /// <param name="joinName">JoinName</param>
        /// <param name="joinId">关联ID--</param>
        public void Upload_UpdateRs(string rndKey, string joinName, int joinId)
        {
            //设置更新值
            var setValue = new Dictionary<string, object>();
            setValue[UploadFileTable.JoinName] = joinName;
            setValue[UploadFileTable.JoinId] = joinId;
            //设置条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>();
            wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, UploadFileTable.RndKey, Comparison.Equals, rndKey));

            //更新
            //UploadFileBll.GetInstence().UpdateValue(page, setValue, wheres);

            var update = new UpdateHelper();
            update.Update<UploadFile>(setValue, wheres);

            //SqlHelper.ExecuteNonQuery("update UploadFile set JoinName='" + joinName + "',JoinId=" + joinId + " where RndKey='" + rndKey + "'");
        }

        /// <summary>根据 FilePath 删除 UploadFile的记录，不删除文件</summary>
        /// <param name="filePath">FilePath</param>
        private void Upload_DelRs(string filePath)
        {
            ////设置条件
            //var wheres = new List<ConditionFun.SqlqueryCondition>();
            //wheres.Add(new ConditionFun.SqlqueryCondition(ConstraintType.And, UploadFileTable.Path, Comparison.Equals, filePath));
            ////查询
            //var select = new SelectHelper();
            //int id = ConvertHelper.Cint0(select.GetColumnsValue<UploadFile>(UploadFileTable.Id, wheres));
            int id = ConvertHelper.Cint0(GetFieldValue(UploadFileTable.Id, UploadFileTable.Path, filePath, false));
            //判断
            if (id > 0)
            {
                UploadFile.Delete(x => x.Id == id);
            }

            //int k = SqlHelper.ExecuteScalarToInt("select top 1 Id from UploadFile where Path='" + filePath + "'");
            //if (k > 0) {
            //    SqlHelper.ExecuteNonQuery("delete from UploadFile where Id=" + k.ToString());
            //}
        }

        /// <summary>比较oldFile 同 newFile,删除旧文件</summary>
        /// <param name="fieldId">InfoID</param>
        /// <param name="fieldImg">PicImg</param>
        /// <param name="tableName">AdInfo</param>
        /// <param name="id">InfoID</param>
        /// <param name="newFile">新文件</param>
        public void Upload_DiffFile(string fieldId, string fieldImg, string tableName, int id, string newFile)
        {
            var select = new SelectHelper();
            string sOldFile = select.ExecuteScalar("select top 1 " + fieldImg + " from " + tableName + " where Len(" + fieldImg + ")>4 and " + fieldId + "=" + id) + "";
            Upload_DiffFile(sOldFile, newFile, true);
        }

        /// <summary>比较_oldFile 同 _newFile,删除旧文件</summary>
        /// <param name="oldFile">旧文件</param>
        /// <param name="newFile">新文件</param>
        /// <param name="isPic">是图片文件,同时删除小图片</param>
        public void Upload_DiffFile(string oldFile, string newFile, bool isPic)
        {
            //删除旧的文件
            if (oldFile.Length > 4 && string.Compare(oldFile, newFile, true) != 0)
            {
                if (isPic)
                {
                    DirFileHelper.DelPicFile(oldFile);
                }
                else
                {
                    DirFileHelper.DeleteFile(oldFile);
                }
                Upload_DelRs(oldFile);
            }
        }
        #endregion

        #region 删除
        /// <summary>删除单一文件，并删除数据库记录</summary>
        /// <param name="picPath">/upload/1.jpg</param>
        /// <returns></returns>
        public void Upload_OneDelPic(string picPath)
        {
            if (string.IsNullOrEmpty(picPath)) { return; }
            if (picPath.Length < 4) { return; }

            DirFileHelper.DelPicFile(picPath);
            Upload_DelRs(picPath);
        }

        /// <summary>删除单一文件，并删除数据库记录</summary>
        /// <param name="picPath">/upload/1.jpg</param>
        /// <returns></returns>
        public void Upload_OneDel(string picPath)
        {
            if (string.IsNullOrEmpty(picPath)) { return; }
            if (picPath.Length < 4) { return; }

            DirFileHelper.DeleteFile(picPath);
            Upload_DelRs(picPath);
        }

        /// <summary>批量删除,使用,分隔</summary>
        /// <param name="picList">/upload/1.jpg,/upload/2.jpg,/upload/3.jpg,</param>
        /// <returns></returns>
        public void Upload_BatDelPic(string picList)
        {
            if (string.IsNullOrEmpty(picList)) { return; }
            if (picList.Length < 4) { return; }

            string[] aFile = StringHelper.SplitMulti(picList, ",");
            foreach (string s in aFile)
            {
                if (s.Length > 3)
                {
                    Upload_OneDelPic(s);
                }
            }
        }

        /// <summary>批量删除，根据JoinName和JoinId</summary>
        /// <param name="joinName">相关表名，即UploadConfig.JoinName</param>
        /// <param name="joinId">相关ID,即UploadConfig.JoinID</param>
        public static void Upload_BatDelPic(string joinName, int joinId)
        {
            if (string.IsNullOrEmpty(joinName) || joinId < 1) { return; }
            //string sql = "select Id,Path from UploadFile where JoinName='+joinName+' and JoinId=" + joinId;

            //-------------------------------------------------
            var dt = UploadFile.Find(x => x.JoinId == joinId && x.JoinName == joinName);

            foreach (var rs in dt)
            {
                DirFileHelper.DelPicFile(rs.Path);
                UploadFile.Delete(x => x.Id == rs.Id);
            }
        }
        #endregion


        #region 取得原文件名
        /// <summary>取得原文件名</summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string Get_SrcName_byFilePath(string filePath)
        {
            //查询
            object obj = GetFieldValue(UploadFileTable.Src, UploadFileTable.Path, filePath, false);
            //判断
            if (obj != null)
            {
                return obj.ToString();
            }

            return "";

            //return SqlHelper.ExecuteScalarToStr("select top 1 Src from UploadFile where Path='" + filePath + "'");
        }
        #endregion


        #region 批量一次修改Size
        /// <summary>生成全部的图片</summary>
        public string fix_PicSizeAll()
        {
            StringBuilder sb = new StringBuilder();
            string[] rs1 = DataTableHelper.GetArrayString(UploadConfigBll.GetInstence().GetDataTable(), UploadConfigTable.Id);
            if (rs1 != null && rs1.Length > 0)
            {
                int ti = rs1.Length;
                for (int i = 0; i < ti; i++)
                {
                    sb.Append(fix_PicSize(ConvertHelper.Cint0(rs1[i])));

                }
            }
            return sb.ToString();
        }

        /// <summary>生成一条配置记录的图片</summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public string fix_PicSize(int vid)
        {
            //---------------------------------------------------
            UploadConfig mC = new UploadConfig(x => x.Id == vid);
            if (mC.Id != vid)
            {
                return "缺少参数:Id！";
            }

            StringBuilder sb = new StringBuilder();
            //只修改关联的图片
            //string sql = "select Path from UploadFile where Id=" + vid + " and JoinId>0 order by Id";

            //修改全部图片（包含非关联）
            //string sql = "select Path from UploadFile where UploadConfig_Id=" + vid + " order by Id";

            //定义要查询出来的列
            var list = new List<string>();
            list.Add(UploadFileTable.Path);
            //定义查询条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>();
            wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, UploadFileTable.UploadConfig_Id, Comparison.Equals, vid));
            //定义排序
            var sort = new List<string>();
            sort.Add(UploadFileTable.Id);

            string[] rs1 = DataTableHelper.GetArrayString(GetDataTable(false, 0, list, 0, 0, wheres, sort), UploadFileTable.Path);
            if (rs1 != null && rs1.Length > 0)
            {
                int ti = rs1.Length;
                for (int i = 0; i < ti; i++)
                {
                    string picImg = rs1[i];
                    string sExt = DirFileHelper.GetFileExtension(picImg).ToLower();
                    if (picImg.Length > 4 && (sExt == "jpg" || sExt == "png" || sExt == "gif"))
                    {
                        string orgImg = DirFileHelper.GetFilePathPostfix(picImg, "o");

                        if (!DirFileHelper.IsExistFile(orgImg))
                        {
                            DirFileHelper.CopyFile(picImg, orgImg);
                        }

                        //原始图存在的情况下，才能生成其它size
                        if (DirFileHelper.IsExistFile(orgImg))
                        {
                            OneMakeThumbImage(picImg, mC);
                        }
                        else
                        {
                            sb.AppendFormat("原始文件不存在＝{0}<br />", picImg);
                        }
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 单一文件缩放
        /// <summary>根据UploadConfig的单一缩放</summary>
        /// <param name="picImg">图片地址（没有任何扩展后缀，比如后台没有："b","m","s","b"）</param>
        /// <param name="mC">根据vid查得的 UploadConfig配置</param>
        public static void OneMakeThumbImage(string picImg, UploadConfig mC)
        {
            string bigImg = DirFileHelper.GetFilePathPostfix(picImg, "b");
            string midImg = DirFileHelper.GetFilePathPostfix(picImg, "m");
            string minImg = DirFileHelper.GetFilePathPostfix(picImg, "s");
            string hotImg = DirFileHelper.GetFilePathPostfix(picImg, "h");
            string orgImg = DirFileHelper.GetFilePathPostfix(picImg, "o");

            //--------------------------------------------------
            //big
            DirFileHelper.DeleteFile(bigImg);
            if (mC.IsBigPic == 1)
            {
                if (mC.BigWidth > 0 && mC.BigHeight > 0)
                {
                    Uploader.MakeThumbImage(orgImg, bigImg,
                                   ConvertHelper.Cint0(mC.BigWidth),
                                   ConvertHelper.Cint0(mC.BigHeight),
                                   ConvertHelper.Cint0(mC.BigQuality),
                                   ConvertHelper.Cint0(mC.CutType));
                }
                else//因为不限制宽高，所以直接复制出来就行了
                {
                    DirFileHelper.CopyFile(orgImg, bigImg);
                }

                //添加水印
                if (mC.IsWaterPic == 1)
                {
                    Uploader.MakeWaterPic(bigImg);
                }
            }


            //--------------------------------------------------
            //mid
            DirFileHelper.DeleteFile(midImg);
            if (mC.IsMidPic == 1)
            {
                if (mC.MidWidth > 0 && mC.MidHeight > 0)
                {
                    Uploader.MakeThumbImage(orgImg, midImg,
                                            ConvertHelper.Cint0(mC.MidWidth),
                                            ConvertHelper.Cint0(mC.MidHeight),
                                            ConvertHelper.Cint0(mC.MidQuality),
                                            ConvertHelper.Cint0(mC.CutType));
                }
                else//因为不限制宽高，所以直接复制出来就行了
                {
                    DirFileHelper.CopyFile(orgImg, midImg);
                }

                //添加水印
                if (mC.IsWaterPic == 1)
                {
                    Uploader.MakeWaterPic(midImg);
                }
            }

            //--------------------------------------------------
            //hot
            DirFileHelper.DeleteFile(hotImg);
            if (mC.IsHotPic == 1)
            {
                if (mC.HotWidth > 0 && mC.HotHeight > 0)
                {
                    Uploader.MakeThumbImage(orgImg, hotImg,
                                            ConvertHelper.Cint0(mC.HotWidth),
                                            ConvertHelper.Cint0(mC.HotHeight),
                                            ConvertHelper.Cint0(mC.HotQuality),
                                            ConvertHelper.Cint0(mC.CutType));
                }
                else//因为不限制宽高，所以直接复制出来就行了
                {
                    DirFileHelper.CopyFile(orgImg, hotImg);
                }

                //添加水印
                if (mC.IsWaterPic == 1)
                {
                    Uploader.MakeWaterPic(hotImg);
                }
            }

            //--------------------------------------------------
            //min
            DirFileHelper.DeleteFile(minImg);
            if (mC.IsMinPic == 1)
            {
                if (mC.IsMinPic > 0 && mC.MinHeight > 0)
                {
                    Uploader.MakeThumbImage(orgImg, minImg,
                                            ConvertHelper.Cint0(mC.MinWidth),
                                            ConvertHelper.Cint0(mC.MinHeight),
                                            ConvertHelper.Cint0(mC.MinQuality),
                                            ConvertHelper.Cint0(mC.CutType));
                }
                else//因为不限制宽高，所以直接复制出来就行了
                {
                    DirFileHelper.CopyFile(orgImg, minImg);
                }

                //微型图不用加水印
            }


            //--------------------------------------------------
            //pic
            DirFileHelper.DeleteFile(picImg);
            if (mC.IsFixPic == 1)
            {
                if (mC.PicWidth > 0 && mC.PicHeight > 0)
                {
                    Uploader.MakeThumbImage(orgImg, picImg,
                                            ConvertHelper.Cint0(mC.PicWidth),
                                            ConvertHelper.Cint0(mC.PicHeight),
                                            ConvertHelper.Cint0(mC.PicQuality),
                                            ConvertHelper.Cint0(mC.CutType));
                }
                else//因为不限制宽高，所以直接复制出来就行了
                {
                    DirFileHelper.CopyFile(orgImg, picImg);
                }

                //添加水印
                if (mC.IsWaterPic == 1)
                {
                    Uploader.MakeWaterPic(picImg);
                }
            }
        }
        #endregion

        
        #endregion 自定义函数

    }

}

