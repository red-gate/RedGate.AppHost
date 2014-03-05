using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Remoting.WPF
{
    public static class NativeHandleContractWithoutIntPtrExtensions
    {
        public static FrameworkElement ToFrameworkElement(this INativeHandleContractWithoutIntPtr remotedElement)
        {
            INativeHandleContract nativeHandleContractAdapter = new NativeHandleContractAdapter(remotedElement);

            return FrameworkElementAdapters.ContractToViewAdapter(nativeHandleContractAdapter);
        }
    }
}