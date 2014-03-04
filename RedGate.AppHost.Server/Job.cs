using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RedGate.AppHost.Server
{
    internal class Job
    {
        private static readonly Version s_Windows8 = new Version(6, 2, 0, 0);

        internal static bool CanAssignProcessToJobObject(Process process)
        {
            if (Environment.OSVersion.Version >= s_Windows8)
            {
                return true;
            }
            else
            {
                bool ret = false;
                Native.ThrowOnFailure(() => Native.IsProcessInJob(process.Handle, IntPtr.Zero, out ret));
                return !ret;
            }
        }


        private readonly IntPtr m_Job;


        internal Job()
        {
            m_Job = Native.CreateJobObject(IntPtr.Zero, null);
            Native.ThrowOnFailure(() => m_Job != IntPtr.Zero);

            Native.JOBOBJECT_EXTENDED_LIMIT_INFORMATION lpJobObjectInfo = new Native.JOBOBJECT_EXTENDED_LIMIT_INFORMATION { BasicLimitInformation = { LimitFlags = Native.JOB_OBJECT_LIMIT.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE } };
            Native.ThrowOnFailure(() => Native.SetInformationJobObject(m_Job, Native.JobObject.JobObjectExtendedLimitInformation, ref lpJobObjectInfo, (uint)Marshal.SizeOf(typeof(Native.JOBOBJECT_EXTENDED_LIMIT_INFORMATION))));
        }


        internal void AssignProcessToJobObject(Process process)
        {
            Native.ThrowOnFailure(() => Native.AssignProcessToJobObject(m_Job, process.Handle));
        }
    }
}