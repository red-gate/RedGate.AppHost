using System;
using System.Diagnostics;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Server
{
    internal class RemotedProcessBootstrapper
    {
        private readonly IProcessStartOperation m_ProcessBootstrapper;
        private readonly string m_RemotingId = string.Format("RedGate.AppHost.IPC.{{{0}}}", Guid.NewGuid());

        public RemotedProcessBootstrapper(IProcessStartOperation processBootstrapper)
        {
            if (processBootstrapper == null) 
                throw new ArgumentNullException("processBootstrapper");

            m_ProcessBootstrapper = processBootstrapper;
        }

        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole)
        {
            Process process = null;
            try
            {
                process = m_ProcessBootstrapper.StartProcess(assemblyName, m_RemotingId, openDebugConsole);
                return new ChildProcessHandle(InitializeRemoting());
            }
            catch
            {
                if (process != null)
                    process.KillAndDispose();

                throw;
            }
        }

        private ISafeChildProcessHandle InitializeRemoting()
        {
            Remoting.Remoting.RegisterChannels(false, m_RemotingId);

            return Remoting.Remoting.ConnectToService<ISafeChildProcessHandle>(m_RemotingId);
        }
    }
}