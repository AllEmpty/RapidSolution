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
 *   创建日期：2014-06-25
 *   文件名称：UploadConfigEdit.aspx.cs
 *   描    述：上传类型编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Set
{
    public partial class UploadConfigEdit : PageBase
    {

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取ID值
                hidId.Text = RequestHelper.GetInt0("Id") + "";
                //绑定下拉列表
                UploadTypeBll.GetInstence().BandDropDownList(this, ddlUploadTypeId);

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
                var model = UploadConfigBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                txtName.Text = model.Name;
                txtJoinName.Text = model.JoinName;
                rblUserType.SelectedValue = model.UserType + "";
                ddlUploadTypeId.SelectedValue = model.UploadType_Id + "";
                txtPicSize.Text = model.PicSize + "";
                txtFileSize.Text = model.FileSize + "";
                txtSaveDir.Text = model.SaveDir;
                rblIsPost.SelectedValue = model.IsPost + "";
                rblIsEditor.SelectedValue = model.IsEditor + "";
                rblIsSwf.SelectedValue = model.IsSwf + "";
                brlIsChkSrcPost.SelectedValue = model.IsChkSrcPost + "";
                rblIsFixPic.SelectedValue = model.IsFixPic + "";
                ddlCutType.SelectedValue = model.CutType + "";
                txtPicWidth.Text = model.PicWidth + "";
                txtPicHeight.Text = model.PicHeight + "";
                txtPicQuality.Text = model.PicQuality + "";
                rblIsBigPic.SelectedValue = model.IsBigPic + "";
                txtBigWidth.Text = model.BigWidth + "";
                txtBigHeight.Text = model.BigHeight + "";
                txtBigQuality.Text = model.BigQuality + "";
                rblIsMidPic.SelectedValue = model.IsMidPic + "";
                txtMidWidth.Text = model.MidWidth + "";
                txtMidHeight.Text = model.MidHeight + "";
                txtMidQuality.Text = model.MidQuality + "";
                rblIsMinPic.SelectedValue = model.IsMinPic + "";
                txtMinWidth.Text = model.MinWidth + "";
                txtMinHeight.Text = model.MinHeight + "";
                txtMinQuality.Text = model.MinQuality + "";
                rblIsHotPic.SelectedValue = model.IsHotPic + "";
                txtHotWidth.Text = model.HotWidth + "";
                txtHotHeight.Text = model.HotHeight + "";
                txtHotQuality.Text = model.HotQuality + "";
                rblIsWaterPic.SelectedValue = model.IsWaterPic + "";
            }
        }

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
                if (UploadConfigBll.GetInstence().Exist(x => x.Name == sName && x.Id != id))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }
                if (string.IsNullOrEmpty(txtJoinName.Text.Trim()))
                {
                    return txtJoinName.Label + "不能为空！";
                }
                if (ddlUploadTypeId.SelectedValue == "0")
                {
                    return ddlUploadTypeId.Label + "为必选项，请选择后再保存！";
                }
                if (string.IsNullOrEmpty(txtPicSize.Text.Trim()))
                {
                    return txtPicSize.Label + "不能为空！";
                }
                if (string.IsNullOrEmpty(txtFileSize.Text.Trim()))
                {
                    return txtFileSize.Label + "不能为空！";
                }
                if (string.IsNullOrEmpty(txtSaveDir.Text.Trim()))
                {
                    return txtSaveDir.Label + "不能为空！";
                }
                #endregion

                #region 赋值
                //获取实体
                var model = new UploadConfig(x => x.Id == id);

                model.Name = sName;
                model.JoinName = StringHelper.Left(txtJoinName.Text, 30);
                model.UserType = (byte)ConvertHelper.Cint1(rblUserType.SelectedValue);
                //读取上传类型
                model.UploadType_Id = ConvertHelper.Cint0(ddlUploadTypeId.SelectedValue);
                var uploadTypeModel = UploadTypeBll.GetInstence().GetModelForCache(model.UploadType_Id);
                if (uploadTypeModel != null)
                {
                    model.UploadType_Name = uploadTypeModel.Name;
                    model.UploadType_TypeKey = uploadTypeModel.TypeKey;
                }

                //上传限制
                model.PicSize = ConvertHelper.Cint0(txtPicSize.Text);
                model.FileSize = ConvertHelper.Cint0(txtFileSize.Text);

                model.SaveDir = StringHelper.Left(txtSaveDir.Text, 50);
                model.IsPost = ConvertHelper.Ctinyint(rblIsPost.SelectedValue);
                model.IsEditor = ConvertHelper.Ctinyint(rblIsEditor.SelectedValue);
                model.IsSwf = ConvertHelper.Ctinyint(rblIsSwf.SelectedValue);
                model.IsChkSrcPost = ConvertHelper.Ctinyint(brlIsChkSrcPost.SelectedValue);

                //按比例生成
                model.IsFixPic = ConvertHelper.Ctinyint(rblIsFixPic.SelectedValue);
                model.CutType = ConvertHelper.Cint0(ddlCutType.SelectedValue);
                model.PicWidth = ConvertHelper.Cint0(txtPicWidth.Text);
                model.PicHeight = ConvertHelper.Cint0(txtPicHeight.Text);
                model.PicQuality = ConvertHelper.Cint0(txtPicQuality.Text);

                //大图
                model.IsBigPic = ConvertHelper.Ctinyint(rblIsBigPic.SelectedValue);
                model.BigWidth = ConvertHelper.Cint0(txtBigWidth.Text);
                model.BigHeight = ConvertHelper.Cint0(txtBigHeight.Text);
                model.BigQuality = ConvertHelper.Cint0(txtBigQuality.Text);

                //中图
                model.IsMidPic = ConvertHelper.Ctinyint(rblIsMidPic.SelectedValue);
                model.MidWidth = ConvertHelper.Cint0(txtMidWidth.Text);
                model.MidWidth = ConvertHelper.Cint0(txtMidWidth.Text);
                model.MidHeight = ConvertHelper.Cint0(txtMidHeight.Text);

                //小图
                model.IsMinPic = ConvertHelper.Ctinyint(rblIsMinPic.SelectedValue);
                model.MinWidth = ConvertHelper.Cint0(txtMinWidth.Text);
                model.MinWidth = ConvertHelper.Cint0(txtMinWidth.Text);
                model.MinHeight = ConvertHelper.Cint0(txtMinHeight.Text);

                //推荐图
                model.IsHotPic = ConvertHelper.Ctinyint(rblIsHotPic.SelectedValue);
                model.HotWidth = ConvertHelper.Cint0(txtHotWidth.Text);
                model.HotWidth = ConvertHelper.Cint0(txtHotWidth.Text);
                model.HotHeight = ConvertHelper.Cint0(txtHotHeight.Text);

                //加水印
                model.IsWaterPic = ConvertHelper.Ctinyint(rblIsWaterPic.SelectedValue);

                //修改时间与管理员
                model.UpdateDate = DateTime.Now;
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();
                
                #endregion

                //----------------------------------------------------------
                //存储到数据库
                UploadConfigBll.GetInstence().Save(this, model);

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