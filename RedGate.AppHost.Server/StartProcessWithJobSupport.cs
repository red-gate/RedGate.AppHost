using System;
using System.Diagnostics;

namespace RedGate.AppHost.Server
{
    internal class StartProcessWithJobSupport : IProcessStartOperation
    {
        private readonly IProcessStartOperation m_WrappedProcessStarter;

        public StartProcessWithJobSupport(IProcessStartOperation wrappedProcessStarter)
        {
            if (wrappedProcessStarter == null)
                throw new ArgumentNullException("wrappedProcessStarter");

            m_WrappedProcessStarter = wrappedProcessStarter;
        }

        public Process StartProcess(string assemblyName, string remotingId, bool openDebugConsole, bool monitorParentProcess)
        {
            var process = m_WrappedProcessStarter.StartProcess(assemblyName, remotingId, openDebugConsole, monitorParentProcess);

            if (Job.CanAssignProcessToJobObject(process))
                new Job().AssignProcessToJobObject(process);

            return process;
        }
    }
}