using System;
using System.Data;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;


/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-22
 *   文件名称：PositionEdit.aspx.cs
 *   描    述：职位编辑页面
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class PositionEdit : PageBase
    {
        private string _pagePower = "";
        private string _controlPower = "";
        string _hidPositionPagePower;
        string _hidPositionControlPower;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            _hidPositionPagePower = ",";
            _hidPositionControlPower = ",";

            if (!IsPostBack)
            {
                //获取ID值
                hidBranchId.Text = RequestHelper.GetInt0("Id") + "";
                hidPositionId.Text = RequestHelper.GetInt0("PositionId") + "";
                
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
            int positionId = ConvertHelper.Cint0(hidPositionId.Text);

            if (positionId != 0)
            {
                //获取指定ID的职位内容
                var model = PositionBll.GetInstence().GetModelForCache(x => x.Id == positionId);
                if (model == null)
                    return;

                //对页面窗体进行赋值
                txtName.Text = model.Name;
                //设置下拉列表选择项
                labBranchName.Text = model.Branch_Name;
                //设置页面权限
                _pagePower = model.PagePower;
                //设置页面控件权限
                _controlPower = model.ControlPower;
            }
            else
            {
                //设置部门
                var branchModel = BranchBll.GetInstence().GetModelForCache(ConvertHelper.Cint0(hidBranchId.Text));
                labBranchName.Text = branchModel.Name;
            }
            
            //创建树节点
            var tnode = new FineUI.TreeNode();
            //设置节点名称
            tnode.Text = "菜单";
            //设置节点ID
            tnode.NodeID = "0";
            //设置当前节点是否为最终节点
            tnode.Leaf = false;
            //是否可以选择（打勾）
            tnode.EnableCheckBox = true;
            //是否已经选择
            tnode.Checked = true;
            //是否自动扩大
            tnode.Expanded = true;
            //开启点击节点全选或取消事件
            tnode.EnableCheckEvent = true;

            //根据指定的父ID去查询相关的子集ID
            var dt = MenuInfoBll.GetInstence().GetDataTable();
            //获取全部页面权限
            var pgdt = PagePowerSignBll.GetInstence().GetDataTable();

            //从一级菜单开始添加
            AddNode(dt, pgdt, tnode, "0");

            MenuTree.Nodes.Add(tnode);
        }

        #endregion
        
        #region 树列表操作

        /// <summary>
        /// 全选反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MenuTree_NodeCheck(object sender, FineUI.TreeCheckEventArgs e)
        {
            //全选当前节点以下了所有子节点
            if (e.Checked)
            {
                MenuTree.CheckAllNodes(e.Node.Nodes);
                CheckNode(e.Node);
            }
            //取消当前节点以下的所有子节点选择
            else
            {
                MenuTree.UncheckAllNodes(e.Node.Nodes);
            }
        }
        
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="node"></param>
        /// <param name="nodeid"></param>
        public void AddNode(DataTable dt, DataTable pgdt, FineUI.TreeNode node, string nodeid)
        {
            //筛选出当前节点下面的子节点
            var childdt = DataTableHelper.GetFilterData(dt, MenuInfoTable.ParentId, nodeid, MenuInfoTable.Sort, "Asc");
            //判断是否有节点存在
            if (childdt.Rows.Count > 0)
            {
                foreach (DataRow item in childdt.Rows)
                {
                    bool ispage = int.Parse(item[MenuInfoTable.IsMenu].ToString()) != 0;
                    var tnode = new FineUI.TreeNode();
                    //设置节点名称
                    tnode.Text = item[MenuInfoTable.Name].ToString();
                    //设置节点ID
                    tnode.NodeID = item[MenuInfoTable.Id].ToString();
                    //开启点击节点全选或取消事件
                    tnode.EnableCheckEvent = true;

                    //判断当前节点是否为最终节点
                    if (ispage)
                    {
                        //添加页面权限
                        //筛选出当前节点下面的页面权限节点
                        DataTable cdt = DataTableHelper.GetFilterData(pgdt, PagePowerSignTable.MenuInfo_Id, item[MenuInfoTable.Id].ToString(), null, null);
                        //判断当前节点下是否有设置页面权限
                        if (cdt == null || cdt.Rows.Count == 0)
                        {
                            tnode.Leaf = true;
                        }
                        else
                        {
                            //设置为非最终节点
                            tnode.Leaf = false;
                            //循环添加页面权限节点
                            for (int i = 0; i < cdt.Rows.Count; i++)
                            {
                                var tn = new FineUI.TreeNode();
                                //设置节点名称
                                tn.Text = cdt.Rows[i][PagePowerSignTable.CName].ToString();
                                //设置节点ID
                                tn.NodeID = item[MenuInfoTable.Id].ToString() + "|" + cdt.Rows[i][PagePowerSignTable.PagePowerSignPublic_Id].ToString();
                                tn.Leaf = true;
                                //是否可以选择（打勾）
                                tn.EnableCheckBox = true;
                                //是否已经选择
                                if (_controlPower.IndexOf("," + tn.NodeID + ",") >= 0)
                                {
                                    tn.Checked = true;
                                }
                                tnode.Nodes.Add(tn);
                            }
                        }
                    }
                    //是否可以选择（打勾）
                    tnode.EnableCheckBox = true;
                    //是否已经选择
                    if (_pagePower.IndexOf("," + tnode.NodeID + ",") >= 0)
                    {
                        tnode.Checked = true;
                    }
                    //是否自动扩大
                    tnode.Expanded = true;

                    //if (!MenuTree.Nodes.Contains(tnode))
                    node.Nodes.Add(tnode);

                    //递归添加子节点
                    AddNode(dt, pgdt, tnode, item[MenuInfoTable.Id].ToString());

                }
            }

        }

        /// <summary>
        /// 勾选当前节点
        /// </summary>
        /// <param name="node"></param>
        public void CheckNode(FineUI.TreeNode node)
        {
            FineUI.TreeNode pnode = node.ParentNode;

            while (pnode != null)
            {
                pnode.Checked = true;
                pnode = pnode.ParentNode;
            }
        }



        /// <summary>
        /// 获取Tree中选中的项
        /// </summary>
        /// <returns>字符串组成的Tree</returns>
        public void GetCheckTreeNode(FineUI.TreeNodeCollection node)
        {
            for (int i = 0; i < node.Count; i++)
            {
                if (node[i].Checked)
                {
                    if (node[i].NodeID != "0")
                    {
                        if (node[i].NodeID.IndexOf("|") < 0)
                        {
                            _hidPositionPagePower += node[i].NodeID + ",";
                        }
                        else
                        {
                            _hidPositionControlPower += node[i].NodeID + ",";
                        }

                    }

                    GetCheckTreeNode(node[i].Nodes);

                }
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
            var branchId = ConvertHelper.Cint0(hidBranchId.Text);
            var positionId = ConvertHelper.Cint0(hidPositionId.Text);
            try
            {
                #region 数据验证

                if (branchId == 0)
                {
                    return "非法路径进入！";
                }

                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    return txtName.Label + "不能为空！";
                }
                //同一个部门里职位不能样同，不同部门可以有同名称的职位
                var sName = StringHelper.Left(txtName.Text, 30);
                if (PositionBll.GetInstence().Exist(x => x.Branch_Id == branchId && x.Name == sName && x.Id != positionId))
                {
                    return txtName.Label + "已存在！请重新输入！";
                }
                #endregion

                #region 赋值
                //定义是否更新其他关联表变量
                bool isUpdate = false;

                //获取实体
                var model = new Position(x => x.Id == positionId);

                //判断是否有改变名称
                if (positionId > 0 && sName != model.Name)
                {
                    isUpdate = true;
                }

                //修改时间与管理员
                model.UpdateDate = DateTime.Now;
                model.Manager_Id = OnlineUsersBll.GetInstence().GetManagerId();
                model.Manager_CName = OnlineUsersBll.GetInstence().GetManagerCName();

                //设置名称
                model.Name = sName;

                //设置部门
                var branchModel = BranchBll.GetInstence().GetModelForCache(branchId);
                model.Branch_Id = branchId;
                model.Branch_Name = branchModel.Name;
                model.Branch_Code = branchModel.Code;

                //设置职位权限
                //从树列表中获取勾选的节点
                GetCheckTreeNode(MenuTree.Nodes);
                //赋予权限
                model.PagePower = StringHelper.FilterSql(_hidPositionPagePower);
                model.ControlPower = StringHelper.FilterSql(_hidPositionControlPower);

                #endregion

                //----------------------------------------------------------
                //存储到数据库
                PositionBll.GetInstence().Save(this, model);

                //如果本次修改改变了相关名称，则同步更新其他关联表的对应名称
                if (isUpdate)
                {
                    ManagerBll.GetInstence().UpdatePositionName(model.Id, model.Name);
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