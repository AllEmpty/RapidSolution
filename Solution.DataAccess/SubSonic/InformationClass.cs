 
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
    /// A class which represents the InformationClass table in the SolutionDataBase Database.
    /// </summary>
    public partial class InformationClass: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<InformationClass> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<InformationClass>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<InformationClass> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(InformationClass item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                InformationClass item=new InformationClass();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<InformationClass> _repo;
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
        public InformationClass(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                InformationClass.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<InformationClass>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public InformationClass(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            Name = readRecord.get_string("Name",null);
               
            Notes = readRecord.get_string("Notes",null);
               
            IsSys = readRecord.get_byte("IsSys",null);
               
            ClassImg = readRecord.get_string("ClassImg",null);
               
            IsShow = readRecord.get_byte("IsShow",null);
               
            IsPage = readRecord.get_byte("IsPage",null);
               
            RootId = readRecord.get_int("RootId",null);
               
            ParentId = readRecord.get_int("ParentId",null);
               
            Depth = readRecord.get_int("Depth",null);
               
            Sort = readRecord.get_int("Sort",null);
               
            SeoTitle = readRecord.get_string("SeoTitle",null);
               
            SeoKey = readRecord.get_string("SeoKey",null);
               
            SeoDesc = readRecord.get_string("SeoDesc",null);
               
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

        public InformationClass(Expression<Func<InformationClass, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<InformationClass> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<InformationClass> _repo;
            
            if(db.TestMode){
                InformationClass.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<InformationClass>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<InformationClass> GetRepo(){
            return GetRepo("","");
        }
        
        public static InformationClass SingleOrDefault(Expression<Func<InformationClass, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            InformationClass single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static InformationClass SingleOrDefault(Expression<Func<InformationClass, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            InformationClass single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<InformationClass, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<InformationClass, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<InformationClass> Find(Expression<Func<InformationClass, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<InformationClass> Find(Expression<Func<InformationClass, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<InformationClass> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<InformationClass> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<InformationClass> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<InformationClass> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<InformationClass> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<InformationClass> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("Notes=" +　Notes + "; ");
			sb.Append("IsSys=" +　IsSys + "; ");
			sb.Append("ClassImg=" +　ClassImg + "; ");
			sb.Append("IsShow=" +　IsShow + "; ");
			sb.Append("IsPage=" +　IsPage + "; ");
			sb.Append("RootId=" +　RootId + "; ");
			sb.Append("ParentId=" +　ParentId + "; ");
			sb.Append("Depth=" +　Depth + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("SeoTitle=" +　SeoTitle + "; ");
			sb.Append("SeoKey=" +　SeoKey + "; ");
			sb.Append("SeoDesc=" +　SeoDesc + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(InformationClass)){
                InformationClass compare=(InformationClass)obj;
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
		/// 信息名称
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

        string _Notes;
		/// <summary>
		/// 描述
		/// </summary>
        public string Notes
        {
            get { return _Notes; }
            set
            {
                if(_Notes!=value || _isLoaded){
                    _Notes=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Notes");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsSys;
		/// <summary>
		/// 1=系统分类（不能删除，不能添加文章，但可以添加子分类）
		/// </summary>
        public byte IsSys
        {
            get { return _IsSys; }
            set
            {
                if(_IsSys!=value || _isLoaded){
                    _IsSys=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsSys");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _ClassImg;
		/// <summary>
		/// 分类图
		/// </summary>
        public string ClassImg
        {
            get { return _ClassImg; }
            set
            {
                if(_ClassImg!=value || _isLoaded){
                    _ClassImg=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ClassImg");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsShow;
		/// <summary>
		/// 是否显示, 0=False,1=True,
		/// </summary>
        public byte IsShow
        {
            get { return _IsShow; }
            set
            {
                if(_IsShow!=value || _isLoaded){
                    _IsShow=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsShow");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsPage;
		/// <summary>
		/// 是否为单页,单页，没有文章封面，没有发表者，也不能评论
		/// </summary>
        public byte IsPage
        {
            get { return _IsPage; }
            set
            {
                if(_IsPage!=value || _isLoaded){
                    _IsPage=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsPage");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _RootId;
		/// <summary>
		/// 分类顶层id
		/// </summary>
        public int RootId
        {
            get { return _RootId; }
            set
            {
                if(_RootId!=value || _isLoaded){
                    _RootId=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RootId");
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
		/// 父id
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

        int _Depth;
		/// <summary>
		/// 层数
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

        string _SeoTitle;
		/// <summary>
		/// Seo标题
		/// </summary>
        public string SeoTitle
        {
            get { return _SeoTitle; }
            set
            {
                if(_SeoTitle!=value || _isLoaded){
                    _SeoTitle=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SeoTitle");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _SeoKey;
		/// <summary>
		/// Seo关键字(搜索文章)
		/// </summary>
        public string SeoKey
        {
            get { return _SeoKey; }
            set
            {
                if(_SeoKey!=value || _isLoaded){
                    _SeoKey=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SeoKey");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _SeoDesc;
		/// <summary>
		/// Seo描述
		/// </summary>
        public string SeoDesc
        {
            get { return _SeoDesc; }
            set
            {
                if(_SeoDesc!=value || _isLoaded){
                    _SeoDesc=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SeoDesc");
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
		/// 修改人员id
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
		/// 修改人员姓名
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


        public static void Delete(Expression<Func<InformationClass, bool>> expression) {
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

