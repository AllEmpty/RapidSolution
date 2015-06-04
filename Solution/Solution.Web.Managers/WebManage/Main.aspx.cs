using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using DotNet.Utilities;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;
using SubSonic.Query;

 /***********************************************************************
  *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
  *   博    客：http://www.cnblogs.com/EmptyFS/
  *   技 术 群：327360708
  *  
  *   创建日期：2014-06-17
  *   文件名称：Main.aspx.cs
  *   描    述：后端首页
  *             
  *   修 改 人：
  *   修改日期：
  *   修改原因：
  ***********************************************************************/
namespace Solution.Web.Managers
{
    public partial class Main : PageBase
    {
        //用户页面操作权限
        string _pagePower = "";

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //添加万年历按键事件，在主窗口中添加新选项卡
                btnCalendar.OnClientClick = mainTabStrip.GetAddTabReference("calendar_tab", "/WebManage/Help/wannianli.htm", "万年历", IconHelper.GetIconUrl(Icon.Calendar), true);

                //加载信息
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
            #region 展示用户信息

            //在线人数
            txtOnlineUserCount.Text = OnlineUsersBll.GetInstence().GetUserOnlineCount() + "";

            //当前用户信息
            var model = OnlineUsersBll.GetInstence().GetOnlineUsersModel();
            if (model == null)
                return;

            //用户名称
            txtUser.Text = model.Manager_CName + " [" + IpHelper.GetUserIp() + "]";

            //部门
            txtBranchName.Text = model.Branch_Name;
            //职位
            txtPositionInfoName.Text = model.Position_Name;
            #endregion
            
            #region 菜单栏数据绑定
            //获取用户页面操作权限
            _pagePower = OnlineUsersBll.GetInstence().GetPagePower();


