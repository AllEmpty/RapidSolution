 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: PagePowerSign
        /// Primary Key: Id
        /// </summary>

        public class PagePowerSignStructs: DatabaseTable {
            
            public PagePowerSignStructs(IDataProvider provider):base("PagePowerSign",provider){
                ClassName = "PagePowerSign";
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

                Columns.Add(new DatabaseColumn("PagePowerSignPublic_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "PagePowerSignPublic_Id"
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
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn PagePowerSignPublic_Id{
                get{
                    return this.GetColumn("PagePowerSignPublic_Id");
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
				
            
            public IColumn MenuInfo_Id{
                get{
                    return this.GetColumn("MenuInfo_Id");
                }
            }
				
            
                    
        }
        
}
