namespace RedGate.AppHost.Interfaces
{
    public interface ISafeChildProcessHandle
    {
        IRemoteElement Initialize(IAppHostServices services);
    }
}