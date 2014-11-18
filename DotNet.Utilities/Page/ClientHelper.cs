using System;
using System.Web;

namespace DotNet.Utilities
{
    /// <summary>
    /// 客户端信息获取类
    /// </summary>
    public class ClientHelper
    {

        private HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
        //定义搜索引擎关键字，用于判断是否是搜索引警进入本站
        string[] SearchEngine = { "google", "360.cn", "baidu", "sogou", "soso", "so.com", "youdao", "bing.com", "yahoo", "lycos", "googlesyndication.com", "sm.cn" };
        //定义搜索引擎关键字，用于判断访问网站的是否是搜索引擎的网络蜘蛛在抓起页面
        public static readonly string[] _searchEngineList = { "spider", "Googlebot", "bingbot", "Yahoo", "YoudaoBot", "MJ12bot", "alexa", "Wget", "msnbot", "DotBot", "yandex", "google", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };

        /// <summary>构造函数
        /// </summary>
		public ClientHelper(HttpRequest Request)
        {
            bc = Request.Browser;
        }

        /// <summary>返回浏览器操作系统名称
        /// </summary>
        /// <returns></returns>
        public string GetBrowserOS()
        {
            return System.Web.HttpContext.Current.Request.Browser.Platform.ToString();
        }

        /// <summary>返回浏览器IP
        /// </summary>
        /// <returns></returns>
        public string GetBrowserIP()
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
            //return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];//此方法也可以
        }

        ///<summary> 返回浏览器是否支持Java
        /// </summary>
        /// <returns></returns>
        public string GetBrowserSupportJava()
        {
            return System.Web.HttpContext.Current.Request.Browser.JavaScript.ToString();
        }

        /// <summary>返回浏览器IE版本
        /// </summary>
        /// <returns></returns>
        public string GetBrowserName()
        {
            return System.Web.HttpContext.Current.Request.Browser.Browser.ToString() + System.Web.HttpContext.Current.Request.Browser.Version.ToString();
        }

        /// <summary>返回浏览器.NET版本
        /// </summary>
        /// <returns></returns>
        public string GetBrowserNETCLR()
        {
            return System.Web.HttpContext.Current.Request.Browser.ClrVersion.ToString();
        }

        /// <summary>返回浏览器是否支持Cookies
        /// </summary>
        /// <returns></returns>
        public string GetBrowserSupportCookies()
        {
            return System.Web.HttpContext.Current.Request.Browser.Cookies.ToString();
        }

