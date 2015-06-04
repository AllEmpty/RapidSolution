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
 *   文件名称：BranchEdit.aspx.cs
 *   描    述：部门编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class BranchEdit : PageBase
    {

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //获取ID值
                hidId.Text = RequestHelper.GetInt0("Id") + "";

                //绑定下拉列表
                BranchBll.GetInstence().BandDropDownListShowAll(this, ddlParentId);

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
                //获取指定ID的部门内容
                var model = BranchBll.GetInstence().GetModelForCache(x => x.Id == id);
                if (model == null)
                    return;

                //对页面窗体进行赋值
                txtName.Text = model.Name;
                //设置下拉列表选择项
                ddlParentId.SelectedValue = model.ParentId + "";
                //编辑时不给改变上级部门
                ddlParentId.Enabled = false;
                //设置部门编码
                txtCode.Text = model.Code;
                //设置父ID
                txtParent.Text = model.ParentId + "";
                //设置排序
                txtSort.Text = model.Sort + "";
                //设置注备
                txtNotes.Text = model.Notes;
            }
        }

        #endregion

        #region 页面控件绑定
        /// <summary>下拉列表改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlParentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            //获取当前节点的父节点Id
            txtParent.Text = ddlParentId.SelectedValue;
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
                if (BranchBll.GetInstence().Exist(x => x.Name == sName && x.Id != id))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }

                #endregion

                #region 赋值
                //定义是否更新其他关联表变量
                bool isUpdate = false;

                //获取实体
                var model = new Branch(x => x.Id == id);

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
                model.ParentId = ConvertHelper.Cint0(txtParent.Text);
                //设置备注
                model.Notes = StringHelper.Left(txtNotes.Text, 100);

                //由于限制了编辑时不能修改父节点，所以这里只对新建记录时处理
                if (id == 0)
                {
                    //设定当前的深度与设定当前的层数级
                    if (model.ParentId == 0)
                    {
                        //设定当前的层数
                        model.Depth = 0;
                    }
                    else
                    {
                        //设定当前的层数级
                        model.Depth = ConvertHelper.Cint0(BranchBll.GetInstence().GetFieldValue(ConvertHelper.Cint0(ddlParentId.SelectedValue), BranchTable.Depth)) + 1;
                    }
                }

                //设置排序
                if (txtSort.Text == "0")
                {
                    model.Sort = BranchBll.GetInstence().GetSortMax(model.ParentId) + 1;
                }
                else
                {
                    model.Sort = ConvertHelper.Cint0(txtSort.Text);
                }

                //新创建部门时，生成对应的部门编码
                if (id == 0)
                {
                    model.Code = SPs.P_Branch_GetMaxBranchCode(model.Depth + 1, model.ParentId).ExecuteScalar().ToString();
                }

                #endregion

                //----------------------------------------------------------
                //存储到数据库
                BranchBll.GetInstence().Save(this, model);

                //如果本次修改改变了相关名称，则同步更新其他关联表的对应名称
                if (isUpdate)
                {
                    PositionBll.GetInstence().UpdateValue_For_Branch_Id(this, model.Id, PositionTable.Branch_Name, model.Name);
                    ManagerBll.GetInstence().UpdateValue_For_Branch_Id(this, model.Id, ManagerTable.Branch_Name, model.Name);
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