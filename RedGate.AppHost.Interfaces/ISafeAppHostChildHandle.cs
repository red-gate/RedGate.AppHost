namespace RedGate.AppHost.Interfaces
{
    public interface ISafeAppHostChildHandle
    {
        INativeHandleContractWithoutIntPtr Initialize(IAppHostServices services);
    }
}