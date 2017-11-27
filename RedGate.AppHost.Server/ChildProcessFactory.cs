namespace RedGate.AppHost.Server
{
    public class ChildProcessFactory
    {
        public string ClientExecutablePath { get; set; }

        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole, bool is64Bit, bool monitorHostProcess) {
            IProcessStartOperation processStarter;

            if (is64Bit) {
                processStarter = new ProcessStarter64Bit() {
                    ClientExecutablePath = ClientExecutablePath
                };
            }
            else {
                processStarter = new ProcessStarter32Bit() {
                    ClientExecutablePath = ClientExecutablePath
                };
            }

            return new RemotedProcessBootstrapper(
                new StartProcessWithTimeout(
                    new StartProcessWithJobSupport(
                        processStarter))).Create(assemblyName, openDebugConsole, monitorHostProcess);
        }

        // the methods below are to support legacy versions of the API to the Create() method

        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole, bool is64Bit)
        {
            return Create(assemblyName, openDebugConsole, is64Bit, false);
        }

        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole)
        {
            return Create(assemblyName, openDebugConsole, false);
        }

        public IChildProcessHandle Create(string assemblyName)
        {
            return Create(assemblyName, false, false);
        }
    }
}
