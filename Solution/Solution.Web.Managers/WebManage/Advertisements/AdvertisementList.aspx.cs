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
 *   创建日期：2014-07-09
 *   文件名称：AdvertisementList.aspx.cs
 *   描    述：广告列表管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Advertisements
{
    public partial class AdvertisementList : PageBase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定下拉列表
                AdvertisingPositionBll.GetInstence().BandDropDownListShowAll(this, dllAdvertisingPosition);

                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {
            //逻辑对象赋值
            bll = AdvertisementBll.GetInstence();
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
            bll.BindGrid(Grid1, Grid1.PageIndex + 1, Grid1.PageSize, InquiryCondition(), sortList);
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
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisementTable.StartTime, Comparison.LessOrEquals, StringHelper.FilterSql(dpStart.Text)));
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisementTable.EndTime, Comparison.GreaterOrEquals, StringHelper.FilterSql(dpStart.Text)));
            }
            //广告位置
            if (dllAdvertisingPosition.SelectedValue != "0")
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisementTable.AdvertisingPosition_Id, Comparison.Equals,
                    StringHelper.FilterSql(dllAdvertisingPosition.SelectedValue)));
            }
            //是否审批
            if (!string.IsNullOrEmpty(ddlIsDisplay.SelectedValue))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisementTable.IsDisplay, Comparison.Equals,
                    StringHelper.FilterSql(ddlIsDisplay.SelectedValue)));
            }
            //广告名称
            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisementTable.Name,
                    Comparison.Like, "%" + StringHelper.FilterSql(txtName.Text) + "%"));
            }
            //Keyword
            if (!string.IsNullOrEmpty(txtKeyword.Text.Trim()))
            {
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, AdvertisementTable.Keyword, Comparison.Like,
                    "%" + StringHelper.FilterSql(txtKeyword.Text) + "%"));
            }

            return wheres;
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
            //绑定是否显示
            GridRow gr = Grid1.Rows[e.RowIndex];
            if (((System.Data.DataRowView)(gr.DataItem)).Row.Table.Rows[e.RowIndex][AdvertisementTable.IsDisplay].ToString() == "0")
            {
                var lbf = Grid1.FindColumn("IsDisplay") as LinkButtonField;
                lbf.Icon = Icon.BulletCross;
                lbf.CommandArgument = "1";
            }
            else
            {
                var lbf = Grid1.FindColumn("IsDisplay") as LinkButtonField;
                lbf.Icon = Icon.BulletTick;
                lbf.CommandArgument = "0";
            }

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
                case "IsDisplay":
                    //更新状态
                    AdvertisementBll.GetInstence().UpdateIsDisplay(this, ConvertHelper.Cint0(id), ConvertHelper.Cint0(e.CommandArgument));
                    //重新加载
                    LoadData();

                    break;

                case "ButtonEdit":
                    //打开编辑窗口
                    Window1.IFrameUrl = "AdvertisementEdit.aspx?Id=" + id + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(id + "");
                    Window1.Hidden = false;

                    break;
            }
        }
        #endregion

        #region 保存自动排序
        /// <summary>
        /// 保存自动排序
        /// </summary>
        public override void SaveAutoSort()
        {
            if (bll.UpdateAutoSort(this))
            {
                Alert.ShowInParent("保存成功", "保存自动排序成功", "window.location.reload();");
            }
            else
            {
                Alert.ShowInParent("保存失败", "保存自动排序失败");
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
            Window1.IFrameUrl = "AdvertisementEdit.aspx?" + MenuInfoBll.GetInstence().PageUrlEncryptString();
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
                //逐个删除对应图片
                foreach (var i in id)
                {
                    //删除文章封面图片
                    AdvertisementBll.GetInstence().DelAdImg(this, i);
                }

                //删除记录
                bll.Delete(this, id);

                return "删除编号Id为[" + string.Join(",", id) + "]的数据记录成功。";
            }
            catch (Exception e)
            {
                string result = "尝试删除编号ID为[" + string.Join(",", id) +"]的数据记录失败！";

                //出现异常，保存出错日志广告
                CommonBll.WriteLog(result, e);

                return result;
            }
        }
        #endregion

    }
}