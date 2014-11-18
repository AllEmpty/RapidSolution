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
 *   创建日期：2014-06-21
 *   文件名称：PagePowerSignPublicEdit.aspx.cs
 *   描    述：公用页面权限标识编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class PagePowerSignPublicEdit : PageBase
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
                var model = PagePowerSignPublicBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //控件名称
                txtCName.Text = model.CName;
                //英文名称
                txtEName.Text = model.EName;
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

                if (string.IsNullOrEmpty(txtCName.Text.Trim()))
                {
                    return txtCName.Label + "不能为空！";
                }
                var sName = StringHelper.Left(txtCName.Text, 20);
                if (PagePowerSignPublicBll.GetInstence().Exist(x => x.CName == sName && x.Id != id))
                {
                    return txtCName.Label + "已存在！请重新输入！";
                }
                if (string.IsNullOrEmpty(txtEName.Text.Trim()))
                {
                    return txtEName.Label + "不能为空！";
                }
                var sEname = StringHelper.Left(txtEName.Text, 50);
                if (PagePowerSignPublicBll.GetInstence().Exist(x => x.EName == sEname && x.Id != id))
                {
                    return txtEName.Label + "已存在！请重新输入！";
                }

                #endregion

                #region 赋值
                //定义是否更新标识——即当前记录的名称是否改变了
                bool isUpdate = false;

                //获取实体
                var model = new PagePowerSignPublic(x => x.Id == id);

                //判断是否有改变名称
                if (id > 0 && (sName != model.CName || sEname != model.EName))
                {
                    isUpdate = true;
                }

                //设置名称
                model.CName = sName;
                //设置英文名称
                model.EName = sEname;
                #endregion

                //----------------------------------------------------------
                //存储到数据库
                PagePowerSignPublicBll.GetInstence().Save(this, model);

                //判断是否需要同步更新关联表字段
                if (isUpdate)
                {
                    //调用更新函数，同步更新对应的所有记录
                    PagePowerSignBll.GetInstence().UpdateValue_For_PagePowerSignPublic_Id(this, model.Id, PagePowerSignTable.CName, model.CName, PagePowerSignTable.EName, model.EName);
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