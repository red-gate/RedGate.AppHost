using System;
using System.AddIn.Contract;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Server
{
    internal class AppHostChildHandle : IAppHostChildHandle
    {
        private readonly ISafeAppHostChildHandle m_SafeAppHostChildHandle;

        public AppHostChildHandle(ISafeAppHostChildHandle safeAppHostChildHandle)
        {
            m_SafeAppHostChildHandle = safeAppHostChildHandle;
        }

        public INativeHandleContract Initialize(IAppHostServices services)
        {
            object handleContractWithoutIntPtr = m_SafeAppHostChildHandle.Initialize(services);
            
            INativeHandleContractWithoutIntPtr nativeHandleContractWithoutIntPtr = (INativeHandleContractWithoutIntPtr) handleContractWithoutIntPtr;

            return new NativeHandleContractAdapter(nativeHandleContractWithoutIntPtr);
        }

        private class NativeHandleContractAdapter : INativeHandleContract
        {
            private readonly INativeHandleContractWithoutIntPtr m_Upstream;

            internal NativeHandleContractAdapter(INativeHandleContractWithoutIntPtr upstream)
            {
                if (upstream == null)
                {
                    throw new ArgumentNullException("upstream");
                }
                m_Upstream = upstream;
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

            public IntPtr GetHandle()
            {
                return (IntPtr)m_Upstream.GetHandle();
            }
        }
    }
}