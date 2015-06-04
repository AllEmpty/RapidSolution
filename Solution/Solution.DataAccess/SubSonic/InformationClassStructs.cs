 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: InformationClass
        /// Primary Key: Id
        /// </summary>

        public class InformationClassStructs: DatabaseTable {
            
            public InformationClassStructs(IDataProvider provider):base("InformationClass",provider){
                ClassName = "InformationClass";
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
	                MaxLength = 20,
					PropertyName = "Name"
                });

                Columns.Add(new DatabaseColumn("Notes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Notes"
                });

                Columns.Add(new DatabaseColumn("IsSys", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsSys"
                });

                Columns.Add(new DatabaseColumn("ClassImg", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "ClassImg"
                });

                Columns.Add(new DatabaseColumn("IsShow", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsShow"
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

                Columns.Add(new DatabaseColumn("RootId", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "RootId"
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
				
            
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
				
            
            public IColumn Notes{
                get{
                    return this.GetColumn("Notes");
                }
            }
				
            
            public IColumn IsSys{
                get{
                    return this.GetColumn("IsSys");
                }
            }
				
            
            public IColumn ClassImg{
                get{
                    return this.GetColumn("ClassImg");
                }
            }
				
            
            public IColumn IsShow{
                get{
                    return this.GetColumn("IsShow");
                }
            }
				
            
            public IColumn IsPage{
                get{
                    return this.GetColumn("IsPage");
                }
            }
				
            
            public IColumn RootId{
                get{
                    return this.GetColumn("RootId");
                }
            }
				
            
            public IColumn ParentId{
                get{
                    return this.GetColumn("ParentId");
                }
            }
				
            
            public IColumn Depth{
                get{
                    return this.GetColumn("Depth");
                }
            }
				
            
            public IColumn Sort{
                get{
                    return this.GetColumn("Sort");
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
