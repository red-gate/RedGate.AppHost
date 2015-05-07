using System;
using System.Diagnostics;

namespace RedGate.AppHost.Client
{
    internal class HostProcessMonitor
    {
        private readonly int m_HostProcessId;
        private readonly Action m_OnHostMissing;

        public HostProcessMonitor(int hostProcessId, Action onHostMissing)
        {
            if (onHostMissing == null)
            {
                throw new ArgumentNullException("onHostMissing");
            }

            m_HostProcessId = hostProcessId;
            m_OnHostMissing = onHostMissing;
        }

        public void Start()
        {
            var hostProcess = Process.GetProcessById(m_HostProcessId);
            hostProcess.EnableRaisingEvents = true;
            hostProcess.Exited += (sender, e) => { m_OnHostMissing(); };
        }
    }
}
