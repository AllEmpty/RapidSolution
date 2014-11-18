 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UseLog
        /// Primary Key: Id
        /// </summary>

        public class UseLogStructs: DatabaseTable {
            
            public UseLogStructs(IDataProvider provider):base("UseLog",provider){
                ClassName = "UseLog";
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

                Columns.Add(new DatabaseColumn("Ip", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Ip"
                });

                Columns.Add(new DatabaseColumn("MenuInfo_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MenuInfo_Id"
                });

                Columns.Add(new DatabaseColumn("MenuInfo_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "MenuInfo_Name"
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
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
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
				
            
            public IColumn Ip{
                get{
                    return this.GetColumn("Ip");
                }
            }
				
            
            public IColumn MenuInfo_Id{
                get{
                    return this.GetColumn("MenuInfo_Id");
                }
            }
				
            
            public IColumn MenuInfo_Name{
                get{
                    return this.GetColumn("MenuInfo_Name");
                }
            }
				
            
            public IColumn Notes{
                get{
                    return this.GetColumn("Notes");
                }
            }
				
            
                    
        }
        
}
