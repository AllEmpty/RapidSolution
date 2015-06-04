 
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
    /// A class which represents the Advertisement table in the SolutionDataBase Database.
    /// </summary>
    public partial class Advertisement: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Advertisement> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Advertisement>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Advertisement> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Advertisement item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Advertisement item=new Advertisement();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Advertisement> _repo;
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
        public Advertisement(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Advertisement.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Advertisement>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Advertisement(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            Name = readRecord.get_string("Name",null);
               
            Content = readRecord.get_string("Content",null);
               
            Url = readRecord.get_string("Url",null);
               
            Keyword = readRecord.get_string("Keyword",null);
               
            AdvertisingPosition_Id = readRecord.get_int("AdvertisingPosition_Id",null);
               
            AdvertisingPosition_Name = readRecord.get_string("AdvertisingPosition_Name",null);
               
            AdImg = readRecord.get_string("AdImg",null);
               
            ShowRate = readRecord.get_int("ShowRate",null);
               
            StartTime = readRecord.get_datetime("StartTime",null);
               
            EndTime = readRecord.get_datetime("EndTime",null);
               
            IsDisplay = readRecord.get_byte("IsDisplay",null);
               
            HitCount = readRecord.get_int("HitCount",null);
               
            Sort = readRecord.get_int("Sort",null);
               
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

        public Advertisement(Expression<Func<Advertisement, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Advertisement> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<Advertisement> _repo;
            
            if(db.TestMode){
                Advertisement.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Advertisement>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Advertisement> GetRepo(){
            return GetRepo("","");
        }
        
        public static Advertisement SingleOrDefault(Expression<Func<Advertisement, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Advertisement single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Advertisement SingleOrDefault(Expression<Func<Advertisement, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Advertisement single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Advertisement, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Advertisement, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Advertisement> Find(Expression<Func<Advertisement, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Advertisement> Find(Expression<Func<Advertisement, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Advertisement> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Advertisement> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Advertisement> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Advertisement> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Advertisement> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Advertisement> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("Content=" +　Content + "; ");
			sb.Append("Url=" +　Url + "; ");
			sb.Append("Keyword=" +　Keyword + "; ");
			sb.Append("AdvertisingPosition_Id=" +　AdvertisingPosition_Id + "; ");
			sb.Append("AdvertisingPosition_Name=" +　AdvertisingPosition_Name + "; ");
			sb.Append("AdImg=" +　AdImg + "; ");
			sb.Append("ShowRate=" +　ShowRate + "; ");
			sb.Append("StartTime=" +　StartTime + "; ");
			sb.Append("EndTime=" +　EndTime + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("HitCount=" +　HitCount + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Advertisement)){
                Advertisement compare=(Advertisement)obj;
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
		/// 标题
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

        string _Content;
		/// <summary>
		/// 备注
		/// </summary>
        public string Content
        {
            get { return _Content; }
            set
            {
                if(_Content!=value || _isLoaded){
                    _Content=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Content");
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
		/// 链接Url
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

        string _Keyword;
		/// <summary>
		/// 关键字，只能由字母数字组成，主要用于模板标签 {%ad-InfoKey%}
		/// </summary>
        public string Keyword
        {
            get { return _Keyword; }
            set
            {
                if(_Keyword!=value || _isLoaded){
                    _Keyword=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Keyword");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _AdvertisingPosition_Id;
		/// <summary>
		/// 广告位置Id
		/// </summary>
        public int AdvertisingPosition_Id
        {
            get { return _AdvertisingPosition_Id; }
            set
            {
                if(_AdvertisingPosition_Id!=value || _isLoaded){
                    _AdvertisingPosition_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AdvertisingPosition_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _AdvertisingPosition_Name;
		/// <summary>
		/// 广告位置名称
		/// </summary>
        public string AdvertisingPosition_Name
        {
            get { return _AdvertisingPosition_Name; }
            set
            {
                if(_AdvertisingPosition_Name!=value || _isLoaded){
                    _AdvertisingPosition_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AdvertisingPosition_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _AdImg;
		/// <summary>
		/// 图片
		/// </summary>
        public string AdImg
        {
            get { return _AdImg; }
            set
            {
                if(_AdImg!=value || _isLoaded){
                    _AdImg=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AdImg");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ShowRate;
		/// <summary>
		/// 显示频率（同一个位置有多个广告时，这里用来计算它随机出现的频率）
		/// </summary>
        public int ShowRate
        {
            get { return _ShowRate; }
            set
            {
                if(_ShowRate!=value || _isLoaded){
                    _ShowRate=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ShowRate");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _StartTime;
		/// <summary>
		/// 开始时间
		/// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set
            {
                if(_StartTime!=value || _isLoaded){
                    _StartTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="StartTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _EndTime;
		/// <summary>
		/// 结束时间
		/// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set
            {
                if(_EndTime!=value || _isLoaded){
                    _EndTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EndTime");
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
		/// 审核, 0=False,1=True,
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

        int _HitCount;
		/// <summary>
		/// 点击数
		/// </summary>
        public int HitCount
        {
            get { return _HitCount; }
            set
            {
                if(_HitCount!=value || _isLoaded){
                    _HitCount=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="HitCount");
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

        int _Manager_Id;
		/// <summary>
		/// 修改人员id
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
		/// 修改人员姓名
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
		/// 修改时间
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


        public static void Delete(Expression<Func<Advertisement, bool>> expression) {
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

