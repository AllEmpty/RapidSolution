using System.Collections.Generic;
using System.Web.UI;
using Solution.DataAccess.DbHelper;

 /***********************************************************************
  *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
  *   博    客：http://www.cnblogs.com/EmptyFS/
  *   技 术 群：327360708
  *  
  *   创建日期：2014-06-17
  *   文件名称：ILogicBase.cs
  *   描    述：逻辑层接口类
  *             
  *   修 改 人：
  *   修改日期：
  *   修改原因：
  ***********************************************************************/
namespace Solution.Logic.Managers.Application
{
    public interface ILogicBase
    {
        #region 绑定表格

        void BindGrid(FineUI.Grid grid, int pageIndex = 0, int pageSize = 0,
            List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> orders = null);

        void BindGrid(FineUI.Grid grid, int parentValue,
            List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> orders = null, string parentId = "ParentId");

        void BindGrid(FineUI.Grid grid, int parentValue, List<string> orders = null,
            string parentId = "ParentId");

        #endregion

        #region 删除记录

        void Delete(Page page, int id, bool isAddUseLog = true);

        void Delete(Page page, int[] id, bool isAddUseLog = true);

        #endregion

        #region 保存排序

        bool UpdateSort(Page page, FineUI.Grid grid1, string tbxSort, string sortName = "Sort");

        #endregion

        #region 自动排序

        bool UpdateAutoSort(Page page, string strWhere = "", bool isExistsMoreLv = false, int pid = 0,
            string fieldName = "Sort", string fieldParentId = "ParentId");

        #endregion
    }
}
