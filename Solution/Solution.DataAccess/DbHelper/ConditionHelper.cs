using System;
using System.Collections.Generic;
using SubSonic.Query;


/***********************************************************************
 *   作    者：Jonson（136646321）
 *   博    客：http://blog.sina.com.cn/u/2388032531
 *   技 术 群：327360708
 *  
 *   创建日期：2013-04-23
 *   文件名称：ConditionHelper.cs
 *   描    述：SubSonic插件Select与SqlQuery查询类的条件类，通过参数组合，生成两个查询类所需要的条件格式(Constraint)
 *             
 *   修 改 人：AllEmpty（陈焕）
 *   修改日期：2013-07
 *   修改原因：将功能进行扩展，以支持SubSonic3.0查询类
 *          
 *   修 改 人：AllEmpty（陈焕）
 *   修改日期：2013-09-12
 *   修改原因：修复无法使用in方式查询问题
 ***********************************************************************/
namespace Solution.DataAccess.DbHelper
{
    /// <summary>
    /// 条件类
    /// </summary>
    public class ConditionHelper
    {
		/// <summary>
		/// SqlQuery条件类
		/// </summary>
		public class SqlqueryCondition
		{
		    /// <summary>
		    /// SqlQuery条件类构造函数
		    /// </summary>
		    /// <param name="ctype">查询类型，包括：Where、And、Or</param>
		    /// <param name="columnname">条件列名</param>
		    /// <param name="cparsion">条件表达式类型</param>
		    /// <param name="value">条件值</param>
		    /// <param name="isParentheses">是否加左括号</param>
		    public SqlqueryCondition(ConstraintType ctype, string columnname, Comparison cparsion, object value, bool isParentheses = false) {
				SQConstraintType = ctype;
				SQColumnName = columnname;
				SQComparison = cparsion;
				SQValue = value;
				IsParentheses = isParentheses;
			}
			/// <summary>
			/// 添加右括号
			/// </summary>
			/// <param name="cparsion"></param>
			public SqlqueryCondition(Comparison cparsion = Comparison.CloseParentheses) {
				SQComparison = cparsion;
				IsParentheses = true;
			}

			private ConstraintType _sqConstraintType;
			/// <summary>
			/// 查询类型，包括：Where、And、Or
			/// </summary>
			public ConstraintType SQConstraintType {
				get { return _sqConstraintType; }
				set { _sqConstraintType = value; }
			}

			private String _sqColumnName;
			/// <summary>
			/// 条件列名
			/// </summary>
			public String SQColumnName {
				get { return _sqColumnName; }
				set { _sqColumnName = value; }
			}

			private Comparison _sqComparison;
			/// <summary>
			/// 条件表达式类型
			/// </summary>
			public Comparison SQComparison {
				get { return _sqComparison; }
				set { _sqComparison = value; }
			}

			private object _sqValue;
			/// <summary>
			/// 条件值
			/// </summary>
			public object SQValue {
				get { return _sqValue; }
				set { _sqValue = value; }
			}

			private bool _isParentheses = false;
			/// <summary>
			/// 加括号
			/// </summary>
			public bool IsParentheses {
				get { return _isParentheses; }
				set { _isParentheses = value; }
			}

