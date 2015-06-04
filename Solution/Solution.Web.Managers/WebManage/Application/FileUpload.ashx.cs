using System.Collections;
using System.Web;
using System.Web.SessionState;
using DotNet.Utilities;
using DotNet.Utilities.Json;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;


/***********************************************************************
 *   作    者：July
 *   博    客：
 *   技 术 群：327360708
 *  
 *   创建日期：很多很多年前
 *   文件名称：FileUpload.ashx.cs
 *   描    述：上传类（本页只用于后台，前台不调用）
 *             
 *   修 改 人：AllEmpty（陈焕）-- 1654937@qq.com
 *   修改日期：2014-07-02
 *   修改原因：将程序进行修改，以适应本框架结构调用
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Application
{
    /// <summary>
    /// FileUpload 上传类（本页只用于后台，前台不调用）
    /// </summary>
    public class FileUpload : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            //---------------------------------------------------
            var m_r = new UploadFile();
            int vid = 0, userId = 0;
            const int userType = 1;
            string key = "", msg = "", userName = "";

            //---------------------------------------------------
            bool isSwf = (RequestHelper.PostText("swf", 1) == "1");
            if (isSwf)
            {
                vid = RequestHelper.PostInt0("vid");
                key = RequestHelper.PostText("key", 20);
                if (!StringHelper.IsRndKey(key)) { key = ""; }
                if (vid < 1 || key.Length < 10)
                {
                    ShowMsg_Swf("缺少参数:key或sid");
                    return;
                }

                //---------------------------------------------------
                #region 判断权限
                userId = RequestHelper.PostInt0("uid");
                if (userId < 1)
                {
                    ShowMsg_Swf("缺少参数:uid");
                    return;
                }

                string uk = RequestHelper.PostText("uk", 32);
                if (uk.Length < 10)
                {
                    ShowMsg_Swf("缺少参数:uk");
                    return;
                }

                string userKey = OnlineUsersBll.GetInstence().GetMd5(ConvertHelper.Cint0(userId));
                if (userKey != uk)
                {
                    ShowMsg_Swf("还未登陆，权限不足！");
                    return;
                }

                userName = OnlineUsersBll.GetInstence().GetManagerCName(ConvertHelper.Cint0(userId));
                #endregion

                //---------------------------------------------------
                msg = new UploadFileBll().Upload_Web(vid, key, userId, userName, m_r, "imgFile", userType, false, true);
                //---------------------------------------------------
                if (msg.Length > 0)
                {
                    ShowMsg_Swf(msg);
                }
                else
                {
                    //上传成功，输出结果
                    ShowMsg_Swf("", 0, ConvertHelper.Cint0(m_r.Id),
                        m_r.Name, m_r.Path, m_r.Src);
                }
            }
            else
            {
                //---------------------------------------------------
                bool isEditor = (RequestHelper.GetQueryString("act") == "edit");
                vid = RequestHelper.GetInt0("vid");
                key = RequestHelper.GetKeyChar("key", 20);
                if (vid < 1 || key.Length < 10)
                {
                    ShowMsg_Swf("缺少参数:key或sid");
                    return;
                }
                //---------------------------------------------------
                #region 判断权限
                if (SessionHelper.GetSession("UserHashKey") != null)
                {
                    var uinfo = OnlineUsersBll.GetInstence().GetOnlineUsersModel();
                    userId = uinfo.Manager_Id;
                    userName = uinfo.Manager_CName;
                }
                else
                {
                    msg = "还未登陆，权限不足！";
                    if (isEditor)//编辑器 (ke4)
                    {
                        ShowMsg_Editor(msg);
                    }
                    else
                    {
                        //RequestHelper.AlertUtf8(msg, 3, "");
                        FineUI.Alert.Show(msg, "提示", MessageBoxIcon.Warning, "history.back();");
                    }
                    return;
                }
                #endregion
                //---------------------------------------------------
                msg = new UploadFileBll().Upload_Web(vid, key, userId, userName, m_r, "imgFile", userType, isEditor, false);
                if (msg.Length > 0)
                {
                    if (isEditor)//编辑器 (ke4)
                    {
                        ShowMsg_Editor(msg);
                    }
                    else
                    {
                        //RequestHelper.AlertUtf8(msg, 3, "");
                        FineUI.Alert.Show(msg, "提示", MessageBoxIcon.Warning, "history.back();");
                    }
                }
                else
                {
                    //上传成功，输出结果
                    if (isEditor)//编辑器 (ke4)
                    {
                        ShowMsg_Editor("上传成功", m_r.Path);
                    }
                    else
                    {
                        //RequestHelper.AlertUtf8("window.parent.OnUploadCompleted('" + m_r.Name + "','" + m_r.Path + "'," + m_r.Id.ToString() + ",'" + m_r.Src + "');", 7, "");
                        FineUI.Alert.Show("上传失败", "提示", MessageBoxIcon.Warning, "window.parent.OnUploadCompleted('" + m_r.Name + "','" + m_r.Path + "'," + m_r.Id.ToString() + ",'" + m_r.Src + "');");
                    }
                }
            }

            Write("系统暂时禁止上传文件！");
            return;
        }

        /// <summary>提示信息输出（编辑器ke4专用）</summary>
        /// <param name="msg">提示内容</param>
        /// <param name="filePath">上传后新的url地址</param>
        private void ShowMsg_Editor(string msg, string filePath = "")
        {
            var hash = new Hashtable();

            if (filePath == "")
            {
                hash["error"] = 1;
                hash["message"] = msg;
            }
            else
            {
                hash["error"] = 0;
                hash["url"] = filePath;
            }

            string str = JsonHelper.ToJson(hash);
            
            Write(str);
            //RequestHelper.AlertUtf8(str, 8, "");
        }

        /// <summary>提示信息输出（swf上传专用）</summary>
        /// <param name="msg">提示内容</param>
        /// <param name="err">1=成功，0=出错</param>
        /// <param name="subId">所以的上传模块id</param>
        /// <param name="fileName">上传后新的文件</param>
        /// <param name="filePath">上传后新的url地址</param>
        /// <param name="fileSrc">上传文件的原始名</param>
        private void ShowMsg_Swf(string msg, int err = 1, int subId = 0, string fileName = "", string filePath = "", string fileSrc = "")
        {
            var hash = new Hashtable();

            if (err == 0)
            {
                hash["err"] = err;
                hash["msg"] = "上传成功";
                hash["FileName"] = fileName;
                hash["FilePath"] = filePath;
                hash["SubID"] = subId;
                hash["FileSrc"] = fileSrc;
            }
            else
            {
                hash["err"] = 1;
                hash["msg"] = msg;
            }

            string str = JsonHelper.ToJson(hash);

            Write(str);
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void Write(string str)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "text/html";
            response.Charset = "utf-8";
            response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            response.Expires = -1;
            response.AddHeader("pragma", "no-cache");
            response.AddHeader("cache-control", "private");
            response.CacheControl = "no-cache";

            response.Write(str);
            response.End();
        }
    }
}