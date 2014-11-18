using System;
using System.Collections.Generic;
using System.Data;
using DotNet.Utilities;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;
/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-07-03
 *   文件名称：PositionSelect.aspx.cs
 *   描    述：页面控件权限管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class PositionSelect : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //检测用户是否超时退出
            OnlineUsersBll.GetInstence().IsTimeOut();
            //检测用户登录的有效性（是否被系统踢下线或管理员踢下线）
            if (OnlineUsersBll.GetInstence().IsOffline(this))
                return;
            //检查是否从正确路径进入
            MenuInfoBll.GetInstence().CheckPageEncrypt(this);

            if (!IsPostBack)
            {
                //获取ID值
                hidPositionId.Text = RequestHelper.GetQueryString("Id");
                //新增用户时生成的Id是随机码，这里处理一下
                if (hidPositionId.Text.IndexOf(",") == -1)
                {
                    hidPositionId.Text = "";
                }
                //绑定下接列表
                BranchBll.GetInstence().BandDropDownListShowAll(this, ddlBranch, true);

                LoadData();
            }
        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public void LoadData()
        {
            //设置排序
            var _order = new List<string>();
            _order.Add(PositionTable.Id);
            //设置查询条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>();
            //判断是否选择了部门，是的话只显示指定部门的职位
            if (ddlBranch.SelectedValue != "0")
            {
                var branchCode = StringHelper.FilterSql(ddlBranch.SelectedValue, true, true);
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, PositionTable.Branch_Code, Comparison.StartsWith, branchCode));
            }

            //读取职位Id
            var positionId = StringHelper.FilterSql(hidPositionId.Text, true, true);
            //如果不存在已选择的职位，则直接运行绑定表格
            if (string.IsNullOrEmpty(positionId))
            {
                Grid2.DataSource = null;
                Grid2.DataBind();

                PositionBll.GetInstence().BindGrid(Grid1, 0, 0, wheres, _order);
            }
            else
            {
                //去掉两边的逗号
                positionId = StringHelper.DelStrSign(positionId);
                //转换成数组
                var positionArr = StringHelper.GetArrayStr(positionId);

                //设置查询条件
                var grid1Where = new List<ConditionHelper.SqlqueryCondition>();
                //添加条件
                grid1Where.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, PositionTable.Id, Comparison.In, positionArr));


                //获取DataTable
                var dt = PositionBll.GetInstence().GetDataTable(false, 0, null, 0, 0, grid1Where, _order);

                if (dt == null || dt.Rows.Count == 0)
                {
                    Grid2.DataSource = null;
                    Grid2.DataBind();
                    PositionBll.GetInstence().BindGrid(Grid1, 0, 0, wheres, _order);
                }
                else
                {
                    //绑定到表格——已绑定控件列表
                    Grid2.DataSource = dt;
                    Grid2.DataBind();

                    wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, PositionTable.Id, Comparison.NotIn, positionArr));

                    PositionBll.GetInstence().BindGrid(Grid1, 0, 0, wheres, _order);

                }
            }
        }
        #endregion

        #region 页面控件绑定
        /// <summary>下拉列表改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion

        #region 按键事件

        #region 确认已选择职位
        /// <summary>
        /// 确认已选择职位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSelect_Click(object sender, EventArgs e)
        {
            //读取职位Id
            var positionId = StringHelper.FilterSql(hidPositionId.Text, true, true);

            PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(positionId) + ActiveWindow.GetHidePostBackReference());
        }
        #endregion

        #region 页面绑定控件
        /// <summary>
        /// 页面绑定控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEmpower_Click(object sender, EventArgs e)
        {
            //读取职位Id
            var positionId = StringHelper.FilterSql(hidPositionId.Text, true, true);
            //获取当前用户选择的全部记录Id
            var id = GridViewHelper.GetSelectedKeyArray(Grid1);
            //如果没有选择项，则直接退出
            if (id == null || id.Length == 0)
                return;

            //如果已选择的职位Id不存在，则选添加个逗号
            if (string.IsNullOrEmpty(positionId))
            {
                positionId = ",";
            }

            //添加到绑定表中
            foreach (var i in id)
            {
                //检查当前控件是否已添加
                //添加前判断一下本职位是否已添加过了，没有则进行添加
                if (positionId.IndexOf("," + i + ",") == -1)
                {
                    positionId += i + ",";
                }
            }
            //保存已选择的职位
            hidPositionId.Text = positionId;

            LoadData();
        }
        #endregion

        #region 取消绑定控件
        /// <summary>
        /// 取消绑定控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            //读取职位Id
            var positionId = StringHelper.FilterSql(hidPositionId.Text, true, true);
            //获取当前用户选择的全部记录Id
            var id = GridViewHelper.GetSelectedKeyArray(Grid2);
            //如果没有选择项，则直接退出
            if (id == null || id.Length == 0)
                return;

            //添加到绑定表中
            foreach (var i in id)
            {
                //检查当前控件是否已存在
                if (positionId.IndexOf("," + i + ",") > -1)
                {
                    //将指定Id直接删除
                    positionId = positionId.Replace("," + i + ",", ",");
                }
            }
            //如果删除了所有选择的职位，则去掉多余的逗号
            if (positionId == ",")
            {
                positionId = "";
            }

            //保存已选择的职位
            hidPositionId.Text = positionId;

            LoadData();
        }
        #endregion

        #region 清空绑定控件
        /// <summary>
        /// 清空绑定控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEmpty_Click(object sender, EventArgs e)
        {
            //清空已选择的职位
            hidPositionId.Text = "";

            LoadData();
        }
        #endregion

        #endregion
    }
}