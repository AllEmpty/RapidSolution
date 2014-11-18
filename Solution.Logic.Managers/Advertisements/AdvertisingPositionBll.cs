using System;
using System.Collections.Generic;
using System.Web.UI;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-07-07
 *   文件名称：AdvertisingPositionBll.cs
 *   描    述：广告位置管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// AdvertisingPositionBll逻辑类
    /// </summary>
    public partial class AdvertisingPositionBll : LogicBase
    {
        /***********************************************************************
		 * 自定义函数                                                          *
		 ***********************************************************************/

        #region 自定义函数

        #region 绑定广告位置下拉列表
        /// <summary>
        /// 绑定广告位置下拉列表——只显示一级广告位置
        /// </summary>
        public void BandDropDownList(Page page, FineUI.DropDownList ddl)
        {
            var dt = DataTableHelper.GetFilterData(GetDataTable(), AdvertisingPositionTable.ParentId, "0", AdvertisingPositionTable.Sort, "desc");

            //显示值
            ddl.DataTextField = AdvertisingPositionTable.Name;
            //绑定Id
            ddl.DataValueField = AdvertisingPositionTable.Id;

            //绑定数据源
            ddl.DataSource = dt;
            ddl.DataBind();
            ddl.Items.Insert(0, new FineUI.ListItem("请选择广告位置", "0"));
            ddl.SelectedValue = "0";
        }

        /// <summary>
        /// 绑定广告位置下拉列表——显示所有
        /// </summary>
        public void BandDropDownListShowAll(Page page, FineUI.DropDownList ddl)
        {
            //设置排序
            var sortList = new List<string>();
            sortList.Add(AdvertisingPositionTable.Depth);
            sortList.Add(AdvertisingPositionTable.Sort);

            //筛选记录
            var dt = GetDataTable(false, 0, null, 0, 0, null, sortList);

            try
            {
                //整理出有层次感的数据
                dt = DataTableHelper.DataTableTidyUp(dt, AdvertisingPositionTable.Id, AdvertisingPositionTable.ParentId, 0);

                ddl.EnableSimulateTree = true;

                //显示值
                ddl.DataTextField = AdvertisingPositionTable.Name;
                //绑定Id
                ddl.DataValueField = AdvertisingPositionTable.Id;
                //数据层次
                ddl.DataSimulateTreeLevelField = AdvertisingPositionTable.Depth;
                //绑定数据源
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.SelectedIndex = 0;

                ddl.Items.Insert(0, new FineUI.ListItem("请选择广告位置", "0"));
                ddl.SelectedValue = "0";
            }
            catch (Exception e)
            {
                // 记录日志
                CommonBll.WriteLog("", e);
            }
        }
        #endregion

        #endregion 自定义函数
    }
}
