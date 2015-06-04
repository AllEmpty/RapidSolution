// 
//   SubSonic - http://subsonicproject.com
// 
//   The contents of this file are subject to the New BSD
//   License (the "License"); you may not use this file
//   except in compliance with the License. You may obtain a copy of
//   the License at http://www.opensource.org/licenses/bsd-license.php
//  
//   Software distributed under the License is distributed on an 
//   "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//   implied. See the License for the specific language governing
//   rights and limitations under the License.
// 
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using SubSonic.Extensions;
using SubSonic.DataProviders;
using SubSonic.Query;

namespace SubSonic.Schema
{
    public class StoredProcedure : IStoredProcedure
    {
        public StoredProcedure(string spName) : this(spName, ProviderFactory.GetProvider()) {}

        public StoredProcedure(string spName, IDataProvider provider)
        {
            Provider = provider;
            Command = new QueryCommand(spName, Provider)
                          {
                              CommandType = CommandType.StoredProcedure
                          };
        }

        public QueryCommand Command { get; private set; }

        public string ParameterName
        {
            get
            {
                const string paramFormat = "{0}{1}";
                return string.Format(paramFormat, Provider.ParameterPrefix, Name);
            }
        }


        #region IStoredProcedure Members

        public object Output { get; set; }

        public string SchemaName { get; set; }
        public string FriendlyName { get; set; }
        public string Name { get; set; }

        public string QualifiedName
        {
            get { return Provider.QualifySPName(this); }
        }

        public IDataProvider Provider { get; set; }

        #endregion


        #region Execution

        /// <summary>
        /// Executes the specified SQL.
        /// </summary>
        public void Execute()
        {
            Provider.ExecuteQuery(Command);
        }

        /// <summary>
        /// 修 改 人：Empty（AllEmpty）
        /// QQ    群：327360708
        /// 博客地址：http://www.cnblogs.com/EmptyFS/
        /// 修改时间：2014-06-27
        /// 功能说明：执行存储过程，返回OutputValues
        /// </summary>
        public List<object> ExecuteReturnValue()
        {
            Provider.ExecuteQuery(Command);
            return Command.OutputValues;
        }

		/// <summary>
		/// 修 改 人：Empty（AllEmpty）
		/// QQ    群：327360708
		/// 博客地址：http://www.cnblogs.com/EmptyFS/
		/// 修改时间：2013-07-20
		/// 功能说明：执行存储过程，返回首行首列值
		/// </summary>
		public object ExecuteScalar() {
			return Provider.ExecuteScalar(Command);
		}

		/// <summary>
		/// 修 改 人：Empty（AllEmpty）
		/// QQ    群：327360708
		/// 博客地址：http://www.cnblogs.com/EmptyFS/
		/// 修改时间：2013-07-20
		/// 功能说明：执行存储过程，返回指定的（泛）类型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T ExecuteSingle<T>() where T : new()
		{
			return Provider.ExecuteSingle<T>(Command);
		}

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <returns></returns>
        public TResult ExecuteScalar<TResult>()
        {
            TResult result = (TResult)Provider.ExecuteScalar(Command).ChangeTypeTo<TResult>();
            return result;
        }

        /// <summary>
        /// Executes the typed list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> ExecuteTypedList<T>() where T : new()
        {
            return Provider.ToList<T>(Command) as List<T>;
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <returns></returns>
        public DbDataReader ExecuteReader()
        {
            return Provider.ExecuteReader(Command);
        }

        public DataSet ExecuteDataSet()
        {
            return Provider.ExecuteDataSet(Command);
        }

        /// <summary>
        /// 修 改 人：Empty（AllEmpty）
        /// QQ    群：327360708
        /// 博客地址：http://www.cnblogs.com/EmptyFS/
        /// 修改时间：2014-03-06
        /// 功能说明：执行存储过程，返回DataTable
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTable()
        {
            using (var ds = Provider.ExecuteDataSet(Command))
            {
                if (ds != null)
                {
                    if (ds.Tables.Count <= 0)
                    {
                        ds.Dispose();
                        return null;
                    }

                    DataTable dt = ds.Tables[0];
                    ds.Dispose();
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}