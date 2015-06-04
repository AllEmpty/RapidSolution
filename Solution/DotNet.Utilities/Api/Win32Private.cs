using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;


namespace DotNet.Utilities
{
    /// <summary>
    /// 执行需要调用 <b>Win32</b> API 的操作辅助类。
    /// </summary>
    [SuppressUnmanagedCodeSecurity()]
    public static partial class Win32
    {
        #region 方法

        /// <summary>
        /// 执行获取当前运行的操作系统版本。
        /// </summary>
        /// <returns><see cref="Platform"/> 的值之一，他表示当前运行的操作系统版本。</returns>
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
        /// 表示操作系统平台。
        /// </summary>
        private enum Platform : byte
        {
            /// <summary>
            /// Windows 95 操作系统.
            /// </summary>
            Windows95,
            /// <summary>
            /// Windows 98 操作系统.
            /// </summary>
            Windows98,
            /// <summary>
            /// Windows 98 第二版操作系统.
            /// </summary>
            Windows982ndEdition,
            /// <summary>
            /// Windows ME 操作系统.
            /// </summary>
            WindowsME,
            /// <summary>
            /// Windows NT 3.51 操作系统.
            /// </summary>
            WindowsNT351,
            /// <summary>
            /// Windows NT 4.0 操作系统.
            /// </summary>
            WindowsNT40,
            /// <summary>
            /// Windows 2000 操作系统.
            /// </summary>
            Windows2000,
            /// <summary>
            /// Windows XP 操作系统.
            /// </summary>
            WindowsXP,
            /// <summary>
            /// Windows 2003 操作系统.
            /// </summary>
            Windows2003,
            /// <summary>
            /// Windows Vista 操作系统.
            /// </summary>
            WindowsVista,
            /// <summary>
            /// Windows CE 操作系统.
            /// </summary>
            WindowsCE,
            /// <summary>
            /// 操作系统版本未知。
            /// </summary>
            UnKnown
        }

