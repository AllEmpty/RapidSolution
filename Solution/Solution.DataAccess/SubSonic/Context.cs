


using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using SubSonic.Linq.Structure;
using SubSonic.Query;
using SubSonic.Schema;
using System.Data.Common;
using System.Collections.Generic;

namespace Solution.DataAccess.DataModel
{
    public partial class SolutionDataBaseDB : IQuerySurface
    {

        public IDataProvider DataProvider;
        public DbQueryProvider provider;
        
        private static IDataProvider _idDataProvider;
        public static IDataProvider GetDataProvider()
        {
            if (_idDataProvider == null)
                _idDataProvider = ProviderFactory.GetProvider("conn");

            return _idDataProvider;
        }

        public bool TestMode
		{
            get
			{
                return DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public SolutionDataBaseDB() 
        {
            if (DataProvider == null) {
                DataProvider = GetDataProvider();
            }
            //else {
            //    DataProvider = DefaultDataProvider;
            //}
            Init();
        }

        public SolutionDataBaseDB(string connectionStringName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionStringName);
            Init();
        }

		public SolutionDataBaseDB(string connectionString, string providerName)
        {
            DataProvider = ProviderFactory.GetProvider(connectionString,providerName);
            Init();
        }

		public ITable FindByPrimaryKey(string pkName)
        {
            return DataProvider.Schema.Tables.SingleOrDefault(x => x.PrimaryKey.Name.Equals(pkName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Query<T> GetQuery<T>()
        {
            return new Query<T>(provider);
        }
        
        public ITable FindTable(string tableName)
        {
            return DataProvider.FindTable(tableName);
        }
               
        public IDataProvider Provider
        {
            get { return DataProvider; }
            set {DataProvider=value;}
        }
        
        public DbQueryProvider QueryProvider
        {
            get { return provider; }
        }
        
        BatchQuery _batch = null;
        public void Queue<T>(IQueryable<T> qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void Queue(ISqlQuery qry)
        {
            if (_batch == null)
                _batch = new BatchQuery(Provider, QueryProvider);
            _batch.Queue(qry);
        }

        public void ExecuteTransaction(IList<DbCommand> commands)
		{
            if(!TestMode)
			{
                using(var connection = commands[0].Connection)
				{
                   if (connection.State == ConnectionState.Closed)
                        connection.Open();
                   
                   using (var trans = connection.BeginTransaction()) 
				   {
                        foreach (var cmd in commands) 
						{
                            cmd.Transaction = trans;
                            cmd.Connection = connection;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    connection.Close();
                }
            }
        }

        public IDataReader ExecuteBatch()
        {
            if (_batch == null)
                throw new InvalidOperationException("There's nothing in the queue");
            if(!TestMode)
                return _batch.ExecuteReader();
            return null;
        }
			
        public Query<DataAccess.Model.Advertisement> Advertisement { get; set; }
        public Query<DataAccess.Model.AdvertisingPosition> AdvertisingPosition { get; set; }
        public Query<DataAccess.Model.Branch> Branch { get; set; }
        public Query<DataAccess.Model.ErrorLog> ErrorLog { get; set; }
        public Query<DataAccess.Model.Information> Information { get; set; }
        public Query<DataAccess.Model.InformationClass> InformationClass { get; set; }
        public Query<DataAccess.Model.LoginLog> LoginLog { get; set; }
        public Query<DataAccess.Model.Manager> Manager { get; set; }
        public Query<DataAccess.Model.MenuInfo> MenuInfo { get; set; }
        public Query<DataAccess.Model.OnlineUsers> OnlineUsers { get; set; }
        public Query<DataAccess.Model.PagePowerSign> PagePowerSign { get; set; }
        public Query<DataAccess.Model.PagePowerSignPublic> PagePowerSignPublic { get; set; }
        public Query<DataAccess.Model.Position> Position { get; set; }
        public Query<DataAccess.Model.UploadConfig> UploadConfig { get; set; }
        public Query<DataAccess.Model.UploadFile> UploadFile { get; set; }
        public Query<DataAccess.Model.UploadType> UploadType { get; set; }
        public Query<DataAccess.Model.UseLog> UseLog { get; set; }
        public Query<DataAccess.Model.V_Position_Branch> V_Position_Branch { get; set; }
        public Query<DataAccess.Model.WebConfig> WebConfig { get; set; }

			

        #region ' Aggregates and SubSonic Queries '
        public Select SelectColumns(params string[] columns)
        {
            return new Select(DataProvider, columns);
        }

        public Select Select
        {
            get { return new Select(this.Provider); }
        }

        public Insert Insert
		{
            get { return new Insert(this.Provider); }
        }

        public Update<T> Update<T>() where T:new()
		{
            return new Update<T>(this.Provider);
        }

        public SqlQuery Delete<T>(Expression<Func<T,bool>> column) where T:new()
        {
            LambdaExpression lamda = column;
            SqlQuery result = new Delete<T>(this.Provider);
            result = result.From<T>();
            result.Constraints=lamda.ParseConstraints().ToList();
            return result;
        }

        public SqlQuery Max<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = DataProvider.FindTable(objectName).Name;
            return new Select(DataProvider, new Aggregate(colName, AggregateFunction.Max)).From(tableName);
        }

        public SqlQuery Min<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Min)).From(tableName);
        }

        public SqlQuery Sum<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Sum)).From(tableName);
        }

