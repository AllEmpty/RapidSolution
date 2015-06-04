using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DotNet.Utilities
{
    class API
    {
        [DllImport("kernel32")]//内存
        public static extern void GlobalMemoryStatus(ref  MEMORY_INFO meminfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct CPU_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        //定义内存的信息结构  
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }  
        [DllImport("kernel32",EntryPoint="GetComputerName",ExactSpelling=false,SetLastError=true)]//计算机名称
         public static extern bool GetComputerName([MarshalAs(UnmanagedType.LPArray)]byte[] IpBuffer,[MarshalAs(UnmanagedType.LPArray)]Int32[] nSize);
        [DllImport("advapi32", EntryPoint = "GetUserName", ExactSpelling = false, SetLastError = true)]//计算机用户名
         public static extern bool GetUserName([MarshalAs(UnmanagedType.LPArray)]byte[] IpBuffer, [MarshalAs(UnmanagedType.LPArray)]Int32[] nSize);
        [DllImport("Iphlpapi.dll")]
        public static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        public static extern Int32 inet_addr(string ip);



    }
}
