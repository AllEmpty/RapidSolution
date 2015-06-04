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
 *   文件名称：UploadTypeEdit.aspx.cs
 *   描    述：上传类型编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Set
{
    public partial class UploadTypeEdit : PageBase
    {

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取ID值
                hidId.Text = RequestHelper.GetInt0("Id") + "";

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
                var model = UploadTypeBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //名称
                txtName.Text = model.Name;
                //关键字
                txtTypeKey.Text = model.TypeKey;
                //编辑时，关键字不能修改
                txtTypeKey.Enabled = false;
                //绑定扩展名
                txtExt.Text = model.Ext;
                //是否系统默认
                //rblIsSys.SelectedValue = model.IsSys + "";
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
                var sName = StringHelper.Left(txtName.Text, 50);
                if (UploadTypeBll.GetInstence().Exist(x => x.Name == sName && x.Id != id))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }
                if (string.IsNullOrEmpty(txtTypeKey.Text.Trim()))
                {
                    return txtTypeKey.Label + "不能为空！";
                }
                if (string.IsNullOrEmpty(txtExt.Text.Trim()))
                {
                    return txtExt.Label + "不能为空！";
                }

                #endregion

                #region 赋值
                //获取实体
                var model = new UploadType(x => x.Id == id);

                //系统默认
                //model.IsSys = ConvertHelper.StringToByte(rblIsSys.SelectedValue);

                //判断是否有改变关键字
                var sTypeKey = StringHelper.Left(txtTypeKey.Text, 20);
                if (id > 0 && model.IsSys == 1 && sTypeKey != model.TypeKey)
                {
                    return "当前记录为系统默认，不能修改关键字！";
                }

                //设置名称
                model.Name = sName;
                //设置关键字
                model.TypeKey = sTypeKey;
                //扩展名
                model.Ext = StringHelper.Left(txtExt.Text, 0);
                
                //修改时间与管理员
                model.UpdateDate = DateTime.Now;
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();
                
                #endregion

                //----------------------------------------------------------
                //存储到数据库
                UploadTypeBll.GetInstence().Save(this, model);

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