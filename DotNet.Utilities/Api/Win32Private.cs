using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;


namespace DotNet.Utilities
{
    /// <summary>
    /// ִ����Ҫ���� <b>Win32</b> API �Ĳ��������ࡣ
    /// </summary>
    [SuppressUnmanagedCodeSecurity()]
    public static partial class Win32
    {
        #region ����

        /// <summary>
        /// ִ�л�ȡ��ǰ���еĲ���ϵͳ�汾��
        /// </summary>
        /// <returns><see cref="Platform"/> ��ֵ֮һ������ʾ��ǰ���еĲ���ϵͳ�汾��</returns>
        private static Platform GetCurrentPlatform()
        {
            OperatingSystem os = Environment.OSVersion;
            Platform pt;
            switch(os.Platform)
            {
                case (PlatformID.Win32Windows): // Win95, Win98 or Me
                    switch(os.Version.Minor)
                    {
                        case (0): // 95
                            pt = Platform.Windows95;
                            break;
                        case (10): // 98
                            if(os.Version.Revision.ToString() == "2222A")
                                pt = Platform.Windows982ndEdition;
                            else
                                pt = Platform.Windows98;
                            break;
                        case (90): // winme
                            pt = Platform.WindowsME;
                            break;
                        default: // Unknown
                            pt = Platform.UnKnown;
                            break;
                    }
                    break;
                case (PlatformID.Win32NT): //Win2k or Xp or 2003
                    switch(os.Version.Major)
                    {
                        case (3):
                            pt = Platform.WindowsNT351;
                            break;
                        case (4):
                            pt = Platform.WindowsNT40;
                            break;
                        case (5):
                            if(os.Version.Minor == 0)
                                pt = Platform.Windows2000;
                            else if(os.Version.Minor == 1)
                                pt = Platform.WindowsXP;
                            else if(os.Version.Minor == 2)
                                pt = Platform.Windows2003;
                            else
                                pt = Platform.UnKnown;
                            break;
                        case (6):
                            pt = Platform.WindowsVista;
                            break;
                        default:
                            pt = Platform.UnKnown;
                            break;
                    }
                    break;
                case (PlatformID.WinCE): // WinCE
                    pt = Platform.WindowsCE;
                    break;
                case (PlatformID.Win32S):
                case (PlatformID.Unix):
                default:
                    pt = Platform.UnKnown;
                    break;
            }
            return pt;
        }

        /// <summary>
        /// ��ʾ����ϵͳƽ̨��
        /// </summary>
        private enum Platform : byte
        {
            /// <summary>
            /// Windows 95 ����ϵͳ.
            /// </summary>
            Windows95,
            /// <summary>
            /// Windows 98 ����ϵͳ.
            /// </summary>
            Windows98,
            /// <summary>
            /// Windows 98 �ڶ������ϵͳ.
            /// </summary>
            Windows982ndEdition,
            /// <summary>
            /// Windows ME ����ϵͳ.
            /// </summary>
            WindowsME,
            /// <summary>
            /// Windows NT 3.51 ����ϵͳ.
            /// </summary>
            WindowsNT351,
            /// <summary>
            /// Windows NT 4.0 ����ϵͳ.
            /// </summary>
            WindowsNT40,
            /// <summary>
            /// Windows 2000 ����ϵͳ.
            /// </summary>
            Windows2000,
            /// <summary>
            /// Windows XP ����ϵͳ.
            /// </summary>
            WindowsXP,
            /// <summary>
            /// Windows 2003 ����ϵͳ.
            /// </summary>
            Windows2003,
            /// <summary>
            /// Windows Vista ����ϵͳ.
            /// </summary>
            WindowsVista,
            /// <summary>
            /// Windows CE ����ϵͳ.
            /// </summary>
            WindowsCE,
            /// <summary>
            /// ����ϵͳ�汾δ֪��
            /// </summary>
            UnKnown
        }

