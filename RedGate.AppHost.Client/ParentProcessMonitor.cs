using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Threading;

namespace RedGate.AppHost.Client
{
    internal class ParentProcessMonitor
    {
        private readonly Action m_OnParentMissing;

        public ParentProcessMonitor(Action onParentMissing)
        {
            if (onParentMissing == null)
            {
                throw new ArgumentNullException("onParentMissing");
            }

            m_OnParentMissing = onParentMissing;
        }

        public void Start()
        {
            var currentProcess = Process.GetCurrentProcess();
            var parentProcessId = currentProcess.GetParentProcessId();
            var parentProcess = Process.GetProcessById(parentProcessId);

            parentProcess.EnableRaisingEvents = true;
            parentProcess.Exited += (sender, e) => { m_OnParentMissing(); };
        }
    }
}
