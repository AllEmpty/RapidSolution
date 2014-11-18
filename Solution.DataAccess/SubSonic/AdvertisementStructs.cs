 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Advertisement
        /// Primary Key: Id
        /// </summary>

        public class AdvertisementStructs: DatabaseTable {
            
            public AdvertisementStructs(IDataProvider provider):base("Advertisement",provider){
                ClassName = "Advertisement";
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

                Columns.Add(new DatabaseColumn("Content", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Content"
                });

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 200,
					PropertyName = "Url"
                });

                Columns.Add(new DatabaseColumn("Keyword", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "Keyword"
                });

                Columns.Add(new DatabaseColumn("AdvertisingPosition_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "AdvertisingPosition_Id"
                });

                Columns.Add(new DatabaseColumn("AdvertisingPosition_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "AdvertisingPosition_Name"
                });

                Columns.Add(new DatabaseColumn("AdImg", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "AdImg"
                });

                Columns.Add(new DatabaseColumn("ShowRate", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "ShowRate"
                });

                Columns.Add(new DatabaseColumn("StartTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "StartTime"
                });

                Columns.Add(new DatabaseColumn("EndTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "EndTime"
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

                Columns.Add(new DatabaseColumn("HitCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "HitCount"
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
				
            
            public IColumn Content{
                get{
                    return this.GetColumn("Content");
                }
            }
				
            
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
				
            
            public IColumn Keyword{
                get{
                    return this.GetColumn("Keyword");
                }
            }
				
            
            public IColumn AdvertisingPosition_Id{
                get{
                    return this.GetColumn("AdvertisingPosition_Id");
                }
            }
				
            
            public IColumn AdvertisingPosition_Name{
                get{
                    return this.GetColumn("AdvertisingPosition_Name");
                }
            }
				
            
            public IColumn AdImg{
                get{
                    return this.GetColumn("AdImg");
                }
            }
				
            
            public IColumn ShowRate{
                get{
                    return this.GetColumn("ShowRate");
                }
            }
				
            
            public IColumn StartTime{
                get{
                    return this.GetColumn("StartTime");
                }
            }
				
            
            public IColumn EndTime{
                get{
                    return this.GetColumn("EndTime");
                }
            }
				
            
            public IColumn IsDisplay{
                get{
                    return this.GetColumn("IsDisplay");
                }
            }
				
            
            public IColumn HitCount{
                get{
                    return this.GetColumn("HitCount");
                }
            }
				
            
            public IColumn Sort{
                get{
                    return this.GetColumn("Sort");
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