        /// <summary>
        /// ��ʾIDE�豸����״̬����ĳ�������ֵ�Ķ�Ӧ��
        /// </summary>
        /// <remarks>����ֵ�볣�������� <b>WinIoCtl.h</b> �ļ��С�</remarks>
        private enum DriverError : byte
        {
            /// <summary>
            /// �豸�޴���
            /// </summary>
            SMART_NO_ERROR = 0, // No error
            /// <summary>
            /// �豸IDE����������
            /// </summary>
            SMART_IDE_ERROR = 1, // Error from IDE controller
            /// <summary>
            /// ��Ч�������ǡ�
            /// </summary>
            SMART_INVALID_FLAG = 2, // Invalid command flag
            /// <summary>
            /// ��Ч���������ݡ�
            /// </summary>
            SMART_INVALID_COMMAND = 3, // Invalid command byte
            /// <summary>
            /// ��������Ч���绺����Ϊ�ջ��ַ���󣩡�
            /// </summary>
            SMART_INVALID_BUFFER = 4, // Bad buffer (null, invalid addr..)
            /// <summary>
            /// �豸��Ŵ���
            /// </summary>
            SMART_INVALID_DRIVE = 5, // Drive number not valid
            /// <summary>
            /// IOCTL����
            /// </summary>
            SMART_INVALID_IOCTL = 6, // Invalid IOCTL
            /// <summary>
            /// �޷������û��Ļ�������
            /// </summary>
            SMART_ERROR_NO_MEM = 7, // Could not lock user's buffer
            /// <summary>
            /// ��Ч��IDEע�����
            /// </summary>
            SMART_INVALID_REGISTER = 8, // Some IDE Register not valid
            /// <summary>
            /// ��Ч���������á�
            /// </summary>
            SMART_NOT_SUPPORTED = 9, // Invalid cmd flag set
            /// <summary>
            /// ָ��Ҫ���ҵ������������Ч��
            /// </summary>
            SMART_NO_IDE_DEVICE = 10
        }

        public static void ChangeByteOrder(byte[] charArray)
        {
            byte temp;
            for(int i = 0; i < charArray.Length; i += 2)
            {
                temp = charArray[i];
                charArray[i] = charArray[i + 1];
                charArray[i + 1] = temp;
            }
        }

        /// <summary>
        /// ����ָ�����豸��Ϣ�����豸����ϸ��Ϣ��
        /// </summary>
        /// <param name="phdinfo">һ�� <see cref="IdSector"/></param>
        /// <returns></returns>
        private static HDiskInfo GetHardDiskInfo(IdSector phdinfo)
        {
            HDiskInfo hdd = new HDiskInfo();
            hdd.ModuleNumber = Encoding.ASCII.GetString(phdinfo.sModelNumber).Trim();
            hdd.Firmware = Encoding.ASCII.GetString(phdinfo.sFirmwareRev).Trim();
            hdd.SerialNumber = Encoding.ASCII.GetString(phdinfo.sSerialNumber).Trim();
            hdd.Capacity = phdinfo.ulTotalAddressableSectors / 2 / 1024;
            hdd.BufferSize = phdinfo.wBufferSize / 1024;
            return hdd;
        }