            //创建查询条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>();
            //条件：只查询出需要显示的菜单
            wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, MenuInfoTable.IsDisplay, Comparison.Equals, 1));
            //进行查询，获取DataTable
            var dt = MenuInfoBll.GetInstence().GetDataTable(false, 0, null, 0, 0, wheres);
            //绑定树列表
            BandingTree(dt);

            #endregion

            #region 开启时钟检测
            Timer1.Enabled = true;
            #endregion

        }
        #endregion

        #region 页面按键

        #region 清空缓存并重新加载
        /// <summary>
        /// 清空缓存并重新加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearCache_Click(object sender, EventArgs e)
        {
            //清空全部后端缓存HttpRuntime.Cache（在线列表缓存除外）
            CacheHelper.RemoveManagersAllCache();

            FineUI.Alert.ShowInTop("缓存清除成功！", "提示", MessageBoxIcon.Information);
        }
        #endregion

        #region 退出系统
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExit_Click(object sender, EventArgs e)
        {
            OnlineUsersBll.GetInstence().UserExit(this);

            FineUI.Alert.ShowInTop("成功退出系统！", "安全退出", MessageBoxIcon.Information, "top.location='Login.aspx'");
        }
        #endregion

        #endregion

        #region 定时器
        /// <summary>
        /// 定时执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Timer1.Enabled = false;

            #region 检测当前用户是否退出
            OnlineUsersBll.GetInstence().IsTimeOut();
            #endregion

            #region 检测用户登录的有效性（是否被系统踢下线或管理员踢下线）
            if (OnlineUsersBll.GetInstence().IsOffline(this))
                return;
            #endregion

            #region 更新信息（在线人数，未读取的短消息）
            if (HttpRuntime.Cache == null)
            {
                txtOnlineUserCount.Text = "--";
            }
            else
            {
                //更新当前在线用户数量
                txtOnlineUserCount.Text = OnlineUsersBll.GetInstence().GetUserOnlineCount() + "";
            }
            #endregion

            #region 修改用户最后在线时间

            //修改用户最后在线时间
            OnlineUsersBll.GetInstence().UpdateTime();

            #endregion

            Timer1.Enabled = true;
        }
        #endregion
        
        #region FineUI控件之--树控件（Tree）

        #region 绑定树控件
        /// <summary>树控件（Tree）
        /// </summary>
        /// <param name="dataTable">DataTable数据源</param>
        /// <returns>树控件（Tree）</returns>
        public void BandingTree(DataTable dataTable)
        {
            try
            {
                //检查指定的列是否在数据源中能否找到
                if (dataTable.Rows.Count == 0)
                {
                    return;
                }
                //筛选出全部一级节点
                DataTable dtRoot = DataTableHelper.GetFilterData(dataTable, MenuInfoTable.ParentId, "0", MenuInfoTable.Sort, "Asc");
                //判断是否有节点存在
                if (dtRoot.Rows.Count != 0)
                {
                    //循环读取节点
                    foreach (DataRow dr in dtRoot.Rows)
                    {
                        //判断当前节点是否有权限访问，没有则跳过本次循环
                        //暂时先注释掉权限判断，等添加相关权限后再开启
                        if (_pagePower.IndexOf("," + dr[MenuInfoTable.Id].ToString() + ",") < 0)
                        {
                            continue;
                        }

                        //创建树节点
                        var treenode = new FineUI.TreeNode();
                        //设置节点ID
                        treenode.NodeID = dr[MenuInfoTable.Id].ToString();
                        //设置节点名称
                        treenode.Text = dr[MenuInfoTable.Name].ToString();
                        treenode.Target = "mainRegion";
                        //判断当前节点是否为最终节点
                        if (int.Parse(dr[MenuInfoTable.IsMenu].ToString()) != 0)
                        {
                            //设置节点链接地址，并在Url后面添加页面加密参数
                            treenode.NavigateUrl = dr[MenuInfoTable.Url].ToString() + "?" + MenuInfoBll.GetInstence().PageUrlEncryptString();
                            treenode.Leaf = true;
                        }
                        else
                        {
                            treenode.NavigateUrl = "";
                            treenode.Leaf = false;
                            //设置树节点收缩起来
                            treenode.Expanded = false;
                        }

                        //添加子节点
                        AddChildrenNode(dataTable, treenode, dr[MenuInfoTable.Id].ToString());
                        //将节点加入树列表中
                        leftMenuTree.Nodes.Add(treenode);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonBll.WriteLog("", ex);
            }
        }
        #endregion

        #region 添加子节点
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="treenode">当前树节点</param>
        /// <param name="parentID">父节点ID值</param>
        private void AddChildrenNode(DataTable dt, FineUI.TreeNode treenode, string parentID)
        {
            //筛选出当前节点下面的子节点
            DataTable Childdt = DataTableHelper.GetFilterData(dt, MenuInfoTable.ParentId, parentID, MenuInfoTable.Sort, "Asc");
            //判断是否有节点存在
            if (Childdt.Rows.Count > 0)
            {
                //循环读取节点
                foreach (DataRow dr in Childdt.Rows)
                {
                    //判断当前节点是否有权限访问，没有则跳过本次循环
                    if (_pagePower.IndexOf("," + dr[MenuInfoTable.Id].ToString() + ",") < 0)
                    {
                        continue;
                    }

                    //创建子节点
                    var TreeChildNode = new FineUI.TreeNode();
                    //设置节点ID
                    TreeChildNode.NodeID = dr[MenuInfoTable.Id].ToString();
                    //设置节点名称
                    TreeChildNode.Text = dr[MenuInfoTable.Name].ToString();
                    TreeChildNode.Target = "mainRegion";
                    //判断当前节点是否为最终节点
                    if (int.Parse(dr[MenuInfoTable.IsMenu].ToString()) != 0)
                    {
                        //设置节点链接地址
                        if (dr[MenuInfoTable.Url].ToString().IndexOf("?") > 0)
                        {
                            TreeChildNode.NavigateUrl = dr[MenuInfoTable.Url].ToString() + "&" + MenuInfoBll.GetInstence().PageUrlEncryptString();
                        }
                        else
                        {
                            TreeChildNode.NavigateUrl = dr[MenuInfoTable.Url].ToString() + "?" + MenuInfoBll.GetInstence().PageUrlEncryptString();
                        }
                        //TreeChildNode.NavigateUrl = dr[MenuInfoTable.Url].ToString() + "?" + MenuInfoBll.PageURLEncryptString();
                        TreeChildNode.Leaf = true;
                    }
                    else
                    {
                        TreeChildNode.NavigateUrl = "";
                        TreeChildNode.Leaf = false;
                        //设置树节点扩张
                        TreeChildNode.Expanded = true;
                    }
                    //将节点添加进树列表中
                    treenode.Nodes.Add(TreeChildNode);

                    //递归添加子节点
                    AddChildrenNode(dt, TreeChildNode, dr[MenuInfoTable.Id].ToString());

                }

            }

        }

        #endregion

        #endregion

    }
}