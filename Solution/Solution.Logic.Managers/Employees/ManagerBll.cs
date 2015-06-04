using System.Collections.Generic;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using SubSonic.Query;


/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-07-03
 *   文件名称：ManagerBll.cs
 *   描    述：管理员管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// Manager表逻辑类
    /// </summary>
    public partial class ManagerBll : LogicBase
    {
        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        #region 同步更新修改了职位名称的管理员表记录
        /// <summary>
        /// 同步更新修改了职位名称的管理员表记录
        /// </summary>
        /// <param name="positionId">职位Id</param>
        /// <param name="positionName">修改后的职位名称</param>
        public void UpdatePositionName(int positionId, string positionName)
        {
            //首先获取有该职位的所有管理员记录
            //设置查询的列，只需要获取Id与职位Id字段
            var columns = new List<string>();
            columns.Add(ManagerTable.Id);
            columns.Add(ManagerTable.Position_Id);

            //设置查询条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>();
            wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, ManagerTable.Position_Id, Comparison.Like, "%," + positionId + ",%"));
            //获取DataTable
            var dt = GetDataTable(false, 0, columns, 0, 0, wheres, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                //逐个更新管理员职位名称
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //获取职位名称
                    var name = PositionBll.GetInstence().GetName(dt.Rows[i][ManagerTable.Position_Id] + "");
                    //更新管理员职位名称
                    UpdateValue(null, ConvertHelper.Cint0(dt.Rows[i][ManagerTable.Id]), ManagerTable.Position_Name, name, "{0}修改了Manager表外键Position_Id值为" + i + "的职位名称[" + positionName + "]");
                }
            }
        }
        #endregion

        #endregion 自定义函数

    }
}
