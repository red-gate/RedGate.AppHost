using System;
using System.Diagnostics;
using System.Threading;

namespace RedGate.AppHost.Client
{
    internal class ParentProcessMonitor
    {
        private readonly Action m_OnParentMissing;
        private readonly int m_PollingIntervalInSeconds;
        private Thread m_PollingThread;

        public ParentProcessMonitor(Action onParentMissing, int pollingIntervalInSeconds = 10)
        {
            if (onParentMissing == null)
            {
                throw new ArgumentNullException("onParentMissing");
            }

            m_OnParentMissing = onParentMissing;
            m_PollingIntervalInSeconds = pollingIntervalInSeconds;
        }

        public void Start()
        {
            m_PollingThread = new Thread(PollForParentProcess);
            m_PollingThread.Start();
        }

        private void PollForParentProcess()
        {
            var currentProcess = Process.GetCurrentProcess();
            var parentProcessId = currentProcess.GetParentProcessId();

            try
            {
                while (true)
                {
                    Process.GetProcessById(parentProcessId);
                    Thread.Sleep(m_PollingIntervalInSeconds * 1000);
                }
            }
            catch
            {
                m_OnParentMissing();
            }
        }
    }
}
