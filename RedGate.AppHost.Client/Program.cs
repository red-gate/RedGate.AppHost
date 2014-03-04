using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Client
{
    internal static class Program
    {
        private static SafeAppHostChildHandle s_SafeAppHostChildHandle;

        [STAThread]
        private static void Main(string[] args)
        {
#if DEBUG
            ConsoleNativeMethods.AllocConsole();
#endif

            if (args.Length != 2)
            {
                MessageBox.Show("Hello :)\n\nI'm the child process for the Red Gate Deployment Manager SSMS add-in.\n\nPlease use SSMS rather than running me directly.", "RedGate.SQLCI.UI", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MainInner(args[0], args[1]);                
            }
        }

        private static void MainInner(string id, string assembly)
        {
            var entryPoint = LoadChildAssembly(assembly);
            InitializeRemoting(id, entryPoint);
            SignalReady(id);
            RunWpf();
        }

        private static IOutOfProcessEntryPoint LoadChildAssembly(string assembly)
        {
            var outOfProcAssembly = Assembly.LoadFile(assembly);

            var entryPoint = outOfProcAssembly.GetTypes().Single(x => x.GetInterfaces().Contains(typeof (IOutOfProcessEntryPoint)));

            return (IOutOfProcessEntryPoint) Activator.CreateInstance(entryPoint);
        }

        private static void InitializeRemoting(string id, IOutOfProcessEntryPoint entryPoint)
        {
            Remoting.Remoting.RegisterChannels(true, id);

            s_SafeAppHostChildHandle = new SafeAppHostChildHandle(Dispatcher.CurrentDispatcher, entryPoint);
            Remoting.Remoting.RegisterService<SafeAppHostChildHandle, ISafeAppHostChildHandle>(s_SafeAppHostChildHandle);
        }

        private static void SignalReady(string id)
        {
            using (EventWaitHandle signal = EventWaitHandle.OpenExisting(id))
            {
                signal.Set();
            }
        }

        private static void RunWpf()
        {
            Dispatcher.Run();
        }
    }
}