        /// <summary>
        /// 表示IDE设备错误状态代码的常量与数值的对应。
        /// </summary>
        /// <remarks>其数值与常量定义在 <b>WinIoCtl.h</b> 文件中。</remarks>
        private enum DriverError : byte
        {
            /// <summary>
            /// 设备无错误。
            /// </summary>
            SMART_NO_ERROR = 0, // No error
            /// <summary>
            /// 设备IDE控制器错误。
            /// </summary>
            SMART_IDE_ERROR = 1, // Error from IDE controller
            /// <summary>
            /// 无效的命令标记。
            /// </summary>
            SMART_INVALID_FLAG = 2, // Invalid command flag
            /// <summary>
            /// 无效的命令数据。
            /// </summary>
            SMART_INVALID_COMMAND = 3, // Invalid command byte
            /// <summary>
            /// 缓冲区无效（如缓冲区为空或地址错误）。
            /// </summary>
            SMART_INVALID_BUFFER = 4, // Bad buffer (null, invalid addr..)
            /// <summary>
            /// 设备编号错误。
            /// </summary>
            SMART_INVALID_DRIVE = 5, // Drive number not valid
            /// <summary>
            /// IOCTL错误。
            /// </summary>
            SMART_INVALID_IOCTL = 6, // Invalid IOCTL
            /// <summary>
            /// 无法锁定用户的缓冲区。
            /// </summary>
            SMART_ERROR_NO_MEM = 7, // Could not lock user's buffer
            /// <summary>
            /// 无效的IDE注册命令。
            /// </summary>
            SMART_INVALID_REGISTER = 8, // Some IDE Register not valid
            /// <summary>
            /// 无效的命令设置。
            /// </summary>
            SMART_NOT_SUPPORTED = 9, // Invalid cmd flag set
            /// <summary>
            /// 指定要查找的设别索引号无效。
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
        /// 根据指定的设备信息生成设备的详细信息。
        /// </summary>
        /// <param name="phdinfo">一个 <see cref="IdSector"/></param>
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
        /// 获取在NT平台下指定序列号的硬盘信息。
        /// </summary>
        /// <param name="driveIndex">物理磁盘的数量。</param>
        /// <returns></returns>
        private static HDiskInfo GetHddInfoNT(byte driveIndex)
        {
            GetVersionOutParams vers = new GetVersionOutParams();
            SendCmdInParams inParam = new SendCmdInParams();
            SendCmdOutParams outParam = new SendCmdOutParams();
            uint bytesReturned = 0;

            // 使用 Win2000 或 Xp下的方法获取硬件信息

            // 获取设备的句柄。
            IntPtr hDevice =
                CreateFile(string.Format(@"\\.\PhysicalDrive{0}", driveIndex), GENERIC_READ | GENERIC_WRITE,
                           FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            // 开始检查
            if(hDevice == IntPtr.Zero)
                throw new UnauthorizedAccessException("执行 Win32 API 函数 CreateFile 失败。");
            if(0 == DeviceIoControl(hDevice, SMART_GET_VERSION, IntPtr.Zero, 0, ref vers,
                (uint)Marshal.SizeOf(vers),
                ref bytesReturned,
                IntPtr.Zero))
            {
                CloseHandle(hDevice);
                throw new IOException(string.Format(ResourcesApi.Win32_DeviceIoControlErr, "SMART_GET_VERSION"));
            }
            // 检测IDE控制命令是否支持
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
                throw new UnauthorizedAccessException("打开 smartvsd.vxd 文件失败。");
            if(0 == DeviceIoControl(hDevice, SMART_GET_VERSION, 
                IntPtr.Zero, 0, 
                ref vers, (uint)Marshal.SizeOf(vers), ref bytesReturned, IntPtr.Zero))
            {
                CloseHandle(hDevice);
                throw new IOException(string.Format(ResourcesApi.Win32_DeviceIoControlErr, "SMART_GET_VERSION"));
            }
            // 如果 IDE 的鉴定命令不被识别或失败
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
                // 检测出IDE为ATAPI类型，无法处理
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
            // 关闭文件句柄
            CloseHandle(hDevice);
            ChangeByteOrder(outParam.bBuffer.sModelNumber);
            ChangeByteOrder(outParam.bBuffer.sSerialNumber);
            ChangeByteOrder(outParam.bBuffer.sFirmwareRev);
            return GetHardDiskInfo(outParam.bBuffer);
        }

        #endregion

        #region Win32

        /// <summary>
        /// 取得指定窗口的系统菜单的句柄。
        /// </summary>
        /// <param name="hwnd">指向要获取系统菜单窗口的 <see cref="IntPtr"/> 句柄。</param>
        /// <param name="bRevert">获取系统菜单的方式。设置为 <b>true</b>，表示接收原始的系统菜单，否则设置为 <b>false</b> 。</param>
        /// <returns>指向要获取的系统菜单的 <see cref="IntPtr"/> 句柄。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);

        /// <summary>
        /// 获取指定的菜单中条目（菜单项）的数量。
        /// </summary>
        /// <param name="hMenu">指向要获取菜单项数量的系统菜单的 <see cref="IntPtr"/> 句柄。</param>
        /// <returns>菜单中的条目数量</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetMenuItemCount(IntPtr hMenu);

        /// <summary>
        /// 删除指定的菜单条目。
        /// </summary>
        /// <param name="hMenu">指向要移除的菜单的 <see cref="IntPtr"/> 。</param>
        /// <param name="uPosition">欲改变的菜单条目的标识符。</param>
        /// <param name="uFlags"></param>
        /// <returns>非零表示成功，零表示失败。</returns>
        /// <remarks>
        /// 如果在 <paramref name="uFlags"/> 中使用了<see cref="MF_BYPOSITION"/> ，则在 <paramref name="uPosition"/> 参数表示菜单项的索引；
        /// 如果在 <paramref name="uFlags"/> 中使用了 <b>MF_BYCOMMAND</b>，则在 <paramref name="uPosition"/> 中使用菜单项的ID。
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        /// <summary>
        /// 关闭一个指定的指针对象指向的设备。。
        /// </summary>
        /// <param name="hObject">要关闭的句柄 <see cref="IntPtr"/> 对象。</param>
        /// <returns>成功返回 <b>0</b> ，不成功返回非零值。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int CloseHandle(IntPtr hObject);

        /// <summary>
        /// 执行打开/建立资源的功能。
        /// </summary>
        /// <param name="lpFileName">指定要打开的设备或文件的名称。</param>
        /// <param name="dwDesiredAccess">
        /// <para>Win32 常量，用于控制对设备的读访问、写访问或读/写访问的常数。内容如下表：
        /// <p><list type="table">
        /// <listheader>
        /// <term>名称</term>
        /// <description>说明</description>
        /// </listheader>
        /// <item>
        /// <term>GENERIC_READ</term><description>指定对设备进行读取访问。</description>
        /// </item>
        /// <item>
        /// <term>GENERIC_WRITE</term><description>指定对设备进行写访问。</description>
        /// </item>
        /// <item><term><b>0</b></term><description>如果值为零，则表示只允许获取与一个设备有关的信息。</description></item>
        /// </list></p>
        /// </para>
        /// </param>
        /// <param name="dwShareMode">指定打开设备时的文件共享模式</param>
        /// <param name="lpSecurityAttributes"></param>
        /// <param name="dwCreationDisposition">Win32 常量，指定操作系统打开文件的方式。内容如下表：
        /// <para><p>
        /// <list type="table">
        /// <listheader><term>名称</term><description>说明</description></listheader>
        /// <item>
        /// <term>CREATE_NEW</term>
        /// <description>指定操作系统应创建新文件。如果文件存在，则抛出 <see cref="IOException"/> 异常。</description>
        /// </item>
        /// <item><term>CREATE_ALWAYS</term><description>指定操作系统应创建新文件。如果文件已存在，它将被改写。</description></item>
        /// </list>
        /// </p></para>
        /// </param>
        /// <param name="dwFlagsAndAttributes"></param>
        /// <param name="hTemplateFile"></param>
        /// <returns>使用函数打开的设备的句柄。</returns>
        /// <remarks>
        /// 本函数可以执行打开或建立文件、文件流、目录/文件夹、物理磁盘、卷、系统控制的缓冲区、磁带设备、
        /// 通信资源、邮件系统和命名管道。
        /// </remarks>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
                                                IntPtr lpSecurityAttributes, uint dwCreationDisposition,
                                                uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        /// <summary>
        /// 对设备执行指定的操作。
        /// </summary>
        /// <param name="hDevice">要执行操作的设备句柄。</param>
        /// <param name="dwIoControlCode">Win32 API 常数，输入的是以 <b>FSCTL_</b> 为前缀的常数，定义在 
        /// <b>WinIoCtl.h</b> 文件内，执行此重载方法必须输入 <b>SMART_GET_VERSION</b> 。</param>
        /// <param name="lpInBuffer">当参数为指针时，默认的输入值是 <b>0</b> 。</param>
        /// <param name="nInBufferSize">输入缓冲区的字节数量。</param>
        /// <param name="lpOutBuffer">一个 <b>GetVersionOutParams</b> ，表示执行函数后输出的设备检查。</param>
        /// <param name="nOutBufferSize">输出缓冲区的字节数量。</param>
        /// <param name="lpBytesReturned">实际装载到输出缓冲区的字节数量。</param>
        /// <param name="lpOverlapped">同步操作控制，一般不使用，默认值为 <b>0</b> 。</param>
        /// <returns>非零表示成功，零表示失败。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, IntPtr lpInBuffer,
                                                  uint nInBufferSize, ref GetVersionOutParams lpOutBuffer,
                                                  uint nOutBufferSize, ref uint lpBytesReturned,
                                                  [Out] IntPtr lpOverlapped);