        /// <summary>
        /// ��ȡ��NTƽ̨��ָ�����кŵ�Ӳ����Ϣ��
        /// </summary>
        /// <param name="driveIndex">������̵�������</param>
        /// <returns></returns>
        private static HDiskInfo GetHddInfoNT(byte driveIndex)
        {
            GetVersionOutParams vers = new GetVersionOutParams();
            SendCmdInParams inParam = new SendCmdInParams();
            SendCmdOutParams outParam = new SendCmdOutParams();
            uint bytesReturned = 0;

            // ʹ�� Win2000 �� Xp�µķ�����ȡӲ����Ϣ

            // ��ȡ�豸�ľ����
            IntPtr hDevice =
                CreateFile(string.Format(@"\\.\PhysicalDrive{0}", driveIndex), GENERIC_READ | GENERIC_WRITE,
                           FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            // ��ʼ���
            if(hDevice == IntPtr.Zero)
                throw new UnauthorizedAccessException("ִ�� Win32 API ���� CreateFile ʧ�ܡ�");
            if(0 == DeviceIoControl(hDevice, SMART_GET_VERSION, IntPtr.Zero, 0, ref vers,
                (uint)Marshal.SizeOf(vers),
                ref bytesReturned,
                IntPtr.Zero))
            {
                CloseHandle(hDevice);
                throw new IOException(string.Format(ResourcesApi.Win32_DeviceIoControlErr, "SMART_GET_VERSION"));
            }
            // ���IDE���������Ƿ�֧��
            if(0 == (vers.fCapabilities & 1))
            {
                CloseHandle(hDevice);
                throw new IOException(ResourcesApi.Win32_DeviceIoControlNotSupport);
            }
            // Identify the IDE drives
            if(0 != (driveIndex & 1))
                inParam.irDriveRegs.bDriveHeadReg = 0xb0;
            else
                inParam.irDriveRegs.bDriveHeadReg = 0xa0;
            if(0 != (vers.fCapabilities & (16 >> driveIndex)))
            {
                // We don't detect a ATAPI device.
                CloseHandle(hDevice);
                throw new IOException(ResourcesApi.Win32_DeviceIoControlNotSupport);
            }
            else
                inParam.irDriveRegs.bCommandReg = 0xec;
            inParam.bDriveNumber = driveIndex;
            inParam.irDriveRegs.bSectorCountReg = 1;
            inParam.irDriveRegs.bSectorNumberReg = 1;
            inParam.cBufferSize = 512;

            if(0 == DeviceIoControl(
                hDevice,
                SMART_RCV_DRIVE_DATA,
                ref inParam,
                (uint)Marshal.SizeOf(inParam),
                ref outParam,
                (uint)Marshal.SizeOf(outParam),
                ref bytesReturned,
                IntPtr.Zero))
            {
                CloseHandle(hDevice);
                throw new IOException(
                        string.Format(ResourcesApi.Win32_DeviceIoControlErr, "SMART_RCV_DRIVE_DATA"));
            }
            CloseHandle(hDevice);
            ChangeByteOrder(outParam.bBuffer.sModelNumber);
            ChangeByteOrder(outParam.bBuffer.sSerialNumber);
            ChangeByteOrder(outParam.bBuffer.sFirmwareRev);
            return GetHardDiskInfo(outParam.bBuffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driveIndex"></param>
        /// <returns></returns>
        private static HDiskInfo GetHddInfo9X(byte driveIndex)
        {
            GetVersionOutParams vers = new GetVersionOutParams();
            SendCmdInParams inParam = new SendCmdInParams();
            SendCmdOutParams outParam = new SendCmdOutParams();
            uint bytesReturned = 0;
            IntPtr hDevice = CreateFile(@"\\.\Smartvsd", 0, 0, IntPtr.Zero, CREATE_NEW, 0, IntPtr.Zero);
            if(hDevice == IntPtr.Zero)
                throw new UnauthorizedAccessException("�� smartvsd.vxd �ļ�ʧ�ܡ�");
            if(0 == DeviceIoControl(hDevice, SMART_GET_VERSION, 
                IntPtr.Zero, 0, 
                ref vers, (uint)Marshal.SizeOf(vers), ref bytesReturned, IntPtr.Zero))
            {
                CloseHandle(hDevice);
                throw new IOException(string.Format(ResourcesApi.Win32_DeviceIoControlErr, "SMART_GET_VERSION"));
            }
            // ��� IDE �ļ��������ʶ���ʧ��
            if(0 == (vers.fCapabilities & 1))
            {
                CloseHandle(hDevice);
                throw new IOException(ResourcesApi.Win32_DeviceIoControlNotSupport);
            }
            if(0 != (driveIndex & 1))
                inParam.irDriveRegs.bDriveHeadReg = 0xb0;
            else
                inParam.irDriveRegs.bDriveHeadReg = 0xa0;
            if(0 != (vers.fCapabilities & (16 >> driveIndex)))
            {
                // ����IDEΪATAPI���ͣ��޷�����
                CloseHandle(hDevice);
                throw new IOException(ResourcesApi.Win32_DeviceIoControlNotSupport);
            }
            else
                inParam.irDriveRegs.bCommandReg = 0xec;
            inParam.bDriveNumber = driveIndex;
            inParam.irDriveRegs.bSectorCountReg = 1;
            inParam.irDriveRegs.bSectorNumberReg = 1;
            inParam.cBufferSize = BUFFER_SIZE;
            if(0 == DeviceIoControl(hDevice, SMART_RCV_DRIVE_DATA, ref inParam, (uint)Marshal.SizeOf(inParam), ref outParam, (uint)Marshal.SizeOf(outParam), ref bytesReturned, IntPtr.Zero))
            {
                CloseHandle(hDevice);
                throw new IOException(string.Format(ResourcesApi.Win32_DeviceIoControlErr, "SMART_RCV_DRIVE_DATA"));
            }
            // �ر��ļ����
            CloseHandle(hDevice);
            ChangeByteOrder(outParam.bBuffer.sModelNumber);
            ChangeByteOrder(outParam.bBuffer.sSerialNumber);
            ChangeByteOrder(outParam.bBuffer.sFirmwareRev);
            return GetHardDiskInfo(outParam.bBuffer);
        }

        #endregion

        #region Win32

        /// <summary>
        /// ȡ��ָ�����ڵ�ϵͳ�˵��ľ����
        /// </summary>
        /// <param name="hwnd">ָ��Ҫ��ȡϵͳ�˵����ڵ� <see cref="IntPtr"/> �����</param>
        /// <param name="bRevert">��ȡϵͳ�˵��ķ�ʽ������Ϊ <b>true</b>����ʾ����ԭʼ��ϵͳ�˵�����������Ϊ <b>false</b> ��</param>
        /// <returns>ָ��Ҫ��ȡ��ϵͳ�˵��� <see cref="IntPtr"/> �����</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        /// <summary>
        /// ��ȡָ���Ĳ˵�����Ŀ���˵����������
        /// </summary>
        /// <param name="hMenu">ָ��Ҫ��ȡ�˵���������ϵͳ�˵��� <see cref="IntPtr"/> �����</param>
        /// <returns>�˵��е���Ŀ����</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetMenuItemCount(IntPtr hMenu);

        /// <summary>
        /// ɾ��ָ���Ĳ˵���Ŀ��
        /// </summary>
        /// <param name="hMenu">ָ��Ҫ�Ƴ��Ĳ˵��� <see cref="IntPtr"/> ��</param>
        /// <param name="uPosition">���ı�Ĳ˵���Ŀ�ı�ʶ����</param>
        /// <param name="uFlags"></param>
        /// <returns>�����ʾ�ɹ������ʾʧ�ܡ�</returns>
        /// <remarks>
        /// ����� <paramref name="uFlags"/> ��ʹ����<see cref="MF_BYPOSITION"/> ������ <paramref name="uPosition"/> ������ʾ�˵����������
        /// ����� <paramref name="uFlags"/> ��ʹ���� <b>MF_BYCOMMAND</b>������ <paramref name="uPosition"/> ��ʹ�ò˵����ID��
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        /// <summary>
        /// �ر�һ��ָ����ָ�����ָ����豸����
        /// </summary>
        /// <param name="hObject">Ҫ�رյľ�� <see cref="IntPtr"/> ����</param>
        /// <returns>�ɹ����� <b>0</b> �����ɹ����ط���ֵ��</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int CloseHandle(IntPtr hObject);

        /// <summary>
        /// ִ�д�/������Դ�Ĺ��ܡ�
        /// </summary>
        /// <param name="lpFileName">ָ��Ҫ�򿪵��豸���ļ������ơ�</param>
        /// <param name="dwDesiredAccess">
        /// <para>Win32 ���������ڿ��ƶ��豸�Ķ����ʡ�д���ʻ��/д���ʵĳ������������±�
        /// <p><list type="table">
        /// <listheader>
        /// <term>����</term>
        /// <description>˵��</description>
        /// </listheader>
        /// <item>
        /// <term>GENERIC_READ</term><description>ָ�����豸���ж�ȡ���ʡ�</description>
        /// </item>
        /// <item>
        /// <term>GENERIC_WRITE</term><description>ָ�����豸����д���ʡ�</description>
        /// </item>
        /// <item><term><b>0</b></term><description>���ֵΪ�㣬���ʾֻ�����ȡ��һ���豸�йص���Ϣ��</description></item>
        /// </list></p>
        /// </para>
        /// </param>
        /// <param name="dwShareMode">ָ�����豸ʱ���ļ�����ģʽ</param>
        /// <param name="lpSecurityAttributes"></param>
        /// <param name="dwCreationDisposition">Win32 ������ָ������ϵͳ���ļ��ķ�ʽ���������±�
        /// <para><p>
        /// <list type="table">
        /// <listheader><term>����</term><description>˵��</description></listheader>
        /// <item>
        /// <term>CREATE_NEW</term>
        /// <description>ָ������ϵͳӦ�������ļ�������ļ����ڣ����׳� <see cref="IOException"/> �쳣��</description>
        /// </item>
        /// <item><term>CREATE_ALWAYS</term><description>ָ������ϵͳӦ�������ļ�������ļ��Ѵ��ڣ���������д��</description></item>
        /// </list>
        /// </p></para>
        /// </param>
        /// <param name="dwFlagsAndAttributes"></param>
        /// <param name="hTemplateFile"></param>
        /// <returns>ʹ�ú����򿪵��豸�ľ����</returns>
        /// <remarks>
        /// ����������ִ�д򿪻����ļ����ļ�����Ŀ¼/�ļ��С�������̡���ϵͳ���ƵĻ��������Ŵ��豸��
        /// ͨ����Դ���ʼ�ϵͳ�������ܵ���
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
                                                IntPtr lpSecurityAttributes, uint dwCreationDisposition,
                                                uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        /// <summary>
        /// ���豸ִ��ָ���Ĳ�����
        /// </summary>
        /// <param name="hDevice">Ҫִ�в������豸�����</param>
        /// <param name="dwIoControlCode">Win32 API ��������������� <b>FSCTL_</b> Ϊǰ׺�ĳ����������� 
        /// <b>WinIoCtl.h</b> �ļ��ڣ�ִ�д����ط����������� <b>SMART_GET_VERSION</b> ��</param>
        /// <param name="lpInBuffer">������Ϊָ��ʱ��Ĭ�ϵ�����ֵ�� <b>0</b> ��</param>
        /// <param name="nInBufferSize">���뻺�������ֽ�������</param>
        /// <param name="lpOutBuffer">һ�� <b>GetVersionOutParams</b> ����ʾִ�к�����������豸��顣</param>
        /// <param name="nOutBufferSize">������������ֽ�������</param>
        /// <param name="lpBytesReturned">ʵ��װ�ص�������������ֽ�������</param>
        /// <param name="lpOverlapped">ͬ���������ƣ�һ�㲻ʹ�ã�Ĭ��ֵΪ <b>0</b> ��</param>
        /// <returns>�����ʾ�ɹ������ʾʧ�ܡ�</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer,
                                                  uint nInBufferSize, ref GetVersionOutParams lpOutBuffer,
                                                  uint nOutBufferSize, ref uint lpBytesReturned,
                                                  [Out] IntPtr lpOverlapped);

        /// <summary>
        /// ���豸ִ��ָ���Ĳ�����
        /// </summary>
        /// <param name="hDevice">Ҫִ�в������豸�����</param>
        /// <param name="dwIoControlCode">Win32 API ��������������� <b>FSCTL_</b> Ϊǰ׺�ĳ����������� 
        /// <b>WinIoCtl.h</b> �ļ��ڣ�ִ�д����ط����������� <b>SMART_SEND_DRIVE_COMMAND</b> �� <b>SMART_RCV_DRIVE_DATA</b> ��</param>
        /// <param name="lpInBuffer">һ�� <b>SendCmdInParams</b> �ṹ����������ϵͳ���͵Ĳ�ѯҪ�������������ݽṹ��</param>
        /// <param name="nInBufferSize">���뻺�������ֽ�������</param>
        /// <param name="lpOutBuffer">һ�� <b>SendCmdOutParams</b> �ṹ��������ϵͳ��������ص��豸������Ϣ���������ݡ�</param>
        /// <param name="nOutBufferSize">������������ֽ�������</param>
        /// <param name="lpBytesReturned">ʵ��װ�ص�������������ֽ�������</param>
        /// <param name="lpOverlapped">ͬ���������ƣ�һ�㲻ʹ�ã�Ĭ��ֵΪ <b>0</b> ��</param>
        /// <returns>�����ʾ�ɹ������ʾʧ�ܡ�</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, ref SendCmdInParams lpInBuffer,
                                                  uint nInBufferSize, ref SendCmdOutParams lpOutBuffer,
                                                  uint nOutBufferSize, ref uint lpBytesReturned,
                                                  [Out] IntPtr lpOverlapped);

        #endregion

        #region �ṹ

        /// <summary>
        /// ���浱ǰ����� IDE �豸��Ӳ�̣���Ӳ����Ϣ�Ľṹ��
        /// </summary>
        [Serializable]
        public struct HDiskInfo
        {
            /// <summary>
            /// Ӳ���ͺš�
            /// </summary>
            public string ModuleNumber;

            /// <summary>
            /// Ӳ�̵Ĺ̼��汾��
            /// </summary>
            public string Firmware;

            /// <summary>
            /// Ӳ�����кš�
            /// </summary>
            public string SerialNumber;

            /// <summary>
            /// Ӳ����������MΪ��λ��
            /// </summary>
            public uint Capacity;
            
            /// <summary>
            /// �豸�����С����MΪ��λ����
            /// </summary>
            public int BufferSize;
        }

        /// <summary>
        /// ��ʾʹ�� <b>DeviceIoControl</b> ����ʱ���淵�ص�������Ӳ����Ϣ�Ľṹ
        /// </summary>
        /// <remarks>>�����ݽṹ������ <b>WinIoCtl.h</b> �ļ���Ϊ <b>_GETVERSIONINPARAMS</b> �ṹ�С�</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct GetVersionOutParams
        {
            /// <summary>
            /// IDE�豸�Ķ�����Ӳ���汾��
            /// </summary>
            public byte bVersion;

            /// <summary>
            /// IDE�豸�Ķ������޶��汾��
            /// </summary>
            public byte bRevision;

            /// <summary>
            /// ��ֵ����ϵͳû��ʹ�ã�ʹ�ô����ݽṹʱ������Ϊ <b>0</b> ��
            /// </summary>
            public byte bReserved;

            /// <summary>
            /// IDE�豸�Ķ�����ӳ�䡣
            /// </summary>
            public byte bIDEDeviceMap;

            /// <summary>
            /// IDE�豸�Ķ������������ݡ�
            /// </summary>
            public uint fCapabilities;

            /// <summary>
            /// �������ݣ���ʹ�á�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public uint[] dwReserved; // For future use.
        }

        /// <summary>
        /// һ�����ݽṹ����ʾʹ�� <b>DeviceIoControl</b> ����ʱ���͵�����ϵͳ�е��������ݽṹ <b>SendCmdInParams</b> �ĳ�Ա�ṹ��
        /// ����ʾҪ��ȡ�����豸���ܲ����ľ��嶨�����
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        /// <remarks>�����ݽṹ������ <b>WinIoCtl.h</b> �ļ���Ϊ <b>_IDEREGS</b> �С�</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct IdeRegs
        {
            /// <summary>
            /// ���͵�����ϵͳ��ע�������Ϊϵͳ�� <b>SMART Command</b> ��
            /// </summary>
            public byte bFeaturesReg;

            /// <summary>
            /// ��ȡIDE�豸��������
            /// </summary>
            public byte bSectorCountReg;

            /// <summary>
            /// ��ȡIDE�豸��š�
            /// </summary>
            public byte bSectorNumberReg;

            /// <summary>
            /// ��ȡIDE�豸�Ͷ�����ֵ��
            /// </summary>
            public byte bCylLowReg;

            /// <summary>
            /// ��ȡIDE�豸�߶�����ֵ��
            /// </summary>
            public byte bCylHighReg;

            /// <summary>
            /// ��ȡIDE�豸��ͷ��Ϣ��
            /// </summary>
            public byte bDriveHeadReg;

            /// <summary>
            /// ��ȡIDE�豸���������
            /// </summary>
            public byte bCommandReg;

            /// <summary>
            /// �������ݣ���ֵӦ����Ϊ <b>0</b> ��
            /// </summary>
            public byte bReserved;
        }

        /// <summary>
        /// ����ִ�� <b>DeviceIoControl</b> ����ʱ��ϵͳ�ύ��ִ�в������
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        /// <remarks>�����ݽṹ������ <b>WinIoCtl.h</b> �ļ���Ϊ <b>_SENDCMDINPARAMS</b> �С�</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct SendCmdInParams
        {
            /// <summary>
            /// ��������ݻ����С��
            /// </summary>
            public uint cBufferSize;

            /// <summary>
            /// ������ϵͳ���͵Ĵ����豸��������ݽṹ��
            /// </summary>
            public IdeRegs irDriveRegs;

            /// <summary>
            /// ϣ��ϵͳ���Ƶ�������̵ı�š�
            /// </summary>
            public byte bDriveNumber;

            /// <summary>
            /// ���������ݣ���ʹ�á�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] bReserved;

            /// <summary>
            /// ���������ݣ���ʹ�á�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public uint[] dwReserved;

            /// <summary>
            /// ���浱ǰ <b>SendCmdInParams</b> �ṹ������ݺ�Ĵ�С��
            /// </summary>
            public byte bBuffer;
        }

        /// <summary>
        /// ��ִ�� <b>DeviceIoControl</b> ������ϵͳ���ص� <b>SendCmdOutParams</b> �ṹ��
        /// ��������豸��ǰ������Ϣ�����ݽṹ��
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        /// <remarks>
        /// �����ݽṹ������ <b>WinIoCtl.h</b> �ļ���Ϊ <b>_DRIVERSTATUS</b> �С�
        /// <para>
        /// ����������±�<br />
        /// <list type="table">
        /// <listheader>
        /// <term>����</term>
        /// <description>˵��</description>
        /// <item><term>SMART_NO_ERROR = 0</term>
        /// <description>û�д���</description></item>
        /// <item><term>SMART_IDE_ERROR = 1</term>
        /// <description>IDE����������</description>��</item>
        /// <item><term>SMART_INVALID_FLAG = 2</term>
        /// <description>���͵���������Ч��</description></item>
        /// <item><term>SMART_INVALID_COMMAND = 3</term>
        /// <description>���͵Ķ�����������Ч��</description></item>
        /// <item><term>SMART_INVALID_BUFFER = 4</term>
        /// <description>�����ƻ�����Ч������Ϊ�ջ�����Ч��ַ����</description></item>
        /// <item><term>SMART_INVALID_DRIVE = 5</term>
        /// <description>���������������Ч��</description></item>
        /// <item><term>SMART_INVALID_IOCTL = 6</term>
        /// <description>��Ч��IOCTL��</description></item>
        /// <item><term>SMART_ERROR_NO_MEM =  7</term>
        /// <description>ʹ�õĻ������޷�������</description></item>
        /// <item><term>SMART_INVALID_REGISTER = 8</term>
        /// <description>IDEע��������Ч��</description></item>
        /// <item><term>SMART_NOT_SUPPORTED = 9</term>
        /// <description>������������Ч��</description></item>
        /// <item><term>SMART_NO_IDE_DEVICE = 10</term>
        /// <description>���͵����������������������ơ�</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct DriverStatus
        {
            /// <summary>
            /// �������IDE�豸�������󣬱���Ĵ�����룬<b>0</b> ��ʾû�д���
            /// </summary>
            public byte bDriverError;

            /// <summary>
            /// IDE�豸��ע��Ĵ������ݡ�
            /// </summary>
            public byte bIDEStatus;

            /// <summary>
            /// ���������ݣ���ʹ�á�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] bReserved;

            /// <summary>
            /// ���������ݣ���ʹ�á�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] dwReserved;
        }

        /// <summary>
        /// ��ʾ��ִ�� <b>DeviceIoControl</b> �����󱣴�ϵͳ���ݲ�ѯ����صĴ����豸��Ϣ�����ݽṹ��
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct SendCmdOutParams
        {
            /// <summary>
            /// ��ʾ���ж�������Ϣ�Ļ����С��
            /// </summary>
            public uint cBufferSize;

            /// <summary>
            /// ��ʾ��ѯ���豸�Ĵ�����Ϣ״̬��
            /// </summary>
            public DriverStatus DriverStatus;

            /// <summary>
            /// ��ʾϵͳ���ص��豸Ӳ����Ϣ�Ķ��������ݽṹ��
            /// </summary>
            public IdSector bBuffer;
        }

        /// <summary>
        /// ��ִ�� <b>DeviceIoControl</b> ������ϵͳ���ص� <b>SendCmdOutParams</b> �ṹ��
        /// ��������豸��Ӳ����Ϣ�����ݽṹ��
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 512)]
        private struct IdSector
        {
            /// <summary>
            /// �豸ͨ��������Ϣ��
            /// </summary>
            public ushort wGenConfig;

