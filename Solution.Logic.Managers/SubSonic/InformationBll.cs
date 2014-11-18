using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using DotNet.Utilities;
using Solution.DataAccess.DataModel;
using Solution.DataAccess.DbHelper;
using SubSonic.Query;

namespace Solution.Logic.Managers {
	/// <summary>
	/// Information表逻辑类
	/// </summary>
	public partial class InformationBll : LogicBase {
 
 		/***********************************************************************
		 * 模版生成函数                                                        *
		 ***********************************************************************/
		#region 模版生成函数
				
		private const string const_CacheKey = "Cache_Information";
        private const string const_CacheKey_Date = "Cache_Information_Date";

		#region 单例模式
		//定义单例实体
		private static InformationBll _InformationBll = null;

		/// <summary>
		/// 获取本逻辑类单例
		/// </summary>
		/// <returns></returns>
		public static InformationBll GetInstence() {
			if (_InformationBll == null) {
				_InformationBll = new InformationBll();
			}
			return _InformationBll;
		}
		#endregion
		
		#region 清空缓存
        /// <summary>清空缓存</summary>
        private void DelAllCache()
        {
            //清除模板缓存
            CacheHelper.RemoveOneCache(const_CacheKey);
			CacheHelper.RemoveOneCache(const_CacheKey_Date);

			//清除前台缓存
			CommonBll.RemoveCache(const_CacheKey);
			//运行自定义缓存清理程序
            DelCache();
        }
		#endregion

		#region IIS缓存函数
		
		#region 从IIS缓存中获取Information表记录
		/// <summary>
        /// 从IIS缓存中获取Information表记录
        /// </summary>
	    /// <param name="isCache">是否从缓存中读取</param>
        public IList<DataAccess.Model.Information> GetList(bool isCache = true)
        {
			try
			{
				//判断是否使用缓存
				if (CommonBll.IsUseCache() && isCache){
					//检查指定缓存是否过期——缓存当天有效，第二天自动清空
					if (CommonBll.CheckCacheIsExpired(const_CacheKey_Date)){		        
						//删除缓存
						DelAllCache();
					}

					//从缓存中获取DataTable
					var obj = CacheHelper.GetCache(const_CacheKey);
					//如果缓存为null，则查询数据库
					if (obj == null)
					{
						var list = GetList(false);

						//将查询出来的数据存储到缓存中
                        CacheHelper.SetCache(const_CacheKey, list);
						//存储当前时间
						CacheHelper.SetCache(const_CacheKey_Date, DateTime.Now);

                        return list;
					}
					//缓存中存在数据，则直接返回
					else
					{
						return (IList<DataAccess.Model.Information>)obj;
					}
				}
				else
				{
					//定义临时实体集
					IList<DataAccess.Model.Information> list = null;

					//获取全表缓存加载条件表达式
					var exp = GetExpression<Information>();
                    //如果条件为空，则查询全表所有记录
					if (exp == null)
					{
						//从数据库中获取所有记录
						var all = Information.All();
                        list = all == null ? null : Transform(all.ToList());
					}
					else
					{
                        //从数据库中查询出指定条件的记录，并转换为指定实体集
						var all = Information.Find(exp);
                        list = all == null ? null : Transform(all);
					}

					return list;
				}				
			}
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("从IIS缓存中获取Information表记录时出现异常", e);
			}
            
            return null;
        }
		#endregion

		#region 获取指定Id记录
		/// <summary>
        /// 获取指定Id记录
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="isCache">是否从缓存中读取</param>
		/// <returns>DataAccess.Model.Information</returns>
        public DataAccess.Model.Information GetModel(long id, bool isCache = true)
        {
            //判断是否使用缓存
		    if (CommonBll.IsUseCache() && isCache)
		    {
                //从缓存中获取List
		        var list = GetList();
		        if (list == null || list.Count == 0)
		        {
		            return null;
		        }
		        else
		        {
                    //在List查询指定主键Id的记录
		            return list.SingleOrDefault(x => x.Id == id);
		        }
		    }
		    else
		    {
                //从数据库中直接读取
                var model = Information.SingleOrDefault(x => x.Id == id);
                if (model == null)
                {
                    return null;
                }
                else
                {
                    //对查询出来的实体进行转换
                    return Transform(model);
                }
		    }
        }
		#endregion

