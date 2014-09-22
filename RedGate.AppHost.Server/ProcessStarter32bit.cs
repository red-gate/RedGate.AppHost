using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace RedGate.AppHost.Server
{
    internal class ProcessStarter32Bit : IProcessStartOperation
    {
        private const string c_FileName = "RedGate.AppHost.Client.exe";

        public Process StartProcess(string assemblyName, string remotingId, bool openDebugConsole = false)
        {
            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string quotedAssemblyArg = "\"" + Path.Combine(executingDirectory, assemblyName) + "\"";

            return Process.Start(Path.Combine(executingDirectory, c_FileName), String.Join(" ", new[] { "-i " + remotingId, "-a " + quotedAssemblyArg, openDebugConsole ? "-d" : "" }));
        }
    }
}