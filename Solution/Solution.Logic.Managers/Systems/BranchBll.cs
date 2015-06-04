using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-21
 *   文件名称：BranchBll.cs
 *   描    述：部门管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// BranchBll逻辑类
    /// </summary>
    public partial class BranchBll : LogicBase
    {
        /***********************************************************************
		 * 自定义函数                                                          *
		 ***********************************************************************/

        #region 自定义函数

        #region 绑定部门下拉列表
        /// <summary>
        /// 绑定部门下拉列表——只显示一级部门
        /// </summary>
        public void BandDropDownList(Page page, FineUI.DropDownList ddl)
        {
            var dt = DataTableHelper.GetFilterData(GetDataTable(), BranchTable.ParentId, "0", BranchTable.Sort, "desc");

            //显示值
            ddl.DataTextField = BranchTable.Name;
            //绑定Id
            ddl.DataValueField = BranchTable.Id;

            //绑定数据源
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new FineUI.ListItem("请选择部门", "0"));
            ddl.SelectedValue = "0";
        }

        /// <summary>
        /// 绑定部门下拉列表——显示所有
        /// </summary>
        public void BandDropDownListShowAll(Page page, FineUI.DropDownList ddl, bool isBandBranchCode = false)
        {
            //设置排序
            var sortList = new List<string>();
            sortList.Add(BranchTable.Depth);
            sortList.Add(BranchTable.Sort);

            //筛选记录
            var dt = GetDataTable(false, 0, null, 0, 0, null, sortList);

            try
            {
                //整理出有层次感的数据
                dt = DataTableHelper.DataTableTidyUp(dt, BranchTable.Id, BranchTable.ParentId, 0);

                ddl.EnableSimulateTree = true;

                //显示值
                ddl.DataTextField = BranchTable.Name;
                //判断是否绑定部门编码
                if (isBandBranchCode)
                {
                    //绑定部门编码
                    ddl.DataValueField = BranchTable.Code;
                }
                else
                {
                    //绑定Id
                    ddl.DataValueField = BranchTable.Id;
                }
                //数据层次
                ddl.DataSimulateTreeLevelField = BranchTable.Depth;
                //绑定数据源
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.SelectedIndex = 0;

                ddl.Items.Insert(0, new FineUI.ListItem("请选择部门", "0"));
                ddl.SelectedValue = "0";
            }
            catch (Exception e)
            {
                // 记录日志
                CommonBll.WriteLog("", e);
            }
        }
        #endregion

        #region 树列表操作
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="node"></param>
        /// <param name="nodeid"></param>
        public int AddNode(DataTable dt, FineUI.TreeNode node, string nodeid)
        {
            //筛选出当前节点下面的子节点
            var childdt = DataTableHelper.GetFilterData(dt, BranchTable.ParentId, nodeid, BranchTable.Sort, "Asc");
            //判断是否有节点存在
            if (childdt.Rows.Count > 0)
            {
                foreach (DataRow item in childdt.Rows)
                {
                    //bool ispage = int.Parse(item[MenuInfo.Columns.MenuInfo_IsMenu].ToString()) == 0 ? false : true;
                    var tnode = new FineUI.TreeNode();
                    //设置节点名称
                    tnode.Text = item[BranchTable.Name].ToString();
                    //设置节点ID
                    tnode.NodeID = item[BranchTable.Id].ToString();

                    //是否自动扩大
                    tnode.Expanded = true;

                    //if (!TreeMenu.Nodes.Contains(tnode))
                    node.Nodes.Add(tnode);

                    //递归添加子节点
                    int count = AddNode(dt, tnode, item[BranchTable.Id].ToString());

                    //判断当前节点是否为最终节点
                    if (count == 0)
                    {
                        tnode.Leaf = true;
                    }
                    tnode.EnableClickEvent = true;
                }
            }
            return childdt.Rows.Count;
        }

        #endregion

        #endregion 自定义函数
    }
}
