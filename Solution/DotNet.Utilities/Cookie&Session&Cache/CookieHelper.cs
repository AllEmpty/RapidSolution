/// <summary>
/// 类说明：CookieHelper
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Web;

namespace DotNet.Utilities
{
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookiename] != null)
            {
                return HttpContext.Current.Request.Cookies[cookiename].Value;
            }

            return "";
        }

        /// <summary>读cookie值,Cookies[key]
        /// </summary>
        /// <param name="cookiename">名称</param>
        /// <param name="key">key</param>
        /// <returns>cookie值</returns>
        public static string GetCookieValue(string cookiename, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[cookiename] != null && HttpContext.Current.Request.Cookies[cookiename][key] != null)
            {
                return HttpContext.Current.Request.Cookies[cookiename][key].ToString();
            }

            return "";
        }

        /// <summary>
        /// 添加一个Cookie（1年后过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddYears(1));
        }
        /// <summary>
        /// 添加一个Cookie,带过期时间
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiename);
            }
            cookie.Value = cookievalue;
            cookie.Expires = expires;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>写cookie值,Cookies[key]（1年后过期）
        /// </summary>
        /// <param name="cookiename">名称</param>
        /// <param name="key">key</param>
        /// <param name="cookievalue">值</param>
        public static void SetCookie(string cookiename, string key, string cookievalue)
        {
            SetCookie(cookiename, key, cookievalue, DateTime.Now.AddYears(1));
        }

        /// <summary>写cookie值,Cookies[key],带过期时间
        /// </summary>
        /// <param name="cookiename">名称</param>
        /// <param name="key">key</param>
        /// <param name="cookievalue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void SetCookie(string cookiename, string key, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie == null)
            {
                cookie = new HttpCookie(cookiename);
            }

            cookie[key] = cookievalue;
            cookie.Expires = expires;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        
    }
}
