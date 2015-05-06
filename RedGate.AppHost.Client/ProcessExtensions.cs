using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RedGate.AppHost.Client
{
    internal static class ProcessExtensions
    {
        public static int GetParentProcessId(this Process childProcess)
        {
            int sizeInfoReturned;
            var pbi = new ProcessInfo();
            NtQueryInformationProcess(childProcess.Handle, 0, ref pbi, pbi.Size, out sizeInfoReturned);
            return (int) pbi.InheritedFromUniqueProcessId;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ProcessInfo
        {
            public IntPtr ExitStatus;
            public IntPtr PebBaseAddress;
            public IntPtr AffinityMask;
            public IntPtr BasePriority;
            public UIntPtr UniqueProcessId;
            public IntPtr InheritedFromUniqueProcessId;

            public int Size
            {
                get { return Marshal.SizeOf(typeof(ProcessInfo)); }
            }
        }

        [DllImport("NTDLL.DLL", SetLastError = true)]
        private static extern int NtQueryInformationProcess(IntPtr hProcess, int pic, ref ProcessInfo pbi, int cb, out int pSize);
    }
}
