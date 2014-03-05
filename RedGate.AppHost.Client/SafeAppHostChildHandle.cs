using System;
using System.Windows;
using System.Windows.Threading;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Remoting.WPF;

namespace RedGate.AppHost.Client
{
    internal class SafeAppHostChildHandle : MarshalByRefObject, ISafeAppHostChildHandle
    {
        private readonly Dispatcher m_UiThreadDispatcher;
        private readonly IOutOfProcessEntryPoint m_EntryPoint;

        public SafeAppHostChildHandle(Dispatcher uiThreadDispatcher, IOutOfProcessEntryPoint entryPoint)
        {
            m_UiThreadDispatcher = uiThreadDispatcher;
            m_EntryPoint = entryPoint;
        }

        public INativeHandleContractWithoutIntPtr Initialize(IAppHostServices services)
        {

            Func<INativeHandleContractWithoutIntPtr> controlMarshalFunc = () =>
                                                                              {
                                                                                  var element = m_EntryPoint.CreateElement(services);

                                                                                  return element.ToRemotedElement();
                                                                              };

            return (INativeHandleContractWithoutIntPtr) m_UiThreadDispatcher.Invoke(controlMarshalFunc);
        }
    }
}