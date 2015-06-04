using System;
using System.Collections.Generic;
using System.Data;
using DotNet.Utilities;
using SubSonic.DataProviders;
using SubSonic.Query;
using SubSonic.Schema;

/***********************************************************************
 *   作    者：Jonson（136646321）
 *   博    客：http://blog.sina.com.cn/u/2388032531
 *   技 术 群：327360708
 *  
 *   创建日期：2013-04-23
 *   文件名称：SelectHelper.cs
 *   描    述：记录查询类
 *             
 *   修 改 人：AllEmpty（陈焕）
 *   修改日期：2013-07
 *   修改原因：删除一些不必要的函数，优化代码，新增很多查询方法
 ***********************************************************************/
namespace Solution.DataAccess.DbHelper
{

    /// <summary>
    /// 记录查询类
    /// </summary>
    public class SelectHelper
    {
        #region 定义全局变量
        //定义数据源
        SubSonic.DataProviders.IDataProvider provider = null;

        /// <summary>
        /// 执行语句信息
        /// </summary>
        public string _executeInfo = "";
        //执行语句
        private string _execSql = "";
        /// <summary>
        /// 执行时的sql语句,允许类的内部读写操作，外部只能读取操作。
        /// </summary>
        public string Sql
        {
            //公共读取
            get { return _execSql; }
            //仅内部写入权限
            internal set { _execSql = value; }
        }
        /// <summary>
        /// 执行语句信息
        /// </summary>
        public string ExecuteInfo
        {
            get { return _executeInfo; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SelectHelper()
        {
            //获取数据源
            this.provider = SubSonic.DataProviders.ProviderFactory.GetProvider();
        }

        /// <summary>
        /// 有参构造函数
        /// </summary>
        public SelectHelper(IDataProvider provider)
        {
            //获取数据源
            this.provider = provider;
        }
        #endregion

        #region 方法

        #region 直接执行SQL
        /// <summary>
        /// 直接执行SQL，返回影响的行数
        /// </summary>
        /// <param name="sql"></param>
        public int ExecuteQuery(string sql)
        {
            try
            {
                //创建执行对象
                var q = new SubSonic.Query.QueryCommand(sql, provider);
                //获取执行语句
                _execSql = q.CommandSql;

                return q.Provider.ExecuteQuery(q);
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// 直接执行SQL，返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        public object ExecuteScalar(string sql)
        {
            try
            {
                //创建执行对象
                var q = new SubSonic.Query.QueryCommand(sql, provider);
                //获取执行语句
                _execSql = q.CommandSql;

                return q.Provider.ExecuteScalar(q);
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// 直接执行SQL，返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        public DataTable ExcuSQLDataTable(string sql)
        {
            try
            {
                //创建执行对象
                var q = new SubSonic.Query.QueryCommand(sql, provider);
                //获取执行语句
                _execSql = q.CommandSql;

                return q.Provider.ExecuteDataSet(q).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }
        #endregion
        
        #region 获取一个SubSonic.Query.Select
        /// <summary>
        /// 获取一个查询，有分页参数（SubSonic.Query.Select）
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="norepeat">是否去重复</param>
        /// <param name="top">查询条数，0=全部记录</param>
        /// <param name="columns">要显示的列</param>
        /// <param name="pageIndex">当前页面，等于0时表示不分页，即查询全部记录</param>
        /// <param name="pageSize">页面大小，即当前页面显示多少条记录</param>
        /// <param name="wheres">条件</param>
        /// <param name="sortList">排序</param>
        /// <returns>实体类</returns>
        public SubSonic.Query.Select Select<T>(bool norepeat = false, int top = 0, List<string> columns = null, int pageIndex = 1, int pageSize = 10, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sortList = null) where T : IActiveRecord, new()
        {
            try
            {
                SubSonic.Query.Select select = null;
                //判断是否查询指定的字段
                //指定列
                if (columns != null)
                {
                    select = new SubSonic.Query.Select(columns.ToArray());
                }
                    //全部字段
                else
                {
                    select = new SubSonic.Query.Select();
                }

                select.From<T>();

                //设置是否去重复
                select.Distinct(norepeat);

                //设置查询数量
                if (top > 0)
                {
                    select.Top(top.ToString());
                }

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //判断是否添加排序
                if (sortList != null)
                {
                    select.OrderBys = sortList;
                }

                //设置分页
                if (pageIndex > 0 && pageSize > 0)
                {
                    select.Paged(pageIndex, pageSize);
                }


                //获取执行语句
                _execSql = select.ToString();

                return select;
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("获取Select出现异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// 获取一个查询，无分页参数（SubSonic.Query.Select）
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="norepeat">是否去重复</param>
        /// <param name="top">查询条数，0=全部记录</param>
        /// <param name="columns">要显示的列</param>
        /// <param name="wheres">条件</param>
        /// <param name="sortList">排序</param>
        /// <returns>实体类</returns>
        public SubSonic.Query.Select Select<T>(bool norepeat = false, int top = 0, List<string> columns = null, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sortList = null) where T : IActiveRecord, new()
        {
            return Select<T>(norepeat, top, columns, 0, 0, wheres, sortList);
        }

        #endregion
        
        #region 获取实体（泛型）

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="norepeat">是否去重复</param>
        /// <param name="columns">要显示的列</param>
        /// <param name="wheres">条件</param>
        /// <param name="orderybys">排序</param>
        /// <returns>实体类</returns>
        public T SelectSingle<T>(bool norepeat = false, List<string> columns = null, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> orderybys = null) where T : IActiveRecord, new()
        {
            try
            {
                return Select<T>(norepeat, 0, columns, wheres, orderybys).ExecuteSingle<T>();
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("查询数据异常" + _execSql, e);
            }
        }

        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="select">SubSonic.Query.Select</param>
        /// <returns>实体类</returns>
        public T SelectSingle<T>(SubSonic.Query.Select select) where T : IActiveRecord, new()
        {
            try
            {
                return select.ExecuteSingle<T>();
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }

        #endregion
        
        #region 获取DataTable

        /// <summary>
        /// 获取一个DataTable
        /// </summary>
        /// <typeparam name="T">泛类型</typeparam>
        /// <param name="norepeat">是否去重复</param>
        /// <param name="top">查询条数，0=全部记录</param>
        /// <param name="columns">要显示的列，等于null时表示显示全部列</param>
        /// <param name="pageIndex">当前页面，等于0时表示不分页，即查询全部记录</param>
        /// <param name="pageSize">页面大小，即当前页面显示多少条记录</param>
        /// <param name="wheres">条件</param>
        /// <param name="sortList">排序</param>
        /// <returns>实体类</returns>
        public DataTable SelectDataTable<T>(bool norepeat = false, int top = 0, List<string> columns = null, int pageIndex = 0, int pageSize = 0, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sortList = null) where T : IActiveRecord, new()
        {
            try
            {
                var dt = Select<T>(norepeat, top, columns, pageIndex, pageSize, wheres, sortList).ExecuteDataTable();

                return dt;
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// 获取一个DataTable
        /// </summary>
        /// <param name="select">SubSonic.Query.Select</param>
        /// <returns>实体类</returns>
        public DataTable SelectDataTable(SubSonic.Query.Select select)
        {
            try
            {
                return select.ExecuteDataTable();
            }
            catch (Exception e)
            {
                //return 0;
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }
        #endregion

        #region 获取记录总数 GetRecordCount

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="wheres">条件</param>
        /// <param name="norepeat">是否去重复</param>
        /// <returns></returns>
        public int GetRecordCount<T>(List<ConditionHelper.SqlqueryCondition> wheres = null, bool norepeat = false) where T : IActiveRecord, new()
        {
            try
            {
                Aggregate[] aggregates = new Aggregate[1];
                aggregates[0] = new Aggregate("1", AggregateFunction.Count);

                SubSonic.Query.SqlQuery select = new Select(aggregates).From<T>();

                //设置是否去重复
                select.Distinct(norepeat);

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //获取执行语句
                _execSql = select.ToString();

                return ConvertHelper.Cint0(select.ExecuteScalar());
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }

        }
        #endregion
        
        #region 获取指定列合计 GetSum
        /// <summary>
        /// 获取指定列合计——多列时可以使用本函数 GetSum
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="wheres">条件</param>
        /// <param name="norepeat">是否去重复</param>
        /// <param name="colName">要合计的列</param>
        /// <returns>DataTable</returns>
        public DataTable GetSum<T>(List<ConditionHelper.SqlqueryCondition> wheres, bool norepeat, params string[] colName) where T : IActiveRecord, new()
        {
            if (colName == null || colName.Length == 0)
                return null;

            try
            {
                Aggregate[] aggregates = new Aggregate[colName.Length];

                for (int i = 0; i < colName.Length; i++)
                {
                    aggregates[i] = new Aggregate(colName[i], AggregateFunction.Sum);
                }

                SubSonic.Query.SqlQuery select = new Select(aggregates).From<T>();

                //设置是否去重复
                select.Distinct(norepeat);

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //获取执行语句
                _execSql = select.ToString();

                return select.ExecuteDataTable();
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }

        /// <summary>
        /// 获取指定列合计 GetSum
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="colName">要合计的列</param>
        /// <param name="wheres">条件</param>
        /// <param name="norepeat">是否去重复</param>
        /// <returns>合计后的数值</returns>
        public object GetSum<T>(string colName, List<ConditionHelper.SqlqueryCondition> wheres = null, bool norepeat = false) where T : IActiveRecord, new()
        {
            if (colName == null || colName.Length == 0)
                return 0;

            try
            {
                Aggregate[] aggregates = new Aggregate[1];
                aggregates[0] = new Aggregate(colName, AggregateFunction.Sum);

                SubSonic.Query.SqlQuery select = new Select(aggregates).From<T>();

                //设置是否去重复
                select.Distinct(norepeat);

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //获取执行语句
                _execSql = select.ToString();

                return select.ExecuteScalar();
            }
            catch (Exception) { throw; }

        }
        #endregion

        #region 获取最小值 GetMin

        /// <summary>
        /// 获取最小值 GetMin
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="colName">要获取最大值的列名</param>
        /// <param name="wheres">条件</param>
        /// <returns>合计后的数值</returns>
        public object GetMin<T>(string colName, List<ConditionHelper.SqlqueryCondition> wheres = null) where T : IActiveRecord, new()
        {
            if (colName == null || colName.Length == 0)
                return 0;

            try
            {
                Aggregate[] aggregates = new Aggregate[1];
                aggregates[0] = new Aggregate(colName, AggregateFunction.Min);

                SubSonic.Query.SqlQuery select = new Select(aggregates).From<T>();

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //获取执行语句
                _execSql = select.ToString();

                return select.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }
        #endregion

        #region 获取最大值 GetMax

        /// <summary>
        /// 获取最大值 GetMax
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="colName">要获取最大值的列名</param>
        /// <param name="wheres">条件</param>
        /// <returns>合计后的数值</returns>
        public object GetMax<T>(string colName, List<ConditionHelper.SqlqueryCondition> wheres = null) where T : IActiveRecord, new()
        {
            if (colName == null || colName.Length == 0)
                return 0;

            try
            {
                Aggregate[] aggregates = new Aggregate[1];
                aggregates[0] = new Aggregate(colName, AggregateFunction.Max);

                SubSonic.Query.SqlQuery select = new Select(aggregates).From<T>();

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //获取执行语句
                _execSql = select.ToString();

                return select.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }
        }
        #endregion
        
        #region 获取指定字段值

        /// <summary>
        /// 获取指定字段值
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="columnsName">列名</param>
        /// <param name="wheres">条件</param>
        /// <returns></returns>
        public object GetColumnsValue<T>(string columnsName, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> orderbys = null) where T : IActiveRecord, new()
        {
            object obj = null;

            try
            {
                SubSonic.Query.Select select = null;

                //指定列
                select = new SubSonic.Query.Select(columnsName);

                select.From<T>();

                //判断是否有条件
                if (wheres != null)
                {
                    //添加条件
                    ConditionHelper.SqlqueryCondition.AddSqlqueryCondition(select, wheres);
                }

                //判断是否添加排序
                if (orderbys != null)
                {
                    select.OrderBys = orderbys;
                }


                //获取执行语句
                _execSql = select.ToString();

                obj = select.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }

            return obj;
        }

        /// <summary>
        /// 获取指定字段值
        /// </summary>
        /// <typeparam name="T">泛类</typeparam>
        /// <param name="columnsName">列名</param>
        /// <param name="pkColumnsName">主键Id列名</param>
        /// <param name="pkValue">主键Id值</param>
        /// <returns></returns>
        public object GetColumnsValue<T>(string columnsName, string pkColumnsName, object pkValue) where T : IActiveRecord, new()
        {
            object obj = null;

            try
            {
                SubSonic.Query.Select select = null;

                //指定列
                select = new SubSonic.Query.Select(columnsName);
                //设置条件
                select.From<T>().Where(pkColumnsName).Equals(pkValue);

                //获取执行语句
                _execSql = select.ToString();

                obj = select.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw new Exception("查询数据异常,执行语句" + _execSql, e);
            }

            return obj;
        }
        #endregion

        #endregion


    }
}
