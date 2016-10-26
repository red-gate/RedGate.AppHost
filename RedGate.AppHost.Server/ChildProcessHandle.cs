using System;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Windows;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Remoting.WPF;

namespace RedGate.AppHost.Server
{
    internal class ChildProcessHandle : IChildProcessHandle
    {
        private readonly ISafeChildProcessHandle m_SafeChildProcessHandle;
        public Process Process { get; }

        public ChildProcessHandle(ISafeChildProcessHandle safeChildProcessHandle, Process process)
        {
            m_SafeChildProcessHandle = safeChildProcessHandle;
            Process = process;
        }

        public FrameworkElement CreateElement(IAppHostServices services)
        {
            try
            {
                return m_SafeChildProcessHandle.CreateElement(services).ToFrameworkElement();
            }
            catch (RemotingException)
            {
                Process?.KillAndDispose();
                throw;
            }
        }
    }
}