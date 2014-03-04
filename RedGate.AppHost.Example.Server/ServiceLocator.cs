using System;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Example.Server
{
    public class ServiceLocator : MarshalByRefObject, IAppHostServices
    {
        public T GetService<T>() where T : class
        {
            return new ServerImplementedThingThatClientNeeds() as T;
        }
    }
}