using System;
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

            if (args.Length != 3)
            {
                MessageBox.Show("Hello :)\n\nI'm the child process for the Red Gate Deployment Manager SSMS add-in.\n\nPlease use SSMS rather than running me directly.", "RedGate.SQLCI.UI", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MainInner(args[0], args[1], args[2]);                
            }
        }

        private static void MainInner(string id, string assembly, string typeName)
        {
            FrameworkElement element = LoadChildAssembly(assembly, typeName);
            InitializeRemoting(id, element);
            SignalReady(id);
            RunWpf();
        }

        private static FrameworkElement LoadChildAssembly(string assembly, string typeName)
        {
            var outOfProcAssembly = Assembly.LoadFile(assembly);

            var type = outOfProcAssembly.GetType(typeName);

            return (FrameworkElement) Activator.CreateInstance(type);
        }

        private static void InitializeRemoting(string id, FrameworkElement element)
        {
            Remoting.Remoting.RegisterChannels(true, id);

            s_SafeAppHostChildHandle = new SafeAppHostChildHandle(Dispatcher.CurrentDispatcher, element);
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
