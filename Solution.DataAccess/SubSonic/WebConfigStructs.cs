 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: WebConfig
        /// Primary Key: Id
        /// </summary>

        public class WebConfigStructs: DatabaseTable {
            
            public WebConfigStructs(IDataProvider provider):base("WebConfig",provider){
                ClassName = "WebConfig";
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

                Columns.Add(new DatabaseColumn("WebName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "WebName"
                });

                Columns.Add(new DatabaseColumn("WebDomain", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "WebDomain"
                });

                Columns.Add(new DatabaseColumn("WebEmail", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "WebEmail"
                });

                Columns.Add(new DatabaseColumn("LoginLogReserveTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "LoginLogReserveTime"
                });

                Columns.Add(new DatabaseColumn("UseLogReserveTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UseLogReserveTime"
                });

                Columns.Add(new DatabaseColumn("EmailSmtp", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "EmailSmtp"
                });

                Columns.Add(new DatabaseColumn("EmailUserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "EmailUserName"
                });

                Columns.Add(new DatabaseColumn("EmailPassWord", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "EmailPassWord"
                });

                Columns.Add(new DatabaseColumn("EmailDomain", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "EmailDomain"
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn WebName{
                get{
                    return this.GetColumn("WebName");
                }
            }
				
            
            public IColumn WebDomain{
                get{
                    return this.GetColumn("WebDomain");
                }
            }
				
            
            public IColumn WebEmail{
                get{
                    return this.GetColumn("WebEmail");
                }
            }
				
            
            public IColumn LoginLogReserveTime{
                get{
                    return this.GetColumn("LoginLogReserveTime");
                }
            }
				
            
            public IColumn UseLogReserveTime{
                get{
                    return this.GetColumn("UseLogReserveTime");
                }
            }
				
            
            public IColumn EmailSmtp{
                get{
                    return this.GetColumn("EmailSmtp");
                }
            }
				
            
            public IColumn EmailUserName{
                get{
                    return this.GetColumn("EmailUserName");
                }
            }
				
            
            public IColumn EmailPassWord{
                get{
                    return this.GetColumn("EmailPassWord");
                }
            }
				
            
            public IColumn EmailDomain{
                get{
                    return this.GetColumn("EmailDomain");
                }
            }
				
            
                    
        }
        
}
