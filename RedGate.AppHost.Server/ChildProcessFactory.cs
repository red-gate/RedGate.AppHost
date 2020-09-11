namespace RedGate.AppHost.Server
{
    public class ChildProcessFactory
    {
        public string ClientExecutablePath { get; set; }

        public IChildProcessHandle Create(string assemblyName, bool openDebugConsole = false, bool is64Bit = false, bool monitorHostProcess = false) {
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
        
    }
}
