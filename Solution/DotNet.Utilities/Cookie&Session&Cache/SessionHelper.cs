/// <summary>
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.Web;

namespace DotNet.Utilities
{
    /// <summary>
    /// Session 操作类
    /// 1、GetSession(string name)根据session名获取session对象
    /// 2、SetSession(string name, object val)设置session
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// 根据session名获取session对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>获取指定Session的值值</summary>
        /// <param name="strName">Session的ID</param>
        /// <returns>返回Session值</returns>
        public static string GetSessionString(string strName)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[strName] != null)
            {
                return HttpContext.Current.Session[strName] + "";
            }
            return "";
        }

        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="name">session 名</param>
        /// <param name="val">session 值</param>
        public static void SetSession(string name, object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
        }

        /// <summary>
        /// 清空所有的Session
        /// </summary>
        /// <returns></returns>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// 删除一个指定的ession
        /// </summary>
        /// <param name="name">Session名称</param>
        /// <returns></returns>
        public static void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>
        /// 删除所有的ession
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSession(string name)
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}
