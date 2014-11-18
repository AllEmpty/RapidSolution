 
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
    /// A class which represents the V_Position_Branch table in the SolutionDataBase Database.
    /// </summary>
    public partial class V_Position_Branch: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<V_Position_Branch> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<V_Position_Branch>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<V_Position_Branch> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(V_Position_Branch item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                V_Position_Branch item=new V_Position_Branch();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<V_Position_Branch> _repo;
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
        public V_Position_Branch(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                V_Position_Branch.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<V_Position_Branch>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public V_Position_Branch(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Code = readRecord.get_string("Code",null);
               
            Id = readRecord.get_int("Id",null);
               
            Name = readRecord.get_string("Name",null);
               
            Branch_Id = readRecord.get_int("Branch_Id",null);
               
            Branch_Code = readRecord.get_string("Branch_Code",null);
               
            Branch_Name = readRecord.get_string("Branch_Name",null);
               
            PagePower = readRecord.get_string("PagePower",null);
               
            ControlPower = readRecord.get_string("ControlPower",null);
               
            IsSetBranchPower = readRecord.get_byte("IsSetBranchPower",null);
               
            SetBranchCode = readRecord.get_string("SetBranchCode",null);
               
            Manager_Id = readRecord.get_int("Manager_Id",null);
               
            Manager_CName = readRecord.get_string("Manager_CName",null);
               
            UpdateDate = readRecord.get_datetime("UpdateDate",null);
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

        public V_Position_Branch(Expression<Func<V_Position_Branch, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<V_Position_Branch> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<V_Position_Branch> _repo;
            
            if(db.TestMode){
                V_Position_Branch.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<V_Position_Branch>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<V_Position_Branch> GetRepo(){
            return GetRepo("","");
        }
        
        public static V_Position_Branch SingleOrDefault(Expression<Func<V_Position_Branch, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            V_Position_Branch single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static V_Position_Branch SingleOrDefault(Expression<Func<V_Position_Branch, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            V_Position_Branch single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<V_Position_Branch, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<V_Position_Branch, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<V_Position_Branch> Find(Expression<Func<V_Position_Branch, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<V_Position_Branch> Find(Expression<Func<V_Position_Branch, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<V_Position_Branch> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<V_Position_Branch> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<V_Position_Branch> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<V_Position_Branch> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<V_Position_Branch> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<V_Position_Branch> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "Code";
        }

        public object KeyValue()
        {
            return this.Code;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<string>();
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
			sb.Append("Code=" +　Code + "; ");
			sb.Append("Id=" +　Id + "; ");
			sb.Append("Name=" +　Name + "; ");
			sb.Append("Branch_Id=" +　Branch_Id + "; ");
			sb.Append("Branch_Code=" +　Branch_Code + "; ");
			sb.Append("Branch_Name=" +　Branch_Name + "; ");
			sb.Append("PagePower=" +　PagePower + "; ");
			sb.Append("ControlPower=" +　ControlPower + "; ");
			sb.Append("IsSetBranchPower=" +　IsSetBranchPower + "; ");
			sb.Append("SetBranchCode=" +　SetBranchCode + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(V_Position_Branch)){
                V_Position_Branch compare=(V_Position_Branch)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
                            return this.Code.ToString();
                    }

        public string DescriptorColumn() {
            return "Code";
        }
        public static string GetKeyColumn()
        {
            return "Code";
        }        
        public static string GetDescriptorColumn()
        {
            return "Code";
        }
        
        #region ' Foreign Keys '
        #endregion
        

        string _Code;
		/// <summary>
		/// 
		/// </summary>
        public string Code
        {
            get { return _Code; }
            set
            {
                if(_Code!=value || _isLoaded){
                    _Code=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Code");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Id;
		/// <summary>
		/// 
		/// </summary>
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
		/// 
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

        int _Branch_Id;
		/// <summary>
		/// 
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
		/// 
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
		/// 
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

        string _PagePower;
		/// <summary>
		/// 
		/// </summary>
        public string PagePower
        {
            get { return _PagePower; }
            set
            {
                if(_PagePower!=value || _isLoaded){
                    _PagePower=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PagePower");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _ControlPower;
		/// <summary>
		/// 
		/// </summary>
        public string ControlPower
        {
            get { return _ControlPower; }
            set
            {
                if(_ControlPower!=value || _isLoaded){
                    _ControlPower=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ControlPower");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsSetBranchPower;
		/// <summary>
		/// 
		/// </summary>
        public byte IsSetBranchPower
        {
            get { return _IsSetBranchPower; }
            set
            {
                if(_IsSetBranchPower!=value || _isLoaded){
                    _IsSetBranchPower=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsSetBranchPower");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _SetBranchCode;
		/// <summary>
		/// 
		/// </summary>
        public string SetBranchCode
        {
            get { return _SetBranchCode; }
            set
            {
                if(_SetBranchCode!=value || _isLoaded){
                    _SetBranchCode=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SetBranchCode");
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
		/// 
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

        string _Manager_CName;
		/// <summary>
		/// 
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

        DateTime _UpdateDate;
		/// <summary>
		/// 
		/// </summary>
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if(_UpdateDate!=value || _isLoaded){
                    _UpdateDate=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UpdateDate");
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


        public static void Delete(Expression<Func<V_Position_Branch, bool>> expression) {
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

