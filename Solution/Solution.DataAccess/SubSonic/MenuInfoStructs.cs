 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: MenuInfo
        /// Primary Key: Id
        /// </summary>

        public class MenuInfoStructs: DatabaseTable {
            
            public MenuInfoStructs(IDataProvider provider):base("MenuInfo",provider){
                ClassName = "MenuInfo";
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

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "Url"
                });

                Columns.Add(new DatabaseColumn("ParentId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "ParentId"
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

                Columns.Add(new DatabaseColumn("Depth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "Depth"
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

                Columns.Add(new DatabaseColumn("IsMenu", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsMenu"
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
				
            
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
				
            
            public IColumn ParentId{
                get{
                    return this.GetColumn("ParentId");
                }
            }
				
            
            public IColumn Sort{
                get{
                    return this.GetColumn("Sort");
                }
            }
				
            
            public IColumn Depth{
                get{
                    return this.GetColumn("Depth");
                }
            }
				
            
            public IColumn IsDisplay{
                get{
                    return this.GetColumn("IsDisplay");
                }
            }
				
            
            public IColumn IsMenu{
                get{
                    return this.GetColumn("IsMenu");
                }
            }
				
            
                    
        }
        
}
