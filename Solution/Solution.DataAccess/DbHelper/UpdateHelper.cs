using System;
using System.Collections.Generic;
using SubSonic.DataProviders;
using SubSonic.Query;
using SubSonic.Schema;

/***********************************************************************
 *   作    者：Jonson（136646321）
 *   博    客：http://blog.sina.com.cn/u/2388032531
 *   技 术 群：327360708
 *  
 *   创建日期：2013-04-23
 *   文件名称：UpdateHelper.cs
 *   描    述：记录修改类
 *             
 *   修 改 人：AllEmpty（陈焕）
 *   修改日期：2013-07
 *   修改原因：删除一些不必要的函数，优化代码
 ***********************************************************************/
namespace Solution.DataAccess.DbHelper
{
    /// <summary>
    /// 记录修改类
    /// </summary>
    public class UpdateHelper
    {
        //定义数据源
        private IDataProvider provider = null;
        //获取执行语句
        private string _execSql = "";
    
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public UpdateHelper()
        {
            //获取数据源
            provider = SubSonic.DataProviders.ProviderFactory.GetProvider();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UpdateHelper(IDataProvider provider)
        { 
            //获取数据源
            this.provider = provider;
        }
        #endregion
        
        #region 方法

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sql">执行语句</param>
        /// <returns>影响行数</returns>
        /// <!--
        ///  UpdateHelper update = new UpdateHelper();
        ///  int result = update.UpdateSql("update channel001 set channelCname='TEST' where ChannelID=1");
        /// -->
        public int Update(String sql)
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
                throw new Exception("更新数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// (有事务)批量修改,通过sql语句进行修改
        /// </summary>
        /// <param name="sqls">执行sql语句集</param>
        /// <returns>被修改的记录数</returns>
        public int Update(params String[] sqls)
        {
            try
            {
                //创建事务
                var batch = new BatchQuery(provider);

                //遍历添加
                foreach (String item in sqls)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        QueryCommand qcmd = new QueryCommand(item, provider);
                        batch.QueueForTransaction(qcmd);
                    }
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
                throw new Exception("更新数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// 泛型式修改——有条件
        /// </summary>
        /// <typeparam name="T">泛型集合对象</typeparam>
        /// <param name="setValue">要更新的字段与值</param>
        /// <param name="wheres">修改条件</param>
        public int Update<T>(Dictionary<string, object> setValue, List<ConditionHelper.SqlqueryCondition> wheres = null) where T : IActiveRecord, new()
        {
            try
            {
                var qry = new Update<T>(provider);

                //添加值
                foreach (KeyValuePair<string, object> keyValue in setValue)
                {
                    qry.Set(keyValue.Key).EqualTo(keyValue.Value);
                }

                //添加条件
                if (wheres != null)
                    qry.Constraints = ConditionHelper.SqlqueryCondition.Condition(wheres);

                var cmd = qry.GetCommand();

                //获取执行语句
                _execSql = cmd.CommandSql;

                return provider.ExecuteQuery(cmd);
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("更新数据异常,执行语句" + _execSql, e);
            }
        }


        /// <summary>
        /// 泛型式修改，只修改一个字段值——有条件
        /// </summary>
        /// <typeparam name="T">泛型集合对象</typeparam>
        /// <param name="column">要修改的字段名</param>
        /// <param name="setValue">要修改的值</param>
        /// <param name="wheres">修改条件</param>
        public int Update<T>(string column, object setValue, List<ConditionHelper.SqlqueryCondition> wheres = null) where T : IActiveRecord, new()
        {
            try
            {
                var qry = new Update<T>(provider).Set(column).EqualTo(setValue);

                //添加条件
                if (wheres != null)
                    qry.Constraints = ConditionHelper.SqlqueryCondition.Condition(wheres);

                var cmd = qry.GetCommand();

                //获取执行语句
                _execSql = cmd.CommandSql;

                return provider.ExecuteQuery(cmd);
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("更新数据异常,执行语句" + _execSql, e);
            }
        }
        #endregion

    }
}
