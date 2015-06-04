/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
/** 1. 功能：处理数据类型转换，数制转换、编码转换相关的类
 *  2. 作者：周兆坤 
 *  3. 创建日期：2010-3-19
 *  4. 最后修改日期：2010-3-19
**/
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNet.Utilities
{
    /// <summary>
    /// 处理数据类型转换，数制转换、编码转换相关的类
    /// </summary>    
    public sealed class ConvertHelper
    {
        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }
        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion

        #region 数字转换

        /// <summary>判断是否为数字类型,包括[+-]号,小数字
        /// 包括（boolean/byte/int16/int32/int64/single/double/decimal）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (str.Length > 18)
            {
                return false;
            }
            else
            {
                return (new Regex("^[\\+\\-]?[0-9]*\\.?[0-9]+$")).IsMatch(str);
            }
        }

        /// <summary>判断是否为数字类型,包括[+-]号,小数字
        /// 包括（boolean/byte/int16/int32/int64/single/double/decimal）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(object str)
        {
            if (str != null)
            {
                return IsNumeric(str.ToString());
            }
            return false;
        }

        /// <summary>判断是否为整型数字(int32),不包括-</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            if ((str.Length > 10) || (str.Length == 10 && str[0] != '1'))
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] < '0') || (str[i] > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>判断是否为整型数字,包括（int32）</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(object str)
        {
            if (str != null)
            {
                return IsInt(str.ToString());
            }
            return false;
        }

        /// <summary>判断是否为整型数字,包括（int64）</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLong(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (str.Length > 18)
            {
                return false;
            }

            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] < '0') || (str[i] > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>判断是否为整型数字,包括（int64）</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLong(object str)
        {
            if (str != null)
            {
                return IsInt(str.ToString());
            }
            return false;
        }

        /// <summary>判断是否为浮点数字,用于货币,数量,包括小数字,但不包[+-]号,最多18位
        /// 包括（single/double/decimal）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFloat(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (str.Length > 18)
            {
                return false;
            }
            else
            {
                return (new Regex("^[0-9]*\\.?[0-9]+$")).IsMatch(str);
            }
        }

        /// <summary>判断是否为浮点数字,用于货币,数量,包括小数字,但不包[+-]号
        /// 包括（single/double/decimal）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFloat(object str)
        {
            if (str != null)
            {
                return IsFloat(str.ToString());
            }
            return false;
        }

        /// <summary>把string 转 int32 ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int Cint(string str, int defValue = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defValue;
            }

            int iLen = str.Length;
            if (iLen > 10)
            {
                return defValue;
            }

            string ss = "";
            bool isFlag = (str[0] == '-');
            int iStart = isFlag ? 1 : 0;

            for (int i = iStart; i < iLen; i++)
            {
                if ((str[i] < '0') || (str[i] > '9'))
                {
                    break;
                }
                else
                {
                    ss += str[i].ToString();
                    if (ss.Length > 9)
                    {
                        break;
                    }
                }
            }

            if (isFlag)
            {
                ss = "-" + ss;
            }
            return ss == "" ? defValue : int.Parse(ss);
        }

        /// <summary>把string 转 int32 ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Cint(object str)
        {
            if (str != null)
            {
                return Cint(str.ToString());
            }
            return 0;
        }

        /// <summary>把string 转 int32,判断是否小于minValue,小于返回minValue</summary>
        /// <param name="str"></param>
        /// <param name="minValue">当Value少于该值时,返回该值</param>
        /// <returns></returns>
        public static int CintMinValue(string str, int minValue)
        {
            int tmp = Cint(str);
            return tmp < minValue ? minValue : tmp;
        }

        /// <summary>把string 转 int32,判断是否小于minValue,小于返回minValue</summary>
        /// <param name="str"></param>
        /// <param name="minValue">当Value少于该值时,返回该值</param>
        /// <returns></returns>
        public static int CintMinValue(object str, int minValue)
        {
            if (str != null)
            {
                return CintMinValue(str.ToString(), minValue);
            }
            return minValue;
        }

        /// <summary>把string 转 int32,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=0的int型</returns>
        public static int Cint0(string str)
        {
            return CintMinValue(str, 0);
        }

        /// <summary>把string 转 int32,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=0的int型</returns>
        public static int Cint0(object str)
        {
            return CintMinValue(str, 0);
        }

        /// <summary>把string 转 int32,小于1返回1,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=1的int型</returns>
        public static int Cint1(string str)
        {
            return CintMinValue(str, 1);
        }

        /// <summary>把string 转 int32,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=1的int型</returns>
        public static int Cint1(object str)
        {
            return CintMinValue(str, 1);
        }

        /// <summary>不等于"1",就为"0",用于审核之类的</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte Ctinyint(string str)
        {
            return StringToByte(str, 0);
        }

        /// <summary>不等于"1",就为"0",用于审核之类的</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte Ctinyint(object str)
        {
            if (str != null)
            {
                return Ctinyint(str.ToString());
            }
            return 0;
        }

        #region Decimal 相关 2013-9-11 周光华

        /// <summary>把string 转 decimal ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static decimal Cdecimal(string str, int defValue = 0)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defValue;
            }

            if (str.Length > 18)
            {
                return defValue;
            }

            if (IsNumeric(str))
            {
                return decimal.Parse(str);
            }
            return defValue;
        }

        /// <summary>把string 转 decimal ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static decimal Cdecimal(object str, int defValue = 0)
        {
            if (str != null)
            {
                return Cdecimal(str.ToString());
            }
            return defValue;
        }

        /// <summary>把string 转 decimal ,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=0的int型</returns>
        public static decimal Cdecimal0(string str)
        {
            decimal tmp = Cdecimal(str);
            return tmp < 0 ? 0 : tmp;
        }

        /// <summary>把string 转 decimal ,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=0的int型</returns>
        public static decimal Cdecimal0(object str)
        {
            return Cdecimal0(str + "");
        }
        #endregion

        /// <summary>把string 转 long/int64 ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long Clng(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            int iLen = str.Length;
            if (iLen > 18)
            {
                return 0;
            }
            string ss = "";

            for (int i = 0; i < iLen; i++)
            {
                if ((str[i] < '0') || (str[i] > '9'))
                {
                    break;
                }
                else
                {
                    ss += str[i].ToString();
                }
            }

            if (ss == "")
            {
                return 0;
            }
            else
            {
                return long.Parse(ss.ToString());
            }
        }

        /// <summary>把string 转 long/int64 ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long Clng(object str)
        {
            if (str != null)
            {
                return Clng(str.ToString());
            }
            return 0;
        }

        /// <summary>把string 转 Double ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double Cdbl(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            if (str.Length > 18)
            {
                return 0;
            }

            if (IsNumeric(str.ToString()))
            {
                return double.Parse(str);
            }
            return 0;
        }

        /// <summary>把string 转 Double ,从左边继位检查转换(不管输入的是小数,还是字母)</summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double Cdbl(object str)
        {
            if (str != null)
            {
                return Cdbl(str.ToString());
            }
            return 0;
        }

        /// <summary>把string 转 Double ,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=0的int型</returns>
        public static double Cdbl0(string str)
        {
            double tmp = Cdbl(str);
            if (tmp < 0)
            {
                return 0;
            }
            else
            {
                return tmp;
            }
        }

        /// <summary>把string 转 Double ,小于0返回0,否则返回int值</summary>
        /// <param name="str"></param>
        /// <returns>返回>=0的int型</returns>
        public static double Cdbl0(object str)
        {
            return Cdbl0(str.ToString());
        }

        /// <summary>限制数值,不得少于 iMin ,比如分页数,不能少于 1</summary>
        /// <param name="str"></param>
        /// <param name="iMin"></param>
        /// <returns></returns>
        /// 
        public static int MinInt(int str, int iMin)
        {
            if (str < iMin)
            {
                return iMin;
            }
            else
            {
                return str;
            }
        }
        /// <summary>限制数值,不得少于 iMin ,比如分页数,不能少于 1</summary>
        /// <param name="str"></param>
        /// <param name="iMin"></param>
        /// <returns></returns>
        public static double MinDbl(double str, double iMin)
        {
            if (str < iMin)
            {
                return iMin;
            }
            else
            {
                return str;
            }
        }

        /// <summary>限制数值,不得少于 iMin ,比如分页数,不能少于 1</summary>
        /// <param name="str"></param>
        /// <param name="iMin"></param>
        /// <returns></returns>
        public static double MinDbl(object str, double iMin)
        {
            if (str != null)
            {
                return MinDbl(Cdbl(str.ToString()), iMin);
            }
            return iMin;
        }

        #endregion

        #region byts转换
        /// <summary>字符串转为Btye类型
        /// </summary>
        /// <param name="str">字符串值</param>
        /// <param name="value">值，请输入0或1</param>
        /// <returns>byte值</returns>
        public static byte StringToByte(String str, int value = 0)
        {
            try
            {
                return byte.Parse(str);
            }
            catch (Exception)
            {
                return (byte)value;
            }
        }

        /// <summary>将String 转为 byte[] </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string str)
        {
            //return Encoding.Default.GetBytes(str);
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>将byte[] 转为  String</summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] bytes)
        {
            //return .Encoding.Default.GetString(bytes);
            return Encoding.UTF8.GetString(bytes);
        }


        /// <summary>将Hex String 转为 byte[] </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>将byte[] 转为 Hex String</summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];
            int b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = bytes[i] >> 4;
                c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }

        /*
        /// <summary>将byte[] 转为 Hex String（和上面的函数相比这个性能会慢一些，但是它是官方函数）</summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString2(byte[] bytes)
        {
            string hex = BitConverter.ToString(bytes); 
            return hex.Replace("-", "");
        }
        */
        #endregion
    }
}