            /// <summary>
            /// �豸����������
            /// </summary>
            public ushort wNumCyls;

            /// <summary>
            /// �������ݣ���ʹ�á�
            /// </summary>
            public ushort wReserved;

            /// <summary>
            /// �豸�Ĵ�ͷ��Ŀ��
            /// </summary>
            public ushort wNumHeads;

            /// <summary>
            /// �豸�Ĵŵ���Ŀ��
            /// </summary>
            public ushort wBytesPerTrack;

            /// <summary>
            /// �豸��������Ŀ��
            /// </summary>
            public ushort wBytesPerSector;

            /// <summary>
            /// �豸�����趨�������ŵ���Ŀ��
            /// </summary>
            public ushort wSectorsPerTrack;

            /// <summary>
            /// �豸�ĳ�Ʒ�������ơ�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public ushort[] wVendorUnique;

            /// <summary>
            /// �豸��Ʒ���̵�ȫ��Ψһ���롣
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sSerialNumber;

            /// <summary>
            /// �豸�Ļ������͡�
            /// </summary>
            public ushort wBufferType;

            /// <summary>
            /// �豸������������λ��byte����
            /// </summary>
            public ushort wBufferSize;

            /// <summary>
            /// �豸�Ĵ�����;�����ECC�����ݵĴ�С��
            /// </summary>
            public ushort wECCSize;

