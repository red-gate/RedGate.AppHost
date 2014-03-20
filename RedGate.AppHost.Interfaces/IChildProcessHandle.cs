using System.Windows;

namespace RedGate.AppHost.Interfaces
{
    /// <summary>
    /// A handle to a process, which when initialized, returns a FrameworkElement for rendering
    /// </summary>
    public interface IChildProcessHandle
    {
        FrameworkElement Initialize(IAppHostServices services);
    }
}