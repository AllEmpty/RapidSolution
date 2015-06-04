 
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
    /// A class which represents the WebConfig table in the SolutionDataBase Database.
    /// </summary>
    public partial class WebConfig: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<WebConfig> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<WebConfig>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<WebConfig> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(WebConfig item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                WebConfig item=new WebConfig();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<WebConfig> _repo;
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
        public WebConfig(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                WebConfig.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<WebConfig>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public WebConfig(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            WebName = readRecord.get_string("WebName",null);
               
            WebDomain = readRecord.get_string("WebDomain",null);
               
            WebEmail = readRecord.get_string("WebEmail",null);
               
            LoginLogReserveTime = readRecord.get_int("LoginLogReserveTime",null);
               
            UseLogReserveTime = readRecord.get_int("UseLogReserveTime",null);
               
            EmailSmtp = readRecord.get_string("EmailSmtp",null);
               
            EmailUserName = readRecord.get_string("EmailUserName",null);
               
            EmailPassWord = readRecord.get_string("EmailPassWord",null);
               
            EmailDomain = readRecord.get_string("EmailDomain",null);
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

        public WebConfig(Expression<Func<WebConfig, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<WebConfig> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<WebConfig> _repo;
            
            if(db.TestMode){
                WebConfig.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<WebConfig>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<WebConfig> GetRepo(){
            return GetRepo("","");
        }
        
        public static WebConfig SingleOrDefault(Expression<Func<WebConfig, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            WebConfig single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static WebConfig SingleOrDefault(Expression<Func<WebConfig, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            WebConfig single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<WebConfig, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<WebConfig, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<WebConfig> Find(Expression<Func<WebConfig, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<WebConfig> Find(Expression<Func<WebConfig, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<WebConfig> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<WebConfig> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<WebConfig> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<WebConfig> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<WebConfig> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<WebConfig> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("WebName=" +　WebName + "; ");
			sb.Append("WebDomain=" +　WebDomain + "; ");
			sb.Append("WebEmail=" +　WebEmail + "; ");
			sb.Append("LoginLogReserveTime=" +　LoginLogReserveTime + "; ");
			sb.Append("UseLogReserveTime=" +　UseLogReserveTime + "; ");
			sb.Append("EmailSmtp=" +　EmailSmtp + "; ");
			sb.Append("EmailUserName=" +　EmailUserName + "; ");
			sb.Append("EmailPassWord=" +　EmailPassWord + "; ");
			sb.Append("EmailDomain=" +　EmailDomain + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(WebConfig)){
                WebConfig compare=(WebConfig)obj;
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
                            return this.WebName.ToString();
                    }

        public string DescriptorColumn() {
            return "WebName";
        }
        public static string GetKeyColumn()
        {
            return "Id";
        }        
        public static string GetDescriptorColumn()
        {
            return "WebName";
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

        string _WebName;
		/// <summary>
		/// 基本信息--网站名称
		/// </summary>
        public string WebName
        {
            get { return _WebName; }
            set
            {
                if(_WebName!=value || _isLoaded){
                    _WebName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="WebName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _WebDomain;
		/// <summary>
		/// 基本信息--网站地址
		/// </summary>
        public string WebDomain
        {
            get { return _WebDomain; }
            set
            {
                if(_WebDomain!=value || _isLoaded){
                    _WebDomain=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="WebDomain");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _WebEmail;
		/// <summary>
		/// 基本信息--管理员邮箱
		/// </summary>
        public string WebEmail
        {
            get { return _WebEmail; }
            set
            {
                if(_WebEmail!=value || _isLoaded){
                    _WebEmail=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="WebEmail");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _LoginLogReserveTime;
		/// <summary>
		/// 日志--系统登陆日志保留时间，0=无限制，N（数字）= X月
		/// </summary>
        public int LoginLogReserveTime
        {
            get { return _LoginLogReserveTime; }
            set
            {
                if(_LoginLogReserveTime!=value || _isLoaded){
                    _LoginLogReserveTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LoginLogReserveTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UseLogReserveTime;
		/// <summary>
		/// 日志--系统操作日志保留时间，0=无限制，N（数字）= X月
		/// </summary>
        public int UseLogReserveTime
        {
            get { return _UseLogReserveTime; }
            set
            {
                if(_UseLogReserveTime!=value || _isLoaded){
                    _UseLogReserveTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UseLogReserveTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _EmailSmtp;
		/// <summary>
		/// 邮件设置--SMTP服务器
		/// </summary>
        public string EmailSmtp
        {
            get { return _EmailSmtp; }
            set
            {
                if(_EmailSmtp!=value || _isLoaded){
                    _EmailSmtp=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EmailSmtp");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _EmailUserName;
		/// <summary>
		/// 邮件设置--登录用户名
		/// </summary>
        public string EmailUserName
        {
            get { return _EmailUserName; }
            set
            {
                if(_EmailUserName!=value || _isLoaded){
                    _EmailUserName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EmailUserName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _EmailPassWord;
		/// <summary>
		/// 邮件设置--登录密码
		/// </summary>
        public string EmailPassWord
        {
            get { return _EmailPassWord; }
            set
            {
                if(_EmailPassWord!=value || _isLoaded){
                    _EmailPassWord=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EmailPassWord");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _EmailDomain;
		/// <summary>
		/// 邮件设置--邮件域名
		/// </summary>
        public string EmailDomain
        {
            get { return _EmailDomain; }
            set
            {
                if(_EmailDomain!=value || _isLoaded){
                    _EmailDomain=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EmailDomain");
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


        public static void Delete(Expression<Func<WebConfig, bool>> expression) {
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

