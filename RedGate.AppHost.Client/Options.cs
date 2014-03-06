using System.Text;
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

        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("Red Gate Out of Process App Host");
            return usage.ToString();
        }
    }
}