			/// <summary>为SqlQuery添加条件
			/// </summary>
			/// <param name="sqlquery">SqlQuery查询对象</param>
			/// <param name="scd">SqlQuery条件实体</param>
			public static void AddSqlqueryCondition(SqlQuery sqlquery, List<SqlqueryCondition> scd) {
				if (scd == null)
					return;
				
				foreach (SqlqueryCondition item in scd) {
					//如果查询条件不为空
					if (item != null)
					{
						switch (item.SQComparison) {
							case Comparison.Equals:
								AddConstraint(sqlquery, item).IsEqualTo(item.SQValue);
								break;
							case Comparison.NotEquals:
								AddConstraint(sqlquery, item).IsNotEqualTo(item.SQValue);
								break;
							case Comparison.Like:
								AddConstraint(sqlquery, item).Like(item.SQValue.ToString());
								break;
							case Comparison.NotLike:
								AddConstraint(sqlquery, item).NotLike(item.SQValue.ToString());
								break;
							case Comparison.GreaterThan:
								AddConstraint(sqlquery, item).IsGreaterThan(item.SQValue);
								break;
							case Comparison.GreaterOrEquals:
								AddConstraint(sqlquery, item).IsGreaterThanOrEqualTo(item.SQValue);
								break;
							case Comparison.LessThan:
								AddConstraint(sqlquery, item).IsLessThan(item.SQValue);
								break;
							case Comparison.LessOrEquals:
								AddConstraint(sqlquery, item).IsLessThanOrEqualTo(item.SQValue);
								break;
							case Comparison.Is:
								AddConstraint(sqlquery, item).IsNull();
								break;
							case Comparison.IsNot:
								AddConstraint(sqlquery, item).IsNotNull();
								break;
							case Comparison.In:
                                /**
                                 * 由于底层插件使用In查询时，查询条件值在最终转换时老是出错，所以就将它转换成单个值拼起来的条件
                                 * For Empty 2013-09-12 
                                 */
                                if (item.SQValue != null) {
									try
									{
                                        //判断值是否是字符串数组
										if (item.SQValue.GetType() == typeof (string[]))
										{
                                            //遍历所有值
											var obj = (string[]) item.SQValue;
											for (int i = 0; i < obj.Length; i++)
											{
                                                //第一个值要加括号
												if (i == 0)
													AddConstraint(sqlquery, item, 1, true).IsEqualTo(obj[i]);
												else
												{
													//In查询时，第二个参数开始就不能再继续加括号了，所以设置为false
                                                    AddConstraint(sqlquery, item, 1).IsEqualTo(obj[i]);
												}
											}
										}
										else if (item.SQValue.GetType() == typeof(int[]))
										{
											var obj = (int[])item.SQValue;
											for (int i = 0; i < obj.Length; i++) {
												if (i == 0)
                                                    AddConstraint(sqlquery, item, 1, true).IsEqualTo(obj[i]);
												else {
													//In查询时，第二个参数开始就不能再继续加括号了，所以设置为false
                                                    AddConstraint(sqlquery, item, 1).IsEqualTo(obj[i]);
												}
											}
										}
										else
										{
											var obj = (object[])item.SQValue;
											for (int i = 0; i < obj.Length; i++) {
												if (i == 0)
                                                    AddConstraint(sqlquery, item, 1, true).IsEqualTo(obj[i]);
												else {
													//In查询时，第二个参数开始就不能再继续加括号了，所以设置为false
                                                    AddConstraint(sqlquery, item, 1).IsEqualTo(obj[i]);
												}
											}
										}
									}
									catch (Exception) {
										AddConstraint(sqlquery, item).In(item.SQValue);
									}
								}

								break;
                            case Comparison.NotIn:
                                /**
                                 * 由于底层插件使用NotIn查询时，查询条件值在最终转换时老是出错，所以就将它转换成单个值拼起来的条件
                                 * 使用“Comparison.NotIn”时，用“ConstraintType.And”拼接
                                 * For Guanghua 2013-12-16 
                                 */
                                if (item.SQValue != null)
                                {
                                    try
                                    {
                                        if (item.SQValue.GetType() == typeof(string[]))
                                        {
                                            var obj = (string[])item.SQValue;
                                            for (int i = 0; i < obj.Length; i++)
                                            {
                                                if (i == 0)
                                                    AddConstraint(sqlquery, item, 2, true).IsNotEqualTo(obj[i]);
                                                else
                                                {
                                                    //In查询时，第二个参数开始就不能再继续加括号了，所以设置为false
                                                    AddConstraint(sqlquery, item, 2).IsNotEqualTo(obj[i]);
                                                }
                                            }
                                        }
                                        else if (item.SQValue.GetType() == typeof(int[]))
                                        {
                                            var obj = (int[])item.SQValue;
                                            for (int i = 0; i < obj.Length; i++)
                                            {
                                                if (i == 0)
                                                    AddConstraint(sqlquery, item, 2, true).IsNotEqualTo(obj[i]);
                                                else
                                                {
                                                    //In查询时，第二个参数开始就不能再继续加括号了，所以设置为false
                                                    AddConstraint(sqlquery, item, 2).IsNotEqualTo(obj[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var obj = (object[])item.SQValue;
                                            for (int i = 0; i < obj.Length; i++)
                                            {
                                                if (i == 0)
                                                    AddConstraint(sqlquery, item, 2, true).IsNotEqualTo(obj[i]);
                                                else
                                                {
                                                    //In查询时，第二个参数开始就不能再继续加括号了，所以设置为false
                                                    AddConstraint(sqlquery, item, 2).IsNotEqualTo(obj[i]);
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        AddConstraint(sqlquery, item).NotIn(item.SQValue);
                                    }
                                }
                                //AddConstraint(sqlquery, item).NotIn(item.SQValue);
								break;
							case Comparison.OpenParentheses:
								AddConstraint(sqlquery, item).In(item.SQValue);
								break;
							case Comparison.CloseParentheses:
								sqlquery.CloseExpression();
								break;
							//case Comparison.BetweenAnd:
							//    AddConstraint(sqlquery, item).IsBetweenAnd(item.SQValue, item.SQValue2);
							//    break;
							case Comparison.StartsWith:
								AddConstraint(sqlquery, item).StartsWith(item.SQValue.ToString());
								break;
							case Comparison.EndsWith:
								AddConstraint(sqlquery, item).EndsWith(item.SQValue.ToString());
								break;
						}
					}
				}
			}

		    /// <summary>
		    /// 添加查询条件，如果存在括号时，则自动判断加上括号
		    /// </summary>
		    /// <param name="sqlquery">SqlQuery查询对象</param>
		    /// <param name="item">当前SqlQuery条件实体</param>
            /// <param name="type">查询类型，0=正常查询，1=in查询，2=not in查询</param>
            /// <param name="isFirst">是否是in查询的第一个值，第一个值要加括号</param>
		    /// <returns></returns>
            public static Constraint AddConstraint(SqlQuery sqlquery, SqlqueryCondition item, int type = 0, bool isFirst = false)
            {
                //是否是in查询
                if (type > 0)
		        {
		            //是否是第一个值
		            if (isFirst)
		            {
                        //判断是Where查询还是And查询
		                if (ConstraintType.Where.Equals(item.SQConstraintType) ||
		                    ConstraintType.And.Equals(item.SQConstraintType))
		                {
		                    return sqlquery.AndExpression(item.SQColumnName);
		                }
		                else
		                {
                            return sqlquery.OrExpression(item.SQColumnName);
		                }
		            }
		            else
		            {
                        //in查询中，每个值之间都是Or关系
                        if (type == 1)
                            return sqlquery.Or(item.SQColumnName);

                        //not in查询中，每个值之间都是And关系
                        else
                            return sqlquery.And(item.SQColumnName);
		            }
		        }
		        else
		        {
                    //判断是Where查询还是And查询
                    if (ConstraintType.Where.Equals(item.SQConstraintType) ||
                        ConstraintType.And.Equals(item.SQConstraintType))
                    {
                        //判断是否需要加括号
                        if (item.IsParentheses)
                        {
                            return sqlquery.AndExpression(item.SQColumnName);
                        }
                        else
                            return sqlquery.And(item.SQColumnName);
                    }
                    //使用Or查询
                    else
                    {
                        //判断是否需要加括号
                        if (item.IsParentheses)
                        {
                            return sqlquery.OrExpression(item.SQColumnName);
                        }
                        else
                            return sqlquery.Or(item.SQColumnName);
                    }
		        }
			}


			/// <summary>
			/// 生成SqlQuery类调用时所需要的条件格式——目前查询条件中不能使用括号，如果需要须修改代码
			/// </summary>
			/// <param name="scd">SqlQuery条件类</param>
			public static List<Constraint> Condition(List<SqlqueryCondition> scd) {
				if (scd == null)
					return null;

				var constrain = new List<Constraint>();

				foreach (SqlqueryCondition item in scd) {
					if (item != null) {
						var con = new Constraint(item.SQConstraintType, item.SQColumnName);
						con.Comparison = item.SQComparison;
						con.ParameterValue = item.SQValue;
						constrain.Add(con);
					}
				}

				return constrain;
			}
		}
	}
}
