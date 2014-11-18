using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

/*
* 修 改 人：黑头
* 博客地址：http://www.cnblogs.com/heitou/
* 修改时间：2014-03-14
* 修改说明：提升subsonic ORMapping的速度
*********************************************/
namespace SubSonic.DataProviders
{
    /// <summary>
    /// 实现IReadRecord接口，从IDataRecord中读取数据
    /// </summary>
    public class SqlReadRecord : IReadRecord
    {
        private static SqlReadRecord sqlReadRecord = null;

        private IDataRecord dataRecord = null;

        public SqlReadRecord()
        {

        }

        public SqlReadRecord(IDataRecord dataRecord)
        {
            this.dataRecord = dataRecord;
        }

        public static IReadRecord GetIReadRecord()
        {
            if (sqlReadRecord == null)
                sqlReadRecord = new SqlReadRecord();

            return (IReadRecord)sqlReadRecord;
        }

        public IDataRecord DataRecord
        {
            get { return dataRecord; }
            set { dataRecord = value; }
        }

        #region public methods
        public string get_string(string AField)
        {
            try
            {
                return dataRecord[AField].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public string get_string(string AField, string ADef)
        {
            return get_string(AField);
        }

        public Int32 get_int(string AField, Int32? ADef)
        {
            try
            {
                return Convert.ToInt32(get_string(AField));
            }
            catch
            {
                return ADef == null ? 0 : ADef.Value;
            }
        }
        public Int64 get_long(string AField, int? ADef)
        {
            try
            {
                return Convert.ToInt64(get_string(AField));
            }
            catch
            {
                return ADef == null ? 0 : ADef.Value;
            }
        }
        public bool get_bool(string AField)
        {
            try
            {
                return Convert.ToBoolean(get_string(AField));
            }
            catch
            {
                return get_bool(AField, false);
            }
        }

        public bool get_bool(string AField, bool? ADef)
        {
            try
            {
                return Convert.ToBoolean(get_string(AField));
            }
            catch
            {
                return ADef == null ? false : ADef.Value;
            }
        }

        public double get_double(string AField, double? ADef)
        {
            try
            {
                return Convert.ToDouble(get_string(AField));
            }
            catch
            {
                return ADef == null ? Convert.ToDouble("0") : ADef.Value;
            }
        }

        public DateTime get_datetime(string AField, DateTime? ADef)
        {
            try
            {
                return Convert.ToDateTime(get_string(AField));
            }
            catch
            {
                return ADef == null ? DateTime.Now : ADef.Value;
            }
        }

        public Decimal get_decimal(string AField, Decimal? ADef)
        {
            try
            {
                return Convert.ToDecimal(get_string(AField));
            }
            catch
            {
                return ADef == null ? Convert.ToDecimal(0M) : ADef.Value;
            }
        }

        public Guid get_guid(string AField, Guid? ADef)
        {
            try
            {
                return new Guid(get_string(AField));
            }
            catch
            {
                return new Guid();
            }
        }

        //reload method
        public Decimal get_decimal(string AField)
        {
            return get_decimal(AField, null);
        }
        public Guid get_guid(string AField)
        {
            return get_guid(AField, Guid.NewGuid());
        }
        public byte get_byte(string AField, byte? ADef)
        {
            try
            {
                return Convert.ToByte(dataRecord[AField]);
            }
            catch
            {
                return ADef == null ? Convert.ToByte(' ') : ADef.Value;
            }
        }
        public byte[] get_bytes(string AField, byte[] ADef)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            try
            {
                return encoding.GetBytes(get_string(AField));
            }
            catch
            {
                return ADef == null ? encoding.GetBytes("") : ADef;
            }
        }
        public short? get_short(string AField, short? ADef)
        {
            try
            {
                return short.Parse(get_string(AField));
            }
            catch
            {
                return ADef == null ? 0 : ADef;
            }
        }
        #endregion
    }

}
