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
 *   创建日期：2014-07-10
 *   文件名称：AdvertisementEdit.aspx.cs
 *   描    述：广告编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Advertisements
{
    public partial class AdvertisementEdit : PageBase
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
                //广告位置下拉框
                AdvertisingPositionBll.GetInstence().BandDropDownListShowAll(this, ddlAdvertisingPosition);

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
                var model = AdvertisementBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //对页面窗体进行赋值
                txtName.Text = model.Name;
                //Key是不能修改的，同一个位置的Key值一样
                txtKeyword.Text = model.Keyword;
                txtKeyword.Readonly = true;
                txtUrl.Text = model.Url;
                txtContent.Text = model.Content;
                //开始时间与结束时间
                dpStartTime.SelectedDate = model.StartTime;
                dpEndTime.SelectedDate = model.EndTime;

                ddlAdvertisingPosition.SelectedValue = model.AdvertisingPosition_Id + "";
                rblIsDisplay.SelectedValue = model.IsDisplay + "";
                txtSort.Text = model.Sort + "";

                if (!String.IsNullOrEmpty(model.AdImg))
                {
                    p_Img = model.AdImg;
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
        protected void ddlAdvertisingPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = ConvertHelper.Cint0(ddlAdvertisingPosition.SelectedValue);
            if (id == 0) return;

            var model = AdvertisingPositionBll.GetInstence().GetModelForCache(id);
            if (model != null)
            {
                //修改Key
                txtKeyword.Text = model.Keyword;
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
                AdvertisementBll.GetInstence().DelAdImg(this, id);

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
                //判断是否重复
                var sName = StringHelper.Left(txtName.Text, 50);
                if (AdvertisementBll.GetInstence().Exist(x => x.Name == sName && x.Id != id))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }
                if (ddlAdvertisingPosition.SelectedValue == "0")
                {
                    return ddlAdvertisingPosition.Label + "为必选项，请选择！";
                }

                if (dpStartTime.SelectedDate == null || TimeHelper.IsDateTime(dpStartTime.SelectedDate) == false)
                {
                    return "请选择" + dpStartTime.Label;
                }
                if (dpEndTime.SelectedDate == null || TimeHelper.IsDateTime(dpEndTime.SelectedDate) == false)
                {
                    return "请选择" + dpEndTime.Label;
                }
                if (dpStartTime.SelectedDate > dpEndTime.SelectedDate)
                {
                    return dpStartTime.Label + "不能大于" + dpEndTime.Label;
                }

                #endregion

                #region 赋值

                //获取实体
                var model = new Advertisement(x => x.Id == id);

                //------------------------------------------
                //设置名称
                model.Name = sName;
                model.Keyword = StringHelper.Left(txtKeyword.Text, 50);
                model.Url = StringHelper.Left(txtUrl.Text, 200, true, false);
                //说明
                model.Content = StringHelper.Left(txtContent.Text, 100);
                //取得位置
                model.AdvertisingPosition_Id = ConvertHelper.Cint0(ddlAdvertisingPosition.SelectedValue);
                model.AdvertisingPosition_Name = StringHelper.Left(ddlAdvertisingPosition.SelectedText, 50);

                //开始时间与结束时间
                model.StartTime = dpStartTime.SelectedDate ?? DateTime.Now;
                model.EndTime = dpEndTime.SelectedDate ?? DateTime.Now.AddDays(1);

                //设定当前项是否显示
                model.IsDisplay = ConvertHelper.StringToByte(rblIsDisplay.SelectedValue);

                model.Sort = ConvertHelper.Cint0(txtSort.Text); ;
                
                //修改时间与用户
                model.UpdateDate = DateTime.Now;
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();

                #endregion

                //------------------------------------------

                #region 上传图片
                //判断前端的ASP.NET上传控件是否选择有上传文件
                if (this.filePhoto.HasFile && this.filePhoto.FileName.Length > 3)
                {
                    //将当前页面上传文件绑定上传配置表Id为7的记录，给上传组件的逻辑层函数调用
                    int vid = 7; //7	广告
                    //---------------------------------------------------
                    //创建上传实体
                    var upload = new UploadFile();
                    //调用ASP.NET上传控件上传函数，并获取上传成功或失败信息
                    result = new UploadFileBll().Upload_AspNet(this.filePhoto.PostedFile, vid, RndKey,
                        OnlineUsersBll.GetInstence().GetManagerId(), OnlineUsersBll.GetInstence().GetManagerCName(),
                        upload);
                    this.filePhoto.Dispose();
                    //---------------------------------------------------
                    //没有返回信息时表示上传成功
                    if (result.Length == 0) 
                    {
                        //将上传到服务器后的路径赋给广告实体对应字段
                        model.AdImg = upload.Path;
                    }
                    else
                    {
                        //将出错写入日志中
                        CommonBll.WriteLog("上传出错：" + result); //收集异常信息
                        //弹出出错提示
                        return "上传出错！" + result;
                    }
                }
                //如果是修改，检查用户是否重新上传过广告图片，如果是删除旧的图片
                if (model.Id > 0)
                {
                    //删除旧图片
                    UploadFileBll.GetInstence()
                        .Upload_DiffFile(AdvertisementTable.Id, AdvertisementTable.AdImg, AdvertisementTable.TableName,
                            model.Id, model.AdImg);

                    //同步UploadFile上传表记录，绑定刚刚上传成功的文件Id为当前记录Id
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, AdvertisementTable.TableName, model.Id);
                }

                #endregion


                //----------------------------------------------------------
                //存储到数据库
                AdvertisementBll.GetInstence().Save(this, model);

                #region 同步更新上传图片表绑定Id
                if (id == 0)
                {
                    //同步UploadFile上传表记录，绑定刚刚上传成功的文件Id为当前记录Id
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, AdvertisementTable.TableName, model.Id);
                }

                #endregion

                //这里放置清空前端页面缓存的代码（如果前端使用了页面缓存的话，必须进行清除操作）


            }
            catch (Exception e)
            {
                result = "保存失败！";

                //出现异常，保存出错日志广告
                CommonBll.WriteLog(result, e);
            }

            return result;
        }
        #endregion
    }
}