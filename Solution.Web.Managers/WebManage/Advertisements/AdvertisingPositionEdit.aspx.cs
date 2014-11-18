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
 *   创建日期：2014-07-08
 *   文件名称：AdvertisingPositionEdit.aspx.cs
 *   描    述：广告位置编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Advertisements
{
    public partial class AdvertisingPositionEdit : PageBase
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
                AdvertisingPositionBll.GetInstence().BandDropDownList(this, ddlParentId);

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
                //获取指定ID的广告位置内容
                var model = AdvertisingPositionBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;
                
                //地址名称
                txtName.Text = model.Name;
                //给下拉列表赋值
                ddlParentId.SelectedValue = model.ParentId + "";
                //编辑时不能修改父节点
                ddlParentId.Enabled = false;
                //设置父ID
                txtParent.Text = model.ParentId + "";
                //设置排序
                txtSort.Text = model.Sort + "";
                //KEY
                txtKey.Text = model.Keyword;
                //给页面图片赋值
                if (model.MapImg != null && model.MapImg.Length > 5)
                {
                    imgMap.ImageUrl = DirFileHelper.GetFilePathPostfix(model.MapImg, "s");
                }
                else
                {
                    //不存在图片，则隐藏图片控件和图片删除按钮
                    imgMap.Visible = false;
                    ButtonDelMapImg.Visible = false;
                }
                //给页面图片赋值
                if (model.PicImg != null && model.PicImg.Length > 5)
                {
                    imgPic.ImageUrl = DirFileHelper.GetFilePathPostfix(model.PicImg, "s");
                }
                else
                {
                    //不存在图片，则隐藏图片控件和图片删除按钮
                    imgPic.Visible = false;
                    ButtonDelPicImg.Visible = false;
                }
                //是否显示（状态）
                rblIsDisplay.SelectedValue = model.IsDisplay + "";

            }
            else
            {
                //新建广告位置时，隐藏图片控件和图片删除按钮
                imgMap.Visible = false;
                imgPic.Visible = false;
                ButtonDelMapImg.Visible = false;
                ButtonDelPicImg.Visible = false;
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
        /// <summary>
        /// 删除位置图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelMapImg_Click(object sender, EventArgs e)
        {
            AdvertisingPositionBll.GetInstence().DelMapImg(this, ConvertHelper.Cint0(hidId.Text));
            //删除后刷新编辑窗口
            Response.Redirect(Request.Url.ToString());
        }

        /// <summary>
        /// 删除默认图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelPicImg_Click(object sender, EventArgs e)
        {
            AdvertisingPositionBll.GetInstence().DelPicImg(this, ConvertHelper.Cint0(hidId.Text));
            //删除后刷新编辑窗口
            Response.Redirect(Request.Url.ToString());
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
                var sName = StringHelper.Left(txtName.Text, 50);
                if (AdvertisingPositionBll.GetInstence().Exist(x => x.Name == sName && x.Id != id))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }
                if (string.IsNullOrEmpty(txtKey.Text.Trim()))
                {
                    return txtKey.Label + "不能为空！";
                }
                var sKeyword = StringHelper.Left(txtKey.Text, 50);
                if (AdvertisingPositionBll.GetInstence().Exist(x => x.Keyword == sKeyword && x.Id != id))
                {
                    return txtKey.Label + "已存在！请重新输入！";
                }

                #endregion

                #region 赋值
                //定义是否更新其他关联表变量
                bool isUpdate = false;

                //读取当前地址信息
                var model = new AdvertisingPosition(x => x.Id == id);

                //判断是否更新关联表
                if (model.Id > 0 && sName != model.Name)
                    isUpdate = true;

                //设置名称
                model.Name = StringHelper.Left(txtName.Text, 50);
                //KEY
                model.Keyword = StringHelper.Left(txtKey.Text, 50);
                //对应的父类id
                model.ParentId = ConvertHelper.Cint0(txtParent.Text);
                //由于限制了编辑时不能修改父节点，所以这里只对新建记录时处理
                if (id == 0)
                {
                    //设定当前的深度与设定当前的层数级
                    if (model.ParentId == 0)
                    {
                        //设定当前的层数级
                        model.Depth = 0;
                    }
                    else
                    {
                        //设定当前的层数
                        model.Depth = ConvertHelper.Cint0(AdvertisingPositionBll.GetInstence().GetFieldValue(ConvertHelper.Cint0(ddlParentId.SelectedValue), AdvertisingPositionTable.Depth)) + 1;
                    }

                    //限制分类层数只能为2层
                    if (model.Depth > 2)
                    {
                        return "广告位置只能创建2层分类！";
                    }
                }
                //设置排序
                if (txtSort.Text == "0")
                {
                    model.Sort = AdvertisingPositionBll.GetInstence().GetSortMax(model.ParentId) + 1;
                }
                else
                {
                    model.Sort = ConvertHelper.Cint0(txtSort.Text);
                }
                //设定当前项是否显示
                model.IsDisplay = ConvertHelper.StringToByte(rblIsDisplay.SelectedValue);

                //广告宽与高
                model.Width = ConvertHelper.Cint0(txtWidth.Text);
                model.Height = ConvertHelper.Cint0(txtHeight.Text);

                //添加最后修改人员
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();
                model.AddDate = DateTime.Now;

                #endregion
                
                #region 上传图片
                //上传广告位置图
                if (this.MapImg.HasFile && this.MapImg.FileName.Length > 3)
                {
                    int vid = 5; //5	广告位置图
                    //---------------------------------------------------
                    var upload = new UploadFile();
                    result = new UploadFileBll().Upload_AspNet(this.MapImg.PostedFile, vid, RndKey, OnlineUsersBll.GetInstence().GetManagerId(), OnlineUsersBll.GetInstence().GetManagerCName(), upload);
                    this.MapImg.Dispose();
                    //---------------------------------------------------
                    if (result.Length == 0)//上传成功
                    {
                        model.MapImg = upload.Path;
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
                    UploadFileBll.GetInstence().Upload_DiffFile(AdvertisingPositionTable.Id, AdvertisingPositionTable.MapImg, AdvertisingPositionTable.TableName, model.Id, model.MapImg);

                    //同步UploadFile上传表
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, AdvertisingPositionTable.TableName, model.Id);
                }


                //——————————————————————————————————————————————————————————————————————
                //上传广告默认图
                if (this.PicImg.HasFile && this.PicImg.FileName.Length > 3)
                {
                    int vid = 6; //6	广告默认图
                    //---------------------------------------------------
                    var upload = new UploadFile();
                    result = new UploadFileBll().Upload_AspNet(this.PicImg.PostedFile, vid, RndKey, OnlineUsersBll.GetInstence().GetManagerId(), OnlineUsersBll.GetInstence().GetManagerCName(), upload);
                    this.PicImg.Dispose();
                    //---------------------------------------------------
                    if (result.Length == 0)//上传成功
                    {
                        model.PicImg = upload.Path;
                    }
                    else
                    {
                        CommonBll.WriteLog("上传出错：" + result, null);//收集异常信息
                        return "上传出错！" + result;
                    }
                }
                //如果是修改，检查用户是否重新上传过默认图片，如果是删除旧的图片
                if (model.Id > 0)
                {
                    UploadFileBll.GetInstence().Upload_DiffFile(AdvertisingPositionTable.Id, AdvertisingPositionTable.PicImg, AdvertisingPositionTable.TableName, model.Id, model.PicImg);

                    //同步UploadFile上传表
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, AdvertisingPositionTable.TableName, model.Id);
                }

                #endregion
                
                //----------------------------------------------------------
                //存储到数据库
                AdvertisingPositionBll.GetInstence().Save(this, model);
                
                #region 同步更新上传图片表绑定Id
                if (id == 0)
                {
                    //同步UploadFile上传表记录，绑定刚刚上传成功的文件Id为当前记录Id
                    UploadFileBll.GetInstence().Upload_UpdateRs(RndKey, AdvertisingPositionTable.TableName, model.Id);
                }

                #endregion
                
                //如果本次修改改变了相关名称，则同步更新其他关联表的对应名称
                if (isUpdate)
                {
                    AdvertisementBll.GetInstence().UpdateValue_For_AdvertisingPosition_Id(this, model.Id, AdvertisementTable.AdvertisingPosition_Name, model.Name);
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