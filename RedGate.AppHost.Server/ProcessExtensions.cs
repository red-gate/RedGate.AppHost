using System.Diagnostics;

namespace RedGate.AppHost.Server
{
    internal static class ProcessExtensions
    {
        internal static bool CanAssignToJobObject(this Process process)
        {
            return Job.CanAssignProcessToJobObject(process);
        }

        internal static void KillAndDispose(this Process process)
        {
            if (!process.HasExited)
            {
                process.Kill();
            }
            process.Dispose();
        }
    }
}