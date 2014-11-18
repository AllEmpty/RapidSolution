using System;
using DotNet.Utilities;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-27
 *   文件名称：ManagerEdit.aspx.cs
 *   描    述：员工编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Employees
{
    public partial class ManagerEdit : PageBase
    {
        //生成一个随机的key值
        protected string RndKey = RandomHelper.GetRndKey();

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取ID值
                hidId.Text = RequestHelper.GetInt0("Id") + "";
                //绑定下拉列表
                //绑定部门
                BranchBll.GetInstence().BandDropDownList(this, ddlBranch_Id);

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

            if (id > 0)
            {
                //获取指定Id的管理员实体
                var model = ManagerBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //给页面控件赋值
                if (!string.IsNullOrEmpty(model.PhotoImg) && model.PhotoImg.Length > 4)
                    imgPhoto.ImageUrl = model.PhotoImg;

                txtCName.Text = model.CName;
                txtEName.Text = model.EName;

                //编辑时，登陆账号不能进行修改操作
                txtLoginName.Enabled = false;

                rblSex.SelectedValue = model.Sex;
                ddlBranch_Id.SelectedValue = model.Branch_Id + "";
                //职位
                hidPositionId.Text = model.Position_Id;
                txtPosition.Text = model.Position_Name;

                dpBirthday.Text = model.Birthday;
                rblIsEnable.SelectedValue = model.IsEnable + "";
                rblIsMultiUser.SelectedValue = model.IsMultiUser + "";

                txtNationalName.Text = model.NationalName;

                txtMobile.Text = model.Mobile;
                txtAddress.Text = model.Address;
                txtLoginName.Text = model.LoginName;
                txtNativePlace.Text = model.NativePlace;
                txtRecord.Text = model.Record;
                txtGraduateCollege.Text = model.GraduateCollege;
                txtGraduateSpecialty.Text = model.GraduateSpecialty;
                txtTel.Text = model.Tel;
                txtQq.Text = model.Qq;
                txtMsn.Text = model.Msn;
                txtEmail.Text = model.Email;
                txtContent.Text = model.Content;

                //绑定选择职位按键
                ButtonSelectPosition.OnClientClick = SelectWindows.GetSaveStateReference(hidPositionId.ClientID) + SelectWindows.GetShowReference("../Systems/Powers/PositionSelect.aspx?Id=" + hidPositionId.Text + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(hidPositionId.Text));
            }
            else
            {
                //绑定选择职位按键
                ButtonSelectPosition.OnClientClick = SelectWindows.GetSaveStateReference(hidPositionId.ClientID) + SelectWindows.GetShowReference("../Systems/Powers/PositionSelect.aspx?" + MenuInfoBll.GetInstence().PageUrlEncryptString());
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

                if (string.IsNullOrEmpty(txtLoginName.Text.Trim()))
                {
                    return txtLoginName.Label + "不能为空！";
                }
                var logName = StringHelper.Left(txtLoginName.Text, 20);
                if (Manager.Exists(x => x.LoginName == logName && x.Id != id))
                {
                    return txtLoginName.Label + "已存在！请重新输入！";
                }

                //新增用户时，密码不能为空
                if (id == 0 && string.IsNullOrEmpty(txtLoginPass.Text.Trim()))
                {
                    return "密码不能为空！";
                }
                //密码长度不能短于6位
                if (!string.IsNullOrEmpty(txtLoginPass.Text.Trim()) && txtLoginPass.Text.Trim().Length < 6)
                {
                    return "密码长度必须6位以上，请重新输入！";
                }
                if (!txtLoginPass.Text.Equals(txtLoginPassaAgin.Text))
                    return "两次输入的密码不一样，请重新输入！";

                if (string.IsNullOrEmpty(txtCName.Text.Trim()))
                {
                    return txtCName.Label + "不能为空！";
                }
                //所属部门
                if (ConvertHelper.Cint0(ddlBranch_Id.SelectedValue) < 1)
                {
                    return ddlBranch_Id.Label + "为必选项，请选择！";
                }
                //所属职位
                if (string.IsNullOrEmpty(hidPositionId.Text))
                {
                    return txtPosition.Label + "为必选项，请选择！";
                }
                #endregion

                #region 赋值
                //获取实体
                var model = new Manager(x => x.Id == id);
                model.LoginName = logName;

                //如果是添加管理员
                if (id == 0)
                {
                    model.CreateTime = DateTime.Now;
                    model.UpdateTime = DateTime.Now;
                    model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                    model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();
                    model.LoginPass = Encrypt.Md5(Encrypt.Md5(txtLoginPass.Text));
                    model.IsWork = 1;
                }
                else
                {
                    //修改时间与管理员
                    model.UpdateTime = DateTime.Now;
                    model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                    model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();

                    //修改用户时，填写了密码，则更新密码
                    if (txtLoginPass.Text.Trim().Length >= 6)
                    {
                        model.LoginPass = Encrypt.Md5(Encrypt.Md5(txtLoginPass.Text));
                    }

                }
                model.Branch_Id = ConvertHelper.Cint0(ddlBranch_Id.SelectedValue);
                var branch = BranchBll.GetInstence().GetModelForCache(x => x.Id == model.Branch_Id);
                if (branch != null)
                {
                    model.Branch_Code = branch.Code;
                    model.Branch_Name = branch.Name;
                }

                model.Position_Id = StringHelper.Left(hidPositionId.Text, 100);
                model.Position_Name = StringHelper.Left(txtPosition.Text, 500);

                model.CName = StringHelper.Left(txtCName.Text, 20);
                model.EName = StringHelper.Left(txtEName.Text, 50);
                model.Sex = StringHelper.Left(rblSex.SelectedValue, 4);
                model.Birthday = StringHelper.Left(dpBirthday.Text, 20);
                model.Record = StringHelper.Left(txtRecord.Text, 25);
                model.GraduateCollege = StringHelper.Left(txtGraduateCollege.Text, 30);
                model.GraduateSpecialty = StringHelper.Left(txtGraduateSpecialty.Text, 50);
                model.Tel = StringHelper.Left(txtTel.Text, 30);
                model.Mobile = StringHelper.Left(txtMobile.Text, 30);
                model.Email = StringHelper.Left(txtEmail.Text, 50);
                model.Qq = StringHelper.Left(txtQq.Text, 30);
                model.Msn = StringHelper.Left(txtMsn.Text, 30);
                model.Address = StringHelper.Left(txtAddress.Text, 100);
                model.IsEnable = ConvertHelper.Ctinyint(rblIsEnable.SelectedValue);
                model.IsMultiUser = ConvertHelper.Ctinyint(rblIsMultiUser.SelectedValue);
                model.Content = StringHelper.Left(txtContent.Text, 0);
                model.NationalName = StringHelper.Left(txtNationalName.Text, 50);
                model.NativePlace = StringHelper.Left(txtNativePlace.Text, 100);

                #region 上传图片
                if (this.fuSinger_AvatarPath.HasFile && this.fuSinger_AvatarPath.FileName.Length > 3)
                {
                    int vid = 1;   //1	管理员头像(头像图片)
                    //---------------------------------------------------
                    var upload = new UploadFile();
                    result = new UploadFileBll().Upload_AspNet(this.fuSinger_AvatarPath.PostedFile, vid, RndKey, OnlineUsersBll.GetInstence().GetManagerId(), OnlineUsersBll.GetInstence().GetManagerCName(), upload);
                    this.fuSinger_AvatarPath.Dispose();
                    //---------------------------------------------------
                    if (result.Length == 0)//上传成功
                    {
                        model.PhotoImg = upload.Path;
                    }
                    else
                    {
                        CommonBll.WriteLog("上传管理员头像图片未成功：" + result, null);//收集异常信息
                        return "上传管理员头像图片未成功！" + result;
                    }
                }
                //如果是修改，检查用户是否重新上传过封面图片，如果是删除旧的图片
                if (model.Id > 0)
                {
                    UploadFileBll.GetInstence().Upload_DiffFile(ManagerTable.Id, ManagerTable.PhotoImg, ManagerTable.TableName, model.Id, model.PhotoImg);

                    //同步UploadFile上传表
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, ManagerTable.TableName, model.Id);
                }
                #endregion



                #endregion

                //----------------------------------------------------------
                //存储到数据库
                ManagerBll.GetInstence().Save(this, model);

                #region 同步更新上传图片表绑定Id
                if (id == 0)
                {
                    //同步UploadFile上传表记录，绑定刚刚上传成功的文件Id为当前记录Id
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, ManagerTable.TableName, model.Id);
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

        #region 关闭子窗口事件
        /// <summary>
        /// 关闭子窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void SelectWindows_Close(object sender, WindowCloseEventArgs e)
        {
            //读取新选择的职位名称
            txtPosition.Text = PositionBll.GetInstence().GetName(hidPositionId.Text);

            //绑定选择职位按键
            ButtonSelectPosition.OnClientClick = SelectWindows.GetSaveStateReference(hidPositionId.ClientID) + SelectWindows.GetShowReference("../Systems/Powers/PositionSelect.aspx?Id=" + hidPositionId.Text + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(hidPositionId.Text));
        }
        #endregion
    }
}