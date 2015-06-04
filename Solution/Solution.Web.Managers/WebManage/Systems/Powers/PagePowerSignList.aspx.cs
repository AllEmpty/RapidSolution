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
 *   创建日期：2014-06-21
 *   文件名称：PagePowerSignList.aspx.cs
 *   描    述：页面控件权限管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class PagePowerSignList : PageBase
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

        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public override void LoadData()
        {
            if (MenuTree.Nodes.Count == 0)
            {
                //创建树节点
                var tnode = new FineUI.TreeNode();
                //设置节点名称
                tnode.Text = "菜单";
                //设置节点ID
                tnode.NodeID = "0";
                //设置当前节点是否为最终节点
                tnode.Leaf = false;
                //是否自动扩大
                tnode.Expanded = true;

                //根据指定的父ID去查询相关的子集ID
                var dt = MenuInfoBll.GetInstence().GetDataTable();
                //从一级菜单开始添加
                AddNode(dt, tnode, "0");

                MenuTree.Nodes.Add(tnode);
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
            var wheres = new List<ConditionHelper.SqlqueryCondition>();
            wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, PagePowerSignTable.MenuInfo_Id, Comparison.Equals, index));

            //设置排序
            var _order = new List<string>();
            _order.Add(PagePowerSignTable.Id);

            //获取DataTable
            var dt = PagePowerSignBll.GetInstence().GetDataTable(false, 0, null, 0, 0, wheres, _order);

            if (dt == null || dt.Rows.Count == 0)
            {
                Grid2.DataSource = null;
                Grid2.DataBind();
                PagePowerSignPublicBll.GetInstence().BindGrid(Grid1, 0, 0, null, _order);
            }
            else
            {
                //绑定到表格——已绑定控件列表
                //PagePowerSignBll.GetInstence().BindGrid(Grid2, 0, 0, list, _order);
                Grid2.DataSource = dt;
                Grid2.DataBind();

                var id = DataTableHelper.GetArrayInt(dt, PagePowerSignTable.PagePowerSignPublic_Id);

                wheres = new List<ConditionHelper.SqlqueryCondition>();
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, PagePowerSignPublicTable.Id, Comparison.NotIn, id));

                PagePowerSignPublicBll.GetInstence().BindGrid(Grid1, 0, 0, wheres, _order);

            }
        }

        #region 添加子节点
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="node"></param>
        /// <param name="nodeid"></param>
        private void AddNode(DataTable dt, FineUI.TreeNode node, string nodeid)
        {
            //筛选出当前节点下面的子节点
            var Childdt = DataTableHelper.GetFilterData(dt, MenuInfoTable.ParentId, nodeid, MenuInfoTable.Sort, "Asc");
            //判断是否有节点存在
            if (Childdt.Rows.Count > 0)
            {
                foreach (DataRow item in Childdt.Rows)
                {
                    bool ispage = int.Parse(item[MenuInfoTable.IsMenu].ToString()) == 0 ? false : true;
                    var tnode = new FineUI.TreeNode();
                    //设置节点名称
                    tnode.Text = item[MenuInfoTable.Name].ToString();
                    //设置节点ID
                    tnode.NodeID = item[MenuInfoTable.Id].ToString();

                    //判断当前节点是否为最终节点
                    if (ispage)
                    {
                        tnode.Leaf = true;
                        tnode.EnableClickEvent = true;
                    }
                    else
                    {
                        tnode.EnableClickEvent = false;
                        tnode.Enabled = false;
                    }
                    //是否自动扩大
                    tnode.Expanded = true;

                    //if (!TreeMenu.Nodes.Contains(tnode))
                    node.Nodes.Add(tnode);

                    //递归添加子节点
                    AddNode(dt, tnode, item[MenuInfoTable.Id].ToString());

                }
            }

        }
        #endregion

        #endregion

        #region 列表属性绑定
        /// <summary>
        /// 树列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MenuTree_NodeCommand(object sender, FineUI.TreeCommandEventArgs e)
        {
            hidId.Text = e.Node.NodeID;
            BindGrid();
        }
        #endregion

        #region 按键事件

        /// <summary>
        /// 页面绑定控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEmpower_Click(object sender, EventArgs e)
        {
            //获取当前选择的菜单项
            var index = ConvertHelper.Cint0(hidId.Text);
            //获取当前用户选择的全部记录Id
            var id = GridViewHelper.GetSelectedKeyIntArray(Grid1);
            //如果没有选择项，则直接退出
            if (index == 0 || id == null || id.Length == 0)
                return;

            //添加到绑定表中
            foreach (var i in id)
            {
                //检查当前控件是否已添加
                //添加前判断一下本权限标签是否已添加过了，没有则进行添加
                if (!PagePowerSignBll.GetInstence().Exist(x => x.MenuInfo_Id == index && x.PagePowerSignPublic_Id == i))
                {
                    var ppsp = PagePowerSignPublicBll.GetInstence().GetModelForCache(i);
                    if (ppsp == null)
                    {
                        continue;
                    }

                    var model = new PagePowerSign();
                    model.MenuInfo_Id = index;
                    model.PagePowerSignPublic_Id = i;
                    model.CName = ppsp.CName;
                    model.EName = ppsp.EName;

                    PagePowerSignBll.GetInstence().Save(this, model);
                }
            }

            BindGrid();
        }

        /// <summary>
        /// 取消绑定控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            //获取当前选择的菜单项
            var index = ConvertHelper.Cint0(hidId.Text);
            //获取当前用户选择的全部记录Id
            var id = GridViewHelper.GetSelectedKeyIntArray(Grid2);
            //如果没有选择项，则直接退出
            if (index == 0 || id == null || id.Length == 0)
                return;

            //删除已绑定控件
            PagePowerSignBll.GetInstence().Delete(this, id);

            BindGrid();
        }

        /// <summary>
        /// 清空绑定控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEmpty_Click(object sender, EventArgs e)
        {
            //获取当前选择的菜单项
            var index = ConvertHelper.Cint0(hidId.Text);
            //如果没有选择项，则直接退出
            if (index == 0)
                return;

            //删除已绑定控件
            PagePowerSignBll.GetInstence().Delete(this, x => x.MenuInfo_Id == index);

            BindGrid();
        }
        #endregion
    }
}