using System;
using System.Collections.Generic;
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
 *   创建日期：2014-06-22
 *   文件名称：PositionList.aspx.cs
 *   描    述：职位管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class PositionList : PageBase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {
            //逻辑对象赋值
            bll = PositionBll.GetInstence();
            //表格对象赋值
            grid = Grid1;
        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public override void LoadData()
        {
            if (BranchTree.Nodes.Count == 0)
            {
                //创建树节点
                var tnode = new FineUI.TreeNode();
                //设置节点名称
                tnode.Text = "部门";
                //设置节点ID
                tnode.NodeID = "0";
                //设置当前节点是否为最终节点
                tnode.Leaf = false;
                //是否自动扩大
                tnode.Expanded = true;

                //根据指定的父ID去查询相关的子集ID
                var dt = BranchBll.GetInstence().GetDataTable();
                //从一级菜单开始添加
                BranchBll.GetInstence().AddNode(dt, tnode, "0");

                BranchTree.Nodes.Add(tnode);
            }

            BindGrid();
        }

        private void BindGrid()
        {
            var index = ConvertHelper.Cint0(hidId.Text);
            if (index == 0)
            {
                return;
            }

            //设置查询条件
            var list = new List<ConditionHelper.SqlqueryCondition>();
            list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, PositionTable.Branch_Id, Comparison.Equals, index));
            //设置排序
            var order = new List<string>();
            order.Add(PagePowerSignTable.Id);
            //绑定Grid表格
            PositionBll.GetInstence().BindGrid(Grid1, Grid1.PageIndex + 1, Grid1.PageSize, list, order);
        }

        #endregion

        #region 列表属性绑定

        #region 树列表点击事件
        /// <summary>
        /// 树列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BranchTree_NodeCommand(object sender, FineUI.TreeCommandEventArgs e)
        {
            hidId.Text = e.Node.NodeID;
            BindGrid();
        }
        #endregion

        #region 列表按键绑定——修改列表控件属性
        /// <summary>
        /// 列表按键绑定——修改列表控件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            //绑定是否编辑列
            var lbfEdit = Grid1.FindColumn("ButtonEdit") as LinkButtonField;
            lbfEdit.Text = "编辑";
            lbfEdit.Enabled = MenuInfoBll.GetInstence().CheckControlPower(this, "ButtonEdit");
        }
        #endregion

        #region Grid点击事件
        /// <summary> 
        /// Grid点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            GridRow gr = Grid1.Rows[e.RowIndex];
            //获取当前点击列的主键ID
            object id = gr.DataKeys[0];

            switch (e.CommandName)
            {
                case "ButtonEdit":
                    //打开编辑窗口
                    Window1.IFrameUrl = "PositionEdit.aspx?Id=" + hidId.Text + "&PositionId=" + id + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(hidId.Text);
                    Window1.Hidden = false;

                    break;
            }
        }
        #endregion

        #endregion

        #region 添加新记录
        /// <summary>
        /// 添加新记录
        /// </summary>
        public override void Add()
        {
            if (!string.IsNullOrEmpty(hidId.Text.Trim()) && hidId.Text.Trim() != "0")
            {
                Window1.IFrameUrl = "PositionEdit.aspx?Id=" + hidId.Text + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(hidId.Text);
                Window1.Hidden = false;
            }
            else
            {
                Alert.ShowInParent("请选择左边列表项！");
            }
        }
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns></returns>
        public override string Delete()
        {
        //获取要删除的Id组
            var id = GridViewHelper.GetSelectedKeyIntArray(Grid1);

        //如果没有选择记录，则直接退出
        if (id == null)
        {
            return "请选择要删除的记录。";
        }

        try
        {
            //逐个判断是否可以删除
            foreach (var i in id)
            {
                //删除前检查
                if (ManagerBll.GetInstence().Exist(x => x.Position_Id.Contains("," + i + ",")))
                {
                    return "删除失败，Id为【" + i + "】的职位已有员工使用，不能直接删除！";
                }
            }

            //删除记录
            bll.Delete(this, id);

            return "删除编号Id为[" + string.Join(",", id) + "]的数据记录成功。";
        }
        catch (Exception e)
        {
            string result = "尝试删除编号ID为[" + string.Join(",", id) +"]的数据记录失败！";

            //出现异常，保存出错日志信息
            CommonBll.WriteLog(result, e);

            return result;
        }
        }
        #endregion
        
    }
}