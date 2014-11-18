 
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
    /// A class which represents the OnlineUsers table in the SolutionDataBase Database.
    /// </summary>
    public partial class OnlineUsers: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<OnlineUsers> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<OnlineUsers>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<OnlineUsers> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(OnlineUsers item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                OnlineUsers item=new OnlineUsers();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<OnlineUsers> _repo;
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
        public OnlineUsers(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                OnlineUsers.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<OnlineUsers>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public OnlineUsers(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            UserHashKey = readRecord.get_string("UserHashKey",null);
               
            Manager_Id = readRecord.get_int("Manager_Id",null);
               
            Manager_LoginName = readRecord.get_string("Manager_LoginName",null);
               
            Manager_LoginPass = readRecord.get_string("Manager_LoginPass",null);
               
            Manager_CName = readRecord.get_string("Manager_CName",null);
               
            LoginTime = readRecord.get_datetime("LoginTime",null);
               
            LoginIp = readRecord.get_string("LoginIp",null);
               
            UserKey = readRecord.get_string("UserKey",null);
               
            Md5 = readRecord.get_string("Md5",null);
               
            UpdateTime = readRecord.get_datetime("UpdateTime",null);
               
            Sex = readRecord.get_string("Sex",null);
               
            Branch_Id = readRecord.get_int("Branch_Id",null);
               
            Branch_Code = readRecord.get_string("Branch_Code",null);
               
            Branch_Name = readRecord.get_string("Branch_Name",null);
               
            Position_Id = readRecord.get_string("Position_Id",null);
               
            Position_Name = readRecord.get_string("Position_Name",null);
               
            CurrentPage = readRecord.get_string("CurrentPage",null);
               
            CurrentPageTitle = readRecord.get_string("CurrentPageTitle",null);
               
            SessionId = readRecord.get_string("SessionId",null);
               
            UserAgent = readRecord.get_string("UserAgent",null);
               
            OperatingSystem = readRecord.get_string("OperatingSystem",null);
               
            TerminalType = readRecord.get_int("TerminalType",null);
               
            BrowserName = readRecord.get_string("BrowserName",null);
               
            BrowserVersion = readRecord.get_string("BrowserVersion",null);
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

        public OnlineUsers(Expression<Func<OnlineUsers, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<OnlineUsers> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<OnlineUsers> _repo;
            
            if(db.TestMode){
                OnlineUsers.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<OnlineUsers>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<OnlineUsers> GetRepo(){
            return GetRepo("","");
        }
        
        public static OnlineUsers SingleOrDefault(Expression<Func<OnlineUsers, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            OnlineUsers single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static OnlineUsers SingleOrDefault(Expression<Func<OnlineUsers, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            OnlineUsers single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<OnlineUsers, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<OnlineUsers, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<OnlineUsers> Find(Expression<Func<OnlineUsers, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<OnlineUsers> Find(Expression<Func<OnlineUsers, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<OnlineUsers> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<OnlineUsers> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<OnlineUsers> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<OnlineUsers> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<OnlineUsers> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<OnlineUsers> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("UserHashKey=" +　UserHashKey + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_LoginName=" +　Manager_LoginName + "; ");
			sb.Append("Manager_LoginPass=" +　Manager_LoginPass + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("LoginTime=" +　LoginTime + "; ");
			sb.Append("LoginIp=" +　LoginIp + "; ");
			sb.Append("UserKey=" +　UserKey + "; ");
			sb.Append("Md5=" +　Md5 + "; ");
			sb.Append("UpdateTime=" +　UpdateTime + "; ");
			sb.Append("Sex=" +　Sex + "; ");
			sb.Append("Branch_Id=" +　Branch_Id + "; ");
			sb.Append("Branch_Code=" +　Branch_Code + "; ");
			sb.Append("Branch_Name=" +　Branch_Name + "; ");
			sb.Append("Position_Id=" +　Position_Id + "; ");
			sb.Append("Position_Name=" +　Position_Name + "; ");
			sb.Append("CurrentPage=" +　CurrentPage + "; ");
			sb.Append("CurrentPageTitle=" +　CurrentPageTitle + "; ");
			sb.Append("SessionId=" +　SessionId + "; ");
			sb.Append("UserAgent=" +　UserAgent + "; ");
			sb.Append("OperatingSystem=" +　OperatingSystem + "; ");
			sb.Append("TerminalType=" +　TerminalType + "; ");
			sb.Append("BrowserName=" +　BrowserName + "; ");
			sb.Append("BrowserVersion=" +　BrowserVersion + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(OnlineUsers)){
                OnlineUsers compare=(OnlineUsers)obj;
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
                            return this.UserHashKey.ToString();
                    }

        public string DescriptorColumn() {
            return "UserHashKey";
        }
        public static string GetKeyColumn()
        {
            return "Id";
        }        
        public static string GetDescriptorColumn()
        {
            return "UserHashKey";
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

        string _UserHashKey;
		/// <summary>
		/// 在线用户列表中的HashTable Key值
		/// </summary>
        public string UserHashKey
        {
            get { return _UserHashKey; }
            set
            {
                if(_UserHashKey!=value || _isLoaded){
                    _UserHashKey=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserHashKey");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Manager_Id;
		/// <summary>
		/// 用户Id
		/// </summary>
        public int Manager_Id
        {
            get { return _Manager_Id; }
            set
            {
                if(_Manager_Id!=value || _isLoaded){
                    _Manager_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Manager_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Manager_LoginName;
		/// <summary>
		/// 登陆账号
		/// </summary>
        public string Manager_LoginName
        {
            get { return _Manager_LoginName; }
            set
            {
                if(_Manager_LoginName!=value || _isLoaded){
                    _Manager_LoginName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Manager_LoginName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Manager_LoginPass;
		/// <summary>
		/// 登陆密码
		/// </summary>
        public string Manager_LoginPass
        {
            get { return _Manager_LoginPass; }
            set
            {
                if(_Manager_LoginPass!=value || _isLoaded){
                    _Manager_LoginPass=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Manager_LoginPass");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Manager_CName;
		/// <summary>
		/// 用户中文名称
		/// </summary>
        public string Manager_CName
        {
            get { return _Manager_CName; }
            set
            {
                if(_Manager_CName!=value || _isLoaded){
                    _Manager_CName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Manager_CName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _LoginTime;
		/// <summary>
		/// 登陆时间
		/// </summary>
        public DateTime LoginTime
        {
            get { return _LoginTime; }
            set
            {
                if(_LoginTime!=value || _isLoaded){
                    _LoginTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LoginTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _LoginIp;
		/// <summary>
		/// 登陆IP
		/// </summary>
        public string LoginIp
        {
            get { return _LoginIp; }
            set
            {
                if(_LoginIp!=value || _isLoaded){
                    _LoginIp=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LoginIp");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _UserKey;
		/// <summary>
		/// 用户密钥
		/// </summary>
        public string UserKey
        {
            get { return _UserKey; }
            set
            {
                if(_UserKey!=value || _isLoaded){
                    _UserKey=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserKey");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Md5;
		/// <summary>
		/// Md5(密钥+登陆帐号+密码+IP+密钥.Substring(6,8))
		/// </summary>
        public string Md5
        {
            get { return _Md5; }
            set
            {
                if(_Md5!=value || _isLoaded){
                    _Md5=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Md5");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _UpdateTime;
		/// <summary>
		/// 最后在线时间
		/// </summary>
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set
            {
                if(_UpdateTime!=value || _isLoaded){
                    _UpdateTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UpdateTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Sex;
		/// <summary>
		/// 性别（0=未知，1=男，2=女）
		/// </summary>
        public string Sex
        {
            get { return _Sex; }
            set
            {
                if(_Sex!=value || _isLoaded){
                    _Sex=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Sex");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Branch_Id;
		/// <summary>
		/// 所属部门ID
		/// </summary>
        public int Branch_Id
        {
            get { return _Branch_Id; }
            set
            {
                if(_Branch_Id!=value || _isLoaded){
                    _Branch_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Branch_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Branch_Code;
		/// <summary>
		/// 所属部门编号，用户只能正式归属于一个部门
		/// </summary>
        public string Branch_Code
        {
            get { return _Branch_Code; }
            set
            {
                if(_Branch_Code!=value || _isLoaded){
                    _Branch_Code=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Branch_Code");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Branch_Name;
		/// <summary>
		/// 部门名称
		/// </summary>
        public string Branch_Name
        {
            get { return _Branch_Name; }
            set
            {
                if(_Branch_Name!=value || _isLoaded){
                    _Branch_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Branch_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Position_Id;
		/// <summary>
		/// 用户职位ID
		/// </summary>
        public string Position_Id
        {
            get { return _Position_Id; }
            set
            {
                if(_Position_Id!=value || _isLoaded){
                    _Position_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Position_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Position_Name;
		/// <summary>
		/// 职位名称
		/// </summary>
        public string Position_Name
        {
            get { return _Position_Name; }
            set
            {
                if(_Position_Name!=value || _isLoaded){
                    _Position_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Position_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _CurrentPage;
		/// <summary>
		/// 用户当前所在页面Url
		/// </summary>
        public string CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                if(_CurrentPage!=value || _isLoaded){
                    _CurrentPage=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CurrentPage");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _CurrentPageTitle;
		/// <summary>
		/// 用户当前所在页面名称
		/// </summary>
        public string CurrentPageTitle
        {
            get { return _CurrentPageTitle; }
            set
            {
                if(_CurrentPageTitle!=value || _isLoaded){
                    _CurrentPageTitle=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CurrentPageTitle");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _SessionId;
		/// <summary>
		/// 用户SessionId
		/// </summary>
        public string SessionId
        {
            get { return _SessionId; }
            set
            {
                if(_SessionId!=value || _isLoaded){
                    _SessionId=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SessionId");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _UserAgent;
		/// <summary>
		/// 客户端UA
		/// </summary>
        public string UserAgent
        {
            get { return _UserAgent; }
            set
            {
                if(_UserAgent!=value || _isLoaded){
                    _UserAgent=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserAgent");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _OperatingSystem;
		/// <summary>
		/// 操作系统
		/// </summary>
        public string OperatingSystem
        {
            get { return _OperatingSystem; }
            set
            {
                if(_OperatingSystem!=value || _isLoaded){
                    _OperatingSystem=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="OperatingSystem");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _TerminalType;
		/// <summary>
		/// 终端类型（0=非移动设备，1=移动设备）
		/// </summary>
        public int TerminalType
        {
            get { return _TerminalType; }
            set
            {
                if(_TerminalType!=value || _isLoaded){
                    _TerminalType=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="TerminalType");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _BrowserName;
		/// <summary>
		/// 浏览器名称
		/// </summary>
        public string BrowserName
        {
            get { return _BrowserName; }
            set
            {
                if(_BrowserName!=value || _isLoaded){
                    _BrowserName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BrowserName");
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
		/// 浏览器的版本
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


        public static void Delete(Expression<Func<OnlineUsers, bool>> expression) {
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

