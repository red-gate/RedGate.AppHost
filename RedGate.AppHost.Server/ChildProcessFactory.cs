namespace RedGate.AppHost.Server
{
    public class ChildProcessFactory
    {
        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole, bool is64Bit = false, bool monitorParentProcess = false)
        {
            IProcessStartOperation processStarter;

            if (is64Bit)
            {
                processStarter = new ProcessStarter64Bit();
            }
            else
            {
                processStarter = new ProcessStarter32Bit();
            }

            return new RemotedProcessBootstrapper(
                new StartProcessWithTimeout(
                    new StartProcessWithJobSupport(
                        processStarter))).Create(assemblyName, openDebugConsole, monitorParentProcess);
        }

        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole)
        {
            // Legacy version of the api
            return Create(assemblyName, openDebugConsole, false);
        }

        public IChildProcessHandle Create(string assemblyName)
        {
            return Create(assemblyName, false, false);
        }
    }
}
