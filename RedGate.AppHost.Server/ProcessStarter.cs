using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RedGate.AppHost.Server
{
    internal abstract class ProcessStarter : IProcessStartOperation
    {
        protected abstract string ProcessFileName { get; }

        public Process StartProcess(string assemblyName, string remotingId, bool openDebugConsole, bool monitorHostProcess)
        {
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string quotedAssemblyArg = "\"" + Path.Combine(executingDirectory, assemblyName) + "\"";

            var processToStart = Path.Combine(executingDirectory, ProcessFileName);
            var processArguments = string.Join(" ", new[]
            {
                "-i " + remotingId,
                "-a " + quotedAssemblyArg,
                openDebugConsole ? "-d" : string.Empty,
                monitorHostProcess ? "-p " + Process.GetCurrentProcess().Id : string.Empty
            });
            return Process.Start(processToStart, processArguments);
        }
    }
}
