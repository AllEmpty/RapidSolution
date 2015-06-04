 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: PagePowerSignPublic
        /// Primary Key: Id
        /// </summary>

        public class PagePowerSignPublicStructs: DatabaseTable {
            
            public PagePowerSignPublicStructs(IDataProvider provider):base("PagePowerSignPublic",provider){
                ClassName = "PagePowerSignPublic";
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

                Columns.Add(new DatabaseColumn("CName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "CName"
                });

                Columns.Add(new DatabaseColumn("EName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "EName"
                });
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn CName{
                get{
                    return this.GetColumn("CName");
                }
            }
				
            
            public IColumn EName{
                get{
                    return this.GetColumn("EName");
                }
            }
				
            
                    
        }
        
}
