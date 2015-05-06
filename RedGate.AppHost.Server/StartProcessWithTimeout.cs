using System;
using System.Diagnostics;
using System.Threading;

namespace RedGate.AppHost.Server
{
    internal class StartProcessWithTimeout : IProcessStartOperation
    {
        private readonly IProcessStartOperation m_WrappedProcessStarter;

        private static readonly TimeSpan s_TimeOut = TimeSpan.FromMinutes(2);

        public StartProcessWithTimeout(IProcessStartOperation wrappedProcessStarter)
        {
            if (wrappedProcessStarter == null)
                throw new ArgumentNullException("wrappedProcessStarter");

            m_WrappedProcessStarter = wrappedProcessStarter;
        }

        public Process StartProcess(string assemblyName, string remotingId, bool openDebugConsole, bool monitorParentProcess)
        {
            using (var signal = new EventWaitHandle(false, EventResetMode.ManualReset, remotingId))
            {
                var process = m_WrappedProcessStarter.StartProcess(assemblyName, remotingId, openDebugConsole, monitorParentProcess);
                WaitForReadySignal(signal);
                return process;
            }
        }

        private static void WaitForReadySignal(EventWaitHandle signal)
        {
            if (!signal.WaitOne(s_TimeOut))
                throw new ApplicationException("WPF child process didn't respond quickly enough");
        }
    }
}