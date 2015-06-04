using System;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-07-01
 *   文件名称：InformationEdit.aspx.cs
 *   描    述：信息编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Informations
{
    public partial class InformationEdit : PageBase
    {
        protected string RndKey = RandomHelper.GetRndKey();
        protected string p_Img = "";

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取ID值
                hidId.Text = RequestHelper.GetInt0("Id") + "";
                //绑定下拉框
                InformationClassBll.GetInstence().BandDropDownListShowAll(this, ddlInformationClass_Id);
                //Key（上传必须）
                txtRndKey.Text = RndKey;

                //加载数据
                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {

        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public override void LoadData()
        {
            int id = ConvertHelper.Cint0(hidId.Text);

            if (id != 0)
            {
                //获取指定ID的菜单内容，如果不存在，则创建一个菜单实体
                var model = InformationBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //对页面窗体进行赋值
                txtTitle.Text = model.Title;
                ddlInformationClass_Id.SelectedValue = model.InformationClass_Id + "";

                txtKeywords.Text = model.Keywords;
                dpNewsTime.SelectedDate = model.NewsTime;

                txtNotes.Text = model.Notes;

                txtAuthor.Text = model.Author;
                txtFromName.Text = model.FromName;

                //置顶、审核、推荐
                rblIsTop.SelectedValue = model.IsTop + "";
                rblIsDisplay.SelectedValue = model.IsDisplay + "";
                rblIsHot.SelectedValue = model.IsHot + "";

                txtRedirectUrl.Text = model.RedirectUrl;

                //SEO
                txtSeoTitle.Text = model.SeoTitle;
                txtSeoKey.Text = model.SeoKey;
                txtSeoDesc.Text = model.SeoDesc;

                //Key（如果存在编辑器必须下面代码）
                txtText.Text = model.Content;
                txtUpload.Text = model.Upload;

                if (!String.IsNullOrEmpty(model.FrontCoverImg))
                {
                    p_Img = model.FrontCoverImg;
                    ButtonDeleteImage.Enabled = MenuInfoBll.GetInstence().CheckControlPower(this, "ButtonDeleteImage");
                }
                else
                {
                    ButtonDeleteImage.Visible = false;
                }
            }
            else
            {
                ButtonDeleteImage.Visible = false;
            }
        }

        #endregion

        #region 页面控件绑定功能

        #region 下拉列表改变事件
        /// <summary>下拉列表改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlInformationClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ConvertHelper.Cint0(hidId.Text);
            if (id > 0) return;

            //如果选择根节点，则将SEO全部置空
            if (ddlInformationClass_Id.SelectedValue == "0")
            {
                txtSeoTitle.Text = "";
                txtSeoKey.Text = "";
                txtSeoDesc.Text = "";
            }
            //否则读取分类节点的SEO值
            else
            {
                var icId = ConvertHelper.Cint0(ddlInformationClass_Id.SelectedValue);
                var model = InformationClassBll.GetInstence().GetModelForCache(x => x.Id == icId);
                if (model != null)
                {
                    txtSeoTitle.Text = model.SeoTitle;
                    txtSeoKey.Text = model.SeoKey;
                    txtSeoDesc.Text = model.SeoDesc;
                }
            }
        }
        #endregion

        #region 删除图片
        /// <summary>删除图片</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ButtonDeleteImage_Click(object sender, EventArgs e)
        {
            int id = ConvertHelper.Cint0(hidId.Text);
            if (id > 0)
            {
                InformationBll.GetInstence().DelFrontCoverImg(this, id);

                FineUI.PageContext.RegisterStartupScript("window.location.reload()");
            }
        }

        #endregion

        #endregion

        #region 保存
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <returns></returns>
        public override string Save()
        {
            string result = string.Empty;
            int id = ConvertHelper.Cint0(hidId.Text);

            try
            {
                #region 数据验证

                if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
                {
                    return txtTitle.Label + "不能为空！";
                }
                if (ddlInformationClass_Id.SelectedValue == "0")
                {
                    return ddlInformationClass_Id.Label + "为必选项，请选择！";
                }
                //判断是否重复
                var sTitle = StringHelper.FilterSql(txtTitle.Text, true);
                var icId = ConvertHelper.Cint0(ddlInformationClass_Id.SelectedValue);
                if (
                    DataAccess.DataModel.Information.Exists(
                        x => x.Title == sTitle && x.InformationClass_Id == icId && x.Id != id))
                {
                    return txtTitle.Label + "已存在！请重新输入！";
                }

                #endregion

                #region 赋值

                //获取实体
                var model = new Information(x => x.Id == id);

                //------------------------------------------
                //设置名称
                model.Title = StringHelper.Left(txtTitle.Text, 100);
                //取得分类
                model.InformationClass_Id = ConvertHelper.Cint0(ddlInformationClass_Id.SelectedValue);

                model.InformationClass_Root_Id =
                    ConvertHelper.Cint0(InformationClassBll.GetInstence()
                        .GetFieldValue(model.InformationClass_Id, InformationClassTable.ParentId));
                if (model.InformationClass_Root_Id > 0)
                {
                    model.InformationClass_Root_Name = InformationClassBll.GetInstence()
                        .GetName(this, model.InformationClass_Root_Id);
                }
                model.InformationClass_Name = StringHelper.Left(ddlInformationClass_Id.SelectedText, 20);

                //重定向
                model.RedirectUrl = StringHelper.Left(txtRedirectUrl.Text, 250);

                //------------------------------------------
                //编辑器
                model.Content = StringHelper.Left(txtText.Text, 0, true, false);
                model.Upload = StringHelper.Left(txtUpload.Text, 0, true, false);
                //这里必须用回前端存放的Key，不然删除时无法同步删除编辑器上传的图片
                RndKey = StringHelper.Left(txtRndKey.Text, 0);

                //检查用户上传的文件和最后保存的文件是否有出入，
                //如果上传的文件大于保存的文件，把不保存，但本次操作已经上传的文件删除。
                model.Upload = UploadFileBll.GetInstence().FCK_BatchDelPic(model.Content, model.Upload);

                //------------------------------------------
                //其它值
                model.NewsTime = dpNewsTime.SelectedDate ?? DateTime.Now;
                model.AddYear = model.NewsTime.Year;
                model.AddMonth = model.NewsTime.Month;
                model.AddDay = model.NewsTime.Day;

                model.Notes = StringHelper.Left(txtNotes.Text, 200);

                model.Keywords = StringHelper.Left(txtKeywords.Text, 50);
                model.Author = StringHelper.Left(txtAuthor.Text, 50);
                model.FromName = StringHelper.Left(txtFromName.Text, 50);


                model.SeoTitle = StringHelper.Left(txtSeoTitle.Text, 100);
                model.SeoKey = StringHelper.Left(txtSeoKey.Text, 100);
                model.SeoDesc = StringHelper.Left(txtSeoDesc.Text, 200);
                model.Sort = 0;

                //设定当前项是否显示
                model.IsDisplay = ConvertHelper.StringToByte(rblIsDisplay.SelectedValue);
                model.IsHot = ConvertHelper.StringToByte(rblIsHot.SelectedValue);
                model.IsTop = ConvertHelper.StringToByte(rblIsTop.SelectedValue);

                //------------------------------------------
                //判断是否是新增
                if (model.Id == 0)
                {
                    //添加时间与用户
                    model.AddDate = DateTime.Now;
                    //修改时间与用户
                    model.UpdateDate = DateTime.Now;
                }
                else
                {
                    //修改时间与用户
                    model.UpdateDate = DateTime.Now;
                }
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();

                #endregion

                //------------------------------------------

                #region 上传图片

                if (this.filePhoto.HasFile && this.filePhoto.FileName.Length > 3)
                {
                    int vid = 3; //3	文章封面
                    //---------------------------------------------------
                    var upload = new UploadFile();
                    result = new UploadFileBll().Upload_AspNet(this.filePhoto.PostedFile, vid, RndKey,
                        OnlineUsersBll.GetInstence().GetManagerId(), OnlineUsersBll.GetInstence().GetManagerCName(),
                        upload);
                    this.filePhoto.Dispose();
                    //---------------------------------------------------
                    if (result.Length == 0) //上传成功
                    {
                        model.FrontCoverImg = upload.Path;
                    }
                    else
                    {
                        CommonBll.WriteLog("上传出错：" + result); //收集异常信息
                        return "上传出错！" + result;
                    }
                }
                //如果是修改，检查用户是否重新上传过封面图片，如果是删除旧的图片
                if (model.Id > 0)
                {
                    UploadFileBll.GetInstence()
                        .Upload_DiffFile(InformationTable.Id, InformationTable.FrontCoverImg, InformationTable.TableName,
                            model.Id, model.FrontCoverImg);

                    //同步UploadFile上传表
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, InformationTable.TableName, model.Id);
                }

                #endregion

                //----------------------------------------------------------
                //存储到数据库
                InformationBll.GetInstence().Save(this, model);

                #region 同步更新上传图片表绑定Id
                if (id == 0)
                {
                    //同步UploadFile上传表记录，绑定刚刚上传成功的文件Id为当前记录Id
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, InformationTable.TableName, model.Id);
                }
                #endregion
                
            }
            catch (Exception e)
            {
                result = "保存失败！";

                //出现异常，保存出错日志信息
                CommonBll.WriteLog(result, e);
            }

            return result;
        }
        #endregion
    }
}