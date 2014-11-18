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
using System;
using System.Text;
using SubSonic.DataProviders;
using SubSonic.Query;
using SubSonic.SqlGeneration;


namespace SubSonic.DataProviders.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    
    public class Sql2005Generator : ANSISqlGenerator
    {
		/*
		 * �� �� �ˣ�Empty��AllEmpty��
		 * QQ    Ⱥ��327360708
		 * ���͵�ַ��http://www.cnblogs.com/EmptyFS/
		 * �޸�ʱ�䣺2013-07-20
		 * �޸�˵���������ȥ�ظ�{7}���ܺ��޸���ʹ��TOP��ѯʱ{8}�������쳣������
		 *********************************************/
		private const string PAGING_SQL =
            @"
SELECT {7} *
FROM     (SELECT {8} ROW_NUMBER() OVER ({1}) AS Row, 
{0} 
{2}
{3}
{4}
)
            AS PagedResults
WHERE  Row >= {5} AND Row <= {6}";

        /// <summary>
        /// Initializes a new instance of the <see cref="Sql2005Generator"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public Sql2005Generator(SqlQuery query)
            : base(query) {
                ClientName = "System.Data.SqlClient";
        }

        /// <summary>
        /// ������ҳselect���
        /// Builds the paged select statement.
        /// </summary>
        /// <returns></returns>
        public override string BuildPagedSelectStatement()
        {
            Select qry = (Select)query;

            string idColumn = GetSelectColumns()[0];

            string select = GenerateCommandLine();
            string columnList = select.Replace("SELECT", String.Empty);
            string fromLine = GenerateFromList();
            string joins = GenerateJoins();
            string wheres = GenerateConstraints();
            string orderby = GenerateOrderBy();
			/*
			 * �޸�˵������ȡDistinct��ʶ
			 *********************************************/
			string distinct = GenerateDistinct();
			/*
			 * �޸�˵������ȡTop��ʶ
			 *********************************************/
            string top = GenerateTop();

            if(String.IsNullOrEmpty(orderby.Trim()))
                orderby = String.Concat(this.sqlFragment.ORDER_BY, idColumn);

            if(qry.Aggregates.Count > 0)
                joins = String.Concat(joins, GenerateGroupBy());

            int pageStart = (qry.CurrentPage - 1) * qry.PageSize + 1;
            int pageEnd = qry.CurrentPage * qry.PageSize;

			/*
			 * �޸�˵�������Distinct��Top��ʶ�滻
			 *********************************************/
			string sql = string.Format(PAGING_SQL, columnList, orderby, fromLine, joins, wheres, pageStart, pageEnd, distinct, top);
            return sql;
        }

        /// <summary>
        /// Builds the insert statement.
        /// </summary>
        /// <returns></returns>
        public override string BuildInsertStatement()
        {
            StringBuilder sb = new StringBuilder();

            //cast it
            Insert i = insert;
            sb.Append(this.sqlFragment.INSERT_INTO);
            sb.Append(i.Table.QualifiedName);
            sb.Append("(");
            sb.Append(i.SelectColumns);
            sb.AppendLine(")");

            //if the values list is set, use that
            if(i.Inserts.Count > 0)
            {
                sb.Append(" VALUES (");
                bool isFirst = true;
                foreach(InsertSetting s in i.Inserts)
                {
                    if(!isFirst)
                        sb.Append(",");
                    if(!s.IsExpression)
                        sb.Append(s.ParameterName);
                    else
                        sb.Append(s.Value);
                    isFirst = false;
                }
                sb.AppendLine(")");
            }
            else
            {
                throw new InvalidOperationException("Need to specify Values or a Select query to insert - can't go on!");
            }

            sb.AppendLine(";");
            sb.AppendLine("SELECT SCOPE_IDENTITY() as new_id");
            return sb.ToString();
        }
    }
}