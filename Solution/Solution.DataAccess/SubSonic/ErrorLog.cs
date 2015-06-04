 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using System.Linq.Expressions;
using SubSonic.Schema;
using SubSonic.Repository;
using System.Data.Common;
using SubSonic.SqlGeneration.Schema;

namespace Solution.DataAccess.DataModel
{    
    /// <summary>
    /// A class which represents the ErrorLog table in the SolutionDataBase Database.
    /// </summary>
    public partial class ErrorLog: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<ErrorLog> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<ErrorLog>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<ErrorLog> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(ErrorLog item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                ErrorLog item=new ErrorLog();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<ErrorLog> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Solution.DataAccess.DataModel.SolutionDataBaseDB _db;
        public ErrorLog(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                ErrorLog.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<ErrorLog>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public ErrorLog(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            ErrTime = readRecord.get_datetime("ErrTime",null);
               
            BrowserVersion = readRecord.get_string("BrowserVersion",null);
               
            BrowserType = readRecord.get_string("BrowserType",null);
               
            Ip = readRecord.get_string("Ip",null);
               
            PageUrl = readRecord.get_string("PageUrl",null);
               
            ErrMessage = readRecord.get_string("ErrMessage",null);
               
            ErrSource = readRecord.get_string("ErrSource",null);
               
            StackTrace = readRecord.get_string("StackTrace",null);
               
            HelpLink = readRecord.get_string("HelpLink",null);
               
            Type = readRecord.get_byte("Type",null);
                }   

        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public ErrorLog(Expression<Func<ErrorLog, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<ErrorLog> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<ErrorLog> _repo;
            
            if(db.TestMode){
                ErrorLog.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<ErrorLog>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<ErrorLog> GetRepo(){
            return GetRepo("","");
        }
        
        public static ErrorLog SingleOrDefault(Expression<Func<ErrorLog, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            ErrorLog single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static ErrorLog SingleOrDefault(Expression<Func<ErrorLog, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            ErrorLog single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<ErrorLog, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<ErrorLog, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<ErrorLog> Find(Expression<Func<ErrorLog, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<ErrorLog> Find(Expression<Func<ErrorLog, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<ErrorLog> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<ErrorLog> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<ErrorLog> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<ErrorLog> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<ErrorLog> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<ErrorLog> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "Id";
        }

        public object KeyValue()
        {
            return this.Id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
		//********************************************/
		// 修 改 人：Empty（AllEmpty）
		// QQ    群：327360708
		// 博客地址：http://www.cnblogs.com/EmptyFS/
		// 修改时间：2014-07-06
		// 修改说明：将整个实体变量名与值生成字符串输出
		//********************************************/
        public override string ToString(){
			var sb = new StringBuilder();
			sb.Append("Id=" +　Id + "; ");
			sb.Append("ErrTime=" +　ErrTime + "; ");
			sb.Append("BrowserVersion=" +　BrowserVersion + "; ");
			sb.Append("BrowserType=" +　BrowserType + "; ");
			sb.Append("Ip=" +　Ip + "; ");
			sb.Append("PageUrl=" +　PageUrl + "; ");
			sb.Append("ErrMessage=" +　ErrMessage + "; ");
			sb.Append("ErrSource=" +　ErrSource + "; ");
			sb.Append("StackTrace=" +　StackTrace + "; ");
			sb.Append("HelpLink=" +　HelpLink + "; ");
			sb.Append("Type=" +　Type + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(ErrorLog)){
                ErrorLog compare=(ErrorLog)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.Id;
        }
        
        public string DescriptorValue()
        {
                            return this.BrowserVersion.ToString();
                    }

        public string DescriptorColumn() {
            return "BrowserVersion";
        }
        public static string GetKeyColumn()
        {
            return "Id";
        }        
        public static string GetDescriptorColumn()
        {
            return "BrowserVersion";
        }
        
        #region ' Foreign Keys '
        #endregion
        

        int _Id;
		/// <summary>
		/// 主键Id
		/// </summary>
		[SubSonicPrimaryKey]
        public int Id
        {
            get { return _Id; }
            set
            {
                if(_Id!=value || _isLoaded){
                    _Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _ErrTime;
		/// <summary>
		/// 出错时间
		/// </summary>
        public DateTime ErrTime
        {
            get { return _ErrTime; }
            set
            {
                if(_ErrTime!=value || _isLoaded){
                    _ErrTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ErrTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _BrowserVersion;
		/// <summary>
		/// 浏览器版本
		/// </summary>
        public string BrowserVersion
        {
            get { return _BrowserVersion; }
            set
            {
                if(_BrowserVersion!=value || _isLoaded){
                    _BrowserVersion=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BrowserVersion");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _BrowserType;
		/// <summary>
		/// 浏览器
		/// </summary>
        public string BrowserType
        {
            get { return _BrowserType; }
            set
            {
                if(_BrowserType!=value || _isLoaded){
                    _BrowserType=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BrowserType");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Ip;
		/// <summary>
		/// IP
		/// </summary>
        public string Ip
        {
            get { return _Ip; }
            set
            {
                if(_Ip!=value || _isLoaded){
                    _Ip=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Ip");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _PageUrl;
		/// <summary>
		/// 异常页面
		/// </summary>
        public string PageUrl
        {
            get { return _PageUrl; }
            set
            {
                if(_PageUrl!=value || _isLoaded){
                    _PageUrl=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PageUrl");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _ErrMessage;
		/// <summary>
		/// 异常消息
		/// </summary>
        public string ErrMessage
        {
            get { return _ErrMessage; }
            set
            {
                if(_ErrMessage!=value || _isLoaded){
                    _ErrMessage=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ErrMessage");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _ErrSource;
		/// <summary>
		/// 异常源
		/// </summary>
        public string ErrSource
        {
            get { return _ErrSource; }
            set
            {
                if(_ErrSource!=value || _isLoaded){
                    _ErrSource=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ErrSource");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _StackTrace;
		/// <summary>
		/// 堆栈轨迹
		/// </summary>
        public string StackTrace
        {
            get { return _StackTrace; }
            set
            {
                if(_StackTrace!=value || _isLoaded){
                    _StackTrace=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="StackTrace");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _HelpLink;
		/// <summary>
		/// 帮助连接
		/// </summary>
        public string HelpLink
        {
            get { return _HelpLink; }
            set
            {
                if(_HelpLink!=value || _isLoaded){
                    _HelpLink=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="HelpLink");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _Type;
		/// <summary>
		/// 错误类型，0=后台，1=前台，......
		/// </summary>
        public byte Type
        {
            get { return _Type; }
            set
            {
                if(_Type!=value || _isLoaded){
                    _Type=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Type");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
				_repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }

        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<ErrorLog, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
}

