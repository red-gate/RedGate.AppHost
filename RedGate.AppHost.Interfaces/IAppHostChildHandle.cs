using System.AddIn.Contract;

namespace RedGate.AppHost.Interfaces
{
    public interface IAppHostChildHandle
    {
        INativeHandleContract Initialize(IAppHostServices services);
    }
}