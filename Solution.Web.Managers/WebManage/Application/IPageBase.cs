
/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：IPageBase.cs
 *   描    述：UI页面接口类——主要用于列表（Grid）页
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Web.Managers.WebManage.Application
{
    /// <summary>
    /// UI页面接口类——主要用于列表（Grid）页
    /// </summary>
    public interface IPageBase
    {
        #region 用于UI页面初始化，给逻辑层对象、列表等对象赋值，主要是列表（Grid）页面使用

        void Init();

        #endregion
    }
}
