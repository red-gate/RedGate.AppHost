using System.Windows;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Remoting.WPF;

namespace RedGate.AppHost.Server
{
    internal class ChildProcessHandle : IChildProcessHandle
    {
        private readonly ISafeChildProcessHandle m_SafeChildProcessHandle;

        public ChildProcessHandle(ISafeChildProcessHandle safeChildProcessHandle)
        {
            m_SafeChildProcessHandle = safeChildProcessHandle;
        }

        public FrameworkElement Initialize(IAppHostServices services)
        {
            object handleContractWithoutIntPtr = m_SafeChildProcessHandle.Initialize(services);
            
            IRemoteElement remoteElement = (IRemoteElement) handleContractWithoutIntPtr;

            return remoteElement.ToFrameworkElement();
        }
    }
}