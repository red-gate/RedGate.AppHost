using System.Windows;

namespace RedGate.AppHost.Interfaces
{
    public interface IAppHostChildHandle
    {
        FrameworkElement Initialize(IAppHostServices services);
    }
}