        /// <summary>获取客户端使用的系统</summary>
        /// <returns></returns>
        public string GetSystem()
        {
            try
            {
                return HttpContext.Current.Request.Browser.Platform;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>判断系统属于win16还是win32,还是其他
        /// </summary>
        /// <returns></returns>
        public string GetSysClass()
        {
            try
            {
                if (bc.Win16)
                {
                    return "16位";
                        //bc.Win16.ToString();
                }
                else
                    if (bc.Win32)
                    {
                        //return bc.Win32.ToString();

                        return "32位";
                    }
                    else
                    {
                        return "Other";
                    }

            }
            catch (Exception)
            {
                return "";
            }


        }

        /// <summary>获取客户端浏览器信息
        /// </summary>
        /// <returns></returns>
        public string GetBrowserInfo()
        {
            try
            {
                return bc.Browser;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>获取浏览器的标识
        /// </summary>
        /// <returns></returns>
        public string GetBrowserIdentifying()
        {
            try
            {

                return bc.Id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>获取浏览器的版本信息
        /// </summary>
        /// <returns></returns>
        public string GetBrowserVersion()
        {
            try
            {
                return bc.Version;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>获取浏览器的主版本信息
        /// </summary>
        /// <returns></returns>
        public string GetBrowerMajorVersion()
        {
            try
            {
                return bc.MajorVersion.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>获取浏览器的次版本信息
        /// </summary>
        /// <returns></returns>
        public string GetBrowserMinorVersion()
        {
            try
            {
                return bc.MinorVersion.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>判断浏览器是否为测试版本
        /// </summary>
        /// <returns></returns>
        public bool? IsBrowserBeta()
        {
            try
            {
                return bc.Beta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>是否是 America Online(美国在线服务)浏览器</summary>
        /// <returns></returns>
        public bool? IsBrowserAOL()
        {
            try
            {
                return bc.AOL;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>客户端安装的 .NET Framework 版本</summary>
        /// <returns></returns>
        public string GetNetClrVersion()
        {
            try
            {
                return bc.ClrVersion.ToString();

            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>是否为是搜索引擎的网络爬虫
        /// </summary>
        /// <returns></returns>
        public bool IsCrawler()
        {
            try
            {
                return bc.Crawler;
            }
            catch (Exception)
            {

            }

            string UA = HttpContext.Current.Request.Headers["User-Agent"];
            if (UA == null || UA == "")
            {
                return false;
            }
            foreach (string ua in _searchEngineList)
            {
                if (UA.IndexOf(ua) > -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为移动设备
        /// </summary>
        /// <returns></returns>
        public bool IsMobileDevice()
        {
            try
            {
                return bc.IsMobileDevice;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 是否为移动设备
        /// </summary>
        /// <returns></returns>
        public bool IsMobileDevice(string ua)
        {
            try
            {
                if (ua.IndexOf("Android") > -1 || ua.IndexOf("iPhone") > -1 || ua.IndexOf("Mobile") > -1)
                    return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>显示的颜色深度
        /// 
        /// </summary>
        /// <returns></returns>
        public int? GetScreenBitDepth()
        {
            try
            {
                return bc.ScreenBitDepth;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>显示的近似宽度（以字符行为单位)
        /// </summary>
        /// <returns></returns>
        public int? GetScreenCharactersWidth()
        {
            try
            {
                return bc.ScreenCharactersWidth;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>显示的近似高度（以字符行为单位）
        /// </summary>
        /// <returns></returns>
        public int? GetScreenCharactersHeight()
        {
            try
            {
                return bc.ScreenCharactersHeight;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>显示的近似宽度（以像素行为单位）
        /// </summary>
        /// <returns></returns>
        public int? GetScreenPixelsWidth()
        {
            try
            {
                return bc.ScreenPixelsWidth;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>显示的近似高度（以像素行为单位）
        /// </summary>
        /// <returns></returns>
        public int? GetScreenPixelsHeight()
        {
            try
            {
                return bc.ScreenPixelsHeight;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>是否支持 CSS
        /// </summary>
        /// <returns></returns>
        public bool? IsSupportsCss()
        {
            try
            {
                return bc.SupportsCss;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>是否支持 ActiveX 控件
        /// </summary>
        /// <returns></returns>
        public bool? IsActiveXControls()
        {
            try
            {
                return bc.ActiveXControls;
            }
            catch (Exception)
            {
                return null;

            }
        }

        /// <summary>是否支持 JavaApplets
        /// </summary>
        /// <returns></returns>
        public bool? IsJavaApplets()
        {
            try
            {
                return bc.JavaApplets;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>是否支持javascript
        /// </summary>
        /// <returns></returns>
        public bool? IsJavaScript()
        {
            try
            {
                return bc.JavaScript;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>获取javascript版本
        /// </summary>
        /// <returns></returns>
        public String GetJScriptVersion()
        {
            try
            {
                return bc.JScriptVersion.ToString();
            }
            catch (Exception)
            {

                return "";
            }
        }

        /// <summary>是否支持VBScript脚本
        /// </summary>
        public bool? IsVBScript()
        {
            try
            {
                return bc.VBScript;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>是否支持Cookie
        /// </summary>
        /// <returns></returns>
        public bool? IsCookies()
        {
            try
            {
                return bc.Cookies;
            }
            catch (Exception)
            {

                return null;

            }
        }

        /// <summary>支持的 MSHTML 的 DOM 版本
        /// </summary>
        /// <returns></returns>
        public string GetMSDomVersion()
        {
            try
            {
                return bc.MSDomVersion.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>支持的 W3C 的 DOM 版本
        /// </summary>
        /// <returns></returns>
        public string GetW3CDomVersion()
        {
            try
            {
                return bc.W3CDomVersion.ToString();
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>是否支持通过 HTTP 接收 XML
        /// </summary>
        /// <returns></returns>
        public bool? IsSupportsXmlHttp()
        {
            try
            {
                return bc.SupportsXmlHttp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>是否支持框架
        /// </summary>
        /// <returns></returns>
        public bool? IsFrames()
        {
            try
            {
                return bc.Frames;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>超链接 a 属性 href 值的最大长度
        /// </summary>
        /// <returns></returns>
        public int? GetMaximumHrefLength()
        {
            try
            {
                return bc.MaximumHrefLength;
            }
            catch (Exception)
            {

                return null;
            }

        }

        /// <summary>是否支持表格
        /// </summary>
        /// <returns></returns>
        public bool? IsTables()
        {
            try
            {
                return bc.Tables;
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>返回移动设备制造商的名称
        /// </summary>
        /// <returns></returns>
        public String GetMobileDeviceManufacturer()
        {
            try
            {
                return bc.MobileDeviceManufacturer;

            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>该浏览器设备是否能够启动语音呼叫
        /// </summary>
        /// <returns></returns>
        public bool? IsCanInitiateVoiceCall()
        {
            try
            {
                return bc.CanInitiateVoiceCall;

            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>判断浏览器是否支持Html中mailto发送电子邮件
        /// </summary>
        /// <returns></returns>
        public bool? IsCanSendMail()
        {
            try
            {
                return bc.CanSendMail;

            }
            catch (Exception)
            {

                throw null;
            }
        }

        /// <summary>判断浏览器是否支持Web广播的频道定义格式
        /// </summary>
        /// <returns></returns>
        public bool? IsCDF()
        {
            try
            {
                return bc.CDF;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>获取浏览器EcmaScriptVersion支持的版本
        /// </summary>
        /// <returns></returns>
        public string GetEcmaScriptVersion()
        {
            try
            {
                return bc.EcmaScriptVersion.ToString();

            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>返回浏览器支持可输入的类型
        /// </summary>
        /// <returns></returns>
        public string GetInputType()
        {
            try
            {
                return bc.InputType;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>判断当前浏览器是否与指定的浏览器相同
        /// </summary>
        /// <param name="BrowserName">指定进行对比的浏览器</param>
        /// <returns></returns>
        public bool? IsAlikeBrowser(string BrowserName)
        {
            try
            {
                return bc.IsBrowser(BrowserName);

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>返回移动设备上软键的数目
        /// </summary>
        /// <returns></returns>
        public int? GetNumberOfSoftkeys()
        {
            try
            {
                return bc.NumberOfSoftkeys;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary> 获取浏览器请求的首选编码
        /// </summary>
        /// <returns></returns>
        public string GetPreferredRequestEncoding()
        {
            try
            {
                return bc.PreferredRequestEncoding;

            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>获取浏览器响应的首选编码
        /// </summary>
        /// <returns></returns>
        public string GetPreferredResponseEncoding()
        {
            try
            {
                return bc.PreferredResponseEncoding;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>浏览器是否支持回调脚本
        /// </summary>
        /// <returns></returns>
        public bool? IsSupportsCallback()
        {
            try
            {
                return bc.SupportsCallback;

            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>获取浏览器的名称+主（整数）版本号
        /// </summary>
        /// <returns></returns>
        public string GetBrowserType()
        {
            try
            {
                return bc.Type;
            }
            catch (Exception)
            {

                return null; ;
            }
        }

        /// <summary>
        /// 判断是否为搜索引擎
        /// </summary>
        /// <returns></returns>
        public bool IsSearchEnginesGet(string refererUrl)
        {
            if (string.IsNullOrEmpty(refererUrl))
                return false;

            string tmpReferrer = refererUrl.ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断是否为搜索引擎
        /// </summary>
        /// <returns></returns>
        public bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }

            return IsSearchEnginesGet(HttpContext.Current.Request.UrlReferrer.ToString());
        }

        /// <summary> 获得当前页面客户端的IP </summary>
        /// <returns>当前页面客户端的IP</returns>
        public string GetIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }
            return result;
        }

        /// <summary>
        /// 将所以客户端收集的信息输出来
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string temp = "";
            temp += "返回浏览器操作系统名称:   " + GetBrowserOS() + "\r\n";
            temp += "返回浏览器IP:   " + GetBrowserIP() + "\r\n";
            temp += "浏览器是否支持JavaScript:   " + GetBrowserSupportJava() + "\r\n";
            temp += "浏览器IE版本:   " + GetBrowserName() + "\r\n";
            temp += "浏览器.NET版本:   " + GetBrowserNETCLR() + "\r\n";
            temp += "浏览器是否支持Cookies:   " + GetBrowserSupportCookies() + "\r\n";
            temp += "客户端使用的系统:   " + GetSystem() + "\r\n";
            temp += "判断系统属于win16还是win32,还是其他:   " + GetSysClass() + "\r\n";
            temp += "客户端浏览器信息:   " + GetBrowserInfo() + "\r\n";
            temp += "浏览器的标识:   " + GetBrowserIdentifying() + "\r\n";
            temp += "浏览器的版本信息:   " + GetBrowserVersion() + "\r\n";
            temp += "浏览器的主版本信息:   " + GetBrowerMajorVersion() + "\r\n";
            temp += "浏览器的次版本信息:   " + GetBrowserMinorVersion() + "\r\n";
            temp += "浏览器是否为测试版本:   " + IsBrowserBeta() + "\r\n";
            temp += "是否是 America Online(美国在线服务)浏览器:   " + IsBrowserAOL() + "\r\n";
            temp += "客户端安装的 .NET Framework 版本:   " + GetNetClrVersion() + "\r\n";
            temp += "是否为是搜索引擎的网络爬虫:   " + IsCrawler() + "\r\n";
            temp += "是否为移动设备:   " + IsMobileDevice() + "\r\n";
            temp += "显示的颜色深度:   " + GetScreenBitDepth() + "\r\n";
            temp += "显示的近似宽度（以字符行为单位):   " + GetScreenCharactersWidth() + "\r\n";
            temp += "显示的近似高度（以字符行为单位）:   " + GetScreenCharactersHeight() + "\r\n";
            temp += "显示的近似宽度（以像素行为单位）:   " + GetScreenPixelsWidth() + "\r\n";
            temp += "显示的近似高度（以像素行为单位）:   " + GetScreenPixelsHeight() + "\r\n";
            temp += "是否支持 CSS:   " + IsSupportsCss() + "\r\n";
            temp += "是否支持 ActiveX 控件:   " + IsActiveXControls() + "\r\n";
            temp += "是否支持 JavaApplets:   " + IsJavaApplets() + "\r\n";
            temp += "是否支持javascript:   " + IsJavaScript() + "\r\n";
            temp += "获取javascript版本:   " + GetJScriptVersion() + "\r\n";
            temp += "是否支持VBScript脚本:   " + IsVBScript() + "\r\n";
            temp += "是否支持Cookie:   " + IsCookies() + "\r\n";
            temp += "支持的 MSHTML 的 DOM 版本:   " + GetMSDomVersion() + "\r\n";
            temp += "支持的 W3C 的 DOM 版本:   " + GetW3CDomVersion() + "\r\n";
            temp += "是否支持通过 HTTP 接收 XML:   " + IsSupportsXmlHttp() + "\r\n";
            temp += "是否支持框架:   " + IsFrames() + "\r\n";
            temp += "超链接 a 属性 href 值的最大长度:   " + GetMaximumHrefLength() + "\r\n";
            temp += "是否支持表格:   " + IsTables() + "\r\n";
            temp += "返回移动设备制造商的名称:   " + GetMobileDeviceManufacturer() + "\r\n";
            temp += "该浏览器设备是否能够启动语音呼叫:   " + IsCanInitiateVoiceCall() + "\r\n";
            temp += "判断浏览器是否支持Html中mailto发送电子邮件:   " + IsCanSendMail() + "\r\n";
            temp += "判断浏览器是否支持Web广播的频道定义格式:   " + IsCDF() + "\r\n";
            temp += "获取浏览器EcmaScriptVersion支持的版本:   " + GetEcmaScriptVersion() + "\r\n";
            temp += "返回浏览器支持可输入的类型:   " + GetInputType() + "\r\n";
            temp += "返回移动设备上软键的数目:   " + GetNumberOfSoftkeys() + "\r\n";
            temp += "获取浏览器请求的首选编码:   " + GetPreferredRequestEncoding() + "\r\n";
            temp += "浏览器是否支持回调脚本:   " + IsSupportsCallback() + "\r\n";
            temp += "获取浏览器的名称+主（整数）版本号:   " + GetBrowserType() + "\r\n";
            temp += "判断是否为搜索引擎:   " + IsSearchEnginesGet() + "\r\n";
            temp += "获得当前页面客户端的IP :   " + GetIP() + "\r\n";

            return temp;
        }

    }
}
