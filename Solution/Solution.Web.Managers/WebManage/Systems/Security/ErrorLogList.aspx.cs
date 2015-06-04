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
 *   文件名称：ErrorLogList.aspx.cs
 *   描    述：错误日志列表文件
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Systems.Security
{
    public partial class ErrorLogList : PageBase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设定初始化时间
                dpStart.Text = DateTime.Now.ToString("yyyy-M-d");
                dpEnd.Text = DateTime.Now.AddDays(1).ToString("yyyy-M-d");

                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {
            //逻辑对象赋值
            bll = ErrorLogBll.GetInstence();
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
            ErrorLogBll.GetInstence().BindGrid(Grid1, Grid1.PageIndex + 1, Grid1.PageSize, InquiryCondition(), sortList);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private List<ConditionHelper.SqlqueryCondition> InquiryCondition()
        {
            var wheres = new List<ConditionHelper.SqlqueryCondition>();

            //起始时间
            if (!string.IsNullOrEmpty(dpStart.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, ErrorLogTable.ErrTime, Comparison.GreaterOrEquals, StringHelper.FilterSql(dpStart.Text)));
                //终止时间
                if (!string.IsNullOrEmpty(dpEnd.Text.Trim()))
                {
                    wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, ErrorLogTable.ErrTime, Comparison.LessOrEquals, StringHelper.FilterSql(dpEnd.Text)));
                }
            }

            //ip地址
            if (!string.IsNullOrEmpty(txtIp.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, ErrorLogTable.Ip, Comparison.Equals, StringHelper.FilterSql(txtIp.Text)));
            }
            //位置
            if (string.IsNullOrEmpty(ddlType.SelectedValue.Trim()) && StringHelper.FilterSql(ddlType.SelectedValue.Trim()) != "-1")
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, ErrorLogTable.Type, Comparison.Equals,
                                                            ConvertHelper.Cint0(ddlType.SelectedValue)));
            }
            return wheres;
        }

        #endregion

        #region 列表属性绑定
        /// <summary>
        /// 列表按键绑定——修改列表控件属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
            GridRow gr = Grid1.Rows[e.RowIndex];

            //位置
            var lbf = Grid1.FindColumn("Type") as LinkButtonField;
            if (lbf != null)
            {
                if (((System.Data.DataRowView)(gr.DataItem)).Row.Table.Rows[e.RowIndex][ErrorLogTable.Type]
                        .ToString() == "0")
                {
                    lbf.Text = "后端";
                }
                else
                {
                    lbf.Text = "前端";
                }
            }
        }
        #endregion

    }
}