using System;

namespace RedGate.AppHost.Interfaces
{
    public interface IAppHostServices
    {
        T GetService<T>() where T : MarshalByRefObject;
    }
}