        /// <summary>
        /// 对设备执行指定的操作。
        /// </summary>
        /// <param name="hDevice">要执行操作的设备句柄。</param>
        /// <param name="dwIoControlCode">Win32 API 常数，输入的是以 <b>FSCTL_</b> 为前缀的常数，定义在 
        /// <b>WinIoCtl.h</b> 文件内，执行此重载方法必须输入 <b>SMART_SEND_DRIVE_COMMAND</b> 或 <b>SMART_RCV_DRIVE_DATA</b> 。</param>
        /// <param name="lpInBuffer">一个 <b>SendCmdInParams</b> 结构，它保存向系统发送的查询要求具体命令的数据结构。</param>
        /// <param name="nInBufferSize">输入缓冲区的字节数量。</param>
        /// <param name="lpOutBuffer">一个 <b>SendCmdOutParams</b> 结构，它保存系统根据命令返回的设备相信信息二进制数据。</param>
        /// <param name="nOutBufferSize">输出缓冲区的字节数量。</param>
        /// <param name="lpBytesReturned">实际装载到输出缓冲区的字节数量。</param>
        /// <param name="lpOverlapped">同步操作控制，一般不使用，默认值为 <b>0</b> 。</param>
        /// <returns>非零表示成功，零表示失败。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int DeviceIoControl(IntPtr hDevice, uint dwIoControlCode, ref SendCmdInParams lpInBuffer,
                                                  uint nInBufferSize, ref SendCmdOutParams lpOutBuffer,
                                                  uint nOutBufferSize, ref uint lpBytesReturned,
                                                  [Out] IntPtr lpOverlapped);

