using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RedGate.AppHost.Server
{
    internal abstract class ProcessStarter : IProcessStartOperation
    {
        protected abstract string ProcessFileName { get; }

        public Process StartProcess(string assemblyName, string remotingId, bool openDebugConsole)
        {
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string quotedAssemblyArg = "\"" + Path.Combine(executingDirectory, assemblyName) + "\"";

            var processToStart = Path.Combine(executingDirectory, ProcessFileName);
            var processArguments = string.Join(" ", new[]
            {
                "-i " + remotingId,
                "-a " + quotedAssemblyArg,
                openDebugConsole ? "-d" : ""
            });
            return Process.Start(processToStart, processArguments);
        }
    }
}
