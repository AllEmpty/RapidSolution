using System;
using DotNet.Utilities;
using FineUI;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-21
 *   文件名称：PagePowerSignPublicList.aspx.cs
 *   描    述：公用页面控件权限标识列表管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Systems.Powers
{
    public partial class PagePowerSignPublicList : PageBase
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
            bll = PagePowerSignPublicBll.GetInstence();
            //表格对象赋值
            grid = Grid1;
        }
        #endregion

        #region 加载数据
        /// <summary>读取数据</summary>
        public override void LoadData()
        {
            //设置排序
            if (sortList == null)
            {
                Sort(null);
            }

            //绑定Grid表格
            bll.BindGrid(Grid1, Grid1.PageIndex + 1, Grid1.PageSize, null, sortList);
        }

        #endregion
        
        #region 列表属性绑定

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
                    Window1.IFrameUrl = "PagePowerSignPublicEdit.aspx?Id=" + id + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(id + "");
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
            Window1.IFrameUrl = "PagePowerSignPublicEdit.aspx?" + MenuInfoBll.GetInstence().PageUrlEncryptString();
            Window1.Hidden = false;
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
                    if (PagePowerSignBll.GetInstence().Exist(x => x.PagePowerSignPublic_Id == i))
                    {
                        return "删除失败，Id为【" + i + "】的记录已被引用，不能直接删除！";
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