        #endregion

        #region 结构

        /// <summary>
        /// 保存当前计算机 IDE 设备（硬盘）的硬件信息的结构。
        /// </summary>
        [Serializable]
        public struct HDiskInfo
        {
            /// <summary>
            /// 硬盘型号。
            /// </summary>
            public string ModuleNumber;

            /// <summary>
            /// 硬盘的固件版本。
            /// </summary>
            public string Firmware;

            /// <summary>
            /// 硬盘序列号。
            /// </summary>
            public string SerialNumber;

            /// <summary>
            /// 硬盘容量，以M为单位。
            /// </summary>
            public uint Capacity;
            
            /// <summary>
            /// 设备缓存大小（以M为单位）。
            /// </summary>
            public int BufferSize;
        }

        /// <summary>
        /// 表示使用 <b>DeviceIoControl</b> 函数时保存返回的驱动器硬件信息的结构
        /// </summary>
        /// <remarks>>此数据结构定义在 <b>WinIoCtl.h</b> 文件名为 <b>_GETVERSIONINPARAMS</b> 结构中。</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct GetVersionOutParams
        {
            /// <summary>
            /// IDE设备的二进制硬件版本。
            /// </summary>
            public byte bVersion;

            /// <summary>
            /// IDE设备的二进制修订版本。
            /// </summary>
            public byte bRevision;

            /// <summary>
            /// 此值操作系统没有使用，使用此数据结构时被设置为 <b>0</b> 。
            /// </summary>
            public byte bReserved;

            /// <summary>
            /// IDE设备的二进制映射。
            /// </summary>
            public byte bIDEDeviceMap;

            /// <summary>
            /// IDE设备的二进制容量数据。
            /// </summary>
            public uint fCapabilities;

            /// <summary>
            /// 保留内容，不使用。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public uint[] dwReserved; // For future use.
        }

        /// <summary>
        /// 一个数据结构，表示使用 <b>DeviceIoControl</b> 函数时发送到操作系统中的命令数据结构 <b>SendCmdInParams</b> 的成员结构。
        /// 它表示要获取磁盘设备性能参数的具体定义规则。
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        /// <remarks>此数据结构定义在 <b>WinIoCtl.h</b> 文件名为 <b>_IDEREGS</b> 中。</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct IdeRegs
        {
            /// <summary>
            /// 发送到操作系统的注册命令，此为系统的 <b>SMART Command</b> 。
            /// </summary>
            public byte bFeaturesReg;

            /// <summary>
            /// 获取IDE设备扇区数。
            /// </summary>
            public byte bSectorCountReg;

            /// <summary>
            /// 获取IDE设备编号。
            /// </summary>
            public byte bSectorNumberReg;

            /// <summary>
            /// 获取IDE设备低端柱面值。
            /// </summary>
            public byte bCylLowReg;

            /// <summary>
            /// 获取IDE设备高端柱面值。
            /// </summary>
            public byte bCylHighReg;

            /// <summary>
            /// 获取IDE设备的头信息。
            /// </summary>
            public byte bDriveHeadReg;

            /// <summary>
            /// 获取IDE设备的真正命令。
            /// </summary>
            public byte bCommandReg;

            /// <summary>
            /// 保留内容，此值应设置为 <b>0</b> 。
            /// </summary>
            public byte bReserved;
        }

