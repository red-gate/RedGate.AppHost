using System.Windows;
using RedGate.AppHost.Example.Remote.Services;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Example.Client
{
    public class OutOfProcessEntryPoint : IOutOfProcessEntryPoint
    {
        public FrameworkElement CreateElement(IAppHostServices service)
        {
            var serverThing = service.GetService<IServerImplementedThingThatClientNeeds>();

            string textToDisplay = serverThing.GetTextToDisplay();

            return new UserControl1(textToDisplay);
        }
    }
}
