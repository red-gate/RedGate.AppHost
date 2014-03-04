using System.Windows;

namespace RedGate.AppHost.Interfaces
{
    public interface IOutOfProcessEntryPoint
    {
        FrameworkElement CreateElement(IAppHostServices service);
    }
}
