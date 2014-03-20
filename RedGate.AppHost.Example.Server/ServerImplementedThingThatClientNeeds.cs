using System;
using System.Runtime.Remoting.Lifetime;
using RedGate.AppHost.Example.Remote.Services;

namespace RedGate.AppHost.Example.Server
{
    public class ServerImplementedThingThatClientNeeds : MarshalByRefObject, IServerImplementedThingThatClientNeeds, ISponsor
    {
        public string GetTextToDisplay()
        {
            return "This is a string that the server needs displayed";
        }

        public TimeSpan Renewal(ILease lease)
        {
            return TimeSpan.FromMinutes(1);
        }

        public override object InitializeLifetimeService()
        {
            ILease ret = (ILease)base.InitializeLifetimeService();
            ret.SponsorshipTimeout = TimeSpan.FromMinutes(2);
            ret.Register(this);
            return ret;
        }
    }
}