            /// <summary>
            /// �豸�Ĺ̼��汾��
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sFirmwareRev;

            /// <summary>
            /// �豸���ͺš�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public byte[] sModelNumber;

            /// <summary>
            /// �豸�������Ƶ���չ���ݣ�����У���
            /// </summary>
            public ushort wMoreVendorUnique;

            /// <summary>
            /// �豸˫ָ���������ģʽ��
            /// </summary>
            public ushort wDoubleWordIO;

            /// <summary>
            /// �豸��������С����λByte����
            /// </summary>
            public ushort wCapabilities;

            /// <summary>
            /// ��һ�����������ݣ���ʹ�á�
            /// </summary>
            public ushort wReserved1;

            /// <summary>
            /// �豸��PIOģʽѲ��ʱ�䡣
            /// </summary>
            public ushort wPIOTiming;

            /// <summary>
            /// �豸DMA ģʽѲ��ʱ�䡣
            /// </summary>
            public ushort wDMATiming;

            /// <summary>
            /// �豸���������ͣ���SCSI,IDE�ȡ�
            /// </summary>
            public ushort wBS;

            /// <summary>
            /// �豸�ĵ�ǰ����������
            /// </summary>
            public ushort wNumCurrentCyls;

            /// <summary>
            /// �豸��ǰ��ͷ������
            /// </summary>
            public ushort wNumCurrentHeads;

