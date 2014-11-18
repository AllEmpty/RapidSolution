 
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
    /// A class which represents the Information table in the SolutionDataBase Database.
    /// </summary>
    public partial class Information: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Information> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Information>(new Solution.DataAccess.DataModel.SolutionDataBaseDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Information> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Information item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Information item=new Information();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Information> _repo;
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
        public Information(string connectionString, string providerName) {

            _db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Information.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Information>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Information(){
			_db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            Init();            
        }

		public void ORMapping(IDataRecord dataRecord)
        {
            IReadRecord readRecord = SqlReadRecord.GetIReadRecord();
            readRecord.DataRecord = dataRecord;   
               
            Id = readRecord.get_int("Id",null);
               
            InformationClass_Root_Id = readRecord.get_int("InformationClass_Root_Id",null);
               
            InformationClass_Root_Name = readRecord.get_string("InformationClass_Root_Name",null);
               
            InformationClass_Id = readRecord.get_int("InformationClass_Id",null);
               
            InformationClass_Name = readRecord.get_string("InformationClass_Name",null);
               
            Title = readRecord.get_string("Title",null);
               
            RedirectUrl = readRecord.get_string("RedirectUrl",null);
               
            Content = readRecord.get_string("Content",null);
               
            Upload = readRecord.get_string("Upload",null);
               
            FrontCoverImg = readRecord.get_string("FrontCoverImg",null);
               
            Notes = readRecord.get_string("Notes",null);
               
            NewsTime = readRecord.get_datetime("NewsTime",null);
               
            Keywords = readRecord.get_string("Keywords",null);
               
            SeoTitle = readRecord.get_string("SeoTitle",null);
               
            SeoKey = readRecord.get_string("SeoKey",null);
               
            SeoDesc = readRecord.get_string("SeoDesc",null);
               
            Author = readRecord.get_string("Author",null);
               
            FromName = readRecord.get_string("FromName",null);
               
            Sort = readRecord.get_int("Sort",null);
               
            IsDisplay = readRecord.get_byte("IsDisplay",null);
               
            IsHot = readRecord.get_byte("IsHot",null);
               
            IsTop = readRecord.get_byte("IsTop",null);
               
            IsPage = readRecord.get_byte("IsPage",null);
               
            IsDel = readRecord.get_byte("IsDel",null);
               
            CommentCount = readRecord.get_int("CommentCount",null);
               
            ViewCount = readRecord.get_int("ViewCount",null);
               
            AddYear = readRecord.get_int("AddYear",null);
               
            AddMonth = readRecord.get_int("AddMonth",null);
               
            AddDay = readRecord.get_int("AddDay",null);
               
            AddDate = readRecord.get_datetime("AddDate",null);
               
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

        public Information(Expression<Func<Information, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Information> GetRepo(string connectionString, string providerName){
            Solution.DataAccess.DataModel.SolutionDataBaseDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB();
            }else{
                db=new Solution.DataAccess.DataModel.SolutionDataBaseDB(connectionString, providerName);
            }
            IRepository<Information> _repo;
            
            if(db.TestMode){
                Information.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Information>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Information> GetRepo(){
            return GetRepo("","");
        }
        
        public static Information SingleOrDefault(Expression<Func<Information, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Information single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Information SingleOrDefault(Expression<Func<Information, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Information single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Information, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Information, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Information> Find(Expression<Func<Information, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Information> Find(Expression<Func<Information, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Information> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Information> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Information> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Information> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Information> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Information> GetPaged(int pageIndex, int pageSize) {
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
			sb.Append("InformationClass_Root_Id=" +　InformationClass_Root_Id + "; ");
			sb.Append("InformationClass_Root_Name=" +　InformationClass_Root_Name + "; ");
			sb.Append("InformationClass_Id=" +　InformationClass_Id + "; ");
			sb.Append("InformationClass_Name=" +　InformationClass_Name + "; ");
			sb.Append("Title=" +　Title + "; ");
			sb.Append("RedirectUrl=" +　RedirectUrl + "; ");
			sb.Append("Content=" +　Content + "; ");
			sb.Append("Upload=" +　Upload + "; ");
			sb.Append("FrontCoverImg=" +　FrontCoverImg + "; ");
			sb.Append("Notes=" +　Notes + "; ");
			sb.Append("NewsTime=" +　NewsTime + "; ");
			sb.Append("Keywords=" +　Keywords + "; ");
			sb.Append("SeoTitle=" +　SeoTitle + "; ");
			sb.Append("SeoKey=" +　SeoKey + "; ");
			sb.Append("SeoDesc=" +　SeoDesc + "; ");
			sb.Append("Author=" +　Author + "; ");
			sb.Append("FromName=" +　FromName + "; ");
			sb.Append("Sort=" +　Sort + "; ");
			sb.Append("IsDisplay=" +　IsDisplay + "; ");
			sb.Append("IsHot=" +　IsHot + "; ");
			sb.Append("IsTop=" +　IsTop + "; ");
			sb.Append("IsPage=" +　IsPage + "; ");
			sb.Append("IsDel=" +　IsDel + "; ");
			sb.Append("CommentCount=" +　CommentCount + "; ");
			sb.Append("ViewCount=" +　ViewCount + "; ");
			sb.Append("AddYear=" +　AddYear + "; ");
			sb.Append("AddMonth=" +　AddMonth + "; ");
			sb.Append("AddDay=" +　AddDay + "; ");
			sb.Append("AddDate=" +　AddDate + "; ");
			sb.Append("Manager_Id=" +　Manager_Id + "; ");
			sb.Append("Manager_CName=" +　Manager_CName + "; ");
			sb.Append("UpdateDate=" +　UpdateDate + "; ");
			return sb.ToString();
        }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Information)){
                Information compare=(Information)obj;
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
                            return this.InformationClass_Root_Name.ToString();
                    }

        public string DescriptorColumn() {
            return "InformationClass_Root_Name";
        }
        public static string GetKeyColumn()
        {
            return "Id";
        }        
        public static string GetDescriptorColumn()
        {
            return "InformationClass_Root_Name";
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

        int _InformationClass_Root_Id;
		/// <summary>
		/// 根类id
		/// </summary>
        public int InformationClass_Root_Id
        {
            get { return _InformationClass_Root_Id; }
            set
            {
                if(_InformationClass_Root_Id!=value || _isLoaded){
                    _InformationClass_Root_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InformationClass_Root_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _InformationClass_Root_Name;
		/// <summary>
		/// 根类名称
		/// </summary>
        public string InformationClass_Root_Name
        {
            get { return _InformationClass_Root_Name; }
            set
            {
                if(_InformationClass_Root_Name!=value || _isLoaded){
                    _InformationClass_Root_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InformationClass_Root_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _InformationClass_Id;
		/// <summary>
		/// 分类id
		/// </summary>
        public int InformationClass_Id
        {
            get { return _InformationClass_Id; }
            set
            {
                if(_InformationClass_Id!=value || _isLoaded){
                    _InformationClass_Id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InformationClass_Id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _InformationClass_Name;
		/// <summary>
		/// 分类名称
		/// </summary>
        public string InformationClass_Name
        {
            get { return _InformationClass_Name; }
            set
            {
                if(_InformationClass_Name!=value || _isLoaded){
                    _InformationClass_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="InformationClass_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Title;
		/// <summary>
		/// 标题
		/// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                if(_Title!=value || _isLoaded){
                    _Title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _RedirectUrl;
		/// <summary>
		/// 重定向页面（跳转页面），不为空时直接跳转到指定页面
		/// </summary>
        public string RedirectUrl
        {
            get { return _RedirectUrl; }
            set
            {
                if(_RedirectUrl!=value || _isLoaded){
                    _RedirectUrl=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RedirectUrl");
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
		/// 文章内容（Html图文编辑）
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

        string _Upload;
		/// <summary>
		/// 上传文件的名字列表: 20040413081811.gif|20040413081811.jpg|
		/// </summary>
        public string Upload
        {
            get { return _Upload; }
            set
            {
                if(_Upload!=value || _isLoaded){
                    _Upload=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Upload");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _FrontCoverImg;
		/// <summary>
		/// 文章封面图片
		/// </summary>
        public string FrontCoverImg
        {
            get { return _FrontCoverImg; }
            set
            {
                if(_FrontCoverImg!=value || _isLoaded){
                    _FrontCoverImg=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FrontCoverImg");
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
		/// 简介
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

        DateTime _NewsTime;
		/// <summary>
		/// 新闻时间(可以修改)
		/// </summary>
        public DateTime NewsTime
        {
            get { return _NewsTime; }
            set
            {
                if(_NewsTime!=value || _isLoaded){
                    _NewsTime=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="NewsTime");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Keywords;
		/// <summary>
		/// 文章关键字
		/// </summary>
        public string Keywords
        {
            get { return _Keywords; }
            set
            {
                if(_Keywords!=value || _isLoaded){
                    _Keywords=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Keywords");
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

        string _Author;
		/// <summary>
		/// 作者姓名
		/// </summary>
        public string Author
        {
            get { return _Author; }
            set
            {
                if(_Author!=value || _isLoaded){
                    _Author=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Author");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _FromName;
		/// <summary>
		/// 转贴自(名)/出处(名)
		/// </summary>
        public string FromName
        {
            get { return _FromName; }
            set
            {
                if(_FromName!=value || _isLoaded){
                    _FromName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FromName");
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

        byte _IsDisplay;
		/// <summary>
		/// 是否显示, 0=False,1=True,
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

        byte _IsHot;
		/// <summary>
		/// 是否要推荐,0=不要,1=要
		/// </summary>
        public byte IsHot
        {
            get { return _IsHot; }
            set
            {
                if(_IsHot!=value || _isLoaded){
                    _IsHot=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsHot");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        byte _IsTop;
		/// <summary>
		/// 是否置顶,0=false,1=true
		/// </summary>
        public byte IsTop
        {
            get { return _IsTop; }
            set
            {
                if(_IsTop!=value || _isLoaded){
                    _IsTop=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsTop");
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
		/// 是否为单页
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

        byte _IsDel;
		/// <summary>
		/// 回收站标志,0=false,1=true
		/// </summary>
        public byte IsDel
        {
            get { return _IsDel; }
            set
            {
                if(_IsDel!=value || _isLoaded){
                    _IsDel=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="IsDel");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _CommentCount;
		/// <summary>
		/// 评论数
		/// </summary>
        public int CommentCount
        {
            get { return _CommentCount; }
            set
            {
                if(_CommentCount!=value || _isLoaded){
                    _CommentCount=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CommentCount");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ViewCount;
		/// <summary>
		/// 浏览量
		/// </summary>
        public int ViewCount
        {
            get { return _ViewCount; }
            set
            {
                if(_ViewCount!=value || _isLoaded){
                    _ViewCount=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ViewCount");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _AddYear;
		/// <summary>
		/// 年（用于查询）
		/// </summary>
        public int AddYear
        {
            get { return _AddYear; }
            set
            {
                if(_AddYear!=value || _isLoaded){
                    _AddYear=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AddYear");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _AddMonth;
		/// <summary>
		/// 月（用于查询）
		/// </summary>
        public int AddMonth
        {
            get { return _AddMonth; }
            set
            {
                if(_AddMonth!=value || _isLoaded){
                    _AddMonth=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AddMonth");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _AddDay;
		/// <summary>
		/// 日（用于查询）
		/// </summary>
        public int AddDay
        {
            get { return _AddDay; }
            set
            {
                if(_AddDay!=value || _isLoaded){
                    _AddDay=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="AddDay");
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


        public static void Delete(Expression<Func<Information, bool>> expression) {
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

