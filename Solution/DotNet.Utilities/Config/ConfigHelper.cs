/// <summary>
/// 类说明：Assistant
/// 编 码 人：苏飞
/// 联系方式：361983679  
/// 更新网站：http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Configuration;

namespace DotNet.Utilities
{
    /// <summary>
    /// web.config操作类
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = CacheHelper.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if (objModel != null)
                    {
                        //参数缓存，10分钟过期
                        CacheHelper.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel + "";
        }

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] GetConfigStringArr(string key)
        {
            try
            {
                string cacheKey = "AppSettings-" + key;
                var objModel = (string[])CacheHelper.GetCache(cacheKey);
                if (objModel == null)
                {
                    var tem = ConfigurationManager.AppSettings[key];
                    objModel = StringHelper.SplitMulti(tem, ",");
                    if (objModel != null)
                    {
                        //参数缓存，10分钟过期
                        CacheHelper.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                    }
                }

                return objModel;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                if (cfgVal == "1" || cfgVal.ToLower() == "true")
                    result = true;
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = ConvertHelper.Cdecimal(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrEmpty(cfgVal))
            {
                try
                {
                    result = ConvertHelper.Cint(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
    }
}
