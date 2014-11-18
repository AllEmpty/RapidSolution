using System.Collections;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;

/***********************************************************************
 *   作    者：AllEmpty（陈焕）-- 1654937@qq.com
 *   博    客：http://www.cnblogs.com/EmptyFS/
 *   技 术 群：327360708
 *  
 *   创建日期：2014-06-17
 *   文件名称：PagePowerSignPublicBll.cs
 *   描    述：公共页面权限标识管理逻辑类
 *             
 *   修 改 人：
 *   修改日期：
 *   修改原因：
 ***********************************************************************/
namespace Solution.Logic.Managers
{
    /// <summary>
    /// PagePowerSignPublicBll逻辑类
    /// </summary>
    public partial class PagePowerSignPublicBll : LogicBase
    {
        /***********************************************************************
         * 自定义函数                                                          *
         ***********************************************************************/
        #region 自定义函数

        private const string const_CacheKey_Model = "Cache_PagePowerSignPublic_AllModel";

        #region 获取PagePowerSignPublic全表内容并放到缓存中
        /// <summary>
        /// 取得PagePowerSignPublic全表内容
        /// </summary>
        /// <returns>返回Hashtable</returns>
        public Hashtable GetHashtable()
        {
            //读取记录
            object obj = CacheHelper.GetCache(const_CacheKey_Model);
            if (obj == null)
            {
                //初始化Hashtable
                Hashtable ht = new Hashtable();
                //获取表全部内容
                var all = PagePowerSignPublic.All();
                //遍历读取
                foreach (var model in all)
                {
                    try
                    {
                        ht.Add(model.EName, model.Id);
                    }
                    catch
                    {
                    }
                }

                CacheHelper.SetCache(const_CacheKey_Model, ht);

                return ht;
            }
            else
            {
                return (Hashtable)obj;
            }
        }

        #endregion

        #region 清空缓存
        /// <summary>清空缓存</summary>
        public override void DelCache()
        {
            CacheHelper.RemoveOneCache(const_CacheKey_Model);
        }
        #endregion

        #endregion 自定义函数

    }
}
