using System.Windows;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Remoting.WPF
{
    public static class FrameworkElementExtensions
    {
        public static IRemoteElement ToRemotedElement(this FrameworkElement element)
        {
            return new NativeHandleContractMarshalByRefObject(element);
        }
    }
}
