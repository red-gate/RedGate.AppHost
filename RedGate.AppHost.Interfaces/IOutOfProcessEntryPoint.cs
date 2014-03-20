using System.Windows;

namespace RedGate.AppHost.Interfaces
{
    /// <summary>
    /// All assemblies that are intended to be run out of process need a single type that implements this interface. It will be 
    /// loaded via reflection and <see cref="CreateElement"/> will be called.
    /// </summary>
    public interface IOutOfProcessEntryPoint
    {
        FrameworkElement CreateElement(IAppHostServices service);
    }
}
