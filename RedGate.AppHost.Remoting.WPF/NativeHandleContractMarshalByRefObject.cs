using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Remoting.WPF
{
    internal class NativeHandleContractMarshalByRefObject : MarshalByRefObject, INativeHandleContractWithoutIntPtr
    {
        private readonly INativeHandleContract m_Upstream;

        internal NativeHandleContractMarshalByRefObject(FrameworkElement frameworkElement)
            : this(FrameworkElementAdapters.ViewToContractAdapter(frameworkElement))
        {
        }

        private NativeHandleContractMarshalByRefObject(INativeHandleContract upstream)
        {
            m_Upstream = upstream;
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public IContract QueryContract(string contractIdentifier)
        {
            return m_Upstream.QueryContract(contractIdentifier);
        }

        public int GetRemoteHashCode()
        {
            return m_Upstream.GetRemoteHashCode();
        }

        public bool RemoteEquals(IContract contract)
        {
            return m_Upstream.RemoteEquals(contract);
        }

        public string RemoteToString()
        {
            return m_Upstream.RemoteToString();
        }

        public int AcquireLifetimeToken()
        {
            return m_Upstream.AcquireLifetimeToken();
        }

        public void RevokeLifetimeToken(int token)
        {
            m_Upstream.RevokeLifetimeToken(token);
        }

        public long GetHandle()
        {
            return (long)m_Upstream.GetHandle();
        }
    }
}