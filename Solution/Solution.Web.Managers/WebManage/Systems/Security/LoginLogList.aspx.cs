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
 *   文件名称：LoginLogList.aspx.cs
 *   描    述：登陆日志列表文件
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Systems.Security
{
    public partial class LoginLogList : PageBase
    {
        int _id = 0;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取管理员Id——用于查询指定管理员日志
            _id = RequestHelper.GetInt0("Id");

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
            bll = LoginLogBll.GetInstence();
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
            LoginLogBll.GetInstence().BindGrid(Grid1, Grid1.PageIndex + 1, Grid1.PageSize, InquiryCondition(), sortList);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private List<ConditionHelper.SqlqueryCondition> InquiryCondition()
        {
            var wheres = new List<ConditionHelper.SqlqueryCondition>();

            //如果Id有值时，即表示查询的是指定管理员的操作日志
            if (_id != 0)
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.Where, LoginLogTable.Manager_Id, Comparison.Equals, _id));
            }

            //起始时间
            if (!string.IsNullOrEmpty(dpStart.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, LoginLogTable.AddDate, Comparison.GreaterOrEquals, StringHelper.FilterSql(dpStart.Text)));
                //终止时间
                if (!string.IsNullOrEmpty(dpEnd.Text.Trim()))
                {
                    wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, LoginLogTable.AddDate, Comparison.LessOrEquals, StringHelper.FilterSql(dpEnd.Text)));
                }
            }

            //ip地址
            if (!string.IsNullOrEmpty(txtIp.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, LoginLogTable.Ip, Comparison.Equals, StringHelper.FilterSql(txtIp.Text)));
            }
            //登录备注信息
            if (!string.IsNullOrEmpty(txtloginfo.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, LoginLogTable.Notes, Comparison.Like, "%" + StringHelper.FilterSql(txtloginfo.Text) + "%"));
            }

            return wheres;
        }

        #endregion
        
    }
}