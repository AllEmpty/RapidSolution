/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Configuration;

namespace DotNet.Utilities
{
    /// <summary>
    /// web.config������
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ
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
                        //�������棬10���ӹ���
                        CacheHelper.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(10), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel + "";
        }

        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ
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
                        //�������棬10���ӹ���
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
        /// �õ�AppSettings�е�����Bool��Ϣ
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
        /// �õ�AppSettings�е�����Decimal��Ϣ
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
        /// �õ�AppSettings�е�����int��Ϣ
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
