 
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
    /// A class which represents the Manager table in the SolutionDataBase Database.
    /// </summary>
    public partial class Manager: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Manager> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Manager>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Manager> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Manager item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Manager item=new Manager();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Manager> _repo;
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
        public Manager(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Manager.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Manager>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Manager(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            LoginName = readRecord.get_string("LoginName",null);
               
            LoginPass = readRecord.get_string("LoginPass",null);
               
            LoginTime = readRecord.get_datetime("LoginTime",null);
               
            LoginIp = readRecord.get_string("LoginIp",null);
               
            LoginCount = readRecord.get_int("LoginCount",null);
               
            CreateTime = readRecord.get_datetime("CreateTime",null);
               
            UpdateTime = readRecord.get_datetime("UpdateTime",null);
               
            IsMultiUser = readRecord.get_byte("IsMultiUser",null);
               
            Branch_Id = readRecord.get_int("Branch_Id",null);
               
            Branch_Code = readRecord.get_string("Branch_Code",null);
               
            Branch_Name = readRecord.get_string("Branch_Name",null);
               
            Position_Id = readRecord.get_string("Position_Id",null);
               
            Position_Name = readRecord.get_string("Position_Name",null);
               
            IsWork = readRecord.get_byte("IsWork",null);
               
            IsEnable = readRecord.get_byte("IsEnable",null);
               
            CName = readRecord.get_string("CName",null);
               
            EName = readRecord.get_string("EName",null);
               
            PhotoImg = readRecord.get_string("PhotoImg",null);
               
            Sex = readRecord.get_string("Sex",null);
               
            Birthday = readRecord.get_string("Birthday",null);
               
            NativePlace = readRecord.get_string("NativePlace",null);
               
            NationalName = readRecord.get_string("NationalName",null);
               
            Record = readRecord.get_string("Record",null);
               
            GraduateCollege = readRecord.get_string("GraduateCollege",null);
               
            GraduateSpecialty = readRecord.get_string("GraduateSpecialty",null);
               
            Tel = readRecord.get_string("Tel",null);
               
            Mobile = readRecord.get_string("Mobile",null);
               
            Email = readRecord.get_string("Email",null);
               
            Qq = readRecord.get_string("Qq",null);
               
            Msn = readRecord.get_string("Msn",null);
               
            Address = readRecord.get_string("Address",null);
               
            Content = readRecord.get_string("Content",null);
               
            Manager_Id = readRecord.get_int("Manager_Id",null);
               
            Manager_CName = readRecord.get_string("Manager_CName",null);
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

        public Manager(Expression<Func<Manager, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Manager> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<Manager> _repo;
            
            if(db.TestMode){
                Manager.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Manager>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Manager> GetRepo(){
            return GetRepo("","");
        }
        
        public static Manager SingleOrDefault(Expression<Func<Manager, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Manager single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Manager SingleOrDefault(Expression<Func<Manager, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Manager single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Manager, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Manager, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Manager> Find(Expression<Func<Manager, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Manager> Find(Expression<Func<Manager, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Manager> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Manager> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Manager> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Manager> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Manager> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Manager> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("LoginName=" +　LoginName + "; ");
			sb.Append("LoginPass=" +　LoginPass + "; ");
			sb.Append("LoginTime=" +　LoginTime + "; ");
			sb.Append("LoginIp=" +　LoginIp + "; ");
			sb.Append("LoginCount=" +　LoginCount + "; ");
			sb.Append("CreateTime=" +　CreateTime + "; ");
			sb.Append("UpdateTime=" +　UpdateTime + "; ");
			sb.Append("IsMultiUser=" +　IsMultiUser + "; ");
			sb.Append("Branch_Id=" +　Branch_Id + "; ");
			sb.Append("Branch_Code=" +　Branch_Code + "; ");
			sb.Append("Branch_Name=" +　Branch_Name + "; ");
			sb.Append("Position_Id=" +　Position_Id + "; ");
			sb.Append("Position_Name=" +　Position_Name + "; ");
			sb.Append("IsWork=" +　IsWork + "; ");
			sb.Append("IsEnable=" +　IsEnable + "; ");
			sb.Append("CName=" +　CName + "; ");
			sb.Append("EName=" +　EName + "; ");
			sb.Append("PhotoImg=" +　PhotoImg + "; ");
			sb.Append("Sex=" +　Sex + "; ");
			sb.Append("Birthday=" +　Birthday + "; ");
			sb.Append("NativePlace=" +　NativePlace + "; ");
			sb.Append("NationalName=" +　NationalName + "; ");
			sb.Append("Record=" +　Record + "; ");
			sb.Append("GraduateCollege=" +　GraduateCollege + "; ");
			sb.Append("GraduateSpecialty=" +　GraduateSpecialty + "; ");
			sb.Append("Tel=" +　Tel + "; ");
			sb.Append("Mobile=" +　Mobile + "; ");
			sb.Append("Email=" +　Email + "; ");
			sb.Append("Qq=" +　Qq + "; ");
			sb.Append("Msn=" +　Msn + "; ");
			sb.Append("Address=" +　Address + "; ");
			sb.Append("Content=" +　Content + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Manager)){
                Manager compare=(Manager)obj;
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
                            return this.LoginName.ToString();
                    }

        public string DescriptorColumn() {
            return "LoginName";
        }
        public static string GetKeyColumn()
        {
            return "Id";
        }        
        public static string GetDescriptorColumn()
        {
            return "LoginName";
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

        string _LoginName;
		/// <summary>
		/// 登陆账号
		/// </summary>
        public string LoginName
        {
            get { return _LoginName; }
            set
            {
                if(_LoginName!=value || _isLoaded){
                    _LoginName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LoginName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _LoginPass;
		/// <summary>
		/// 登陆密码
		/// </summary>
        public string LoginPass
        {
            get { return _LoginPass; }
            set
            {
                if(_LoginPass!=value || _isLoaded){
                    _LoginPass=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LoginPass");
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
		/// 最后登陆时间
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
		/// 最后登陆IP
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

        int _LoginCount;
		/// <summary>
		/// 登陆次数
		/// </summary>
        public int LoginCount
        {
            get { return _LoginCount; }
            set
            {
                if(_LoginCount!=value || _isLoaded){
                    _LoginCount=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="LoginCount");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _CreateTime;
		/// <summary>
		/// 注册时间
		/// </summary>
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set
            {
                if(_CreateTime!=value || _isLoaded){
                    _CreateTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CreateTime");
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
		/// 资料最后修改日期
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

        byte _IsMultiUser;
		/// <summary>
		/// 是否允许同一帐号多人使用，0=只能单个在线，1=可以多人同时在线
		/// </summary>
        public byte IsMultiUser
        {
            get { return _IsMultiUser; }
            set
            {
                if(_IsMultiUser!=value || _isLoaded){
                    _IsMultiUser=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsMultiUser");
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

        byte _IsWork;
		/// <summary>
		/// 0=离职，1=就职
		/// </summary>
        public byte IsWork
        {
            get { return _IsWork; }
            set
            {
                if(_IsWork!=value || _isLoaded){
                    _IsWork=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsWork");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsEnable;
		/// <summary>
		/// 账号是否启用，1=true(启用)，0=false（禁用）
		/// </summary>
        public byte IsEnable
        {
            get { return _IsEnable; }
            set
            {
                if(_IsEnable!=value || _isLoaded){
                    _IsEnable=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsEnable");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _CName;
		/// <summary>
		/// 用户中文名称
		/// </summary>
        public string CName
        {
            get { return _CName; }
            set
            {
                if(_CName!=value || _isLoaded){
                    _CName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _EName;
		/// <summary>
		/// 用户英文名称
		/// </summary>
        public string EName
        {
            get { return _EName; }
            set
            {
                if(_EName!=value || _isLoaded){
                    _EName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="EName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _PhotoImg;
		/// <summary>
		/// 头像图片路径
		/// </summary>
        public string PhotoImg
        {
            get { return _PhotoImg; }
            set
            {
                if(_PhotoImg!=value || _isLoaded){
                    _PhotoImg=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PhotoImg");
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

        string _Birthday;
		/// <summary>
		/// 出生日期
		/// </summary>
        public string Birthday
        {
            get { return _Birthday; }
            set
            {
                if(_Birthday!=value || _isLoaded){
                    _Birthday=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Birthday");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _NativePlace;
		/// <summary>
		/// 籍贯
		/// </summary>
        public string NativePlace
        {
            get { return _NativePlace; }
            set
            {
                if(_NativePlace!=value || _isLoaded){
                    _NativePlace=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="NativePlace");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _NationalName;
		/// <summary>
		/// 民族
		/// </summary>
        public string NationalName
        {
            get { return _NationalName; }
            set
            {
                if(_NationalName!=value || _isLoaded){
                    _NationalName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="NationalName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Record;
		/// <summary>
		/// 个人--学历
		/// </summary>
        public string Record
        {
            get { return _Record; }
            set
            {
                if(_Record!=value || _isLoaded){
                    _Record=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Record");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _GraduateCollege;
		/// <summary>
		/// 毕业学校
		/// </summary>
        public string GraduateCollege
        {
            get { return _GraduateCollege; }
            set
            {
                if(_GraduateCollege!=value || _isLoaded){
                    _GraduateCollege=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="GraduateCollege");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _GraduateSpecialty;
		/// <summary>
		/// 毕业专业
		/// </summary>
        public string GraduateSpecialty
        {
            get { return _GraduateSpecialty; }
            set
            {
                if(_GraduateSpecialty!=value || _isLoaded){
                    _GraduateSpecialty=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="GraduateSpecialty");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Tel;
		/// <summary>
		/// 个人--联系电话
		/// </summary>
        public string Tel
        {
            get { return _Tel; }
            set
            {
                if(_Tel!=value || _isLoaded){
                    _Tel=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Tel");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Mobile;
		/// <summary>
		/// 个人--移动电话
		/// </summary>
        public string Mobile
        {
            get { return _Mobile; }
            set
            {
                if(_Mobile!=value || _isLoaded){
                    _Mobile=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Mobile");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Email;
		/// <summary>
		/// 个人--联系邮箱
		/// </summary>
        public string Email
        {
            get { return _Email; }
            set
            {
                if(_Email!=value || _isLoaded){
                    _Email=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Email");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Qq;
		/// <summary>
		/// 个人--QQ
		/// </summary>
        public string Qq
        {
            get { return _Qq; }
            set
            {
                if(_Qq!=value || _isLoaded){
                    _Qq=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Qq");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Msn;
		/// <summary>
		/// 个人--Msn
		/// </summary>
        public string Msn
        {
            get { return _Msn; }
            set
            {
                if(_Msn!=value || _isLoaded){
                    _Msn=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Msn");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Address;
		/// <summary>
		/// 个人--通讯地址
		/// </summary>
        public string Address
        {
            get { return _Address; }
            set
            {
                if(_Address!=value || _isLoaded){
                    _Address=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Address");
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
		/// 修改人中文名称
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


        public static void Delete(Expression<Func<Manager, bool>> expression) {
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

