 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Information
        /// Primary Key: Id
        /// </summary>

        public class InformationStructs: DatabaseTable {
            
            public InformationStructs(IDataProvider provider):base("Information",provider){
                ClassName = "Information";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Id"
                });

                Columns.Add(new DatabaseColumn("InformationClass_Root_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "InformationClass_Root_Id"
                });

                Columns.Add(new DatabaseColumn("InformationClass_Root_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "InformationClass_Root_Name"
                });

                Columns.Add(new DatabaseColumn("InformationClass_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "InformationClass_Id"
                });

                Columns.Add(new DatabaseColumn("InformationClass_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "InformationClass_Name"
                });

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Title"
                });

                Columns.Add(new DatabaseColumn("RedirectUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "RedirectUrl"
                });

                Columns.Add(new DatabaseColumn("Content", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "Content"
                });

                Columns.Add(new DatabaseColumn("Upload", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "Upload"
                });

                Columns.Add(new DatabaseColumn("FrontCoverImg", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "FrontCoverImg"
                });

                Columns.Add(new DatabaseColumn("Notes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200,
					PropertyName = "Notes"
                });

                Columns.Add(new DatabaseColumn("NewsTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "NewsTime"
                });

                Columns.Add(new DatabaseColumn("Keywords", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "Keywords"
                });

                Columns.Add(new DatabaseColumn("SeoTitle", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "SeoTitle"
                });

                Columns.Add(new DatabaseColumn("SeoKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "SeoKey"
                });

                Columns.Add(new DatabaseColumn("SeoDesc", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200,
					PropertyName = "SeoDesc"
                });

                Columns.Add(new DatabaseColumn("Author", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "Author"
                });

                Columns.Add(new DatabaseColumn("FromName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "FromName"
                });

                Columns.Add(new DatabaseColumn("Sort", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Sort"
                });

                Columns.Add(new DatabaseColumn("IsDisplay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsDisplay"
                });

                Columns.Add(new DatabaseColumn("IsHot", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsHot"
                });

                Columns.Add(new DatabaseColumn("IsTop", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsTop"
                });

                Columns.Add(new DatabaseColumn("IsPage", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsPage"
                });

                Columns.Add(new DatabaseColumn("IsDel", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsDel"
                });

                Columns.Add(new DatabaseColumn("CommentCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "CommentCount"
                });

                Columns.Add(new DatabaseColumn("ViewCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "ViewCount"
                });

                Columns.Add(new DatabaseColumn("AddYear", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "AddYear"
                });

                Columns.Add(new DatabaseColumn("AddMonth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "AddMonth"
                });

                Columns.Add(new DatabaseColumn("AddDay", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "AddDay"
                });

                Columns.Add(new DatabaseColumn("AddDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "AddDate"
                });

                Columns.Add(new DatabaseColumn("Manager_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Manager_Id"
                });

                Columns.Add(new DatabaseColumn("Manager_CName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "Manager_CName"
                });

                Columns.Add(new DatabaseColumn("UpdateDate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UpdateDate"
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn InformationClass_Root_Id{
                get{
                    return this.GetColumn("InformationClass_Root_Id");
                }
            }
				
            
            public IColumn InformationClass_Root_Name{
                get{
                    return this.GetColumn("InformationClass_Root_Name");
                }
            }
				
            
            public IColumn InformationClass_Id{
                get{
                    return this.GetColumn("InformationClass_Id");
                }
            }
				
            
            public IColumn InformationClass_Name{
                get{
                    return this.GetColumn("InformationClass_Name");
                }
            }
				
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
				
            
            public IColumn RedirectUrl{
                get{
                    return this.GetColumn("RedirectUrl");
                }
            }
				
            
            public IColumn Content{
                get{
                    return this.GetColumn("Content");
                }
            }
				
            
            public IColumn Upload{
                get{
                    return this.GetColumn("Upload");
                }
            }
				
            
            public IColumn FrontCoverImg{
                get{
                    return this.GetColumn("FrontCoverImg");
                }
            }
				
            
            public IColumn Notes{
                get{
                    return this.GetColumn("Notes");
                }
            }
				
            
            public IColumn NewsTime{
                get{
                    return this.GetColumn("NewsTime");
                }
            }
				
            
            public IColumn Keywords{
                get{
                    return this.GetColumn("Keywords");
                }
            }
				
            
            public IColumn SeoTitle{
                get{
                    return this.GetColumn("SeoTitle");
                }
            }
				
            
            public IColumn SeoKey{
                get{
                    return this.GetColumn("SeoKey");
                }
            }
				
            
            public IColumn SeoDesc{
                get{
                    return this.GetColumn("SeoDesc");
                }
            }
				
            
            public IColumn Author{
                get{
                    return this.GetColumn("Author");
                }
            }
				
            
            public IColumn FromName{
                get{
                    return this.GetColumn("FromName");
                }
            }
				
            
            public IColumn Sort{
                get{
                    return this.GetColumn("Sort");
                }
            }
				
            
            public IColumn IsDisplay{
                get{
                    return this.GetColumn("IsDisplay");
                }
            }
				
            
            public IColumn IsHot{
                get{
                    return this.GetColumn("IsHot");
                }
            }
				
            
            public IColumn IsTop{
                get{
                    return this.GetColumn("IsTop");
                }
            }
				
            
            public IColumn IsPage{
                get{
                    return this.GetColumn("IsPage");
                }
            }
				
            
            public IColumn IsDel{
                get{
                    return this.GetColumn("IsDel");
                }
            }
				
            
            public IColumn CommentCount{
                get{
                    return this.GetColumn("CommentCount");
                }
            }
				
            
            public IColumn ViewCount{
                get{
                    return this.GetColumn("ViewCount");
                }
            }
				
            
            public IColumn AddYear{
                get{
                    return this.GetColumn("AddYear");
                }
            }
				
            
            public IColumn AddMonth{
                get{
                    return this.GetColumn("AddMonth");
                }
            }
				
            
            public IColumn AddDay{
                get{
                    return this.GetColumn("AddDay");
                }
            }
				
            
            public IColumn AddDate{
                get{
                    return this.GetColumn("AddDate");
                }
            }
				
            
            public IColumn Manager_Id{
                get{
                    return this.GetColumn("Manager_Id");
                }
            }
				
            
            public IColumn Manager_CName{
                get{
                    return this.GetColumn("Manager_CName");
                }
            }
				
            
            public IColumn UpdateDate{
                get{
                    return this.GetColumn("UpdateDate");
                }
            }
				
            
                    
        }
        
}
