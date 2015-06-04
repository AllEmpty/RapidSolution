 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: ErrorLog
        /// Primary Key: Id
        /// </summary>

        public class ErrorLogStructs: DatabaseTable {
            
            public ErrorLogStructs(IDataProvider provider):base("ErrorLog",provider){
                ClassName = "ErrorLog";
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

                Columns.Add(new DatabaseColumn("ErrTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "ErrTime"
                });

                Columns.Add(new DatabaseColumn("BrowserVersion", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "BrowserVersion"
                });

                Columns.Add(new DatabaseColumn("BrowserType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "BrowserType"
                });

                Columns.Add(new DatabaseColumn("Ip", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Ip"
                });

                Columns.Add(new DatabaseColumn("PageUrl", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "PageUrl"
                });

                Columns.Add(new DatabaseColumn("ErrMessage", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "ErrMessage"
                });

                Columns.Add(new DatabaseColumn("ErrSource", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "ErrSource"
                });

                Columns.Add(new DatabaseColumn("StackTrace", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "StackTrace"
                });

                Columns.Add(new DatabaseColumn("HelpLink", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200,
					PropertyName = "HelpLink"
                });

                Columns.Add(new DatabaseColumn("Type", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Type"
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn ErrTime{
                get{
                    return this.GetColumn("ErrTime");
                }
            }
				
            
            public IColumn BrowserVersion{
                get{
                    return this.GetColumn("BrowserVersion");
                }
            }
				
            
            public IColumn BrowserType{
                get{
                    return this.GetColumn("BrowserType");
                }
            }
				
            
            public IColumn Ip{
                get{
                    return this.GetColumn("Ip");
                }
            }
				
            
            public IColumn PageUrl{
                get{
                    return this.GetColumn("PageUrl");
                }
            }
				
            
            public IColumn ErrMessage{
                get{
                    return this.GetColumn("ErrMessage");
                }
            }
				
            
            public IColumn ErrSource{
                get{
                    return this.GetColumn("ErrSource");
                }
            }
				
            
            public IColumn StackTrace{
                get{
                    return this.GetColumn("StackTrace");
                }
            }
				
            
            public IColumn HelpLink{
                get{
                    return this.GetColumn("HelpLink");
                }
            }
				
            
            public IColumn Type{
                get{
                    return this.GetColumn("Type");
                }
            }
				
            
                    
        }
        
}
