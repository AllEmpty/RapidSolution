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
 *   创建日期：2014-06-29
 *   文件名称：InformationClassEdit.aspx.cs
 *   描    述：信息分类编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Informations
{
    public partial class InformationClassEdit : PageBase
    {
        protected string RndKey = RandomHelper.GetRndKey();

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取ID值
                hidId.Text = RequestHelper.GetInt0("Id") + "";

                //绑定下拉列表
                InformationClassBll.GetInstence().BandDropDownListShowAll(this, ddlParentId);

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
                //获取指定ID的信息分类内容
                var model = InformationClassBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //对页面窗体进行赋值
                txtName.Text = model.Name;
                //设置下拉列表选择项
                ddlParentId.SelectedValue = model.ParentId + "";
                //编辑时不能修改父节点
                ddlParentId.Enabled = false;
                //设置父ID
                txtParent.Text = model.ParentId + "";
                //设置排序
                txtSort.Text = model.Sort + "";
                //设置是否显示
                rblIsShow.SelectedValue = model.IsShow + "";
                //设置是否单页
                rblIsPage.SelectedValue = model.IsPage + "";
                //SEO
                txtSeoTitle.Text = model.SeoTitle;
                txtSeoKey.Text = model.SeoKey;
                txtSeoDesc.Text = model.SeoDesc;
                //备注
                txtNotes.Text = model.Notes;

                //设置图片
                if (model.ClassImg != null && model.ClassImg.Length > 5)
                {
                    imgClassImg.ImageUrl = model.ClassImg;
                    ButtonDeleteImage.Enabled = MenuInfoBll.GetInstence().CheckControlPower(this, "ButtonDeleteImage");
                }
                else
                {
                    ButtonDeleteImage.Visible = false;
                    imgClassImg.Visible = false;
                }
            }
            else
            {
                ButtonDeleteImage.Visible = false;
                imgClassImg.Visible = false;
            }
        }

        #endregion

        #region 页面控件绑定

        #region 下拉列表改变事件
        /// <summary>
        /// 下拉列表改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取当前节点的父节点Id
            txtParent.Text = ddlParentId.SelectedValue;
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
                InformationClassBll.GetInstence().DelClassImg(this, id);

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

                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    return txtName.Label + "不能为空！";
                }
                var sName = StringHelper.Left(txtName.Text, 20);
                if (InformationClassBll.GetInstence().Exist(x => x.Name == sName && x.Id != id))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }

                #endregion

                #region 赋值
                //定义是否更新其他关联表变量
                bool isUpdate = false;
                var oldParentId = ConvertHelper.Cint0(txtParent.Text);

                //获取实体
                var model = new InformationClass(x => x.Id == id);
                //判断是否有改变名称
                if (id > 0 && sName != model.Name)
                {
                    isUpdate = true;
                }
                //修改时间与管理员
                model.UpdateDate = DateTime.Now;
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();

                //设置名称
                model.Name = sName;
                //对应的父类id
                model.ParentId = oldParentId;
                //设置备注
                model.Notes = StringHelper.Left(txtNotes.Text, 100);

                //由于限制了编辑时不能修改父节点，所以这里只对新建记录时处理
                if (id == 0)
                {
                    //设定当前的深度与设定当前的层数级
                    if (model.ParentId == 0)
                    {
                        //设定当前的层数级
                        model.Depth = 0;
                        //父Id为0时，根Id也为0
                        model.RootId = 0;
                    }
                    else
                    {
                        //设定当前的层数
                        model.Depth = ConvertHelper.Cint0(InformationClassBll.GetInstence().GetFieldValue(ConvertHelper.Cint0(ddlParentId.SelectedValue),InformationClassTable.Depth)) + 1;
                        //获取父类的父Id
                        model.RootId = ConvertHelper.Cint0(InformationClassBll.GetInstence().GetFieldValue(model.ParentId, InformationClassTable.ParentId));
                    }

                    //限制分类层数只能为3层
                    if (model.Depth > 3)
                    {
                        return "信息分类只能创建3层分类！";
                    }
                }

                //设置排序
                model.Sort = ConvertHelper.Cint0(txtSort.Text);
                if (model.Sort == 0)
                {
                    model.Sort = InformationClassBll.GetInstence().GetSortMax(model.ParentId) + 1;
                }

                //设定当前项是否显示
                model.IsShow = ConvertHelper.StringToByte(rblIsShow.SelectedValue);
                //设定当前项是否单页
                model.IsPage = ConvertHelper.StringToByte(rblIsPage.SelectedValue);

                //SEO
                model.SeoTitle = StringHelper.Left(txtSeoTitle.Text, 100);
                model.SeoKey = StringHelper.Left(txtSeoKey.Text, 100);
                model.SeoDesc = StringHelper.Left(txtSeoDesc.Text, 200);
                #endregion


                #region 上传图片
                //上传分类大图
                if (this.fuClassImg.HasFile && this.fuClassImg.FileName.Length > 3)
                {
                    int vid = 2; //2	信息(新闻)分类图
                    //---------------------------------------------------
                    var upload = new UploadFile();
                    result = new UploadFileBll().Upload_AspNet(this.fuClassImg.PostedFile, vid, RndKey, OnlineUsersBll.GetInstence().GetManagerId(), OnlineUsersBll.GetInstence().GetManagerCName(), upload);
                    this.fuClassImg.Dispose();
                    //---------------------------------------------------
                    if (result.Length == 0)//上传成功
                    {
                        model.ClassImg = upload.Path;
                    }
                    else
                    {
                        CommonBll.WriteLog("上传出错：" + result, null);//收集异常信息
                        return "上传出错！" + result;
                    }
                }
                //如果是修改，检查用户是否重新上传过新图片，如果是删除旧的图片
                if (model.Id > 0)
                {
                    UploadFileBll.GetInstence().Upload_DiffFile(InformationClassTable.Id, InformationClassTable.ClassImg, InformationClassTable.TableName, model.Id, model.ClassImg);

                    //同步UploadFile上传表
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, InformationClassTable.TableName, model.Id);
                }

                #endregion


                //----------------------------------------------------------
                //存储到数据库
                InformationClassBll.GetInstence().Save(this, model);

                #region 同步更新上传图片表绑定Id
                if (id == 0)
                {
                    //同步UploadFile上传表记录，绑定刚刚上传成功的文件Id为当前记录Id
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, InformationClassTable.TableName, model.Id);
                }

                #endregion
                
                //如果本次修改改变了相关名称，则同步更新其他关联表的对应名称
                if (isUpdate)
                {
                    InformationBll.GetInstence().UpdateValue_For_InformationClass_Id(this, model.Id, InformationTable.InformationClass_Name, model.Name);
                    InformationBll.GetInstence().UpdateValue_For_InformationClass_Root_Id(this, model.Id, InformationTable.InformationClass_Root_Name, model.Name);
                }
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