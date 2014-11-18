 
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
    /// A class which represents the UploadConfig table in the SolutionDataBase Database.
    /// </summary>
    public partial class UploadConfig: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<UploadConfig> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<UploadConfig>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<UploadConfig> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(UploadConfig item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                UploadConfig item=new UploadConfig();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<UploadConfig> _repo;
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
        public UploadConfig(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                UploadConfig.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<UploadConfig>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public UploadConfig(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            Name = readRecord.get_string("Name",null);
               
            JoinName = readRecord.get_string("JoinName",null);
               
            UserType = readRecord.get_byte("UserType",null);
               
            UploadType_Id = readRecord.get_int("UploadType_Id",null);
               
            UploadType_Name = readRecord.get_string("UploadType_Name",null);
               
            UploadType_TypeKey = readRecord.get_string("UploadType_TypeKey",null);
               
            PicSize = readRecord.get_int("PicSize",null);
               
            FileSize = readRecord.get_int("FileSize",null);
               
            SaveDir = readRecord.get_string("SaveDir",null);
               
            IsPost = readRecord.get_byte("IsPost",null);
               
            IsSwf = readRecord.get_byte("IsSwf",null);
               
            IsChkSrcPost = readRecord.get_byte("IsChkSrcPost",null);
               
            IsFixPic = readRecord.get_byte("IsFixPic",null);
               
            CutType = readRecord.get_int("CutType",null);
               
            PicWidth = readRecord.get_int("PicWidth",null);
               
            PicHeight = readRecord.get_int("PicHeight",null);
               
            PicQuality = readRecord.get_int("PicQuality",null);
               
            IsEditor = readRecord.get_byte("IsEditor",null);
               
            IsBigPic = readRecord.get_byte("IsBigPic",null);
               
            BigWidth = readRecord.get_int("BigWidth",null);
               
            BigHeight = readRecord.get_int("BigHeight",null);
               
            BigQuality = readRecord.get_int("BigQuality",null);
               
            IsMidPic = readRecord.get_byte("IsMidPic",null);
               
            MidWidth = readRecord.get_int("MidWidth",null);
               
            MidHeight = readRecord.get_int("MidHeight",null);
               
            MidQuality = readRecord.get_int("MidQuality",null);
               
            IsMinPic = readRecord.get_byte("IsMinPic",null);
               
            MinWidth = readRecord.get_int("MinWidth",null);
               
            MinHeight = readRecord.get_int("MinHeight",null);
               
            MinQuality = readRecord.get_int("MinQuality",null);
               
            IsHotPic = readRecord.get_byte("IsHotPic",null);
               
            HotWidth = readRecord.get_int("HotWidth",null);
               
            HotHeight = readRecord.get_int("HotHeight",null);
               
            HotQuality = readRecord.get_int("HotQuality",null);
               
            IsWaterPic = readRecord.get_byte("IsWaterPic",null);
               
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

        public UploadConfig(Expression<Func<UploadConfig, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<UploadConfig> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<UploadConfig> _repo;
            
            if(db.TestMode){
                UploadConfig.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<UploadConfig>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<UploadConfig> GetRepo(){
            return GetRepo("","");
        }
        
        public static UploadConfig SingleOrDefault(Expression<Func<UploadConfig, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            UploadConfig single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static UploadConfig SingleOrDefault(Expression<Func<UploadConfig, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            UploadConfig single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<UploadConfig, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<UploadConfig, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<UploadConfig> Find(Expression<Func<UploadConfig, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<UploadConfig> Find(Expression<Func<UploadConfig, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<UploadConfig> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<UploadConfig> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<UploadConfig> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<UploadConfig> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<UploadConfig> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<UploadConfig> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("JoinName=" +　JoinName + "; ");
			sb.Append("UserType=" +　UserType + "; ");
			sb.Append("UploadType_Id=" +　UploadType_Id + "; ");
			sb.Append("UploadType_Name=" +　UploadType_Name + "; ");
			sb.Append("UploadType_TypeKey=" +　UploadType_TypeKey + "; ");
			sb.Append("PicSize=" +　PicSize + "; ");
			sb.Append("FileSize=" +　FileSize + "; ");
			sb.Append("SaveDir=" +　SaveDir + "; ");
			sb.Append("IsPost=" +　IsPost + "; ");
			sb.Append("IsSwf=" +　IsSwf + "; ");
			sb.Append("IsChkSrcPost=" +　IsChkSrcPost + "; ");
			sb.Append("IsFixPic=" +　IsFixPic + "; ");
			sb.Append("CutType=" +　CutType + "; ");
			sb.Append("PicWidth=" +　PicWidth + "; ");
			sb.Append("PicHeight=" +　PicHeight + "; ");
			sb.Append("PicQuality=" +　PicQuality + "; ");
			sb.Append("IsEditor=" +　IsEditor + "; ");
			sb.Append("IsBigPic=" +　IsBigPic + "; ");
			sb.Append("BigWidth=" +　BigWidth + "; ");
			sb.Append("BigHeight=" +　BigHeight + "; ");
			sb.Append("BigQuality=" +　BigQuality + "; ");
			sb.Append("IsMidPic=" +　IsMidPic + "; ");
			sb.Append("MidWidth=" +　MidWidth + "; ");
			sb.Append("MidHeight=" +　MidHeight + "; ");
			sb.Append("MidQuality=" +　MidQuality + "; ");
			sb.Append("IsMinPic=" +　IsMinPic + "; ");
			sb.Append("MinWidth=" +　MinWidth + "; ");
			sb.Append("MinHeight=" +　MinHeight + "; ");
			sb.Append("MinQuality=" +　MinQuality + "; ");
			sb.Append("IsHotPic=" +　IsHotPic + "; ");
			sb.Append("HotWidth=" +　HotWidth + "; ");
			sb.Append("HotHeight=" +　HotHeight + "; ");
			sb.Append("HotQuality=" +　HotQuality + "; ");
			sb.Append("IsWaterPic=" +　IsWaterPic + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(UploadConfig)){
                UploadConfig compare=(UploadConfig)obj;
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
		/// 唯一索引，但不自增，
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
		/// 模块名称
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

        string _JoinName;
		/// <summary>
		/// 关联表名
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

        byte _UserType;
		/// <summary>
		/// 用户类别：1=管理员上传，2=会员上传
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

        int _UploadType_Id;
		/// <summary>
		/// 上传类型表主键
		/// </summary>
        public int UploadType_Id
        {
            get { return _UploadType_Id; }
            set
            {
                if(_UploadType_Id!=value || _isLoaded){
                    _UploadType_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UploadType_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _UploadType_Name;
		/// <summary>
		/// 上传类型名称
		/// </summary>
        public string UploadType_Name
        {
            get { return _UploadType_Name; }
            set
            {
                if(_UploadType_Name!=value || _isLoaded){
                    _UploadType_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UploadType_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _UploadType_TypeKey;
		/// <summary>
		/// 上传类型：image(默认),flash,media,file,editor，绑定UploadType表对应字段
		/// </summary>
        public string UploadType_TypeKey
        {
            get { return _UploadType_TypeKey; }
            set
            {
                if(_UploadType_TypeKey!=value || _isLoaded){
                    _UploadType_TypeKey=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UploadType_TypeKey");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _PicSize;
		/// <summary>
		/// 图片类,允许最大上传Size（单位：KB），默认:200 =200 KB，"上传类别"为image专用
		/// </summary>
        public int PicSize
        {
            get { return _PicSize; }
            set
            {
                if(_PicSize!=value || _isLoaded){
                    _PicSize=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PicSize");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _FileSize;
		/// <summary>
		/// 附件类,允许最大上传Size（单位：KB），默认:20000 = 20 M，当使用"上传类别"非image的情况下使用
		/// </summary>
        public int FileSize
        {
            get { return _FileSize; }
            set
            {
                if(_FileSize!=value || _isLoaded){
                    _FileSize=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FileSize");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _SaveDir;
		/// <summary>
		/// 保存的目录"/UploadFile/n/","/UploadFile/p/"
		/// </summary>
        public string SaveDir
        {
            get { return _SaveDir; }
            set
            {
                if(_SaveDir!=value || _isLoaded){
                    _SaveDir=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SaveDir");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsPost;
		/// <summary>
		/// 1=使用中,0=停止使用
		/// </summary>
        public byte IsPost
        {
            get { return _IsPost; }
            set
            {
                if(_IsPost!=value || _isLoaded){
                    _IsPost=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsPost");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsSwf;
		/// <summary>
		/// 1=flash上传，0=web上传
		/// </summary>
        public byte IsSwf
        {
            get { return _IsSwf; }
            set
            {
                if(_IsSwf!=value || _isLoaded){
                    _IsSwf=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsSwf");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsChkSrcPost;
		/// <summary>
		/// 是否检查提交输入口是否为本服务器（Flash提交的必须设置为false，不用检查）
		/// </summary>
        public byte IsChkSrcPost
        {
            get { return _IsChkSrcPost; }
            set
            {
                if(_IsChkSrcPost!=value || _isLoaded){
                    _IsChkSrcPost=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsChkSrcPost");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsFixPic;
		/// <summary>
		/// 是否按比例生成
		/// </summary>
        public byte IsFixPic
        {
            get { return _IsFixPic; }
            set
            {
                if(_IsFixPic!=value || _isLoaded){
                    _IsFixPic=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsFixPic");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _CutType;
		/// <summary>
		/// 0=按比例生成宽高，1=固定图片宽高，2=固定背景宽高，图片按比例生成
		/// </summary>
        public int CutType
        {
            get { return _CutType; }
            set
            {
                if(_CutType!=value || _isLoaded){
                    _CutType=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CutType");
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
		/// 最大宽度，超过将按比例进行缩放
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
		/// 最大高度，超过将按比例进行缩放
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

        int _PicQuality;
		/// <summary>
		/// 图片质量，0=使用默认值，>0指定质量值（指定值的情况下，范围：50-100）
		/// </summary>
        public int PicQuality
        {
            get { return _PicQuality; }
            set
            {
                if(_PicQuality!=value || _isLoaded){
                    _PicQuality=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PicQuality");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsEditor;
		/// <summary>
		/// 1=编辑器专用,0=web
		/// </summary>
        public byte IsEditor
        {
            get { return _IsEditor; }
            set
            {
                if(_IsEditor!=value || _isLoaded){
                    _IsEditor=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsEditor");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsBigPic;
		/// <summary>
		/// 是否创建大图(原始图片)，1=是，0=否
		/// </summary>
        public byte IsBigPic
        {
            get { return _IsBigPic; }
            set
            {
                if(_IsBigPic!=value || _isLoaded){
                    _IsBigPic=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsBigPic");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BigWidth;
		/// <summary>
		/// 大图宽度
		/// </summary>
        public int BigWidth
        {
            get { return _BigWidth; }
            set
            {
                if(_BigWidth!=value || _isLoaded){
                    _BigWidth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BigWidth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BigHeight;
		/// <summary>
		/// 大图高度
		/// </summary>
        public int BigHeight
        {
            get { return _BigHeight; }
            set
            {
                if(_BigHeight!=value || _isLoaded){
                    _BigHeight=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BigHeight");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BigQuality;
		/// <summary>
		/// 大图压缩质量
		/// </summary>
        public int BigQuality
        {
            get { return _BigQuality; }
            set
            {
                if(_BigQuality!=value || _isLoaded){
                    _BigQuality=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BigQuality");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsMidPic;
		/// <summary>
		/// 是否创建中图，1=是，0=否
		/// </summary>
        public byte IsMidPic
        {
            get { return _IsMidPic; }
            set
            {
                if(_IsMidPic!=value || _isLoaded){
                    _IsMidPic=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsMidPic");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _MidWidth;
		/// <summary>
		/// 中图宽度
		/// </summary>
        public int MidWidth
        {
            get { return _MidWidth; }
            set
            {
                if(_MidWidth!=value || _isLoaded){
                    _MidWidth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MidWidth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _MidHeight;
		/// <summary>
		/// 中图高度
		/// </summary>
        public int MidHeight
        {
            get { return _MidHeight; }
            set
            {
                if(_MidHeight!=value || _isLoaded){
                    _MidHeight=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MidHeight");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _MidQuality;
		/// <summary>
		/// 中图压缩质量
		/// </summary>
        public int MidQuality
        {
            get { return _MidQuality; }
            set
            {
                if(_MidQuality!=value || _isLoaded){
                    _MidQuality=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MidQuality");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsMinPic;
		/// <summary>
		/// 是否创建小图，1=是，0=否
		/// </summary>
        public byte IsMinPic
        {
            get { return _IsMinPic; }
            set
            {
                if(_IsMinPic!=value || _isLoaded){
                    _IsMinPic=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsMinPic");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _MinWidth;
		/// <summary>
		/// 小图宽度
		/// </summary>
        public int MinWidth
        {
            get { return _MinWidth; }
            set
            {
                if(_MinWidth!=value || _isLoaded){
                    _MinWidth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MinWidth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _MinHeight;
		/// <summary>
		/// 小图高度
		/// </summary>
        public int MinHeight
        {
            get { return _MinHeight; }
            set
            {
                if(_MinHeight!=value || _isLoaded){
                    _MinHeight=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MinHeight");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _MinQuality;
		/// <summary>
		/// 小图压缩质量
		/// </summary>
        public int MinQuality
        {
            get { return _MinQuality; }
            set
            {
                if(_MinQuality!=value || _isLoaded){
                    _MinQuality=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MinQuality");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsHotPic;
		/// <summary>
		/// 是否创建推荐图，1=是，0=否
		/// </summary>
        public byte IsHotPic
        {
            get { return _IsHotPic; }
            set
            {
                if(_IsHotPic!=value || _isLoaded){
                    _IsHotPic=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsHotPic");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _HotWidth;
		/// <summary>
		/// 推荐图宽度
		/// </summary>
        public int HotWidth
        {
            get { return _HotWidth; }
            set
            {
                if(_HotWidth!=value || _isLoaded){
                    _HotWidth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="HotWidth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _HotHeight;
		/// <summary>
		/// 推荐图高度
		/// </summary>
        public int HotHeight
        {
            get { return _HotHeight; }
            set
            {
                if(_HotHeight!=value || _isLoaded){
                    _HotHeight=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="HotHeight");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _HotQuality;
		/// <summary>
		/// 推荐图压缩质量
		/// </summary>
        public int HotQuality
        {
            get { return _HotQuality; }
            set
            {
                if(_HotQuality!=value || _isLoaded){
                    _HotQuality=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="HotQuality");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsWaterPic;
		/// <summary>
		/// 是否加水印，1=是，0=否
		/// </summary>
        public byte IsWaterPic
        {
            get { return _IsWaterPic; }
            set
            {
                if(_IsWaterPic!=value || _isLoaded){
                    _IsWaterPic=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsWaterPic");
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


        public static void Delete(Expression<Func<UploadConfig, bool>> expression) {
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

