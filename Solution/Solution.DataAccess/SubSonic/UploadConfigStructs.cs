 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: UploadConfig
        /// Primary Key: Id
        /// </summary>

        public class UploadConfigStructs: DatabaseTable {
            
            public UploadConfigStructs(IDataProvider provider):base("UploadConfig",provider){
                ClassName = "UploadConfig";
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

                Columns.Add(new DatabaseColumn("JoinName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "JoinName"
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

                Columns.Add(new DatabaseColumn("UploadType_Id", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "UploadType_Id"
                });

                Columns.Add(new DatabaseColumn("UploadType_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "UploadType_Name"
                });

                Columns.Add(new DatabaseColumn("UploadType_TypeKey", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 10,
					PropertyName = "UploadType_TypeKey"
                });

                Columns.Add(new DatabaseColumn("PicSize", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "PicSize"
                });

                Columns.Add(new DatabaseColumn("FileSize", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "FileSize"
                });

                Columns.Add(new DatabaseColumn("SaveDir", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "SaveDir"
                });

                Columns.Add(new DatabaseColumn("IsPost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsPost"
                });

                Columns.Add(new DatabaseColumn("IsSwf", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsSwf"
                });

                Columns.Add(new DatabaseColumn("IsChkSrcPost", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsChkSrcPost"
                });

                Columns.Add(new DatabaseColumn("IsFixPic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsFixPic"
                });

                Columns.Add(new DatabaseColumn("CutType", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "CutType"
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

                Columns.Add(new DatabaseColumn("PicQuality", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "PicQuality"
                });

                Columns.Add(new DatabaseColumn("IsEditor", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsEditor"
                });

                Columns.Add(new DatabaseColumn("IsBigPic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsBigPic"
                });

                Columns.Add(new DatabaseColumn("BigWidth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "BigWidth"
                });

                Columns.Add(new DatabaseColumn("BigHeight", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "BigHeight"
                });

                Columns.Add(new DatabaseColumn("BigQuality", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "BigQuality"
                });

                Columns.Add(new DatabaseColumn("IsMidPic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsMidPic"
                });

                Columns.Add(new DatabaseColumn("MidWidth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MidWidth"
                });

                Columns.Add(new DatabaseColumn("MidHeight", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MidHeight"
                });

                Columns.Add(new DatabaseColumn("MidQuality", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MidQuality"
                });

                Columns.Add(new DatabaseColumn("IsMinPic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsMinPic"
                });

                Columns.Add(new DatabaseColumn("MinWidth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MinWidth"
                });

                Columns.Add(new DatabaseColumn("MinHeight", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MinHeight"
                });

                Columns.Add(new DatabaseColumn("MinQuality", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "MinQuality"
                });

                Columns.Add(new DatabaseColumn("IsHotPic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsHotPic"
                });

                Columns.Add(new DatabaseColumn("HotWidth", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "HotWidth"
                });

                Columns.Add(new DatabaseColumn("HotHeight", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "HotHeight"
                });

                Columns.Add(new DatabaseColumn("HotQuality", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "HotQuality"
                });

                Columns.Add(new DatabaseColumn("IsWaterPic", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsWaterPic"
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
				
            
            public IColumn JoinName{
                get{
                    return this.GetColumn("JoinName");
                }
            }
				
            
            public IColumn UserType{
                get{
                    return this.GetColumn("UserType");
                }
            }
				
            
            public IColumn UploadType_Id{
                get{
                    return this.GetColumn("UploadType_Id");
                }
            }
				
            
            public IColumn UploadType_Name{
                get{
                    return this.GetColumn("UploadType_Name");
                }
            }
				
            
            public IColumn UploadType_TypeKey{
                get{
                    return this.GetColumn("UploadType_TypeKey");
                }
            }
				
            
            public IColumn PicSize{
                get{
                    return this.GetColumn("PicSize");
                }
            }
				
            
            public IColumn FileSize{
                get{
                    return this.GetColumn("FileSize");
                }
            }
				
            
            public IColumn SaveDir{
                get{
                    return this.GetColumn("SaveDir");
                }
            }
				
            
            public IColumn IsPost{
                get{
                    return this.GetColumn("IsPost");
                }
            }
				
            
            public IColumn IsSwf{
                get{
                    return this.GetColumn("IsSwf");
                }
            }
				
            
            public IColumn IsChkSrcPost{
                get{
                    return this.GetColumn("IsChkSrcPost");
                }
            }
				
            
            public IColumn IsFixPic{
                get{
                    return this.GetColumn("IsFixPic");
                }
            }
				
            
            public IColumn CutType{
                get{
                    return this.GetColumn("CutType");
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
				
            
            public IColumn PicQuality{
                get{
                    return this.GetColumn("PicQuality");
                }
            }
				
            
            public IColumn IsEditor{
                get{
                    return this.GetColumn("IsEditor");
                }
            }
				
            
            public IColumn IsBigPic{
                get{
                    return this.GetColumn("IsBigPic");
                }
            }
				
            
            public IColumn BigWidth{
                get{
                    return this.GetColumn("BigWidth");
                }
            }
				
            
            public IColumn BigHeight{
                get{
                    return this.GetColumn("BigHeight");
                }
            }
				
            
            public IColumn BigQuality{
                get{
                    return this.GetColumn("BigQuality");
                }
            }
				
            
            public IColumn IsMidPic{
                get{
                    return this.GetColumn("IsMidPic");
                }
            }
				
            
            public IColumn MidWidth{
                get{
                    return this.GetColumn("MidWidth");
                }
            }
				
            
            public IColumn MidHeight{
                get{
                    return this.GetColumn("MidHeight");
                }
            }
				
            
            public IColumn MidQuality{
                get{
                    return this.GetColumn("MidQuality");
                }
            }
				
            
            public IColumn IsMinPic{
                get{
                    return this.GetColumn("IsMinPic");
                }
            }
				
            
            public IColumn MinWidth{
                get{
                    return this.GetColumn("MinWidth");
                }
            }
				
            
            public IColumn MinHeight{
                get{
                    return this.GetColumn("MinHeight");
                }
            }
				
            
            public IColumn MinQuality{
                get{
                    return this.GetColumn("MinQuality");
                }
            }
				
            
            public IColumn IsHotPic{
                get{
                    return this.GetColumn("IsHotPic");
                }
            }
				
            
            public IColumn HotWidth{
                get{
                    return this.GetColumn("HotWidth");
                }
            }
				
            
            public IColumn HotHeight{
                get{
                    return this.GetColumn("HotHeight");
                }
            }
				
            
            public IColumn HotQuality{
                get{
                    return this.GetColumn("HotQuality");
                }
            }
				
            
            public IColumn IsWaterPic{
                get{
                    return this.GetColumn("IsWaterPic");
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
