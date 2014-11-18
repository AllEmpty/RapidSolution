 
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
    /// A class which represents the MenuInfo table in the SolutionDataBase Database.
    /// </summary>
    public partial class MenuInfo: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<MenuInfo> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<MenuInfo>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<MenuInfo> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(MenuInfo item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                MenuInfo item=new MenuInfo();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<MenuInfo> _repo;
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
        public MenuInfo(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                MenuInfo.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<MenuInfo>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public MenuInfo(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            Name = readRecord.get_string("Name",null);
               
            Url = readRecord.get_string("Url",null);
               
            ParentId = readRecord.get_int("ParentId",null);
               
            Sort = readRecord.get_int("Sort",null);
               
            Depth = readRecord.get_int("Depth",null);
               
            IsDisplay = readRecord.get_byte("IsDisplay",null);
               
            IsMenu = readRecord.get_byte("IsMenu",null);
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

        public MenuInfo(Expression<Func<MenuInfo, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<MenuInfo> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<MenuInfo> _repo;
            
            if(db.TestMode){
                MenuInfo.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<MenuInfo>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<MenuInfo> GetRepo(){
            return GetRepo("","");
        }
        
        public static MenuInfo SingleOrDefault(Expression<Func<MenuInfo, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            MenuInfo single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static MenuInfo SingleOrDefault(Expression<Func<MenuInfo, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            MenuInfo single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<MenuInfo, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<MenuInfo, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<MenuInfo> Find(Expression<Func<MenuInfo, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<MenuInfo> Find(Expression<Func<MenuInfo, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<MenuInfo> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<MenuInfo> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<MenuInfo> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<MenuInfo> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<MenuInfo> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<MenuInfo> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Url=" +　Url + "; ");
			sb.Append("ParentId=" +　ParentId + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("Depth=" +　Depth + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("IsMenu=" +　IsMenu + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(MenuInfo)){
                MenuInfo compare=(MenuInfo)obj;
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
                            return this.Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Name";
        }
        public static string GetKeyColumn()
        {
            return "Id";
        }        
        public static string GetDescriptorColumn()
        {
            return "Name";
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

        string _Name;
		/// <summary>
		/// 菜单名称或各个页面功能名称
		/// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name!=value || _isLoaded){
                    _Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Url;
		/// <summary>
		/// 各页面URL（主菜单与分类菜单没有URL）
		/// </summary>
        public string Url
        {
            get { return _Url; }
            set
            {
                if(_Url!=value || _isLoaded){
                    _Url=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Url");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ParentId;
		/// <summary>
		/// 父ID
		/// </summary>
        public int ParentId
        {
            get { return _ParentId; }
            set
            {
                if(_ParentId!=value || _isLoaded){
                    _ParentId=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ParentId");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Sort;
		/// <summary>
		/// 排序
		/// </summary>
        public int Sort
        {
            get { return _Sort; }
            set
            {
                if(_Sort!=value || _isLoaded){
                    _Sort=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Sort");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Depth;
		/// <summary>
		/// 深度
		/// </summary>
        public int Depth
        {
            get { return _Depth; }
            set
            {
                if(_Depth!=value || _isLoaded){
                    _Depth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Depth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsDisplay;
		/// <summary>
		/// 该菜单是否在菜单栏显示，0=不显示，1=显示
		/// </summary>
        public byte IsDisplay
        {
            get { return _IsDisplay; }
            set
            {
                if(_IsDisplay!=value || _isLoaded){
                    _IsDisplay=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsDisplay");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsMenu;
		/// <summary>
		/// 是否是菜单还是页面
		/// </summary>
        public byte IsMenu
        {
            get { return _IsMenu; }
            set
            {
                if(_IsMenu!=value || _isLoaded){
                    _IsMenu=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsMenu");
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


        public static void Delete(Expression<Func<MenuInfo, bool>> expression) {
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