        /// <summary>
        /// 保存执行 <b>DeviceIoControl</b> 函数时向系统提交的执行操作命令。
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        /// <remarks>此数据结构定义在 <b>WinIoCtl.h</b> 文件名为 <b>_SENDCMDINPARAMS</b> 中。</remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct SendCmdInParams
        {
            /// <summary>
            /// 输出的数据缓冲大小。
            /// </summary>
            public uint cBufferSize;

            /// <summary>
            /// 保存向系统发送的磁盘设备命令的数据结构。
            /// </summary>
            public IdeRegs irDriveRegs;

            /// <summary>
            /// 希望系统控制的物理磁盘的编号。
            /// </summary>
            public byte bDriveNumber;

            /// <summary>
            /// 保留的数据，不使用。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] bReserved;

            /// <summary>
            /// 保留的数据，不使用。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public uint[] dwReserved;

            /// <summary>
            /// 保存当前 <b>SendCmdInParams</b> 结构填充数据后的大小。
            /// </summary>
            public byte bBuffer;
        }

        /// <summary>
        /// 当执行 <b>DeviceIoControl</b> 函数后系统返回的 <b>SendCmdOutParams</b> 结构中
        /// 保存磁盘设备当前错误信息的数据结构。
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        /// <remarks>
        /// 此数据结构定义在 <b>WinIoCtl.h</b> 文件名为 <b>_DRIVERSTATUS</b> 中。
        /// <para>
        /// 错误代码如下表：<br />
        /// <list type="table">
        /// <listheader>
        /// <term>名称</term>
        /// <description>说明</description>
        /// <item><term>SMART_NO_ERROR = 0</term>
        /// <description>没有错误。</description></item>
        /// <item><term>SMART_IDE_ERROR = 1</term>
        /// <description>IDE控制器错误</description>。</item>
        /// <item><term>SMART_INVALID_FLAG = 2</term>
        /// <description>发送的命令标记无效。</description></item>
        /// <item><term>SMART_INVALID_COMMAND = 3</term>
        /// <description>发送的二进制命令无效。</description></item>
        /// <item><term>SMART_INVALID_BUFFER = 4</term>
        /// <description>二进制缓存无效（缓存为空或者无效地址）。</description></item>
        /// <item><term>SMART_INVALID_DRIVE = 5</term>
        /// <description>物理驱动器编号无效。</description></item>
        /// <item><term>SMART_INVALID_IOCTL = 6</term>
        /// <description>无效的IOCTL。</description></item>
        /// <item><term>SMART_ERROR_NO_MEM =  7</term>
        /// <description>使用的缓冲区无法锁定。</description></item>
        /// <item><term>SMART_INVALID_REGISTER = 8</term>
        /// <description>IDE注册命令无效。</description></item>
        /// <item><term>SMART_NOT_SUPPORTED = 9</term>
        /// <description>命令标记设置无效。</description></item>
        /// <item><term>SMART_NO_IDE_DEVICE = 10</term>
        /// <description>发送的物理驱动器索引超过限制。</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct DriverStatus
        {
            /// <summary>
            /// 如果检查的IDE设备发生错误，保存的错误代码，<b>0</b> 表示没有错误。
            /// </summary>
            public byte bDriverError;

            /// <summary>
            /// IDE设备被注册的错误内容。
            /// </summary>
            public byte bIDEStatus;

            /// <summary>
            /// 保留的数据，不使用。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] bReserved;

            /// <summary>
            /// 保留的数据，不使用。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] dwReserved;
        }

        /// <summary>
        /// 表示当执行 <b>DeviceIoControl</b> 函数后保存系统根据查询命令返回的磁盘设备信息的数据结构。
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct SendCmdOutParams
        {
            /// <summary>
            /// 表示所有二进制信息的缓存大小。
            /// </summary>
            public uint cBufferSize;

            /// <summary>
            /// 表示查询到设备的错误信息状态。
            /// </summary>
            public DriverStatus DriverStatus;

            /// <summary>
            /// 表示系统返回的设备硬件信息的二进制数据结构。
            /// </summary>
            public IdSector bBuffer;
        }

