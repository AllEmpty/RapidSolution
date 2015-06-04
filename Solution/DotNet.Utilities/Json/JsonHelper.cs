using System.Data;
using Newtonsoft.Json;

namespace DotNet.Utilities.Json
{
    /// <summary>Json的封装函数</summary>
    public class JsonHelper
    {
        #region 通用

        /// <summary>检查字符串是否json格式</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static bool IsJson(string jText)
        {
            if (string.IsNullOrEmpty(jText) || jText.Length < 3)
            {
                return false;
            }

            if (jText.Substring(0, 2) == "{\"" || jText.Substring(0, 3) == "[{\"")
            {
                return true;
            }
            return false;
        }

        /// <summary>检查字符串是否json格式数组</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static bool IsJsonRs(string jText)
        {
            if (string.IsNullOrEmpty(jText) || jText.Length < 3)
            {
                return false;
            }

            if (jText.Substring(0, 3) == "[{\"")
            {
                return true;
            }
            return false;
        }

        /// <summary>格式化 json</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static string Fmt_Null(string jText)
        {
            return StringHelper.ReplaceString(jText, ":null,", ":\"\",", true);
        }

        /// <summary>格式化 json ，删除左右二边的[]</summary>
        /// <param name="jText"></param>
        /// <returns></returns>
        public static string Fmt_Rs(string jText)
        {
            jText = jText.Trim();
            jText = jText.Trim('[');
            jText = jText.Trim(']');
            return jText;
        }

        #endregion

        #region Json序列化

        /// <summary>序列化</summary>
        /// <param name="obj">object </param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            var idtc = new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd hh:mm:ss" };

            return JsonConvert.SerializeObject(obj, idtc);
        }


        /// <summary>序列化--sql</summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>   
        public static string ToJson_FromSQL(DataTable dt)
        {
            string ss = ToJson(dt);
            dt.Dispose();
            return ss;
        }

        #endregion

        #region Json反序列化

        /// <summary>反序列化</summary>
        /// <param name="jText"></param>
        /// <returns></returns>      
        public static DataTable ToDataTable(string jText)
        {
            if (string.IsNullOrEmpty(jText))
            {
                return null;
            }
            else
            {
                try
                {
                    return JsonConvert.DeserializeObject<DataTable>(jText);
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>反序列化</summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="jText">json字符串</param>
        /// <returns>类型数据</returns>
        public static T ToObject<T>(string jText)
        {
            return (T)JsonConvert.DeserializeObject(jText, typeof(T));
        }

        #endregion
    }
}