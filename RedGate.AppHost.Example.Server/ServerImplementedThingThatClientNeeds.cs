using System;
using RedGate.AppHost.Example.Client;

namespace RedGate.AppHost.Example.Server
{
    public class ServerImplementedThingThatClientNeeds : MarshalByRefObject, IServerImplementedThingThatClientNeeds
    {
        public string GetTextToDisplay()
        {
            return "This is a string that the server needs displayed";
        }
    }
}
