using System.Windows;

namespace RedGate.AppHost.Interfaces
{
    public interface IChildProcessHandle
    {
        FrameworkElement Initialize(IAppHostServices services);
    }
}