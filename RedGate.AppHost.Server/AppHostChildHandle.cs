using System;
using System.AddIn.Contract;
using System.Windows;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Remoting.WPF;

namespace RedGate.AppHost.Server
{
    internal class AppHostChildHandle : IAppHostChildHandle
    {
        private readonly ISafeAppHostChildHandle m_SafeAppHostChildHandle;

        public AppHostChildHandle(ISafeAppHostChildHandle safeAppHostChildHandle)
        {
            m_SafeAppHostChildHandle = safeAppHostChildHandle;
        }

        public FrameworkElement Initialize(IAppHostServices services)
        {
            object handleContractWithoutIntPtr = m_SafeAppHostChildHandle.Initialize(services);
            
            INativeHandleContractWithoutIntPtr nativeHandleContractWithoutIntPtr = (INativeHandleContractWithoutIntPtr) handleContractWithoutIntPtr;

            return nativeHandleContractWithoutIntPtr.ToFrameworkElement();
        }
    }
}