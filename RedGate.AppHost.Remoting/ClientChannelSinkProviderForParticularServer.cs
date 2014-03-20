using System.Runtime.Remoting.Channels;

namespace RedGate.AppHost.Remoting
{
    internal class ClientChannelSinkProviderForParticularServer : IClientChannelSinkProvider
    {
        private readonly IClientChannelSinkProvider m_Upstream;
        private readonly string m_Url;

        internal ClientChannelSinkProviderForParticularServer(IClientChannelSinkProvider upstream, string id)
        {
            if (upstream == null) 
                throw new ArgumentNullException("upstream");

            if (String.IsNullOrEmpty(id)) 
                throw new ArgumentNullException("id");

            m_Upstream = upstream;
            m_Url = string.Format("ipc://{0}", id);
        }

        public IClientChannelSinkProvider Next
        {
            get { return m_Upstream.Next; }
            set { m_Upstream.Next = value; }
        }

        public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
        {
            return url == m_Url ? m_Upstream.CreateSink(channel, url, remoteChannelData) : null;
        }
    }
}