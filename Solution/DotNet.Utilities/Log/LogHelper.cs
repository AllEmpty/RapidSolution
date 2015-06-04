using System;

namespace DotNet.Utilities.Log
{
    /// <summary>  
    /// LogHelper的摘要说明。   
    /// </summary>   
    public class LogHelper
    {
        /// <summary>
        /// 静态只读实体对象info信息
        /// </summary>
        public static readonly log4net.ILog Loginfo = log4net.LogManager.GetLogger("loginfo");
        /// <summary>
        ///  静态只读实体对象error信息
        /// </summary>
        public static readonly log4net.ILog Logerror = log4net.LogManager.GetLogger("logerror");

        /// <summary>
        ///  添加info信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        public static void WriteLog(string info)
        {
            try
            {
                if (Loginfo.IsInfoEnabled)
                {
                    Loginfo.Info(info);
                }
            }
            catch { }
        }


        /// <summary>
        /// 添加异常信息
        /// </summary>
        /// <param name="info">自定义日志内容说明</param>
        /// <param name="ex">异常信息</param>
        public static void WriteLog(string info, Exception ex)
        {
            try
            {
                if (Logerror.IsErrorEnabled)
                {
                    Logerror.Error(info, ex);
                }
            }
            catch { }
        }
    }
}