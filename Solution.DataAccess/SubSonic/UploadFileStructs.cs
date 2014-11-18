 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UploadFile
        /// Primary Key: Id
        /// </summary>

        public class UploadFileStructs: DatabaseTable {
            
            public UploadFileStructs(IDataProvider provider):base("UploadFile",provider){
                ClassName = "UploadFile";
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

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "Name"
                });

                Columns.Add(new DatabaseColumn("Path", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200,
					PropertyName = "Path"
                });

                Columns.Add(new DatabaseColumn("Ext", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10,
					PropertyName = "Ext"
                });

                Columns.Add(new DatabaseColumn("Src", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Src"
                });

                Columns.Add(new DatabaseColumn("Size", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Size"
                });

                Columns.Add(new DatabaseColumn("PicWidth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "PicWidth"
                });

                Columns.Add(new DatabaseColumn("PicHeight", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "PicHeight"
                });

                Columns.Add(new DatabaseColumn("UploadConfig_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UploadConfig_Id"
                });

                Columns.Add(new DatabaseColumn("JoinName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "JoinName"
                });

                Columns.Add(new DatabaseColumn("JoinId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "JoinId"
                });

                Columns.Add(new DatabaseColumn("UserType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UserType"
                });

                Columns.Add(new DatabaseColumn("UserId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UserId"
                });

                Columns.Add(new DatabaseColumn("UserName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "UserName"
                });

                Columns.Add(new DatabaseColumn("UserIp", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "UserIp"
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

                Columns.Add(new DatabaseColumn("InfoText", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "InfoText"
                });

                Columns.Add(new DatabaseColumn("RndKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "RndKey"
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
            
            public IColumn Path{
                get{
                    return this.GetColumn("Path");
                }
            }
				
            
            public IColumn Ext{
                get{
                    return this.GetColumn("Ext");
                }
            }
				
            
            public IColumn Src{
                get{
                    return this.GetColumn("Src");
                }
            }
				
            
            public IColumn Size{
                get{
                    return this.GetColumn("Size");
                }
            }
				
            
            public IColumn PicWidth{
                get{
                    return this.GetColumn("PicWidth");
                }
            }
				
            
            public IColumn PicHeight{
                get{
                    return this.GetColumn("PicHeight");
                }
            }
				
            
            public IColumn UploadConfig_Id{
                get{
                    return this.GetColumn("UploadConfig_Id");
                }
            }
				
            
            public IColumn JoinName{
                get{
                    return this.GetColumn("JoinName");
                }
            }
				
            
            public IColumn JoinId{
                get{
                    return this.GetColumn("JoinId");
                }
            }
				
            
            public IColumn UserType{
                get{
                    return this.GetColumn("UserType");
                }
            }
				
            
            public IColumn UserId{
                get{
                    return this.GetColumn("UserId");
                }
            }
				
            
            public IColumn UserName{
                get{
                    return this.GetColumn("UserName");
                }
            }
				
            
            public IColumn UserIp{
                get{
                    return this.GetColumn("UserIp");
                }
            }
				
            
            public IColumn AddDate{
                get{
                    return this.GetColumn("AddDate");
                }
            }
				
            
            public IColumn InfoText{
                get{
                    return this.GetColumn("InfoText");
                }
            }
				
            
            public IColumn RndKey{
                get{
                    return this.GetColumn("RndKey");
                }
            }
				
            
                    
        }
        
}
