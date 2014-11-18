/// <summary>
/// 类说明：ADSL重新连接、拨号
/// 编码日期：2013-04-10
/// 编 码 人：Eagle
/// 联系方式：838010363  
/// 更新网址：http://www.sufeinet.com/thread-655-1-1.html
/// 修改日期：2013-04-11
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DotNet.Utilities
{
    public class Ras
    {
        // Fields
        private bool bConnected;
        private ConnectionNotify ConnectNotify;
        private const int DNLEN = 15;
        private string EntryName;
        private const int ERROR_BUFFER_TOO_SMALL = 0x25b;
        private int hrasconn;
        public const int MAX_PATH = 260;
        public Timer NotifyTimer;
        private const int PWLEN = 0x100;
        private const string Ras_Authenticate = "正在验证用户名与密码.";
        public const string Ras_Connected = "成功连接到";
        public const string Ras_Connecting = "正在连接";
        private const string Ras_DialUping = "正在拨...";
        public const string Ras_Disconnected = "连接中断.";
        private const string Ras_Dot = "...";
        private const int RAS_MaxCallbackNumber = 0x80;
        private const int RAS_MaxDeviceName = 0x80;
        private const int RAS_MaxDeviceType = 0x10;
        public const int RAS_MaxEntryName = 0x100;
        private const int RAS_MaxPhoneNumber = 0x80;
        private const string Ras_OpenPort = "正在打开端口...";
        private const string Ras_PortOpend = "端口已经打开.";
        private RASCONN[] Rasconn;
        private const int RASCS_DONE = 0x2000;
        private const int RASCS_PAUSED = 0x1000;
        private const int UNLEN = 0x100;

        // Methods
        public Ras()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ConnectionDelegate">连接通知委托</param>
        /// <param name="interval"></param>
        public Ras(ConnectionNotify ConnectionDelegate, double interval)
        {
            this.ConnectNotify = ConnectionDelegate;
            this.NotifyTimer = new Timer(interval);
            this.NotifyTimer.Elapsed += new ElapsedEventHandler(this.TimerEvent);
            this.Rasconn = new RASCONN[1];
            this.Rasconn[0].dwSize = Marshal.SizeOf(this.Rasconn[0]);
            this.NotifyTimer.Start();
            this.bConnected = false;
        }

        /// <summary>
        /// 创建条目
        /// </summary>
        /// <param name="hWnd">柄</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool CreateEntry(int hWnd, out string strError)
        {
            int nErrorValue = RasCreatePhonebookEntry(hWnd, null);
            if (nErrorValue == 0)
            {
                strError = null;
                return true;
            }
            strError = this.GetErrorString(nErrorValue);
            return false;
        }

        /// <summary>
        /// 删除条目
        /// </summary>
        /// <param name="strEntryName">条目名称</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool DeleteEntry(string strEntryName, out string strError)
        {
            int nErrorValue = RasDeleteEntry(null, strEntryName);
            if (nErrorValue == 0)
            {
                strError = null;
                return true;
            }
            strError = this.GetErrorString(nErrorValue);
            return false;
        }

        /// <summary>
        /// 拨号
        /// </summary>
        /// <param name="strEntryName">条目名称</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool DialUp(string strEntryName, out string strError)
        {
            bool lpfPassword = false;
            RASDIALPARAMS structure = new RASDIALPARAMS();
            structure.dwSize = Marshal.SizeOf(structure);
            structure.szEntryName = strEntryName;
            RasDialEvent lpvNotifier = new RasDialEvent(this.RasDialFunc);
            int nErrorValue = RasGetEntryDialParams(null, ref structure, ref lpfPassword);
            if (nErrorValue != 0)
            {
                strError = this.GetErrorString(nErrorValue);
                return false;
            }
            this.ConnectNotify("正在连接" + structure.szEntryName + "...", 1);
            this.EntryName = strEntryName;
            this.hrasconn = 0;
            nErrorValue = RasDial(0, null, ref structure, 0, lpvNotifier, ref this.hrasconn);
            if (nErrorValue != 0)
            {
                strError = this.GetErrorString(nErrorValue);
                this.ConnectNotify(strError, 3);
                return false;
            }
            this.ConnectNotify("正在打开端口...", 1);
            strError = null;
            return true;
        }

        /// <summary>
        /// 编辑条目
        /// </summary>
        /// <param name="hWnd">柄</param>
        /// <param name="strEntryName">条目名称</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool EditEntry(int hWnd, string strEntryName, out string strError)
        {
            int nErrorValue = RasEditPhonebookEntry(hWnd, null, strEntryName);
            if (nErrorValue == 0)
            {
                strError = null;
                return true;
            }
            strError = this.GetErrorString(nErrorValue);
            return false;
        }
        /// <summary>
        /// 获取默认条目
        /// </summary>
        /// <param name="strEntry">条目</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool GetDefaultEntry(out string strEntry, out string strError)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE/Microsoft/RAS AutoDial/Default");
            if (key != null)
            {
                string str = (string)key.GetValue("DefaultInternet");
                if ((str != null) && (str.Length > 0))
                {
                    strEntry = str;
                    strError = null;
                    return true;
                }
            }
            strEntry = null;
            strError = "注册表访问失败.";
            return false;
        }

        /// <summary>
        /// 得到条目
        /// </summary>
        /// <param name="strEntryName">条目名称</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool GetEntries(out string[] strEntryName, out string strError)
        {
            RASENTRYNAME[] lprasentryname = new RASENTRYNAME[1];
            lprasentryname[0].dwSize = Marshal.SizeOf(lprasentryname[0]);
            int lpcb = 0;
            int lpcEntries = 0;
            int nErrorValue = RasEnumEntries(null, null, lprasentryname, ref lpcb, ref lpcEntries);
            switch (nErrorValue)
            {
                case 0:
                    break;

                case 0x25b:
                    lprasentryname = new RASENTRYNAME[lpcEntries];
                    lprasentryname[0].dwSize = Marshal.SizeOf(lprasentryname[0]);
                    break;

                default:
                    strError = this.GetErrorString(nErrorValue);
                    strEntryName = null;
                    return false;
            }
            nErrorValue = RasEnumEntries(null, null, lprasentryname, ref lpcb, ref lpcEntries);
            if (nErrorValue != 0)
            {
                strError = this.GetErrorString(nErrorValue);
                strEntryName = null;
                return false;
            }
            strEntryName = new string[lpcEntries];
            for (int i = 0; i < lpcEntries; i++)
            {
                strEntryName[i] = lprasentryname[i].szEntryName;
            }
            strError = null;
            return true;
        }

        /// <summary>
        /// 获得入口参数
        /// </summary>
        /// <param name="strEntryName">条目名称</param>
        /// <param name="strPhoneNumber">电话号码</param>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPassword">密码</param>
        /// <param name="bRememberPassword">记住密码</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool GetEntryParams(string strEntryName, out string strPhoneNumber, out string strUserName, out string strPassword, out bool bRememberPassword, out string strError)
        {
            bool lpfPassword = false;
            RASDIALPARAMS structure = new RASDIALPARAMS();
            structure.dwSize = Marshal.SizeOf(structure);
            structure.szEntryName = strEntryName;
            int nErrorValue = RasGetEntryDialParams(null, ref structure, ref lpfPassword);
            if (nErrorValue != 0)
            {
                strError = this.GetErrorString(nErrorValue);
                strPhoneNumber = null;
                strUserName = null;
                strPassword = null;
                bRememberPassword = false;
                strError = null;
                return false;
            }
            strPhoneNumber = structure.szPhoneNumber;
            strUserName = structure.szUserName;
            if (lpfPassword)
            {
                strPassword = structure.szPassword;
            }
            else
            {
                strPassword = null;
            }
            bRememberPassword = lpfPassword;
            strError = null;
            return true;
        }

        /// <summary>
        /// 获得错误
        /// </summary>
        /// <param name="nErrorValue"></param>
        /// <returns></returns>
        internal string GetErrorString(int nErrorValue)
        {
            if ((nErrorValue >= 600) && (nErrorValue < 0x2f2))
            {
                int cBufSize = 0x100;
                string lpszErrorString = new string(new char[cBufSize]);
                if (RasGetErrorString(nErrorValue, lpszErrorString, cBufSize) != 0)
                {
                    lpszErrorString = null;
                }
                if ((lpszErrorString != null) && (lpszErrorString.Length > 0))
                {
                    return lpszErrorString;
                }
            }
            return null;
        }

        /// <summary>
        /// 挂断
        /// </summary>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool HangUp(out string strError)
        {
            this.bConnected = false;
            if (this.hrasconn != 0)
            {
                int nErrorValue = RasHangUp(this.hrasconn);
                if (nErrorValue != 0)
                {
                    strError = this.GetErrorString(nErrorValue);
                    this.ConnectNotify(strError, 0);
                    return false;
                }
            }
            foreach (RASCONN rasconn in this.Rasconn)
            {
                if (rasconn.hrasconn != 0)
                {
                    int num2 = RasHangUp(rasconn.hrasconn);
                    if (num2 != 0)
                    {
                        strError = this.GetErrorString(num2);
                        this.ConnectNotify(strError, 0);
                        return false;
                    }
                }
            }
            strError = null;
            this.ConnectNotify("连接中断.", 0);
            return true;
        }

        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasCreatePhonebookEntry(int hwnd, string lpszPhonebook);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasDeleteEntry(string lpszPhonebook, string lpszEntry);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasDial(int lpRasDialExtensions, string lpszPhonebook, ref RASDIALPARAMS lpRasDialParams, int dwNotifierType, RasDialEvent lpvNotifier, ref int lphRasConn);
        /// <summary>
        /// 拨
        /// </summary>
        /// <param name="unMsg"></param>
        /// <param name="rasconnstate"></param>
        /// <param name="dwError"></param>
        private void RasDialFunc(uint unMsg, RASCONNSTATE rasconnstate, int dwError)
        {
            if (dwError != 0)
            {
                this.ConnectNotify(this.GetErrorString(dwError), 3);
                this.bConnected = false;
                if (this.hrasconn != 0)
                {
                    int nErrorValue = RasHangUp(this.hrasconn);
                    if (nErrorValue == 0)
                    {
                        this.ConnectNotify("连接中断.", 0);
                    }
                    else
                    {
                        this.ConnectNotify(this.GetErrorString(nErrorValue), 0);
                    }
                }
            }
            else
            {
                if (rasconnstate == RASCONNSTATE.RASCS_PortOpened)
                {
                    this.ConnectNotify("端口已经打开.", 1);
                }
                if (rasconnstate == RASCONNSTATE.RASCS_ConnectDevice)
                {
                    this.ConnectNotify("正在拨...", 1);
                }
                if (rasconnstate == RASCONNSTATE.RASCS_Authenticate)
                {
                    this.ConnectNotify("正在验证用户名与密码.", 1);
                }
                if (rasconnstate == RASCONNSTATE.RASCS_Connected)
                {
                    this.bConnected = true;
                    this.ConnectNotify("成功连接到" + this.EntryName + '.', 2);
                }
            }
        }

        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasEditPhonebookEntry(int hwnd, string lpszPhonebook, string lpszEntryName);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasEnumConnections([In, Out] RASCONN[] lprasconn, ref int lpcb, ref int lpcConnections);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasEnumEntries(string reserved, string lpszPhonebook, [In, Out] RASENTRYNAME[] lprasentryname, ref int lpcb, ref int lpcEntries);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasGetConnectStatus(int hrasconn, ref RASCONNSTATUS lprasconnstatus);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasGetEntryDialParams(string lpszPhonebook, ref RASDIALPARAMS lprasdialparams, ref bool lpfPassword);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasGetErrorString(int uErrorValue, string lpszErrorString, int cBufSize);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasHangUp(int hrasconn);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasRenameEntry(string lpszPhonebook, string lpszOldEntry, string lpszNewEntry);
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        private static extern int RasSetEntryDialParams(string lpszPhonebook, ref RASDIALPARAMS lprasdialparams, bool fRemovePassword);
        /// <summary>
        /// 重命名输入
        /// </summary>
        /// <param name="strOldEntry">旧的条目</param>
        /// <param name="strNewEntry">新的条目</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool RenameEntry(string strOldEntry, string strNewEntry, out string strError)
        {
            int nErrorValue = RasRenameEntry(null, strOldEntry, strNewEntry);
            if (nErrorValue == 0)
            {
                strError = null;
                return true;
            }
            strError = this.GetErrorString(nErrorValue);
            return false;
        }

        /// <summary>
        /// 设置默认条目
        /// </summary>
        /// <param name="strEntry"></param>
        public void SetDefaultEntry(string strEntry)
        {
            if ((strEntry != null) && (strEntry.Length > 0))
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE/Microsoft/RAS AutoDial/Default", true);
                if (key == null)
                {
                    key = Registry.LocalMachine.CreateSubKey(@"SOFTWARE/Microsoft/RAS AutoDial/Default");
                }
                key.SetValue("DefaultInternet", strEntry);
            }
        }

        /// <summary>
        /// 设置输入参数个数
        /// </summary>
        /// <param name="strEntryName">条目名称</param>
        /// <param name="strPhoneNumber">电话号码</param>
        /// <param name="strUserName">用户名</param>
        /// <param name="strPassword">密码</param>
        /// <param name="bRememberPassword">记住密码</param>
        /// <param name="strError">错误</param>
        /// <returns>结果</returns>
        public bool SetEntryParams(string strEntryName, string strPhoneNumber, string strUserName, string strPassword, bool bRememberPassword, out string strError)
        {
            RASDIALPARAMS structure = new RASDIALPARAMS();
            structure.dwSize = Marshal.SizeOf(structure);
            structure.szEntryName = strEntryName;
            structure.szPhoneNumber = strPhoneNumber;
            structure.szUserName = strUserName;
            structure.szPassword = strPassword;
            int nErrorValue = RasSetEntryDialParams(null, ref structure, !bRememberPassword);
            if (nErrorValue != 0)
            {
                strError = this.GetErrorString(nErrorValue);
                return false;
            }
            strError = null;
            return true;
        }

        /// <summary>
        /// 计时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerEvent(object sender, ElapsedEventArgs e)
        {
            RASCONNSTATUS structure = new RASCONNSTATUS();
            int lpcb = 0;
            int lpcConnections = 0;
            structure.dwSize = Marshal.SizeOf(structure);
            int nErrorValue = RasEnumConnections(this.Rasconn, ref lpcb, ref lpcConnections);
            switch (nErrorValue)
            {
                case 0:
                    break;

                case 0x25b:
                    this.Rasconn = new RASCONN[lpcConnections];
                    lpcb = this.Rasconn[0].dwSize = Marshal.SizeOf(this.Rasconn[0]);
                    nErrorValue = RasEnumConnections(this.Rasconn, ref lpcb, ref lpcConnections);
                    break;

                default:
                    this.ConnectNotify(this.GetErrorString(nErrorValue), 3);
                    return;
            }
            if (nErrorValue != 0)
            {
                this.ConnectNotify(this.GetErrorString(nErrorValue), 3);
            }
            else if ((lpcConnections < 1) && this.bConnected)
            {
                this.bConnected = false;
                this.ConnectNotify("连接中断.", 0);
            }
            else
            {
                for (int i = 0; i < lpcConnections; i++)
                {
                    nErrorValue = RasGetConnectStatus(this.Rasconn[i].hrasconn, ref structure);
                    if (nErrorValue != 0)
                    {
                        this.ConnectNotify(this.GetErrorString(nErrorValue), 3);
                        return;
                    }
                    if ((structure.rasconnstate == RASCONNSTATE.RASCS_Connected) && !this.bConnected)
                    {
                        this.bConnected = true;
                        this.ConnectNotify("成功连接到" + this.Rasconn[i].szEntryName + '.', 2);
                    }
                    if ((structure.rasconnstate == RASCONNSTATE.RASCS_Disconnected) && this.bConnected)
                    {
                        this.bConnected = false;
                        this.ConnectNotify("连接中断.", 0);
                    }
                }
            }
        }

        /// <summary>
        /// 嵌套类型
        /// </summary>
        /// <param name="strNotify"></param>
        /// <param name="bConnect"></param>
        public delegate void ConnectionNotify(string strNotify, int bConnect);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct RASCONN
        {
            internal int dwSize;
            internal int hrasconn;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x101)]
            internal string szEntryName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x11)]
            internal string szDeviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x81)]
            internal string szDeviceName;
        }

        private enum RASCONNSTATE
        {
            RASCS_AllDevicesConnected = 4,
            RASCS_AuthAck = 12,
            RASCS_AuthCallback = 8,
            RASCS_AuthChangePassword = 9,
            RASCS_Authenticate = 5,
            RASCS_Authenticated = 14,
            RASCS_AuthLinkSpeed = 11,
            RASCS_AuthNotify = 6,
            RASCS_AuthProject = 10,
            RASCS_AuthRetry = 7,
            RASCS_CallbackSetByCaller = 0x1002,
            RASCS_ConnectDevice = 2,
            RASCS_Connected = 0x2000,
            RASCS_DeviceConnected = 3,
            RASCS_Disconnected = 0x2001,
            RASCS_Interactive = 0x1000,
            RASCS_OpenPort = 0,
            RASCS_PasswordExpired = 0x1003,
            RASCS_PortOpened = 1,
            RASCS_PrepareForCallback = 15,
            RASCS_Projected = 0x12,
            RASCS_ReAuthenticate = 13,
            RASCS_RetryAuthentication = 0x1001,
            RASCS_SubEntryConnected = 0x13,
            RASCS_SubEntryDisconnected = 20,
            RASCS_WaitForCallback = 0x11,
            RASCS_WaitForModemReset = 0x10
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct RASCONNSTATUS
        {
            internal int dwSize;
            internal Ras.RASCONNSTATE rasconnstate;
            internal int dwError;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x11)]
            internal string szDeviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x81)]
            internal string szDeviceName;
        }

        /// <summary>
        /// 拨事件
        /// </summary>
        /// <param name="unMsg">显示</param>
        /// <param name="rasconnstate"></param>
        /// <param name="dwError">错误</param>
        private delegate void RasDialEvent(uint unMsg, Ras.RASCONNSTATE rasconnstate, int dwError);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct RASDIALPARAMS
        {
            internal int dwSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x101)]
            internal string szEntryName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x81)]
            internal string szPhoneNumber;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x81)]
            internal string szCallbackNumber;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x101)]
            internal string szUserName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x101)]
            internal string szPassword;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
            internal string szDomain;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct RASENTRYNAME
        {
            internal int dwSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x101)]
            internal string szEntryName;
        }
    }
}