using CommandLine;

namespace RedGate.AppHost.Client
{
    internal class Options
    {
        [Option('a', "assembly", Required = true, HelpText = "Assembly that contains an IOutOfProcessEntryPoint to load")]
        public string Assembly { get; set; }

        [Option('i', "id", Required = true, HelpText = "The communication channel to call back to the host")]
        public string ChannelId { get; set; }

        [Option('d', "debug", Required = false, HelpText = "Opens the client in debug mode")]
        public bool Debug { get; set; }

        [Option('m', "monitor", Required = false, HelpText = "Exits the process if the parent process exits")]
        public bool MonitorParentProcess { get; set; }
    }
}