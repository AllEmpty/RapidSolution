 
using System;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Solution.DataAccess.DataModel {
        /// <summary>
        /// Table: Manager
        /// Primary Key: Id
        /// </summary>

        public class ManagerStructs: DatabaseTable {
            
            public ManagerStructs(IDataProvider provider):base("Manager",provider){
                ClassName = "Manager";
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

                Columns.Add(new DatabaseColumn("LoginName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "LoginName"
                });

                Columns.Add(new DatabaseColumn("LoginPass", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 32,
					PropertyName = "LoginPass"
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

                Columns.Add(new DatabaseColumn("LoginCount", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "LoginCount"
                });

                Columns.Add(new DatabaseColumn("CreateTime", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "CreateTime"
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

                Columns.Add(new DatabaseColumn("IsMultiUser", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsMultiUser"
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
	                MaxLength = 50,
					PropertyName = "Position_Id"
                });

                Columns.Add(new DatabaseColumn("Position_Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Position_Name"
                });

                Columns.Add(new DatabaseColumn("IsWork", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsWork"
                });

                Columns.Add(new DatabaseColumn("IsEnable", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Byte,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0,
					PropertyName = "IsEnable"
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

                Columns.Add(new DatabaseColumn("PhotoImg", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 250,
					PropertyName = "PhotoImg"
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

                Columns.Add(new DatabaseColumn("Birthday", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 20,
					PropertyName = "Birthday"
                });

                Columns.Add(new DatabaseColumn("NativePlace", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "NativePlace"
                });

                Columns.Add(new DatabaseColumn("NationalName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "NationalName"
                });

                Columns.Add(new DatabaseColumn("Record", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 25,
					PropertyName = "Record"
                });

                Columns.Add(new DatabaseColumn("GraduateCollege", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "GraduateCollege"
                });

                Columns.Add(new DatabaseColumn("GraduateSpecialty", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "GraduateSpecialty"
                });

                Columns.Add(new DatabaseColumn("Tel", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Tel"
                });

                Columns.Add(new DatabaseColumn("Mobile", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Mobile"
                });

                Columns.Add(new DatabaseColumn("Email", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 50,
					PropertyName = "Email"
                });

                Columns.Add(new DatabaseColumn("Qq", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Qq"
                });

                Columns.Add(new DatabaseColumn("Msn", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 30,
					PropertyName = "Msn"
                });

                Columns.Add(new DatabaseColumn("Address", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 100,
					PropertyName = "Address"
                });

                Columns.Add(new DatabaseColumn("Content", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.String,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1073741823,
					PropertyName = "Content"
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
                    
                
                
            }

            public IColumn Id{
                get{
                    return this.GetColumn("Id");
                }
            }
				
            
            public IColumn LoginName{
                get{
                    return this.GetColumn("LoginName");
                }
            }
				
            
            public IColumn LoginPass{
                get{
                    return this.GetColumn("LoginPass");
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
				
            
            public IColumn LoginCount{
                get{
                    return this.GetColumn("LoginCount");
                }
            }
				
            
            public IColumn CreateTime{
                get{
                    return this.GetColumn("CreateTime");
                }
            }
				
            
            public IColumn UpdateTime{
                get{
                    return this.GetColumn("UpdateTime");
                }
            }
				
            
            public IColumn IsMultiUser{
                get{
                    return this.GetColumn("IsMultiUser");
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
				
            
            public IColumn IsWork{
                get{
                    return this.GetColumn("IsWork");
                }
            }
				
            
            public IColumn IsEnable{
                get{
                    return this.GetColumn("IsEnable");
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
				
            
            public IColumn PhotoImg{
                get{
                    return this.GetColumn("PhotoImg");
                }
            }
				
            
            public IColumn Sex{
                get{
                    return this.GetColumn("Sex");
                }
            }
				
            
            public IColumn Birthday{
                get{
                    return this.GetColumn("Birthday");
                }
            }
				
            
            public IColumn NativePlace{
                get{
                    return this.GetColumn("NativePlace");
                }
            }
				
            
            public IColumn NationalName{
                get{
                    return this.GetColumn("NationalName");
                }
            }
				
            
            public IColumn Record{
                get{
                    return this.GetColumn("Record");
                }
            }
				
            
            public IColumn GraduateCollege{
                get{
                    return this.GetColumn("GraduateCollege");
                }
            }
				
            
            public IColumn GraduateSpecialty{
                get{
                    return this.GetColumn("GraduateSpecialty");
                }
            }
				
            
            public IColumn Tel{
                get{
                    return this.GetColumn("Tel");
                }
            }
				
            
            public IColumn Mobile{
                get{
                    return this.GetColumn("Mobile");
                }
            }
				
            
            public IColumn Email{
                get{
                    return this.GetColumn("Email");
                }
            }
				
            
            public IColumn Qq{
                get{
                    return this.GetColumn("Qq");
                }
            }
				
            
            public IColumn Msn{
                get{
                    return this.GetColumn("Msn");
                }
            }
				
            
            public IColumn Address{
                get{
                    return this.GetColumn("Address");
                }
            }
				
            
            public IColumn Content{
                get{
                    return this.GetColumn("Content");
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
				
            
                    
        }
        
}
