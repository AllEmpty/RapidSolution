 
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
    /// A class which represents the UploadFile table in the SolutionDataBase Database.
    /// </summary>
    public partial class UploadFile: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<UploadFile> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<UploadFile>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<UploadFile> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(UploadFile item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                UploadFile item=new UploadFile();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<UploadFile> _repo;
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
        public UploadFile(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                UploadFile.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<UploadFile>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public UploadFile(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            Name = readRecord.get_string("Name",null);
               
            Path = readRecord.get_string("Path",null);
               
            Ext = readRecord.get_string("Ext",null);
               
            Src = readRecord.get_string("Src",null);
               
            Size = readRecord.get_int("Size",null);
               
            PicWidth = readRecord.get_int("PicWidth",null);
               
            PicHeight = readRecord.get_int("PicHeight",null);
               
            UploadConfig_Id = readRecord.get_int("UploadConfig_Id",null);
               
            JoinName = readRecord.get_string("JoinName",null);
               
            JoinId = readRecord.get_int("JoinId",null);
               
            UserType = readRecord.get_byte("UserType",null);
               
            UserId = readRecord.get_int("UserId",null);
               
            UserName = readRecord.get_string("UserName",null);
               
            UserIp = readRecord.get_string("UserIp",null);
               
            AddDate = readRecord.get_datetime("AddDate",null);
               
            InfoText = readRecord.get_string("InfoText",null);
               
            RndKey = readRecord.get_string("RndKey",null);
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

        public UploadFile(Expression<Func<UploadFile, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<UploadFile> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<UploadFile> _repo;
            
            if(db.TestMode){
                UploadFile.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<UploadFile>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<UploadFile> GetRepo(){
            return GetRepo("","");
        }
        
        public static UploadFile SingleOrDefault(Expression<Func<UploadFile, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            UploadFile single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static UploadFile SingleOrDefault(Expression<Func<UploadFile, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            UploadFile single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<UploadFile, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<UploadFile, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<UploadFile> Find(Expression<Func<UploadFile, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<UploadFile> Find(Expression<Func<UploadFile, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<UploadFile> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<UploadFile> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<UploadFile> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<UploadFile> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<UploadFile> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<UploadFile> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("Path=" +　Path + "; ");
			sb.Append("Ext=" +　Ext + "; ");
			sb.Append("Src=" +　Src + "; ");
			sb.Append("Size=" +　Size + "; ");
			sb.Append("PicWidth=" +　PicWidth + "; ");
			sb.Append("PicHeight=" +　PicHeight + "; ");
			sb.Append("UploadConfig_Id=" +　UploadConfig_Id + "; ");
			sb.Append("JoinName=" +　JoinName + "; ");
			sb.Append("JoinId=" +　JoinId + "; ");
			sb.Append("UserType=" +　UserType + "; ");
			sb.Append("UserId=" +　UserId + "; ");
			sb.Append("UserName=" +　UserName + "; ");
			sb.Append("UserIp=" +　UserIp + "; ");
			sb.Append("AddDate=" +　AddDate + "; ");
			sb.Append("InfoText=" +　InfoText + "; ");
			sb.Append("RndKey=" +　RndKey + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(UploadFile)){
                UploadFile compare=(UploadFile)obj;
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
		/// 新文件名（包括扩展名）
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

        string _Path;
		/// <summary>
		/// 新路径（包括文件名）
		/// </summary>
        public string Path
        {
            get { return _Path; }
            set
            {
                if(_Path!=value || _isLoaded){
                    _Path=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Path");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Ext;
		/// <summary>
		/// 扩展名
		/// </summary>
        public string Ext
        {
            get { return _Ext; }
            set
            {
                if(_Ext!=value || _isLoaded){
                    _Ext=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Ext");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Src;
		/// <summary>
		/// 原文件名（包括扩展名）
		/// </summary>
        public string Src
        {
            get { return _Src; }
            set
            {
                if(_Src!=value || _isLoaded){
                    _Src=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Src");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Size;
		/// <summary>
		/// 文件大小
		/// </summary>
        public int Size
        {
            get { return _Size; }
            set
            {
                if(_Size!=value || _isLoaded){
                    _Size=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Size");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _PicWidth;
		/// <summary>
		/// 图片的宽
		/// </summary>
        public int PicWidth
        {
            get { return _PicWidth; }
            set
            {
                if(_PicWidth!=value || _isLoaded){
                    _PicWidth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PicWidth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _PicHeight;
		/// <summary>
		/// 图片的高
		/// </summary>
        public int PicHeight
        {
            get { return _PicHeight; }
            set
            {
                if(_PicHeight!=value || _isLoaded){
                    _PicHeight=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PicHeight");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UploadConfig_Id;
		/// <summary>
		/// 系统ID:---UploadConfig_Id
			/// 1=后台--新闻封面/新闻编辑器
		/// </summary>
        public int UploadConfig_Id
        {
            get { return _UploadConfig_Id; }
            set
            {
                if(_UploadConfig_Id!=value || _isLoaded){
                    _UploadConfig_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UploadConfig_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _JoinName;
		/// <summary>
		/// 关联表ID--1=NewsInfo,2=PrdInfo,
		/// </summary>
        public string JoinName
        {
            get { return _JoinName; }
            set
            {
                if(_JoinName!=value || _isLoaded){
                    _JoinName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="JoinName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _JoinId;
		/// <summary>
		/// 关联ID--所属的文章ID,产品ID，头像等
		/// </summary>
        public int JoinId
        {
            get { return _JoinId; }
            set
            {
                if(_JoinId!=value || _isLoaded){
                    _JoinId=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="JoinId");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _UserType;
		/// <summary>
		/// 用户类别:1=管理员上传，2=会员上传
		/// </summary>
        public byte UserType
        {
            get { return _UserType; }
            set
            {
                if(_UserType!=value || _isLoaded){
                    _UserType=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserType");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserId;
		/// <summary>
		/// 上传者ID
		/// </summary>
        public int UserId
        {
            get { return _UserId; }
            set
            {
                if(_UserId!=value || _isLoaded){
                    _UserId=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserId");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _UserName;
		/// <summary>
		/// 上传者Name
		/// </summary>
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if(_UserName!=value || _isLoaded){
                    _UserName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _UserIp;
		/// <summary>
		/// 上传者Ip
		/// </summary>
        public string UserIp
        {
            get { return _UserIp; }
            set
            {
                if(_UserIp!=value || _isLoaded){
                    _UserIp=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserIp");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _AddDate;
		/// <summary>
		/// 添加时间
		/// </summary>
        public DateTime AddDate
        {
            get { return _AddDate; }
            set
            {
                if(_AddDate!=value || _isLoaded){
                    _AddDate=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AddDate");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _InfoText;
		/// <summary>
		/// 备注
		/// </summary>
        public string InfoText
        {
            get { return _InfoText; }
            set
            {
                if(_InfoText!=value || _isLoaded){
                    _InfoText=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InfoText");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _RndKey;
		/// <summary>
		/// 随机Key
		/// </summary>
        public string RndKey
        {
            get { return _RndKey; }
            set
            {
                if(_RndKey!=value || _isLoaded){
                    _RndKey=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RndKey");
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


        public static void Delete(Expression<Func<UploadFile, bool>> expression) {
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

