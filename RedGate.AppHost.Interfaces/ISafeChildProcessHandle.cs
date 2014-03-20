namespace RedGate.AppHost.Interfaces
{
    /// <summary>
    /// Registered by the client to provide a remotable handle to the type returned by the <see cref="IOutOfProcessEntryPoint" />
    /// </summary>
    public interface ISafeChildProcessHandle
    {
        IRemoteElement CreateElement(IAppHostServices services);
    }
}