            /// <summary>
            /// �豸�ĵ�ǰ�����Ĵŵ�������
            /// </summary>
            public ushort wNumCurrentSectorsPerTrack;

            /// <summary>
            /// �豸�ĵ�ǰ������������λbyte����
            /// </summary>
            public uint ulCurrentSectorCapacity;

            /// <summary>
            /// ��������дģʽ֧�֡�
            /// </summary>
            public ushort wMultSectorStuff;

            /// <summary>
            /// �û��Ƿ���Զ���������ַ(LBAģʽ��֧�֡�
            /// </summary>
            public uint ulTotalAddressableSectors;

            /// <summary>
            /// ��ָ��DMAģʽ��
            /// </summary>
            public ushort wSingleWordDMA;

            /// <summary>
            /// ��ָ��DMAģʽ��
            /// </summary>
            public ushort wMultiWordDMA;

            /// <summary>
            /// �������ݣ���ʹ�á�
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
            public byte[] bReserved;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="left"></param>
            /// <param name="top"></param>
            /// <param name="right"></param>
            /// <param name="bottom"></param>
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="r"></param>
            public RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }
            /// <summary>
            /// 
            /// </summary>
            public int left;
            /// <summary>
            /// 
            /// </summary>
            public int top;
            /// <summary>
            /// 
            /// </summary>
            public int right;
            /// <summary>
            /// 
            /// </summary>
            public int bottom;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            /// <summary>
            /// 
            /// </summary>
            public Size Size
            {
                get { return new Size(right - left, bottom - top); }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public sealed class SCROLLINFO
        {
            public SCROLLINFO()
            {
                this.cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
            }

            public SCROLLINFO(int mask, int min, int max, int page, int pos)
            {
                this.cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
                this.fMask = mask;
                this.nMin = min;
                this.nMax = max;
                this.nPage = page;
                this.nPos = pos;
            }

            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }
        #endregion

        #region ����

        /// <summary>
        /// Win32 API ������ָʾ��ʹ�� <see cref="RemoveMenu"/> ����ʱָ��ʹ��������������ʹ��ID��
        /// </summary>
        private const int MF_BYPOSITION = 0x00000400;
        private const uint FILE_SHARE_READ = 0x00000001;
        private const uint FILE_SHARE_WRITE = 0x00000002;
        private const uint FILE_SHARE_DELETE = 0x00000004;
        private const uint SMART_GET_VERSION = 0x00074080; // SMART_GET_VERSION
        private const uint SMART_SEND_DRIVE_COMMAND = 0x0007c084; // SMART_SEND_DRIVE_COMMAND
        private const uint SMART_RCV_DRIVE_DATA = 0x0007c088; // SMART_RCV_DRIVE_DATA
        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint CREATE_NEW = 1;
        private const uint OPEN_EXISTING = 3;
        private const uint BUFFER_SIZE = 512;
        private static readonly Platform currentOs;

        #endregion
    }
}