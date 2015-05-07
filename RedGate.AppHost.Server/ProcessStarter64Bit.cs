namespace RedGate.AppHost.Server
{
    internal class ProcessStarter64Bit : ProcessStarter
    {
        protected override string ProcessFileName
        {
            get { return "RedGate.AppHost.Client.x64.exe"; }
        }
    }
}
