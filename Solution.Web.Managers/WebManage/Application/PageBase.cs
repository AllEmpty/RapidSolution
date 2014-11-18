using System;
using System.Collections.Generic;
using System.Web.UI;
using DotNet.Utilities;
using FineUI;
using Solution.Logic.Managers;
using Solution.Logic.Managers.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：PageBase.cs
 *   描    述：Web层页面父类
 *             封装了各种常用函数，减少重复代码的编写
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Application
{
    /// <summary>
    /// Web层页面父类
    /// </summary>
    public abstract class PageBase : System.Web.UI.Page, IPageBase
    {
        #region 定义对象
        //逻辑层接口对象
        protected ILogicBase bll = null;
        //定义列表对象
        protected FineUI.Grid grid = null;
        //页面排序容器
        protected List<string> sortList = null;
        #endregion

        #region 初始化函数
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //检测用户是否超时退出
            OnlineUsersBll.GetInstence().IsTimeOut();
            //检测用户登录的有效性（是否被系统踢下线或管理员踢下线）
            if (OnlineUsersBll.GetInstence().IsOffline(this))
                return;

            if (!IsPostBack)
            {
                //检测当前页面是否有访问权限
                MenuInfoBll.GetInstence().CheckPagePower(this);

                #region 设置页面按键权限
                try
                {
                    //定义按键控件
                    Control btnControl = null;
                    //找到页面放置按键控件的位置
                    ControlCollection controls = MenuInfoBll.GetInstence().GetControls(this.Controls, "toolBar");
                    //逐个读取出来
                    for (int i = 0; i < controls.Count; i++)
                    {
                        //取出控件
                        btnControl = controls[i];
                        //判断是否除了刷新、查询和关闭以外的按键
                        if (btnControl.ID != "ButtonRefresh" && btnControl.ID != "ButtonSearch" && btnControl.ID != "ButtonClose" && btnControl.ID != "ButtonReset")
                        {
                            //是的话检查该按键当前用户是否有控件权限，没有的话则禁用该按键
                            ((FineUI.Button)btnControl).Enabled = MenuInfoBll.GetInstence().CheckControlPower(this, btnControl.ID);
                        }
                    }
                }
                catch (Exception) { }
                #endregion

                //记录用户当前所在的页面位置
                CommonBll.UserRecord(this);
            }

            //运行UI页面初始化函数，子类继承后需要重写本函数，以提供给本初始化函数调用
            Init();

            //如果列表项不为空时，绑定空数据显示内容
            if (grid != null)
                grid.EmptyText = String.Format("<img src=\"{0}\" alt=\"No Data Found!\"/>", ResolveUrl("/WebManage/Images/no_data_found.jpg"));
        }


        /// <summary>
        /// 对页面或其控件的内容进行最后更改
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            //利用反射的方式给页面控件赋值
            //查找指定名称控件
            var control = MenuInfoBll.GetInstence().FindControl(this.Controls, "lblSpendingTime");
            if (control != null)
            {
                //判断是否是FineUI.HiddenField类型
                var type = control.GetType();
                if (type.FullName == "FineUI.Label")
                {
                    //存储排序列字段名称
                    ((FineUI.Label)control).Text = "执行耗时：" + Session["SpendingTime"] + "秒";
                }
            }
            
            base.OnPreRender(e);
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值

        /// <summary>
        /// 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        /// </summary>
        public abstract void Init();

        #endregion

        #region 页面各种按键事件

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonRefresh_Click(object sender, EventArgs e)
        {
            FineUI.PageContext.RegisterStartupScript("window.location.reload()");
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHideRefreshReference());

        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Add();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            //执行删除操作，返回删除结果
            string result = Delete();

            if (string.IsNullOrEmpty(result))
                return;
            //弹出提示框
            FineUI.Alert.ShowInParent(result, FineUI.MessageBoxIcon.Information);

            //重新加载页面表格
            LoadData();
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            //执行保存操作，返回保存结果
            string result = Save();

            if (string.IsNullOrEmpty(result))
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInParent("保存成功", FineUI.MessageBoxIcon.Information);
            }
            else
            {
                //by july，部分页面保存后，必须刷新原页面的，把返回的值用 "{url}" + 跳转地址的方式传过来
                if (StringHelper.Left(result, 5) == "{url}")
                {
                    string url = result.Trim().Substring(5);
                    FineUI.Alert.ShowInParent("保存成功", "", FineUI.MessageBoxIcon.Information, "self.location='" + url + "'");
                }
                else
                {
                    FineUI.Alert.ShowInParent(result, FineUI.MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>保存排序</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveSort_Click(object sender, EventArgs e)
        {
            SaveSort();
        }

        /// <summary>自动排序</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSaveAutoSort_Click(object sender, EventArgs e)
        {
            //默认使用多级分类
            SaveAutoSort();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            //生成排序关键字
            Sort(e);
            //刷新列表
            LoadData();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            if (grid != null)
            {
                grid.PageIndex = e.NewPageIndex;

                LoadData();
            }
        }

        #region 关闭子窗口事件
        /// <summary>
        /// 关闭子窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 关闭子窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Window2_Close(object sender, WindowCloseEventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 关闭子窗口事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            LoadData();
        }
        #endregion

        #endregion

        #region 虚函数，主要给页面各种按键事件调用，子类需要使用到相关功能时必须将它实现

        /// <summary>
        /// 加载事件
        /// </summary>
        public abstract void LoadData();

        /// <summary>
        /// 添加记录
        /// </summary>
        public virtual void Add() { }

        /// <summary>
        /// 修改记录
        /// </summary>
        public virtual void Edit() { }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <returns>返回删除结果</returns>
        public virtual string Delete()
        {
            return null;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns>返回保存结果</returns>
        public virtual string Save()
        {
            return "";
        }

        /// <summary>
        /// 保存排序
        /// </summary>
        /// <returns>返回保存结果</returns>
        public virtual void SaveSort()
        {
            //保存排序
            if (grid != null && bll != null)
            {
                //更新排序
                if (bll.UpdateSort(this, grid, "tbSort"))
                {
                    //重新加载列表
                    LoadData();

                    Alert.ShowInParent("操作成功", "保存排序成功", "window.location.reload();");
                }
                else
                {
                    Alert.ShowInParent("操作成失败", "保存排序失败");
                }
            }
        }

        /// <summary>
        /// 保存自动排序
        /// </summary>
        public virtual void SaveAutoSort()
        {
            if (bll == null)
            {
                Alert.ShowInParent("保存失败", "逻辑层对象为null，请联系开发人员给当前页面的逻辑层对象赋值");
                return;
            }

            if (bll.UpdateAutoSort(this, "", true))
            {
                //刷新列表
                LoadData();

                Alert.ShowInParent("保存成功", "保存自动排序成功", "window.location.reload();");
            }
            else
            {
                Alert.ShowInParent("保存失败", "保存自动排序失败");
            }
        }

        /// <summary>
        /// 生成排序关键字
        /// </summary>
        /// <param name="e"></param>
        public virtual void Sort(FineUI.GridSortEventArgs e)
        {
            //处理排序
            sortList = null;
            sortList = new List<string>();
            //排序列字段名称
            string sortName = "";

            if (e != null && e.SortField.Length > 0)
            {
                //判断是升序还是降序
                if (e.SortDirection != null && e.SortDirection.ToUpper() == "DESC")
                {
                    sortList.Add(e.SortField + " desc");
                }
                else
                {
                    sortList.Add(e.SortField + " asc");
                }
                sortName = e.SortField;
            }
            else
            {
                //使用默认排序——主键列降序排序
                sortList.Add("Id desc");
                sortName = "Id";
            }

            //利用反射的方式给页面控件赋值
            //查找指定名称控件
            var control = MenuInfoBll.GetInstence().FindControl(this.Controls, "SortColumn");
            if (control != null)
            {
                //判断是否是FineUI.HiddenField类型
                var type = control.GetType();
                if (type.FullName == "FineUI.HiddenField")
                {
                    //存储排序列字段名称
                    ((FineUI.HiddenField)control).Text = sortName;
                }
            }
        }

        #endregion

    }
}