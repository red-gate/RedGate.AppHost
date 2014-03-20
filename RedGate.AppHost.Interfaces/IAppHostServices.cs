namespace RedGate.AppHost.Interfaces
{
    /// <summary>
    /// Provides a way for the server to proffer services to the client
    /// </summary>
    public interface IAppHostServices
    {
        T GetService<T>() where T : class;
    }
}
