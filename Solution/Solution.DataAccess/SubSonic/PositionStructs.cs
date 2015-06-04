 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Position
        /// Primary Key: Id
        /// </summary>

        public class PositionStructs: DatabaseTable {
            
            public PositionStructs(IDataProvider provider):base("Position",provider){
                ClassName = "Position";
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
	                MaxLength = 30,
					PropertyName = "Name"
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
	                MaxLength = 50,
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

                Columns.Add(new DatabaseColumn("PagePower", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "PagePower"
                });

                Columns.Add(new DatabaseColumn("ControlPower", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "ControlPower"
                });

                Columns.Add(new DatabaseColumn("IsSetBranchPower", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsSetBranchPower"
                });

                Columns.Add(new DatabaseColumn("SetBranchCode", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "SetBranchCode"
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
				
            
            public IColumn PagePower{
                get{
                    return this.GetColumn("PagePower");
                }
            }
				
            
            public IColumn ControlPower{
                get{
                    return this.GetColumn("ControlPower");
                }
            }
				
            
            public IColumn IsSetBranchPower{
                get{
                    return this.GetColumn("IsSetBranchPower");
                }
            }
				
            
            public IColumn SetBranchCode{
                get{
                    return this.GetColumn("SetBranchCode");
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
