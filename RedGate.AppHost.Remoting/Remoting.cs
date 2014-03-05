using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Serialization.Formatters;

namespace RedGate.AppHost.Remoting
{
    public static class Remoting
    {
#if DEBUG  // to make bugs like RGD-473 more visible - Kevin: Make this do SponsoredMarshaling
        static Remoting()
        {
            //TimeSpan oneSecond = TimeSpan.FromSeconds(1);
            //LifetimeServices.LeaseManagerPollTime = oneSecond; // the default is 10 s
            //LifetimeServices.LeaseTime = oneSecond; // the default is 5 minutes
            //LifetimeServices.RenewOnCallTime = oneSecond; // the default is 2 minutes
            //LifetimeServices.SponsorshipTimeout = oneSecond; // the default is 2 minutes
        }
#endif

        public static void RegisterService<TService, TInterface>(TService service)
            where TService : MarshalByRefObject, TInterface
        {
            RemotingServices.Marshal(service, GetName<TInterface>());
        }

        public static T ConnectToService<T>(string hostname)
        {
            return (T)Activator.GetObject(typeof(T), string.Format("ipc://{0}/{1}", hostname, GetName<T>()));
        }

        private static string GetName<T>()
        {
            return typeof(T).FullName;
        }

        public static void RegisterChannels(bool childProcess, string id)
        {
            string callback = id + ".Callback";
            RegisterClientChannel(childProcess ? callback : id);
            RegisterServerChannel(childProcess ? id : callback);
        }

        private static void RegisterClientChannel(string id)
        {
            BinaryClientFormatterSinkProvider clientSinkProvider = new BinaryClientFormatterSinkProvider();
            ChannelServices.RegisterChannel(new IpcClientChannel(id, new ClientChannelSinkProviderForParticularServer(clientSinkProvider, id)), false);
        }

        private static void RegisterServerChannel(string id)
        {
            BinaryServerFormatterSinkProvider serverSinkProvider = new BinaryServerFormatterSinkProvider { TypeFilterLevel = TypeFilterLevel.Full };
            ChannelServices.RegisterChannel(new IpcServerChannel(id, id, serverSinkProvider), false);
        }
    }
}
