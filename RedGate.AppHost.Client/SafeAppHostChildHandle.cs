using System;
using System.Windows;
using System.Windows.Threading;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Client
{
    internal class SafeAppHostChildHandle : MarshalByRefObject, ISafeAppHostChildHandle
    {
        private readonly Dispatcher m_UiThreadDispatcher;
        private readonly FrameworkElement m_Element;

        public SafeAppHostChildHandle(Dispatcher uiThreadDispatcher, FrameworkElement element)
        {
            m_UiThreadDispatcher = uiThreadDispatcher;
            m_Element = element;
        }

        public INativeHandleContractWithoutIntPtr Initialize(IAppHostServices services)
        {
            Func<NativeHandleContractMarshalByRefObject> controlMarshalFunc = () => new NativeHandleContractMarshalByRefObject(m_Element);

            return (INativeHandleContractWithoutIntPtr) m_UiThreadDispatcher.Invoke(controlMarshalFunc);
        }
    }
}