		#region 从IIS缓存中获取指定Id记录
		/// <summary>
        /// 从IIS缓存中获取指定Id记录
        /// </summary>
        /// <param name="id">主键Id</param>
		/// <returns>DataAccess.Model.Information</returns>
        public DataAccess.Model.Information GetModelForCache(long id)
        {
			try
			{
				//从缓存中读取指定Id记录
                var model = GetModelForCache(x => x.Id == id);

				if (model == null){
					//从数据库中读取
					var tem = Information.SingleOrDefault(x => x.Id == id);
					if (tem == null)
					{
						return null;
					}
					else
					{
						//对查询出来的实体进行转换
						model = Transform(tem);
						SetModelForCache(model);
						return model;
					}
				}
				else
				{
					return model;
				}
			}
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("从IIS缓存中获取Information表记录时出现异常", e);

                return null;
            }
        }
		#endregion

		#region 从IIS缓存中获取指定条件的记录
        /// <summary>
        /// 从IIS缓存中获取指定条件的记录
        /// </summary>
        /// <param name="conditionColName">条件列名</param>
        /// <param name="value">条件值</param>
        /// <returns>DataAccess.Model.Information</returns>
        public DataAccess.Model.Information GetModelForCache(string conditionColName, object value)
        {
			try
            {
                //从缓存中获取List
                var list = GetList();
                DataAccess.Model.Information model = null;
                Expression<Func<Information, bool>> expression = null;

                //返回指定条件的实体
                switch (conditionColName)
                {
					case "Id" :
						model = list.SingleOrDefault(x => x.Id == (int)value);
                        expression = x => x.Id == (int)value;
                        break;
					case "InformationClass_Root_Id" :
						model = list.SingleOrDefault(x => x.InformationClass_Root_Id == (int)value);
                        expression = x => x.InformationClass_Root_Id == (int)value;
                        break;
					case "InformationClass_Root_Name" :
						model = list.SingleOrDefault(x => x.InformationClass_Root_Name == (string)value);
                        expression = x => x.InformationClass_Root_Name == (string)value;
                        break;
					case "InformationClass_Id" :
						model = list.SingleOrDefault(x => x.InformationClass_Id == (int)value);
                        expression = x => x.InformationClass_Id == (int)value;
                        break;
					case "InformationClass_Name" :
						model = list.SingleOrDefault(x => x.InformationClass_Name == (string)value);
                        expression = x => x.InformationClass_Name == (string)value;
                        break;
					case "Title" :
						model = list.SingleOrDefault(x => x.Title == (string)value);
                        expression = x => x.Title == (string)value;
                        break;
					case "RedirectUrl" :
						model = list.SingleOrDefault(x => x.RedirectUrl == (string)value);
                        expression = x => x.RedirectUrl == (string)value;
                        break;
					case "Content" :
						model = list.SingleOrDefault(x => x.Content == (string)value);
                        expression = x => x.Content == (string)value;
                        break;
					case "Upload" :
						model = list.SingleOrDefault(x => x.Upload == (string)value);
                        expression = x => x.Upload == (string)value;
                        break;
					case "FrontCoverImg" :
						model = list.SingleOrDefault(x => x.FrontCoverImg == (string)value);
                        expression = x => x.FrontCoverImg == (string)value;
                        break;
					case "Notes" :
						model = list.SingleOrDefault(x => x.Notes == (string)value);
                        expression = x => x.Notes == (string)value;
                        break;
					case "NewsTime" :
						model = list.SingleOrDefault(x => x.NewsTime == (DateTime)value);
                        expression = x => x.NewsTime == (DateTime)value;
                        break;
					case "Keywords" :
						model = list.SingleOrDefault(x => x.Keywords == (string)value);
                        expression = x => x.Keywords == (string)value;
                        break;
					case "SeoTitle" :
						model = list.SingleOrDefault(x => x.SeoTitle == (string)value);
                        expression = x => x.SeoTitle == (string)value;
                        break;
					case "SeoKey" :
						model = list.SingleOrDefault(x => x.SeoKey == (string)value);
                        expression = x => x.SeoKey == (string)value;
                        break;
					case "SeoDesc" :
						model = list.SingleOrDefault(x => x.SeoDesc == (string)value);
                        expression = x => x.SeoDesc == (string)value;
                        break;
					case "Author" :
						model = list.SingleOrDefault(x => x.Author == (string)value);
                        expression = x => x.Author == (string)value;
                        break;
					case "FromName" :
						model = list.SingleOrDefault(x => x.FromName == (string)value);
                        expression = x => x.FromName == (string)value;
                        break;
					case "Sort" :
						model = list.SingleOrDefault(x => x.Sort == (int)value);
                        expression = x => x.Sort == (int)value;
                        break;
					case "IsDisplay" :
						model = list.SingleOrDefault(x => x.IsDisplay == (byte)value);
                        expression = x => x.IsDisplay == (byte)value;
                        break;
					case "IsHot" :
						model = list.SingleOrDefault(x => x.IsHot == (byte)value);
                        expression = x => x.IsHot == (byte)value;
                        break;
					case "IsTop" :
						model = list.SingleOrDefault(x => x.IsTop == (byte)value);
                        expression = x => x.IsTop == (byte)value;
                        break;
					case "IsPage" :
						model = list.SingleOrDefault(x => x.IsPage == (byte)value);
                        expression = x => x.IsPage == (byte)value;
                        break;
					case "IsDel" :
						model = list.SingleOrDefault(x => x.IsDel == (byte)value);
                        expression = x => x.IsDel == (byte)value;
                        break;
					case "CommentCount" :
						model = list.SingleOrDefault(x => x.CommentCount == (int)value);
                        expression = x => x.CommentCount == (int)value;
                        break;
					case "ViewCount" :
						model = list.SingleOrDefault(x => x.ViewCount == (int)value);
                        expression = x => x.ViewCount == (int)value;
                        break;
					case "AddYear" :
						model = list.SingleOrDefault(x => x.AddYear == (int)value);
                        expression = x => x.AddYear == (int)value;
                        break;
					case "AddMonth" :
						model = list.SingleOrDefault(x => x.AddMonth == (int)value);
                        expression = x => x.AddMonth == (int)value;
                        break;
					case "AddDay" :
						model = list.SingleOrDefault(x => x.AddDay == (int)value);
                        expression = x => x.AddDay == (int)value;
                        break;
					case "AddDate" :
						model = list.SingleOrDefault(x => x.AddDate == (DateTime)value);
                        expression = x => x.AddDate == (DateTime)value;
                        break;
					case "Manager_Id" :
						model = list.SingleOrDefault(x => x.Manager_Id == (int)value);
                        expression = x => x.Manager_Id == (int)value;
                        break;
					case "Manager_CName" :
						model = list.SingleOrDefault(x => x.Manager_CName == (string)value);
                        expression = x => x.Manager_CName == (string)value;
                        break;
					case "UpdateDate" :
						model = list.SingleOrDefault(x => x.UpdateDate == (DateTime)value);
                        expression = x => x.UpdateDate == (DateTime)value;
                        break;

                    default :
                        return null;
                }

                if (model == null)
                {
                    //从数据库中读取
                    var tem = Information.SingleOrDefault(expression);
                    if (tem == null)
                    {
                        return null;
                    }
                    else
                    {
                        //对查询出来的实体进行转换
                        model = Transform(tem);
						SetModelForCache(model);
                        return model;
                    }
                }
                else
                {
                    return model;
                }
            }
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("从IIS缓存中获取Information表记录时出现异常", e);

                return null;
            }
        }
        #endregion

		#region 从IIS缓存中获取指定条件的记录
        /// <summary>
        /// 从IIS缓存中获取指定条件的记录
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>DataAccess.Model.Information</returns>
        public DataAccess.Model.Information GetModelForCache(Expression<Func<DataAccess.Model.Information, bool>> expression)
        {
			//从缓存中读取记录列表
			var list = GetList();
            //如果条件为空，则查询全表所有记录
            if (expression == null)
            {
                //查找并返回记录实体
                if (list == null || list.Count == 0)
                {
                    return null;
                }
                else
                {
                    return list.First();
                }
            }
            else
            {
                //查找并返回记录实体
                if (list == null || list.Count == 0)
                {
                    return null;
                }
                else
                {
					//先进行条件筛选，得出的数据，再取第一个
                    var tmp = list.AsQueryable().Where(expression);
                    if (tmp.Any())
                    {
                        return tmp.First();
                    }

                    return null;
                }
            }
        }
        #endregion

		#region 更新IIS缓存中指定Id记录
		/// <summary>
        /// 更新IIS缓存中指定Id记录
        /// </summary>
        /// <param name="model">记录实体</param>
        public void SetModelForCache(DataAccess.Model.Information model)
        {
			if (model == null) return;
			
            //从缓存中删除记录
            DelCache(model.Id);

            //从缓存中读取记录列表
            var list = GetList();
		    if (list == null)
		    {
                list = new List<DataAccess.Model.Information>();
		    }
            //添加记录
            list.Add(model);
        }

        /// <summary>
        /// 更新IIS缓存中指定Id记录
        /// </summary>
        /// <param name="model">记录实体</param>
        public void SetModelForCache(Information model)
        {
            SetModelForCache(Transform(model));
        }
		#endregion

		#region 删除IIS缓存中指定Id记录
        /// <summary>
        /// 删除IIS缓存中指定Id记录
        /// </summary>
        /// <param name="id">主键Id</param>
        public bool DelCache(long id)
        {
            //从缓存中获取List
            var list = GetList(true);
            if (list == null || list.Count == 0)
            {
                return false;
            }
            else
            {
                //找到指定主键Id的实体
                var model = list.SingleOrDefault(x => x.Id == id);
                //删除指定Id的记录
                return model != null && list.Remove(model);
            }
        }

		/// <summary>
        /// 批量删除IIS缓存中指定Id记录
        /// </summary>
        /// <param name="ids">主键Id</param>
        public void DelCache(IEnumerable ids)
        {
            //循环删除指定Id队列
		    foreach (var id in ids)
		    {
		        DelCache((int)id);
		    }
        }

		/// <summary>
        /// 按条件删除IIS缓存中Information表的指定记录
        /// </summary>
        /// <param name="expression">条件，值为null时删除全有记录</param>
		public void DelCache(Expression<Func<DataAccess.Model.Information, bool>> expression)
        {
            //从缓存中获取List
		    var list = GetList();
            //如果缓存为null，则不做任何处理
            if (list == null || list.Count == 0)
            {
                return;
            }

            //如果条件为空，则删除全部记录
            if (expression == null)
            {
                //删除所有记录
                DelAllCache();
            }
            else
            {
                var tem = list.AsQueryable().Where(expression);
                foreach (var model in tem)
                {
                    list.Remove(model);
                }
            }
        }
		#endregion

		#region 实体转换
		/// <summary>
		/// 将Information记录实体（SubSonic实体）转换为普通的实体（DataAccess.Model.Information）
		/// </summary>
        /// <param name="model">SubSonic插件生成的实体</param>
		/// <returns>DataAccess.Model.Information</returns>
		public DataAccess.Model.Information Transform(Information model)
        {			
			if (model == null) 
				return null;

            return new DataAccess.Model.Information
            {
                Id = model.Id,
                InformationClass_Root_Id = model.InformationClass_Root_Id,
                InformationClass_Root_Name = model.InformationClass_Root_Name,
                InformationClass_Id = model.InformationClass_Id,
                InformationClass_Name = model.InformationClass_Name,
                Title = model.Title,
                RedirectUrl = model.RedirectUrl,
                Content = model.Content,
                Upload = model.Upload,
                FrontCoverImg = model.FrontCoverImg,
                Notes = model.Notes,
                NewsTime = model.NewsTime,
                Keywords = model.Keywords,
                SeoTitle = model.SeoTitle,
                SeoKey = model.SeoKey,
                SeoDesc = model.SeoDesc,
                Author = model.Author,
                FromName = model.FromName,
                Sort = model.Sort,
                IsDisplay = model.IsDisplay,
                IsHot = model.IsHot,
                IsTop = model.IsTop,
                IsPage = model.IsPage,
                IsDel = model.IsDel,
                CommentCount = model.CommentCount,
                ViewCount = model.ViewCount,
                AddYear = model.AddYear,
                AddMonth = model.AddMonth,
                AddDay = model.AddDay,
                AddDate = model.AddDate,
                Manager_Id = model.Manager_Id,
                Manager_CName = model.Manager_CName,
                UpdateDate = model.UpdateDate,
            };
        }

		/// <summary>
		/// 将Information记录实体集（SubSonic实体）转换为普通的实体集（DataAccess.Model.Information）
		/// </summary>
        /// <param name="sourceList">SubSonic插件生成的实体集</param>
        public IList<DataAccess.Model.Information> Transform(IList<Information> sourceList)
        {
			//创建List容器
            var list = new List<DataAccess.Model.Information>();
			//将SubSonic插件生成的实体集转换后存储到刚创建的List容器中
            sourceList.ToList().ForEach(r => list.Add(Transform(r)));
            return list;
        }

		/// <summary>
		/// 将Information记录实体由普通的实体（DataAccess.Model.Information）转换为SubSonic插件生成的实体
		/// </summary>
        /// <param name="model">普通的实体（DataAccess.Model.Information）</param>
		/// <returns>Information</returns>
		public Information Transform(DataAccess.Model.Information model)
        {
			if (model == null) 
				return null;

            return new Information
            {
                Id = model.Id,
                InformationClass_Root_Id = model.InformationClass_Root_Id,
                InformationClass_Root_Name = model.InformationClass_Root_Name,
                InformationClass_Id = model.InformationClass_Id,
                InformationClass_Name = model.InformationClass_Name,
                Title = model.Title,
                RedirectUrl = model.RedirectUrl,
                Content = model.Content,
                Upload = model.Upload,
                FrontCoverImg = model.FrontCoverImg,
                Notes = model.Notes,
                NewsTime = model.NewsTime,
                Keywords = model.Keywords,
                SeoTitle = model.SeoTitle,
                SeoKey = model.SeoKey,
                SeoDesc = model.SeoDesc,
                Author = model.Author,
                FromName = model.FromName,
                Sort = model.Sort,
                IsDisplay = model.IsDisplay,
                IsHot = model.IsHot,
                IsTop = model.IsTop,
                IsPage = model.IsPage,
                IsDel = model.IsDel,
                CommentCount = model.CommentCount,
                ViewCount = model.ViewCount,
                AddYear = model.AddYear,
                AddMonth = model.AddMonth,
                AddDay = model.AddDay,
                AddDate = model.AddDate,
                Manager_Id = model.Manager_Id,
                Manager_CName = model.Manager_CName,
                UpdateDate = model.UpdateDate,
            };
        }

		/// <summary>
		/// 将Information记录实体由普通实体集（DataAccess.Model.Information）转换为SubSonic插件生成的实体集
		/// </summary>
        /// <param name="sourceList">普通实体集（DataAccess.Model.Information）</param>
        public IList<Information> Transform(IList<DataAccess.Model.Information> sourceList)
        {
			//创建List容器
            var list = new List<Information>();
			//将普通实体集转换后存储到刚创建的List容器中
            sourceList.ToList().ForEach(r => list.Add(Transform(r)));
            return list;
        }
		#endregion

		#region 给实体赋值
		/// <summary>
        /// 给实体赋值
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="dic">列名与值</param>
		public void SetModelValue(DataAccess.Model.Information model, Dictionary<string, object> dic)
		{
			if (model == null || dic == null) return;

            //遍历字典，逐个给字段赋值
            foreach (var d in dic)
            {
                SetModelValue(model, d.Key, d.Value);
            }
		}

        /// <summary>
        /// 给实体赋值
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="colName">列名</param>
        /// <param name="value">值</param>
		public void SetModelValue(DataAccess.Model.Information model, string colName, object value)
		{
			if (model == null || string.IsNullOrEmpty(colName)) return;

			//返回指定条件的实体
            switch (colName)
            {
				case "Id" :
					model.Id = (int)value;
                    break;
				case "InformationClass_Root_Id" :
					model.InformationClass_Root_Id = (int)value;
                    break;
				case "InformationClass_Root_Name" :
					model.InformationClass_Root_Name = (string)value;
                    break;
				case "InformationClass_Id" :
					model.InformationClass_Id = (int)value;
                    break;
				case "InformationClass_Name" :
					model.InformationClass_Name = (string)value;
                    break;
				case "Title" :
					model.Title = (string)value;
                    break;
				case "RedirectUrl" :
					model.RedirectUrl = (string)value;
                    break;
				case "Content" :
					model.Content = (string)value;
                    break;
				case "Upload" :
					model.Upload = (string)value;
                    break;
				case "FrontCoverImg" :
					model.FrontCoverImg = (string)value;
                    break;
				case "Notes" :
					model.Notes = (string)value;
                    break;
				case "NewsTime" :
					model.NewsTime = (DateTime)value;
                    break;
				case "Keywords" :
					model.Keywords = (string)value;
                    break;
				case "SeoTitle" :
					model.SeoTitle = (string)value;
                    break;
				case "SeoKey" :
					model.SeoKey = (string)value;
                    break;
				case "SeoDesc" :
					model.SeoDesc = (string)value;
                    break;
				case "Author" :
					model.Author = (string)value;
                    break;
				case "FromName" :
					model.FromName = (string)value;
                    break;
				case "Sort" :
					model.Sort = (int)value;
                    break;
				case "IsDisplay" :
					model.IsDisplay = ConvertHelper.Ctinyint(value);
                    break;
				case "IsHot" :
					model.IsHot = ConvertHelper.Ctinyint(value);
                    break;
				case "IsTop" :
					model.IsTop = ConvertHelper.Ctinyint(value);
                    break;
				case "IsPage" :
					model.IsPage = ConvertHelper.Ctinyint(value);
                    break;
				case "IsDel" :
					model.IsDel = ConvertHelper.Ctinyint(value);
                    break;
				case "CommentCount" :
					model.CommentCount = (int)value;
                    break;
				case "ViewCount" :
					model.ViewCount = (int)value;
                    break;
				case "AddYear" :
					model.AddYear = (int)value;
                    break;
				case "AddMonth" :
					model.AddMonth = (int)value;
                    break;
				case "AddDay" :
					model.AddDay = (int)value;
                    break;
				case "AddDate" :
					model.AddDate = (DateTime)value;
                    break;
				case "Manager_Id" :
					model.Manager_Id = (int)value;
                    break;
				case "Manager_CName" :
					model.Manager_CName = (string)value;
                    break;
				case "UpdateDate" :
					model.UpdateDate = (DateTime)value;
                    break;
            }
		}

        #endregion

		#endregion

		#region 获取Information表记录总数
        /// <summary>
        /// 获取Information表记录总数
        /// </summary>
        /// <returns>记录总数</returns>
        public int GetRecordCount()
        {
            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
				//从缓存中获取记录集
                var list = GetList();
                return list == null ? 0 : list.Count;
            }
			else
			{
				//从数据库中查询记录集数量
				var select = new SelectHelper();
				return select.GetRecordCount<Information>();
			}
        }

		/// <summary>
		/// 获取Information表记录总数——从数据库中查询
		/// </summary>
        /// <param name="wheres">条件</param>
		/// <returns>int</returns>
		public int GetRecordCount(List<ConditionHelper.SqlqueryCondition> wheres) {
			var select = new SelectHelper();
			return select.GetRecordCount<Information>(wheres);

		}

		/// <summary>
		/// 获取Information表指定条件的记录总数——从数据库中查询
		/// </summary>
        /// <param name="expression">条件</param>
		/// <returns>int</returns>
		public int GetRecordCount(Expression<Func<Information, bool>> expression) {
            return new Select().From<Information>().Where(expression).GetRecordCount();
		}

        #endregion

		#region 查找指定条件的记录集合
        /// <summary>
        /// 查找指定条件的记录集合——从IIS缓存中查找
        /// </summary>
        /// <param name="expression">条件语句</param>
        public IList<DataAccess.Model.Information> Find(Expression<Func<DataAccess.Model.Information, bool>> expression)
        {
			//从缓存中获取记录集
			var list = GetList();
            //判断获取记录集是否为null
            if (list == null)
            {
                return null;
            }
            else
            {
                //在返回的记录集中查询
                var result = list.AsQueryable().Where(expression);
                //不存在指定记录集
                if (!result.Any())
                    return null;
                else
                    return result.ToList();
            }
        }
		#endregion

		#region 判断指定条件的记录是否存在
        /// <summary>
        /// 判断指定主键Id的记录是否存在——在IIS缓存或数据库中查找
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        public bool Exist(int id)
        {
            if (id <= 0)
                return false;

            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                return Exist(x => x.Id == id);
            }
            
            //从数据库中查找
            return Information.Exists(x => x.Id == id);
        }

        /// <summary>
        /// 判断指定条件的记录是否存在——默认在IIS缓存中查找，如果没开启缓存时，则直接在数据库中查询出列表后，再从列表中查询
        /// </summary>
        /// <param name="expression">条件语句</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<DataAccess.Model.Information, bool>> expression)
        {
            var list = GetList();
            if (list == null) 
                return false;
            else
            {
                return list.AsQueryable().Any(expression);
            }
        }
        #endregion

		#region 获取Information表记录
		/// <summary>
		/// 获取Information表记录
		/// </summary>
		/// <param name="norepeat">是否使用去重复</param>
		/// <param name="top">获取指定数量记录</param>
		/// <param name="columns">获取指定的列记录</param>
		/// <param name="pageIndex">当前分页页面索引</param>
		/// <param name="pageSize">每个页面记录数量</param>
		/// <param name="wheres">查询条件</param>
		/// <param name="sorts">排序方式</param>
        /// <returns>返回DataTable</returns>
		public DataTable GetDataTable(bool norepeat = false, int top = 0, List<string> columns = null, int pageIndex = 0, int pageSize = 0, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sorts = null) {
			try
            {
                //分页查询
                var select = new SelectHelper();
                return select.SelectDataTable<Information>(norepeat, top, columns, pageIndex, pageSize, wheres, sorts);
            }
            catch (Exception e)
            {
                //记录日志
                CommonBll.WriteLog("获取Information表记录时出现异常", e);

                return null;
            }
		}
		#endregion

		#region 绑定Grid表格
		/// <summary>
		/// 绑定Grid表格，并实现分页
		/// </summary>
		/// <param name="grid">表格控件</param>
		/// <param name="pageIndex">第几页</param>
		/// <param name="pageSize">每页显示记录数量</param>
		/// <param name="wheres">查询条件</param>
		/// <param name="sorts">排序</param>
		public override void BindGrid(FineUI.Grid grid, int pageIndex = 0, int pageSize = 0, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sorts = null) {
			//用于统计执行时长(耗时)
			var swatch = new Stopwatch();
			swatch.Start();

			try {
				// 1.设置总项数
				grid.RecordCount = GetRecordCount(wheres);
				// 2.如果不存在记录，则清空Grid表格
				if (grid.RecordCount == 0) {
					grid.Rows.Clear();
					// 查询并绑定到Grid
                    grid.DataBind();
                    grid.AllowPaging = false;
				}
				else
				{
					//3.查询并绑定到Grid
					grid.DataSource = GetDataTable(false, 0, null, pageIndex, pageSize, wheres, sorts);
					grid.DataBind();
				}
			}
			catch (Exception e) {
				// 记录日志
				CommonBll.WriteLog("获取用户操作日志表记录时出现异常", e);

			}

			// 统计结束
			swatch.Stop();
			// 计算查询数据库使用时间，并存储到Session里，以便UI显示
			HttpContext.Current.Session["SpendingTime"] = (swatch.ElapsedMilliseconds / 1000.00).ToString();
		}
		#endregion

		#region 绑定Grid表格
		/// <summary>
		/// 绑定Grid表格，使用内存分页，显示有层次感
		/// </summary>
		/// <param name="grid">表格控件</param>
		/// <param name="parentValue">父Id值</param>
		/// <param name="wheres">查询条件</param>
		/// <param name="sorts">排序</param>
		/// <param name="parentId">父Id</param>
		public override void BindGrid(FineUI.Grid grid, int parentValue, List<ConditionHelper.SqlqueryCondition> wheres = null, List<string> sorts = null, string parentId = "ParentId") {
			//用于统计执行时长(耗时)
			var swatch = new Stopwatch();
			swatch.Start();

			try
			{
				// 查询数据库
				var dt = GetDataTable(false, 0, null, 0, 0, wheres, sorts);
                
                // 1.设置总项数
                grid.RecordCount = dt == null ? 0 : dt.Rows.Count;
                // 2.如果不存在记录，则清空Grid表格
                if (grid.RecordCount == 0)
                {
                    grid.Rows.Clear();
                    // 查询并绑定到Grid
                    grid.DataBind();
                    grid.AllowPaging = false;
                }
                else
                {
                    // 对查询出来的记录进行层次处理
                    grid.DataSource = DataTableHelper.DataTableTidyUp(dt, "Id", parentId, parentValue);
                    // 查询并绑定到Grid
                    grid.DataBind();
                    grid.AllowPaging = true;
                }
			}
			catch (Exception e) {
				// 记录日志
				CommonBll.WriteLog("绑定表格时出现异常", e);

			}

			//统计结束
			swatch.Stop();
			//计算查询数据库使用时间，并存储到Session里，以便UI显示
			HttpContext.Current.Session["SpendingTime"] = (swatch.ElapsedMilliseconds / 1000.00).ToString();
		}

		/// <summary>
		/// 绑定Grid表格，使用内存分页，显示有层次感
		/// </summary>
		/// <param name="grid">表格控件</param>
		/// <param name="parentValue">父Id值</param>
		/// <param name="sorts">排序</param>
		/// <param name="parentId">父Id</param>
		public override void BindGrid(FineUI.Grid grid, int parentValue, List<string> sorts = null, string parentId = "ParentId") {
			BindGrid(grid, parentValue, null, sorts, parentId);
		}
		#endregion

		#region 添加与编辑Information表记录
		/// <summary>
		/// 添加与编辑Information记录
		/// </summary>
	    /// <param name="page">当前页面指针</param>
		/// <param name="model">Information表实体</param>
        /// <param name="content">更新说明</param>
        /// <param name="isCache">是否更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void Save(Page page, Information model, string content = null, bool isCache = true, bool isAddUseLog = true)
        {
			try {
				//保存
				model.Save();
				
				//判断是否启用缓存
			    if (CommonBll.IsUseCache() && isCache)
			    {
                    SetModelForCache(model);
			    }
				
				if (isAddUseLog)
				{
					if (string.IsNullOrEmpty(content))
					{
						content = "{0}" + (model.Id == 0 ? "添加" : "编辑") + "Information记录成功，ID为【" + model.Id + "】";
					}

					//添加用户访问记录
					UseLogBll.GetInstence().Save(page, content);
				}
			}
			catch (Exception e) {
				var result = "执行InformationBll.Save()函数出错！";

				//出现异常，保存出错日志信息
				CommonBll.WriteLog(result, e);
			}
		}
		#endregion

		#region 删除Information表记录
		/// <summary>
		/// 删除Information表记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public override void Delete(Page page, int id, bool isAddUseLog = true) 
		{
			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} = {2}", InformationTable.TableName,  InformationTable.Id, id);

			//删除
			var delete = new DeleteHelper();
		    delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //删除缓存
                DelCache(id);
            }
			
			if (isAddUseLog)
		    {
				//添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}删除了Information表id为【" + id + "】的记录！");
			}
		}

		/// <summary>
		/// 删除Information表记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public override void Delete(Page page, int[] id, bool isAddUseLog = true) 
		{
			if (id == null) return;
			//将数组转为逗号分隔的字串
			var str = string.Join(",", id);

			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} in ({2})", InformationTable.TableName,  InformationTable.Id, str);

			//删除
			var delete = new DeleteHelper();
		    delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //删除缓存
                DelCache(id.ToList());
            }
			
			if (isAddUseLog)
		    {
				//添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}删除了Information表id为【" + str + "】的记录！");
			}
		}

		/// <summary>
        /// 删除Information表记录——如果使用了缓存，删除成功后会清空本表的所有缓存记录，然后重新加载进缓存
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="expression">条件语句</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void Delete(Page page, Expression<Func<Information, bool>> expression, bool isAddUseLog = true)
        {
			//执行删除
			Information.Delete(expression);

            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
				//清空当前表所有缓存记录
				DelAllCache();
                //重新载入缓存
                GetList();
            }
			
			if (isAddUseLog)
		    {
				//添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}删除了Information表记录！");
			}
        }

		/// <summary>
        /// 删除Information表所有记录
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void DeleteAll(Page page, bool isAddUseLog = true)
        {
            //设置Sql语句
            var sql = string.Format("delete from {0}", InformationTable.TableName);

            //删除
            var delete = new DeleteHelper();
            delete.Delete(sql);

            //判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //清空当前表所有缓存记录
                DelAllCache();
            }

            if (isAddUseLog)
            {
                //添加用户操作记录
                UseLogBll.GetInstence().Save(page, "{0}删除了Information表所有记录！");
            }
        }
		#endregion
		
		#region 保存列表排序
		/// <summary>
		/// 保存列表排序，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="grid1">页面表格</param>
		/// <param name="tbxSort">表格中绑定排序的表单名</param>
		/// <param name="sortName">排序字段名</param>
		/// <returns>更新成功返回true，失败返回false</returns>
		public override bool UpdateSort(Page page, FineUI.Grid grid1, string tbxSort, string sortName = "Sort")
	    {
		     //更新排序
			if (CommonBll.UpdateSort(page, grid1, tbxSort, "Information", sortName, "Id"))
		    {
				//判断是否启用缓存
                if (CommonBll.IsUseCache())
                {
                    //删除所有缓存
                    DelAllCache();
                    //重新载入缓存
                    GetList();
                }
				
			    //添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}更新了Information表排序！");

			    return true;
		    }

			return false;
	    }
		#endregion

		#region 自动排序
		/// <summary>自动排序，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="strWhere">附加Where : " sid=1 "</param>
		/// <param name="isExistsMoreLv">是否存在多级分类,一级时,请使用false,多级使用true，(一级不包括ParentID字段)</param>
		/// <param name="pid">父级分类的ParentID</param>
		/// <param name="fieldName">字段名:"SortId"</param>
		/// <param name="fieldParentId">字段名:"ParentId"</param>
		/// <returns>更新成功返回true，失败返回false</returns>
		public override bool UpdateAutoSort(Page page, string strWhere = "", bool isExistsMoreLv = false, int pid = 0, string fieldName = "Sort", string fieldParentId = "ParentId")
	    {
		    //更新排序
			if (CommonBll.AutoSort("Id", "Information", strWhere, isExistsMoreLv, pid, fieldName, fieldParentId))
		    {
				//判断是否启用缓存
                if (CommonBll.IsUseCache())
                {
                    //删除所有缓存
                    DelAllCache();
                    //重新载入缓存
                    GetList();
                }

			    //添加用户操作记录
				UseLogBll.GetInstence().Save(page, "{0}对Information表进行了自动排序操作！");

			    return true;
		    }

			return false;
	    }
		#endregion
		
		#region 获取数据表中的某个值
		/// <summary>
        /// 获取数据表中的某个值——主要用于内存查询，数据量大的表请将isCache设为false
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="colName">获取的列名</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns>指定列的值</returns>
        public object GetFieldValue(int id, string colName, bool isCache = true)
	    {
			return GetFieldValue(colName, null, id, isCache);            
	    }

	    /// <summary>
        /// 获取数据表中的某个值——主要用于内存查询，数据量大的表请将isCache设为false
	    /// </summary>
	    /// <param name="colName">获取的列名</param>
	    /// <param name="conditionColName">条件列名，为null时默认为主键Id</param>
	    /// <param name="value">条件值</param>
	    /// <param name="isCache">是否从缓存中读取</param>
	    /// <returns></returns>
	    public object GetFieldValue(string colName, string conditionColName, object value, bool isCache = true)
	    {
            //在内存中查询
	        if (isCache)
	        {
                //判断是否启用缓存
                if (CommonBll.IsUseCache())
                {
                    //如果条件列为空，则默认为主键列
                    if (string.IsNullOrEmpty(conditionColName))
                    {
                        //获取实体
                        var model = GetModelForCache(ConvertHelper.Cint0(value));
                        //返回指定字段名的值
                        return GetFieldValue(model, colName);
                    }
                    else
                    {
                        //获取实体
                        var model = GetModelForCache(conditionColName, value);
                        //返回指定字段名的值
                        return GetFieldValue(model, colName);
                    }
                }

				//递归调用，从数据库中查询
	            return GetFieldValue(colName, conditionColName, value, false);
	        }
            //从数据库中查询
	        else
	        {
				if (string.IsNullOrEmpty(conditionColName))
	            {
	                conditionColName = InformationTable.Id;
	            }

                //设置条件
                var wheres = new List<ConditionHelper.SqlqueryCondition>();
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, conditionColName, Comparison.Equals, value));

                return GetFieldValue(colName, wheres);
	        }
	    }
		
	    /// <summary>
        /// 获取数据表中的某个值——使用IIS缓存查询
        /// </summary>
        /// <param name="colName">获取的列名</param>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        public object GetFieldValue(string colName, Expression<Func<DataAccess.Model.Information, bool>> expression)
	    {
	        return GetFieldValue(GetModelForCache(expression), colName);
	    }

	    /// <summary>
        /// 获取数据表中的某个值——从数据库中查询
        /// </summary>
        /// <param name="colName">获取的列名</param>
        /// <param name="wheres">条件，例：Id=100 and xx=20</param>
        /// <returns></returns>
        public object GetFieldValue(string colName, string wheres)
        {
            try
            {
                return DataTableHelper.DataTable_Find_Value(GetDataTable(), wheres, colName);
			}
			catch (Exception e)
			{
                //记录日志
                CommonBll.WriteLog("查询数据出现异常", e);
			}

            return null;
        }

        /// <summary>
        /// 获取数据表中的某个值——从数据库中查询
        /// </summary>
        /// <param name="colName">获取的列名</param>
        /// <param name="wheres">条件</param>
        /// <returns></returns>
        public object GetFieldValue(string colName, List<ConditionHelper.SqlqueryCondition> wheres)
        {
            var select = new SelectHelper();
            return select.GetColumnsValue<Information>(colName, wheres);
        }

		/// <summary>
        /// 返回实体中指定字段名的值
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="colName">获取的字段名</param>
        /// <returns></returns>
		private object GetFieldValue(DataAccess.Model.Information model, string colName)
		{
			if (model == null || string.IsNullOrEmpty(colName)) return null;
			//返回指定的列值
			switch (colName)
			{
				case "Id" :
					return model.Id;
				case "InformationClass_Root_Id" :
					return model.InformationClass_Root_Id;
				case "InformationClass_Root_Name" :
					return model.InformationClass_Root_Name;
				case "InformationClass_Id" :
					return model.InformationClass_Id;
				case "InformationClass_Name" :
					return model.InformationClass_Name;
				case "Title" :
					return model.Title;
				case "RedirectUrl" :
					return model.RedirectUrl;
				case "Content" :
					return model.Content;
				case "Upload" :
					return model.Upload;
				case "FrontCoverImg" :
					return model.FrontCoverImg;
				case "Notes" :
					return model.Notes;
				case "NewsTime" :
					return model.NewsTime;
				case "Keywords" :
					return model.Keywords;
				case "SeoTitle" :
					return model.SeoTitle;
				case "SeoKey" :
					return model.SeoKey;
				case "SeoDesc" :
					return model.SeoDesc;
				case "Author" :
					return model.Author;
				case "FromName" :
					return model.FromName;
				case "Sort" :
					return model.Sort;
				case "IsDisplay" :
					return model.IsDisplay;
				case "IsHot" :
					return model.IsHot;
				case "IsTop" :
					return model.IsTop;
				case "IsPage" :
					return model.IsPage;
				case "IsDel" :
					return model.IsDel;
				case "CommentCount" :
					return model.CommentCount;
				case "ViewCount" :
					return model.ViewCount;
				case "AddYear" :
					return model.AddYear;
				case "AddMonth" :
					return model.AddMonth;
				case "AddDay" :
					return model.AddDay;
				case "AddDate" :
					return model.AddDate;
				case "Manager_Id" :
					return model.Manager_Id;
				case "Manager_CName" :
					return model.Manager_CName;
				case "UpdateDate" :
					return model.UpdateDate;
			}

			return null;
		}

		#endregion
		
		#region 更新Information表指定字段值
		/// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="dic">需要更新的字段与值</param>
		/// <param name="wheres">条件</param>
		/// <param name="content">更新说明</param>
		/// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateValue(Page page, Dictionary<string, object> dic, List<ConditionHelper.SqlqueryCondition> wheres = null, string content = "", bool isCache = true, bool isAddUseLog = true) {
			//更新
			var update = new UpdateHelper();
			update.Update<Information>(dic, wheres);

			//判断是否启用缓存
			if (isCache && CommonBll.IsUseCache())
			{
				//删除全部缓存	
				DelAllCache();
				//重新载入缓存
				GetList();
			}
			
			if (isAddUseLog){
				if (string.IsNullOrEmpty(content))
				{
					//添加用户操作记录
					UseLogBll.GetInstence().Save(page, content != "" ? content : "{0}修改了Information表记录。");				
				}
				else
				{
					//添加用户操作记录
					UseLogBll.GetInstence().Save(page, content);
				}
			}
		}
		#endregion
				
		#region 更新Information表指定主键Id的字段值
		/// <summary>更新Information表记录指定字段值</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="id">主键Id，当小于等于0时，则更新所有记录</param>
	    /// <param name="dic">需要更新的字段与值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue(Page page, int id, Dictionary<string, object> dic, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表主键Id值为" + id + "的记录。";
			
            //条件
		    List<ConditionHelper.SqlqueryCondition> wheres = null;
            if (id > 0)
            {
                wheres = new List<ConditionHelper.SqlqueryCondition>();
                wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.Id, Comparison.Equals, id));
            };

			//判断是否启用缓存——为了防止并发问题，所以先更新缓存再更新数据库
			if (isCache && CommonBll.IsUseCache())
			{
				//从缓存中获取实体
				var model = GetModelForCache(id);
				if (model != null)
				{
					//给获取的实体赋值
					SetModelValue(model, dic);
					//更新缓存中的实体
					SetModelForCache(model);
				}
			}

            //执行更新
            UpdateValue(page, dic, wheres, content, false, isAddUseLog);
        }

        /// <summary>更新Information表记录指定字段值（更新一个字段值）</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="id">主键Id，当小于等于0时，则更新所有记录</param>
        /// <param name="columnName">要更新的列名</param>
        /// <param name="columnValue">要更新的列值</param>
        /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void UpdateValue(Page page, int id, string columnName, object columnValue, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
            content = content != "" ? content : "{0}修改了Information表主键Id值为" + id + "的记录，将" + columnName + "字段值修改为" + columnValue;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName, columnValue);

			//执行更新
            UpdateValue(page, id, dic, content, isCache, isAddUseLog);
        }

		 /// <summary>更新Information表记录指定字段值（更新两个字段值）</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="id">主键Id，当小于等于0时，则更新所有记录</param>
        /// <param name="columnName1">要更新的列名</param>
        /// <param name="columnValue1">要更新的列值</param>
        /// <param name="columnName2">要更新的列名</param>
        /// <param name="columnValue2">要更新的列值</param>
        /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
        public void UpdateValue(Page page, int id, string columnName1, object columnValue1, string columnName2, object columnValue2, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
            content = content != "" ? content : "{0}修改了Information表主键Id值为" + id + "的记录，将" + columnName1 + "字段值修改为" + columnValue1 + "，" + columnName2 + "字段值修改为" + columnValue2;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName1, columnValue1);
            dic.Add(columnName2, columnValue2);

			//执行更新
            UpdateValue(page, id, dic, content, isCache, isAddUseLog);
        }
        #endregion
		
		#region 删除Information表指定InformationClass_Root_Id的字段值记录
		/// <summary>
		/// 删除Information表指定InformationClass_Root_Id的字段值记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		public void DeleteByInformationClass_Root_Id(Page page, int id) {
			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} = {2}", InformationTable.TableName, InformationTable.InformationClass_Root_Id, id);

			//删除
			var delete = new DeleteHelper();
            delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //删除缓存
                DelCache(x => x.InformationClass_Root_Id == id);
            }
			
			//添加用户操作记录
			UseLogBll.GetInstence().Save(page, "{0}删除了Information表InformationClass_Root_Id值为【" + id + "】的记录！");
		}

		/// <summary>
		/// 删除Information表指定InformationClass_Root_Id的字段值记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		public void DeleteByInformationClass_Root_Id(Page page, int[] id) {
			if (id == null) return;
			//将数组转为逗号分隔的字串
			var str = string.Join(",", id);

			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} in ({2})", InformationTable.TableName, InformationTable.InformationClass_Root_Id, id);

			//删除
			var delete = new DeleteHelper();
            delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                var ids = id.ToList();
                foreach (var i in ids)
                {
                    //删除缓存
                    DelCache(x => x.InformationClass_Root_Id == i);
                }
            }
			
			//添加用户操作记录
			UseLogBll.GetInstence().Save(page, "{0}删除了Information表InformationClass_Root_Id值为【" + str + "】的记录！");
		}
		#endregion

		#region 更新Information表指定InformationClass_Root_Id的字段值
        /// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
        /// <param name="page">当前页面指针</param>
	    /// <param name="InformationClass_Root_Id">字段InformationClass_Root_Id的值</param>
	    /// <param name="dic">需要更新的字段与值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue_For_InformationClass_Root_Id(Page page, int InformationClass_Root_Id, Dictionary<string, object> dic, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表外键InformationClass_Root_Id值为" + InformationClass_Root_Id + "的所有记录。";
			
            //条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>
            {
                new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.InformationClass_Root_Id, Comparison.Equals, InformationClass_Root_Id)
            };

            //执行更新
            UpdateValue(page, dic, wheres, content, isCache, isAddUseLog);
        }

		/// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
        /// <param name="page">当前页面指针</param>
	    /// <param name="InformationClass_Root_Id">字段InformationClass_Root_Id的值</param>
        /// <param name="columnName">要更新的列名</param>
        /// <param name="columnValue">要更新的列值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue_For_InformationClass_Root_Id(Page page, int InformationClass_Root_Id, string columnName, object columnValue, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表外键InformationClass_Root_Id值为" + InformationClass_Root_Id + "的所有记录，将" + columnName + "字段值修改为" + columnValue;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName, columnValue);

			//执行更新
            UpdateValue_For_InformationClass_Root_Id(page, InformationClass_Root_Id, dic, content, isCache, isAddUseLog);
        }

		/// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
        /// <param name="page">当前页面指针</param>
	    /// <param name="InformationClass_Root_Id">字段InformationClass_Root_Id的值</param>
        /// <param name="columnName1">要更新的列名</param>
        /// <param name="columnValue1">要更新的列值</param>
        /// <param name="columnName2">要更新的列名</param>
        /// <param name="columnValue2">要更新的列值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue_For_InformationClass_Root_Id(Page page, int InformationClass_Root_Id, string columnName1, object columnValue1, string columnName2, object columnValue2, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表外键InformationClass_Root_Id值为" + InformationClass_Root_Id + "的所有记录，将" + columnName1 + "字段值修改为" + columnValue1 + "，" + columnName2 + "字段值修改为" + columnValue2;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName1, columnValue1);
            dic.Add(columnName2, columnValue2);

			//执行更新
            UpdateValue_For_InformationClass_Root_Id(page, InformationClass_Root_Id, dic, content, isCache, isAddUseLog);
        }
        #endregion
		
		#region 获取InformationClass_Root_Name字段值
        /// <summary>
        /// 获取InformationClass_Root_Name字段值
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns></returns>
        public string GetInformationClass_Root_Name(Page page, int pkValue, bool isCache = true)
        {
            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                var model = GetModelForCache(pkValue);
                return model == null ? "" : model.InformationClass_Root_Name;
            }
            else
            {
                //从数据库中查询
                var model = Information.SingleOrDefault(x => x.Id == pkValue);
                return model == null ? "" : model.InformationClass_Root_Name;
            }
        }
        #endregion

		#region 删除Information表指定InformationClass_Id的字段值记录
		/// <summary>
		/// 删除Information表指定InformationClass_Id的字段值记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		public void DeleteByInformationClass_Id(Page page, int id) {
			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} = {2}", InformationTable.TableName, InformationTable.InformationClass_Id, id);

			//删除
			var delete = new DeleteHelper();
            delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                //删除缓存
                DelCache(x => x.InformationClass_Id == id);
            }
			
			//添加用户操作记录
			UseLogBll.GetInstence().Save(page, "{0}删除了Information表InformationClass_Id值为【" + id + "】的记录！");
		}

		/// <summary>
		/// 删除Information表指定InformationClass_Id的字段值记录
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="id">记录的主键值</param>
		public void DeleteByInformationClass_Id(Page page, int[] id) {
			if (id == null) return;
			//将数组转为逗号分隔的字串
			var str = string.Join(",", id);

			//设置Sql语句
			var sql = string.Format("delete from {0} where {1} in ({2})", InformationTable.TableName, InformationTable.InformationClass_Id, id);

			//删除
			var delete = new DeleteHelper();
            delete.Delete(sql);
			
			//判断是否启用缓存
            if (CommonBll.IsUseCache())
            {
                var ids = id.ToList();
                foreach (var i in ids)
                {
                    //删除缓存
                    DelCache(x => x.InformationClass_Id == i);
                }
            }
			
			//添加用户操作记录
			UseLogBll.GetInstence().Save(page, "{0}删除了Information表InformationClass_Id值为【" + str + "】的记录！");
		}
		#endregion

		#region 更新Information表指定InformationClass_Id的字段值
        /// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
        /// <param name="page">当前页面指针</param>
	    /// <param name="InformationClass_Id">字段InformationClass_Id的值</param>
	    /// <param name="dic">需要更新的字段与值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue_For_InformationClass_Id(Page page, int InformationClass_Id, Dictionary<string, object> dic, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表外键InformationClass_Id值为" + InformationClass_Id + "的所有记录。";
			
            //条件
            var wheres = new List<ConditionHelper.SqlqueryCondition>
            {
                new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.InformationClass_Id, Comparison.Equals, InformationClass_Id)
            };

            //执行更新
            UpdateValue(page, dic, wheres, content, isCache, isAddUseLog);
        }

		/// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
        /// <param name="page">当前页面指针</param>
	    /// <param name="InformationClass_Id">字段InformationClass_Id的值</param>
        /// <param name="columnName">要更新的列名</param>
        /// <param name="columnValue">要更新的列值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue_For_InformationClass_Id(Page page, int InformationClass_Id, string columnName, object columnValue, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表外键InformationClass_Id值为" + InformationClass_Id + "的所有记录，将" + columnName + "字段值修改为" + columnValue;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName, columnValue);

			//执行更新
            UpdateValue_For_InformationClass_Id(page, InformationClass_Id, dic, content, isCache, isAddUseLog);
        }

		/// <summary>更新Information表记录指定字段值，如果使用了缓存，保存成功后会清空本表的所有缓存记录，然后重新加载进缓存</summary>
        /// <param name="page">当前页面指针</param>
	    /// <param name="InformationClass_Id">字段InformationClass_Id的值</param>
        /// <param name="columnName1">要更新的列名</param>
        /// <param name="columnValue1">要更新的列值</param>
        /// <param name="columnName2">要更新的列名</param>
        /// <param name="columnValue2">要更新的列值</param>
	    /// <param name="content">更新说明</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
	    public void UpdateValue_For_InformationClass_Id(Page page, int InformationClass_Id, string columnName1, object columnValue1, string columnName2, object columnValue2, string content = "", bool isCache = true, bool isAddUseLog = true)
        {
			content = content != "" ? content : "{0}修改了Information表外键InformationClass_Id值为" + InformationClass_Id + "的所有记录，将" + columnName1 + "字段值修改为" + columnValue1 + "，" + columnName2 + "字段值修改为" + columnValue2;
            //设置更新字段
            var dic = new Dictionary<string, object>();
            dic.Add(columnName1, columnValue1);
            dic.Add(columnName2, columnValue2);

			//执行更新
            UpdateValue_For_InformationClass_Id(page, InformationClass_Id, dic, content, isCache, isAddUseLog);
        }
        #endregion
		
		#region 获取InformationClass_Name字段值
        /// <summary>
        /// 获取InformationClass_Name字段值
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns></returns>
        public string GetInformationClass_Name(Page page, int pkValue, bool isCache = true)
        {
            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                var model = GetModelForCache(pkValue);
                return model == null ? "" : model.InformationClass_Name;
            }
            else
            {
                //从数据库中查询
                var model = Information.SingleOrDefault(x => x.Id == pkValue);
                return model == null ? "" : model.InformationClass_Name;
            }
        }
        #endregion

		#region 删除FrontCoverImg字段存储的对应图片
		/// <summary>删除FrontCoverImg字段存储的对应图片</summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否同步更新缓存</param>
		public void DelFrontCoverImg(Page page, int pkValue, bool isCache = true) 
		{
			try {
				string img = "";

				//设置条件
				var wheres = new List<ConditionHelper.SqlqueryCondition>();
				wheres.Add(new ConditionHelper.SqlqueryCondition(ConstraintType.And, InformationTable.Id, Comparison.Equals, pkValue));

				//获取图片地址
				var select = new SelectHelper();
				img = select.GetColumnsValue<Information>(InformationTable.FrontCoverImg, wheres) + "";

				//删除图片
				UploadFileBll.GetInstence().Upload_OneDelPic(img);

				//设置更新值
				var setValue = new Dictionary<string, object>();
				setValue[InformationTable.FrontCoverImg] = "";
				//更新
				UpdateValue(page, setValue, wheres, "{0}更新了Ad表id为【" + pkValue + "】的记录，将图片Img删除", false);

                //判断是否启用缓存
                if (isCache && CommonBll.IsUseCache())
                {
                    //从缓存中获取实体
                    var model = GetModelForCache(pkValue);
					if (model != null)
					{
						//给获取的实体赋值
						SetModelValue(model, InformationTable.FrontCoverImg, "");
						//更新缓存中的实体
						SetModelForCache(model);
					}
                }
			}
			catch (Exception e) {
				//出现异常，保存出错日志信息
				CommonBll.WriteLog("", e);
			}
		}
		#endregion

		#region 使用Keywords来查询，获取一个Information实体对象
        /// <summary>使用Key来查询，获取一个Information实体对象</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="key">Key值</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns>DataAccess.Model.Information 实体</returns>
        public DataAccess.Model.Information GetModel_ByKeywords(Page page, string key, bool isCache = true)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                return GetModelForCache(x => x.Keywords == key);
            }
			else
			{
				//从数据库中查询
				return Transform(Information.SingleOrDefault(x => x.Keywords == key));
			}
        }
        #endregion

		#region 使用SeoKey来查询，获取一个Information实体对象
        /// <summary>使用Key来查询，获取一个Information实体对象</summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="key">Key值</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns>DataAccess.Model.Information 实体</returns>
        public DataAccess.Model.Information GetModel_BySeoKey(Page page, string key, bool isCache = true)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                return GetModelForCache(x => x.SeoKey == key);
            }
			else
			{
				//从数据库中查询
				return Transform(Information.SingleOrDefault(x => x.SeoKey == key));
			}
        }
        #endregion

		#region 获取FromName字段值
        /// <summary>
        /// 获取FromName字段值
        /// </summary>
        /// <param name="page">当前页面指针</param>
        /// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否从缓存中读取</param>
        /// <returns></returns>
        public string GetFromName(Page page, int pkValue, bool isCache = true)
        {
            //判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //从缓存中获取实体
                var model = GetModelForCache(pkValue);
                return model == null ? "" : model.FromName;
            }
            else
            {
                //从数据库中查询
                var model = Information.SingleOrDefault(x => x.Id == pkValue);
                return model == null ? "" : model.FromName;
            }
        }
        #endregion

		#region 更新IsDisplay字段值
		/// <summary>
		/// 更新IsDisplay字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id，当等于0时，则更新所有记录</param>
		/// <param name="updateValue">更新值</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateIsDisplay(Page page, int pkValue, int updateValue, bool isCache = true, bool isAddUseLog = true) {
			//设置更新值
			var setValue = new Dictionary<string, object>();
			setValue[InformationTable.IsDisplay] = updateValue;

			//更新
			UpdateValue(page, pkValue, setValue, "{0}更新了Information表id为【" + pkValue + "】的记录，更新内容为将IsDisplay字段值修改为" + updateValue, isCache, isAddUseLog);
		}
		#endregion
		
		#region 更新IsHot字段值
		/// <summary>
		/// 更新IsHot字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id，当等于0时，则更新所有记录</param>
		/// <param name="updateValue">更新值</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateIsHot(Page page, int pkValue, int updateValue, bool isCache = true, bool isAddUseLog = true) {
			//设置更新值
			var setValue = new Dictionary<string, object>();
			setValue[InformationTable.IsHot] = updateValue;

			//更新
			UpdateValue(page, pkValue, setValue, "{0}更新了Information表id为【" + pkValue + "】的记录，更新内容为将IsHot字段值修改为" + updateValue, isCache, isAddUseLog);
		}
		#endregion
		
		#region 更新IsTop字段值
		/// <summary>
		/// 更新IsTop字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id，当等于0时，则更新所有记录</param>
		/// <param name="updateValue">更新值</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateIsTop(Page page, int pkValue, int updateValue, bool isCache = true, bool isAddUseLog = true) {
			//设置更新值
			var setValue = new Dictionary<string, object>();
			setValue[InformationTable.IsTop] = updateValue;

			//更新
			UpdateValue(page, pkValue, setValue, "{0}更新了Information表id为【" + pkValue + "】的记录，更新内容为将IsTop字段值修改为" + updateValue, isCache, isAddUseLog);
		}
		#endregion
		
		#region 更新IsPage字段值
		/// <summary>
		/// 更新IsPage字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id，当等于0时，则更新所有记录</param>
		/// <param name="updateValue">更新值</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateIsPage(Page page, int pkValue, int updateValue, bool isCache = true, bool isAddUseLog = true) {
			//设置更新值
			var setValue = new Dictionary<string, object>();
			setValue[InformationTable.IsPage] = updateValue;

			//更新
			UpdateValue(page, pkValue, setValue, "{0}更新了Information表id为【" + pkValue + "】的记录，更新内容为将IsPage字段值修改为" + updateValue, isCache, isAddUseLog);
		}
		#endregion
		
		#region 更新IsDel字段值
		/// <summary>
		/// 更新IsDel字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id，当等于0时，则更新所有记录</param>
		/// <param name="updateValue">更新值</param>
        /// <param name="isCache">是否同步更新缓存</param>
		/// <param name="isAddUseLog">是否添加用户操作日志</param>
		public void UpdateIsDel(Page page, int pkValue, int updateValue, bool isCache = true, bool isAddUseLog = true) {
			//设置更新值
			var setValue = new Dictionary<string, object>();
			setValue[InformationTable.IsDel] = updateValue;

			//更新
			UpdateValue(page, pkValue, setValue, "{0}更新了Information表id为【" + pkValue + "】的记录，更新内容为将IsDel字段值修改为" + updateValue, isCache, isAddUseLog);
		}
		#endregion
		
		#region 更新CommentCount字段值
		/// <summary>
		/// 更新CommentCount字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否同步更新缓存</param>
		public void UpdateCommentCount(Page page, int pkValue, bool isCache = true) 
		{
			if (pkValue <= 0) return;
			
			//判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //获取实体
                var model = GetModelForCache(pkValue);
				if (model != null)
				{
					model.CommentCount++;

					SetModelForCache(model);
				}
            }

			//设置更新Sql语句
			var sql = string.Format("update {0} set {1}={1} + 1 where {2} = {3}", InformationTable.TableName, InformationTable.CommentCount, "Id", pkValue);

			//更新
			var update = new UpdateHelper();
			update.Update(sql);
		}
		#endregion
		
		#region 更新ViewCount字段值
		/// <summary>
		/// 更新ViewCount字段值
		/// </summary>
		/// <param name="page">当前页面指针</param>
		/// <param name="pkValue">主键Id</param>
        /// <param name="isCache">是否同步更新缓存</param>
		public void UpdateViewCount(Page page, int pkValue, bool isCache = true) 
		{
			if (pkValue <= 0) return;
			
			//判断是否启用缓存
            if (isCache && CommonBll.IsUseCache())
            {
                //获取实体
                var model = GetModelForCache(pkValue);
				if (model != null)
				{
					model.ViewCount++;

					SetModelForCache(model);
				}
            }

			//设置更新Sql语句
			var sql = string.Format("update {0} set {1}={1} + 1 where {2} = {3}", InformationTable.TableName, InformationTable.ViewCount, "Id", pkValue);

			//更新
			var update = new UpdateHelper();
			update.Update(sql);
		}
		#endregion
		
		#region 获取排序字段Sort的最大值
		/// <summary>
		/// 获取排序字段Sort的最大值
		/// </summary>
		public int GetSortMax() 
		{
			//查询
			var select = new SelectHelper();
		    return ConvertHelper.Cint0(select.GetMax<Information>(InformationTable.Sort));
		}
		#endregion
		
    
		#endregion 模版生成函数

    } 
}
