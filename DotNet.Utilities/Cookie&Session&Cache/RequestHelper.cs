using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DotNet.Utilities
{
    public class RequestHelper
    {

        /// <summary>获得Post提交的参数值</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="isFilterXss">是否过滤XSS</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool isFilterXss = true)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }

            return StringHelper.FilterSql(HttpContext.Current.Request.Form[strName], true, isFilterXss);
        }

        /// <summary> 获得Get提交的参数值 </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="isFilterXss">是否过滤XSS</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool isFilterXss = true)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }

            return StringHelper.FilterSql(HttpContext.Current.Request.QueryString[strName], true, isFilterXss);
        }
        
        /// <summary>获得Post提交的参数值,带截取和sql注入过滤</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="minLen">截取的长度(字数)</param>
        /// <param name="isFilterXss">是否过滤XSS</param>
        /// <returns>表单参数的值(sql过滤,截取后)</returns>
        public static string PostText(string strName, int minLen, bool isFilterXss = true)
        {
            string str = GetFormString(strName, isFilterXss);

            if (str.Length > 0 && minLen > 0 && str.Length > minLen)
            {
                return str.Substring(0, minLen);
            }
            else
            {
                return str;
            }
        }

        /// <summary>获得Get提交的参数值,带截取和sql注入过滤</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="minLen">截取的长度(字数)</param>
        /// <param name="isFilterXss">是否过滤XSS</param>
        /// <returns>表单参数的值(sql过滤,截取后)</returns>
        public static string GetText(string strName, int minLen, bool isFilterXss = true)
        {
            string str = GetQueryString(strName, isFilterXss);

            if (str.Length > 0 && minLen > 0 && str.Length > minLen)
            {
                return str.Substring(0, minLen);
            }
            else
            {
                return str;
            }
        }

        /// <summary>获得Get提交的参数值,判断是否abcdefghijklmnopqrstuvwxyz0123456789组成的,是返回,否返回""</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="minLen">截取的长度(字数)</param>
        /// <returns>返回int型</returns>
        public static string GetKeyChar(string strName, int minLen = 32)
        {
            string str = GetText(strName, minLen);
            if (str.Length > 0 && StringHelper.IsRndKey(str))
            {
                return str;
            }
            else
            {
                return "";
            }
        }

        /// <summary>获得Post提交的参数值,判断是否int,是返回,否返回0</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回int型</returns>
        public static int PostInt(string strName)
        {
            string str = GetFormString(strName);
            if (str.Length > 0 && ConvertHelper.IsInt(str))
            {
                return ConvertHelper.Cint(str);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>获得Post提交的参数值,判断是否小于minValue,小于返回minValue</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="minValue">当Value少于该值时,返回该值</param>
        /// <returns>返回int型</returns>
        public static int PostIntMinValue(string strName, int minValue)
        {
            int str = PostInt(strName);
            if (str < minValue)
            {
                return minValue;
            }
            else
            {
                return str;
            }
        }

        /// <summary>获得Post提交的参数值,小于0返回0,否则返回int值</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回 大于等于0的int型</returns>
        public static int PostInt0(string strName)
        {
            return PostIntMinValue(strName, 0);
        }

        /// <summary>获得Post提交的参数值,小于1返回1,否则返回int值</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回 大于等于1的int型</returns>
        public static int PostInt1(string strName)
        {
            return PostIntMinValue(strName, 1);
        }

        /// <summary>获得Post提交的参数值,判断是否Long,是返回,否返回0</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回int型</returns>
        public static long PostLong(string strName)
        {
            string str = GetFormString(strName);
            if (str.Length > 0 && ConvertHelper.IsLong(str))
            {
                return ConvertHelper.Clng(str);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>获得Post提交的参数值,判断是否小于minValue,小于返回minValue</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="minValue">当Value少于该值时,返回该值</param>
        /// <returns>返回int型</returns>
        public static long PostLongMinValue(string strName, long minValue)
        {
            long str = PostLong(strName);
            if (str < minValue)
            {
                return minValue;
            }
            else
            {
                return str;
            }
        }

        /// <summary>获得Post提交的参数值,小于0返回0,否则返回int值</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回 大于等于0的int型</returns>
        public static long PostLong0(string strName)
        {
            return PostLongMinValue(strName, 0);
        }

        /// <summary>获得Post提交的参数值,小于1返回1,否则返回int值</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回 大于等于1的int型</returns>
        public static long PostLong1(string strName)
        {
            return PostLongMinValue(strName, 1);
        }


        /// <summary>获得Get提交的参数值,判断是否int,是返回,否返回"0"</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回int型</returns>
        public static int GetInt(string strName)
        {
            string str = GetQueryString(strName);
            if (str.Length > 0 && ConvertHelper.IsInt(str))
            {
                return ConvertHelper.Cint(str);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>获得Get提交的参数值,判断是否小于minValue,小于返回minValue</summary>
        /// <param name="strName">表单参数</param>
        /// <param name="minValue">当Value少于该值时,返回该值</param>
        /// <returns>返回int型</returns>
        public static int GetIntMinValue(string strName, int minValue)
        {
            int str = GetInt(strName);
            if (str < minValue)
            {
                return minValue;
            }
            else
            {
                return str;
            }
        }

        /// <summary>获得Get提交的参数值,小于0返回0,否则返回int值</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回 大于等于0的int型</returns>
        public static int GetInt0(string strName)
        {
            return GetIntMinValue(strName, 0);
        }

        /// <summary>获得Get提交的参数值,小于1返回1,否则返回int值</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回 大于等于1的int型</returns>
        public static int GetInt1(string strName)
        {
            return GetIntMinValue(strName, 1);
        }

        /// <summary>获得Post提交的参数值,判断是否double,是返回,否返回0</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回double型</returns>
        public static double PostDouble(string strName)
        {
            string str = GetFormString(strName);
            if (str.Length > 0 && ConvertHelper.IsNumeric(str))
            {
                return ConvertHelper.Cdbl(str);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>获得Post提交的参数值,判断是否double,是返回,否返回0</summary>
        /// <param name="strName">表单参数</param>
        /// <returns>返回double型</returns>
        public static double PostDouble0(string strName)
        {
            string str = GetFormString(strName);
            if (str.Length > 0 && ConvertHelper.IsNumeric(str))
            {
                double t = ConvertHelper.Cdbl(str);
                return (t < 0) ? 0 : t;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>获得Url或表单参数的值(不区分Post或Get提交,同样都取值,但Get优先处理)</summary>
        /// <param name="strName">参数</param>
        /// <param name="isFilterXss">是否过滤XSS</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool isFilterXss = true)
        {
            if ("".Equals(GetQueryString(strName)))
            {
                return GetFormString(strName, isFilterXss);
            }
            else
            {
                return GetQueryString(strName, isFilterXss);
            }
        }

        /// <summary>获得Post提交的全部值</summary>
        /// <returns>获得Post提交的全部值</returns>
        public static string GetFormAll(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            int ti = context.Request.Form.Count;
            if (ti > 0)
            {
                for (int i = 0; i < ti; i++)
                {
                    sb.Append(context.Request.Form.Keys[i].ToString());
                    sb.Append("=");
                    sb.AppendLine(context.Request.Form[i].ToString());
                }
            }
            return sb.ToString();
        }

        /// <summary>返回指定的服务器变量信息</summary>
        /// <param name="strName">服务器变量名</param>
        /// <returns>服务器变量信息</returns>
        public static string GetServerString(string strName)
        {
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.ServerVariables[strName].ToString();
        }

        /// <summary>检查输入口,是否为本服务器</summary>
        /// <returns></returns>
        public static bool ChkSrcPost()
        {
            string strV1 = "", strV2 = "";
            strV1 = GetServerString("HTTP_REFERER");
            strV2 = GetServerString("SERVER_NAME");

            if (strV1.Length > 8)
                strV1 = strV1.Substring(7);

            if (strV1.Length > strV2.Length)
                strV1 = strV1.Substring(0, strV2.Length);

            return (strV1 == strV2);
        }

        /// <summary>取得当前页面的域名</summary>
        /// <returns></returns>
        public static string GetRequestHost()
        {
            string url = "http://" + HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port != 80)
            {
                url += ":" + HttpContext.Current.Request.Url.Port;
            }
            return url;
        }

        /// <summary>取得当前页面的域名</summary>
        /// <returns></returns>
        public static string GetRequestHost2()
        {
            string url = HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port != 80)
            {
                url += ":" + HttpContext.Current.Request.Url.Port;
            }
            return url;
        }

        /// <summary>取得当前页面的路径</summary>
        /// <returns></returns>
        public static string GetRequestFileName()
        {
            var fileName = System.IO.Path.GetFileName(HttpContext.Current.Request.Path);
            return fileName != null ? fileName.ToString() : "";
        }
    }
}