        /// <summary>
        /// 当执行 <b>DeviceIoControl</b> 函数后系统返回的 <b>SendCmdOutParams</b> 结构中
        /// 保存磁盘设备的硬件信息的数据结构。
        /// </summary>
        /// <seealso cref="SendCmdInParams"/>
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 512)]
        private struct IdSector
        {
            /// <summary>
            /// 设备通用配置信息。
            /// </summary>
            public ushort wGenConfig;

            /// <summary>
            /// 设备的柱面数。
            /// </summary>
            public ushort wNumCyls;

            /// <summary>
            /// 保留内容，不使用。
            /// </summary>
            public ushort wReserved;

            /// <summary>
            /// 设备的磁头数目。
            /// </summary>
            public ushort wNumHeads;

            /// <summary>
            /// 设备的磁道数目。
            /// </summary>
            public ushort wBytesPerTrack;

            /// <summary>
            /// 设备的扇区数目。
            /// </summary>
            public ushort wBytesPerSector;

            /// <summary>
            /// 设备厂商设定的扇区磁道数目。
            /// </summary>
            public ushort wSectorsPerTrack;

            /// <summary>
            /// 设备的出品厂商名称。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public ushort[] wVendorUnique;

            /// <summary>
            /// 设备出品厂商的全球唯一编码。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] sSerialNumber;

            /// <summary>
            /// 设备的缓存类型。
            /// </summary>
            public ushort wBufferType;

            /// <summary>
            /// 设备缓存容量（单位是byte）。
            /// </summary>
            public ushort wBufferSize;

            /// <summary>
            /// 设备的错误检查和纠正（ECC）数据的大小。
            /// </summary>
            public ushort wECCSize;

            /// <summary>
            /// 设备的固件版本。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sFirmwareRev;

            /// <summary>
            /// 设备的型号。
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public byte[] sModelNumber;

            /// <summary>
            /// 设备厂商名称的扩展内容（如果有）。
            /// </summary>
            public ushort wMoreVendorUnique;

            /// <summary>
            /// 设备双指令输入输出模式。
            /// </summary>
            public ushort wDoubleWordIO;

            /// <summary>
            /// 设备的容量大小（单位Byte）。
            /// </summary>
            public ushort wCapabilities;

            /// <summary>
            /// 第一个保留的内容，不使用。
            /// </summary>
            public ushort wReserved1;

            /// <summary>
            /// 设备的PIO模式巡道时间。
            /// </summary>
            public ushort wPIOTiming;

            /// <summary>
            /// 设备DMA 模式巡道时间。
            /// </summary>
            public ushort wDMATiming;

            /// <summary>
            /// 设备的总线类型，如SCSI,IDE等。
            /// </summary>
            public ushort wBS;

            /// <summary>
            /// 设备的当前柱面数量。
            /// </summary>
            public ushort wNumCurrentCyls;

            /// <summary>
            /// 设备当前磁头数量。
            /// </summary>
            public ushort wNumCurrentHeads;

            /// <summary>
            /// 设备的当前扇区的磁道数量。
            /// </summary>
            public ushort wNumCurrentSectorsPerTrack;

            /// <summary>
            /// 设备的当前扇区容量（单位byte）。
            /// </summary>
            public uint ulCurrentSectorCapacity;

            /// <summary>
            /// 多扇区读写模式支持。
            /// </summary>
            public ushort wMultSectorStuff;

            /// <summary>
            /// 用户是否可自定义扇区地址(LBA模式）支持。
            /// </summary>
            public uint ulTotalAddressableSectors;

            /// <summary>
            /// 单指令DMA模式。
            /// </summary>
            public ushort wSingleWordDMA;

            /// <summary>
            /// 多指令DMA模式。
            /// </summary>
            public ushort wMultiWordDMA;

            /// <summary>
            /// 保留内容，不使用。
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

        #region 常量

        /// <summary>
        /// Win32 API 常数，指示在使用 <see cref="RemoveMenu"/> 函数时指定使用索引数而不是使用ID。
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