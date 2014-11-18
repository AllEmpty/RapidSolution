using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.UI;
using FineUI;
using Solution.DataAccess.DbHelper;
using Solution.Logic.Managers.Application;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：LogicBase.cs
 *   描    述：逻辑层抽象类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// 逻辑层抽象类
    /// </summary>
    public abstract class LogicBase : ILogicBase
    {
        #region 清空缓存
        /// <summary>清空缓存</summary>
        public virtual void DelCache()
        {

        }
        #endregion
        
        #region 全表缓存加载条件
        /// <summary>
        /// 全表缓存加载条件——对于有些表并不用所有记录都加载到缓存当中，这个时候就可以重写本函数来实现每次加载时只加载指定的记录到缓存中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Expression<Func<T, bool>> GetExpression<T>()
        {
            return null;
        }
        #endregion

        #region 绑定表格
        public abstract void BindGrid(FineUI.Grid grid, int pageIndex = 0, int pageSize = 0, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> orders = null);

        public abstract void BindGrid(FineUI.Grid grid, int parentValue, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> orders = null,
            string parentId = "ParentId");

        public abstract void BindGrid(FineUI.Grid grid, int parentValue, List<string> orders = null, string parentId = "ParentId");
        #endregion

        #region 删除记录
        public abstract void Delete(Page page, int id, bool isAddUseLog = true);
        public abstract void Delete(Page page, int[] id, bool isAddUseLog = true);
        #endregion

        #region 保存排序
        public abstract bool UpdateSort(Page page, Grid grid1, string tbxSort, string sortName = "Sort");
        #endregion

        #region 自动排序
        public abstract bool UpdateAutoSort(Page page, string strWhere = "", bool isExistsMoreLv = false, int pid = 0,
            string fieldName = "Sort", string fieldParentId = "ParentId");
        #endregion
    }
}
