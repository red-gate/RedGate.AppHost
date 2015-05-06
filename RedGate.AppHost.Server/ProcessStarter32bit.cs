namespace RedGate.AppHost.Server
{
    internal class ProcessStarter32Bit : ProcessStarter
    {
        protected override string ProcessFileName
        {
            get { return "RedGate.AppHost.Client.exe"; }
        }
    }
}
