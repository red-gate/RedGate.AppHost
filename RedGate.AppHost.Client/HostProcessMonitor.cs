using System;
using System.Diagnostics;

namespace RedGate.AppHost.Client
{
    internal class HostProcessMonitor
    {
        private readonly Action m_OnHostMissing;

        public HostProcessMonitor(Action onHostMissing)
        {
            if (onHostMissing == null)
            {
                throw new ArgumentNullException("onHostMissing");
            }

            m_OnHostMissing = onHostMissing;
        }

        public void Start()
        {
            var currentProcess = Process.GetCurrentProcess();
            var hostProcessId = currentProcess.GetParentProcessId();
            var hostProcess = Process.GetProcessById(hostProcessId);

            hostProcess.EnableRaisingEvents = true;
            hostProcess.Exited += (sender, e) => { m_OnHostMissing(); };
        }
    }
}
