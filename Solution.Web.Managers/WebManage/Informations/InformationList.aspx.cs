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
 *   创建日期：2014-07-01
 *   文件名称：InformationList.aspx.cs
 *   描    述：信息列表管理
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
using SubSonic.Query;

namespace Solution.Web.Managers.WebManage.Informations
{
    public partial class InformationList : PageBase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定下拉框
                InformationClassBll.GetInstence().BandDropDownListShowAll(this, dllInformationClass);

                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {
            //逻辑对象赋值
            bll = InformationBll.GetInstence();
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

        //条件
        private List<ConditionHelper.SqlqueryCondition> InquiryCondition()
        {
            var list = new List<ConditionHelper.SqlqueryCondition>();

            //所属栏目
            if (dllInformationClass.SelectedValue != "0")
            {
                list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.InformationClass_Root_Id, Comparison.Equals, StringHelper.FilterSql(dllInformationClass.SelectedValue), true));
                list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.Or, InformationTable.InformationClass_Id, Comparison.Equals, StringHelper.FilterSql(dllInformationClass.SelectedValue)));
                list.Add(new ConditionHelper.SqlqueryCondition());
            }
            //审批
            if (!string.IsNullOrEmpty(ddlIsDisplay.SelectedValue))
            {
                list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.IsDisplay, Comparison.Equals, StringHelper.FilterSql(ddlIsDisplay.SelectedValue)));
            }
            //推荐状态
            if (!string.IsNullOrEmpty(ddlIsHot.SelectedValue))
            {
                list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.IsHot, Comparison.Equals, StringHelper.FilterSql(ddlIsHot.SelectedValue)));
            }
            //标题
            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.Title, Comparison.Like, "%" + StringHelper.FilterSql(txtKey.Text) + "%", true));
                list.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.Or, InformationTable.Keywords, Comparison.Like, "%" + StringHelper.FilterSql(txtKey.Text) + "%"));
                list.Add(new ConditionHelper.SqlqueryCondition());
            }

            return list;
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
            DataRowView row = e.DataItem as DataRowView;
            if (row != null)
            {
                if (row.Row.Table.Rows[e.RowIndex][InformationTable.IsDisplay].ToString() == "0")
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

                //绑定是否置顶
                if (row.Row.Table.Rows[e.RowIndex][InformationTable.IsTop]
                        .ToString() == "0")
                {
                    var lbf = Grid1.FindColumn("IsTop") as LinkButtonField;
                    lbf.Icon = Icon.BulletCross;
                    lbf.CommandArgument = "1";
                }
                else
                {
                    var lbf = Grid1.FindColumn("IsTop") as LinkButtonField;
                    lbf.Icon = Icon.BulletTick;
                    lbf.CommandArgument = "0";
                }

                //绑定是否推荐
                if (row.Row.Table.Rows[e.RowIndex][InformationTable.IsHot]
                        .ToString() == "0")
                {
                    var lbf = Grid1.FindColumn("IsHot") as LinkButtonField;
                    lbf.Icon = Icon.BulletCross;
                    lbf.CommandArgument = "1";
                }
                else
                {
                    var lbf = Grid1.FindColumn("IsHot") as LinkButtonField;
                    lbf.Icon = Icon.BulletTick;
                    lbf.CommandArgument = "0";
                }
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
                    InformationBll.GetInstence().UpdateIsDisplay(this, ConvertHelper.Cint0(id), ConvertHelper.Cint0(e.CommandArgument));
                    //重新加载
                    LoadData();

                    break;
                case "IsTop":
                    //更新状态
                    InformationBll.GetInstence().UpdateIsTop(this, ConvertHelper.Cint0(id), ConvertHelper.Cint0(e.CommandArgument));
                    //重新加载
                    LoadData();

                    break;

                case "IsHot":
                    //更新状态
                    InformationBll.GetInstence().UpdateIsHot(this, ConvertHelper.Cint0(id), ConvertHelper.Cint0(e.CommandArgument));
                    //重新加载
                    LoadData();

                    break;

                case "ButtonEdit":
                    //打开编辑窗口
                    Window1.IFrameUrl = "InformationEdit.aspx?Id=" + id + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(id + "");
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
            Window1.IFrameUrl = "InformationEdit.aspx?" + MenuInfoBll.GetInstence().PageUrlEncryptString();
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
                    InformationBll.GetInstence().DelFrontCoverImg(this, i);
                    //删除编辑器上传图片
                    UploadFileBll.Upload_BatDelPic(InformationTable.TableName, i);
                }

                //删除记录
                bll.Delete(this, id);

                return "删除编号Id为[" + string.Join(",", id) + "]的数据记录成功。";
            }
            catch (Exception e)
            {
                string result = "尝试删除编号ID为[" + string.Join(",", id) + "]的数据记录失败！";

                //出现异常，保存出错日志信息
                CommonBll.WriteLog(result, e);

                return result;
            }
        }
        #endregion

    }
}