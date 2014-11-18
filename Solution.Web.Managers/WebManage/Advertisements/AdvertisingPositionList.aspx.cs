using System;
using System.Collections.Generic;
using DotNet.Utilities;
using FineUI;
using Solution.DataAccess.DataModel;
using Solution.Logic.Managers;
using Solution.Web.Managers.WebManage.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-07-07
 *   文件名称：AdvertisingPositionList.aspx.cs
 *   描    述：广告位置列表文件
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Advertisements
{
    public partial class AdvertisingPositionList : PageBase
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定下拉列表
                AdvertisingPositionBll.GetInstence().BandDropDownList(this, ddlParentId);

                LoadData();
            }
        }
        #endregion

        #region 接口函数，用于UI页面初始化，给逻辑层对象、列表等对象赋值
        public override void Init()
        {
            //逻辑对象赋值
            bll = AdvertisingPositionBll.GetInstence();
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
                Sort();
            }

            //绑定Grid表格
            bll.BindGrid(Grid1, InquiryCondition(), sortList);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private int InquiryCondition()
        {
            int value = 0;

            //选择广告位置
            if (ddlParentId.SelectedValue != "0")
            {
                value = ConvertHelper.Cint0(ddlParentId.SelectedValue);
            }
            return value;
        }

        #region 排序
        /// <summary>
        /// 页面表格绑定排序
        /// </summary>
        public void Sort()
        {
            //设置排序
            sortList = new List<string>();
            sortList.Add(AdvertisingPositionTable.Depth + " asc");
            sortList.Add(AdvertisingPositionTable.Sort + " asc");
        }
        #endregion

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
            //绑定是否显示状态列
            GridRow gr = Grid1.Rows[e.RowIndex];
            if (((System.Data.DataRowView)(gr.DataItem)).Row.Table.Rows[e.RowIndex][AdvertisingPositionTable.IsDisplay].ToString() == "0")
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
                    AdvertisingPositionBll.GetInstence().UpdateIsDisplay(this, ConvertHelper.Cint0(id), ConvertHelper.Cint0(e.CommandArgument));
                    //重新加载
                    LoadData();

                    break;
                case "ButtonEdit":
                    //打开编辑窗口
                    Window1.IFrameUrl = "AdvertisingPositionEdit.aspx?Id=" + id + "&" + MenuInfoBll.GetInstence().PageUrlEncryptStringNoKey(id + "");
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
            Window1.IFrameUrl = "AdvertisingPositionEdit.aspx?" + MenuInfoBll.GetInstence().PageUrlEncryptString();
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
            //获取要删除的ID
            int id = ConvertHelper.Cint0(GridViewHelper.GetSelectedKey(Grid1, true));
            
            //如果没有选择记录，则直接退出
            if (id == 0)
            {
                return "请选择要删除的记录。";
            }

            try
            {
                //删除前判断一下
                if (AdvertisingPositionBll.GetInstence().Exist(x => x.ParentId == id))
                {
                    return "删除失败，本广告位置下面存在子广告位置，不能直接删除！";
                }
                //删除前判断一下
                if (AdvertisementBll.GetInstence().Exist(x => x.AdvertisingPosition_Id == id))
                {
                    return "删除失败，本广告位置已被信息表的记录引用，不能直接删除！";
                }

                //删除记录
                bll.Delete(this, id);

                return "删除编号ID为[" + id + "]的数据记录成功。";
            }
            catch (Exception e)
            {
                string result = "尝试删除编号ID为[" + id + "]的数据记录失败！";

                //出现异常，保存出错日志信息
                CommonBll.WriteLog(result, e);

                return result;
            }
        }
        #endregion

    }
}