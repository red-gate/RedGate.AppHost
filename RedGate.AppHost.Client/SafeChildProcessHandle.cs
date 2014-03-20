using System;
using System.Windows.Threading;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Remoting.WPF;

namespace RedGate.AppHost.Client
{
    internal class SafeChildProcessHandle : MarshalByRefObject, ISafeChildProcessHandle
    {
        private readonly Dispatcher m_UiThreadDispatcher;
        private readonly IOutOfProcessEntryPoint m_EntryPoint;

        public SafeChildProcessHandle(Dispatcher uiThreadDispatcher, IOutOfProcessEntryPoint entryPoint)
        {
            m_UiThreadDispatcher = uiThreadDispatcher;
            m_EntryPoint = entryPoint;
        }

        public IRemoteElement CreateElement(IAppHostServices services)
        {

            Func<IRemoteElement> controlMarshalFunc = () =>
                                                      {
                                                          var element = m_EntryPoint.CreateElement(services);

                                                          return element.ToRemotedElement();
                                                      };

            return (IRemoteElement) m_UiThreadDispatcher.Invoke(controlMarshalFunc);
        }
    }
}