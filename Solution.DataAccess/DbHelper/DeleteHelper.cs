using System;
using SubSonic.DataProviders;
using SubSonic.Query;

/***********************************************************************
*   作    者：Jonson（136646321）
*   博    客：http://blog.sina.com.cn/u/2388032531
*   技 术 群：327360708
*  
*   创建日期：2013-04-23
*   文件名称：DeleteHelper.cs
*   描    述：记录删除类
*             
*   修 改 人：AllEmpty（陈焕）
*   修改日期：2013-07
*   修改原因：删除一些不必要的函数，优化代码
***********************************************************************/
namespace Solution.DataAccess.DbHelper
{
    /// <summary>
    /// 记录删除类
    /// </summary>
    public class DeleteHelper
    {
        //定义数据源
        IDataProvider provider = null;
        //获取执行语句
        private string _execSql = "";

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeleteHelper()
        {
            //获取数据源
            provider = SubSonic.DataProviders.ProviderFactory.GetProvider();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DeleteHelper(IDataProvider provider)
        {
            //获取数据源
            this.provider = provider;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 删除,通过sql语句进行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>被删除的记录数</returns>
        public int Delete(String sql)
        {
            try
            {
                //创建执行对象
                var q = new SubSonic.Query.QueryCommand(sql, provider);

                //获取执行语句
                _execSql = sql;

                return q.Provider.ExecuteQuery(q);
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("删除数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// (有事务)批量删除,通过sql语句进行删除
        /// </summary>
        /// <param name="sqls">执行sql语句集</param>
        /// <returns>被删除的记录数</returns>
        public int Delete(params String[] sqls)
        {
            try
            {
                //创建事务
                var batch = new BatchQuery(provider);

                //遍历添加
                foreach (String item in sqls)
                {
                    QueryCommand qcmd = new QueryCommand(item, provider);

                    batch.QueueForTransaction(qcmd);
                }

                //获取执行语句
                _execSql = string.Join(",", sqls);

                //执行事务
                batch.ExecuteTransaction();
                return batch.QueryCount;
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("删除数据异常,执行语句" + _execSql, e);
            }

        }

        #endregion
    }
}
