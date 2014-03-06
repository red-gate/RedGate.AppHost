// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo
using System;
using System.Runtime.InteropServices;

namespace RedGate.AppHost.Server
{
    internal static class Native
    {
        internal static void ThrowOnFailure(Func<bool> action)
        {
            bool ret = action();
            if (!ret)
            {
                int error = Marshal.GetLastWin32Error();
                throw new ApplicationException(string.Format("ERROR: {0} failed with error code {1}", action.Method, error));
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool IsProcessInJob(
            [In] IntPtr ProcessHandle,
            [In] IntPtr JobHandle,
            [Out] out bool Result);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateJobObject(
            [In] IntPtr lpJobAttributes,
            [In] string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetInformationJobObject(
            [In] IntPtr hJob,
            [In] JobObject JobObjectInfoClass,
            [In] ref JOBOBJECT_EXTENDED_LIMIT_INFORMATION lpJobObjectInfo,
            [In] uint cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AssignProcessToJobObject(
            [In] IntPtr hJob,
            [In] IntPtr hProcess);


        internal enum JobObject
        {
            JobObjectBasicLimitInformation = 2,
            JobObjectBasicUIRestrictions = 4,
            JobObjectSecurityLimitInformation,
            JobObjectEndOfJobTimeInformation,
            JobObjectAssociateCompletionPortInformation,
            JobObjectExtendedLimitInformation = 9,
            JobObjectGroupInformation = 11,
            JobObjectNotificationLimitInformation,
            JobObjectGroupInformationEx = 14,
            JobObjectCpuRateControlInformation
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public IntPtr ProcessMemoryLimit;
            public IntPtr JobMemoryLimit;
            public IntPtr PeakProcessMemoryUsed;
            public IntPtr PeakJobMemoryUsed;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct IO_COUNTERS
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public long PerProcessUserTimeLimit;
            public long PerJobUserTimeLimit;
            public JOB_OBJECT_LIMIT LimitFlags;
            public IntPtr MinimumWorkingSetSize;
            public IntPtr MaximumWorkingSetSize;
            public uint ActiveProcessLimit;
            public IntPtr Affinity;
            public uint PriorityClass;
            public uint SchedulingClass;
        }

        [Flags]
        internal enum JOB_OBJECT_LIMIT : uint
        {

            JOB_OBJECT_LIMIT_WORKINGSET = 0x00000001,
            JOB_OBJECT_LIMIT_PROCESS_TIME = 0x00000002,
            JOB_OBJECT_LIMIT_JOB_TIME = 0x00000004,
            JOB_OBJECT_LIMIT_ACTIVE_PROCESS = 0x00000008,

            JOB_OBJECT_LIMIT_AFFINITY = 0x00000010,
            JOB_OBJECT_LIMIT_PRIORITY_CLASS = 0x00000020,
            JOB_OBJECT_LIMIT_PRESERVE_JOB_TIME = 0x00000040,
            JOB_OBJECT_LIMIT_SCHEDULING_CLASS = 0x00000080,
            JOB_OBJECT_LIMIT_PROCESS_MEMORY = 0x00000100,
            JOB_OBJECT_LIMIT_JOB_MEMORY = 0x00000200,
            JOB_OBJECT_LIMIT_DIE_ON_UNHANDLED_EXCEPTION = 0x00000400,
            JOB_OBJECT_LIMIT_BREAKAWAY_OK = 0x00000800,
            JOB_OBJECT_LIMIT_SILENT_BREAKAWAY_OK = 0x00001000,
            JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x00002000,
            JOB_OBJECT_LIMIT_SUBSET_AFFINITY = 0x00004000
        }
    }
}
// ReSharper restore InconsistentNaming
// ReSharper restore IdentifierTypo
// ReSharper restore StringLiteralTypo