        public SqlQuery Avg<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Avg)).From(tableName);
        }

        public SqlQuery Count<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Count)).From(tableName);
        }

        public SqlQuery Variance<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.Var)).From(tableName);
        }

        public SqlQuery StandardDeviation<T>(Expression<Func<T,object>> column)
        {
            LambdaExpression lamda = column;
            string colName = lamda.ParseObjectValue();
            string objectName = typeof(T).Name;
            string tableName = this.Provider.FindTable(objectName).Name;
            return new Select(this.Provider, new Aggregate(colName, AggregateFunction.StDev)).From(tableName);
        }

        #endregion

        void Init()
        {
            provider = new DbQueryProvider(this.Provider);

            #region ' Query Defs '
            Advertisement = new Query<DataAccess.Model.Advertisement>(provider);
            AdvertisingPosition = new Query<DataAccess.Model.AdvertisingPosition>(provider);
            Branch = new Query<DataAccess.Model.Branch>(provider);
            ErrorLog = new Query<DataAccess.Model.ErrorLog>(provider);
            Information = new Query<DataAccess.Model.Information>(provider);
            InformationClass = new Query<DataAccess.Model.InformationClass>(provider);
            LoginLog = new Query<DataAccess.Model.LoginLog>(provider);
            Manager = new Query<DataAccess.Model.Manager>(provider);
            MenuInfo = new Query<DataAccess.Model.MenuInfo>(provider);
            OnlineUsers = new Query<DataAccess.Model.OnlineUsers>(provider);
            PagePowerSign = new Query<DataAccess.Model.PagePowerSign>(provider);
            PagePowerSignPublic = new Query<DataAccess.Model.PagePowerSignPublic>(provider);
            Position = new Query<DataAccess.Model.Position>(provider);
            UploadConfig = new Query<DataAccess.Model.UploadConfig>(provider);
            UploadFile = new Query<DataAccess.Model.UploadFile>(provider);
            UploadType = new Query<DataAccess.Model.UploadType>(provider);
            UseLog = new Query<DataAccess.Model.UseLog>(provider);
            V_Position_Branch = new Query<DataAccess.Model.V_Position_Branch>(provider);
            WebConfig = new Query<DataAccess.Model.WebConfig>(provider);
            #endregion


            #region ' Schemas '
        	if(DataProvider.Schema.Tables.Count == 0)
			{
            	DataProvider.Schema.Tables.Add(new AdvertisementStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new AdvertisingPositionStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new BranchStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new ErrorLogStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new InformationStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new InformationClassStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new LoginLogStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new ManagerStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new MenuInfoStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new OnlineUsersStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new PagePowerSignStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new PagePowerSignPublicStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new PositionStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new UploadConfigStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new UploadFileStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new UploadTypeStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new UseLogStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new V_Position_BranchStructs(DataProvider));
            	DataProvider.Schema.Tables.Add(new WebConfigStructs(DataProvider));
            }
            #endregion
        }
        

        #region ' Helpers '
            
        internal static DateTime DateTimeNowTruncatedDownToSecond() {
            var now = DateTime.Now;
            return now.AddTicks(-now.Ticks % TimeSpan.TicksPerSecond);
        }

        #endregion

    }
}