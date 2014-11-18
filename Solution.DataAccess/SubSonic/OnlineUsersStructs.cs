 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: OnlineUsers
        /// Primary Key: Id
        /// </summary>

        public class OnlineUsersStructs: DatabaseTable {
            
            public OnlineUsersStructs(IDataProvider provider):base("OnlineUsers",provider){
                ClassName = "OnlineUsers";
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

                Columns.Add(new DatabaseColumn("UserHashKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "UserHashKey"
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

                Columns.Add(new DatabaseColumn("Manager_LoginName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "Manager_LoginName"
                });

                Columns.Add(new DatabaseColumn("Manager_LoginPass", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 32,
					PropertyName = "Manager_LoginPass"
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

                Columns.Add(new DatabaseColumn("LoginTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "LoginTime"
                });

                Columns.Add(new DatabaseColumn("LoginIp", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "LoginIp"
                });

                Columns.Add(new DatabaseColumn("UserKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 32,
					PropertyName = "UserKey"
                });

                Columns.Add(new DatabaseColumn("Md5", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 32,
					PropertyName = "Md5"
                });

                Columns.Add(new DatabaseColumn("UpdateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UpdateTime"
                });

                Columns.Add(new DatabaseColumn("Sex", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 4,
					PropertyName = "Sex"
                });

                Columns.Add(new DatabaseColumn("Branch_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Branch_Id"
                });

                Columns.Add(new DatabaseColumn("Branch_Code", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "Branch_Code"
                });

                Columns.Add(new DatabaseColumn("Branch_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 25,
					PropertyName = "Branch_Name"
                });

                Columns.Add(new DatabaseColumn("Position_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Position_Id"
                });

                Columns.Add(new DatabaseColumn("Position_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Position_Name"
                });

                Columns.Add(new DatabaseColumn("CurrentPage", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "CurrentPage"
                });

                Columns.Add(new DatabaseColumn("CurrentPageTitle", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "CurrentPageTitle"
                });

                Columns.Add(new DatabaseColumn("SessionId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "SessionId"
                });

                Columns.Add(new DatabaseColumn("UserAgent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1000,
					PropertyName = "UserAgent"
                });

                Columns.Add(new DatabaseColumn("OperatingSystem", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "OperatingSystem"
                });

                Columns.Add(new DatabaseColumn("TerminalType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "TerminalType"
                });

                Columns.Add(new DatabaseColumn("BrowserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "BrowserName"
                });

                Columns.Add(new DatabaseColumn("BrowserVersion", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10,
					PropertyName = "BrowserVersion"
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn UserHashKey{
                get{
                    return this.GetColumn("UserHashKey");
                }
            }
				
            
            public IColumn Manager_Id{
                get{
                    return this.GetColumn("Manager_Id");
                }
            }
				
            
            public IColumn Manager_LoginName{
                get{
                    return this.GetColumn("Manager_LoginName");
                }
            }
				
            
            public IColumn Manager_LoginPass{
                get{
                    return this.GetColumn("Manager_LoginPass");
                }
            }
				
            
            public IColumn Manager_CName{
                get{
                    return this.GetColumn("Manager_CName");
                }
            }
				
            
            public IColumn LoginTime{
                get{
                    return this.GetColumn("LoginTime");
                }
            }
				
            
            public IColumn LoginIp{
                get{
                    return this.GetColumn("LoginIp");
                }
            }
				
            
            public IColumn UserKey{
                get{
                    return this.GetColumn("UserKey");
                }
            }
				
            
            public IColumn Md5{
                get{
                    return this.GetColumn("Md5");
                }
            }
				
            
            public IColumn UpdateTime{
                get{
                    return this.GetColumn("UpdateTime");
                }
            }
				
            
            public IColumn Sex{
                get{
                    return this.GetColumn("Sex");
                }
            }
				
            
            public IColumn Branch_Id{
                get{
                    return this.GetColumn("Branch_Id");
                }
            }
				
            
            public IColumn Branch_Code{
                get{
                    return this.GetColumn("Branch_Code");
                }
            }
				
            
            public IColumn Branch_Name{
                get{
                    return this.GetColumn("Branch_Name");
                }
            }
				
            
            public IColumn Position_Id{
                get{
                    return this.GetColumn("Position_Id");
                }
            }
				
            
            public IColumn Position_Name{
                get{
                    return this.GetColumn("Position_Name");
                }
            }
				
            
            public IColumn CurrentPage{
                get{
                    return this.GetColumn("CurrentPage");
                }
            }
				
            
            public IColumn CurrentPageTitle{
                get{
                    return this.GetColumn("CurrentPageTitle");
                }
            }
				
            
            public IColumn SessionId{
                get{
                    return this.GetColumn("SessionId");
                }
            }
				
            
            public IColumn UserAgent{
                get{
                    return this.GetColumn("UserAgent");
                }
            }
				
            
            public IColumn OperatingSystem{
                get{
                    return this.GetColumn("OperatingSystem");
                }
            }
				
            
            public IColumn TerminalType{
                get{
                    return this.GetColumn("TerminalType");
                }
            }
				
            
            public IColumn BrowserName{
                get{
                    return this.GetColumn("BrowserName");
                }
            }
				
            
            public IColumn BrowserVersion{
                get{
                    return this.GetColumn("BrowserVersion");
                }
            }
				
            
                    
        }
        
}
