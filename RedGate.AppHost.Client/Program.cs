using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using CommandLine;
using RedGate.AppHost.Interfaces;

namespace RedGate.AppHost.Client
{
    internal static class Program
    {
        private static SafeChildProcessHandle s_SafeChildProcessHandle;

        [STAThread]
        private static void Main(string[] args)
        {
            var parseResult = Parser.Default.ParseArguments<Options>(args);

            if (parseResult.Tag == ParserResultType.Parsed) {
                var options = parseResult.MapResult(el => el, el => null);
#if DEBUG
                options.Debug = true;
#endif
                if (options.Debug)
                {
                    ConsoleNativeMethods.AllocConsole();
                }

                if (options.HostProcessId.HasValue)
                {
                    new HostProcessMonitor(options.HostProcessId.Value, () => { Process.GetCurrentProcess().Kill(); }).Start();
                }

                MainInner(options.ChannelId, options.Assembly);
            }
            else
            {
                MessageBox.Show("This application is used by RedGate.AppHost and should not be started manually. See https://github.com/red-gate/RedGate.AppHost", "Red Gate", MessageBoxButton.OK, MessageBoxImage.Information);
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

            s_SafeChildProcessHandle = new SafeChildProcessHandle(Dispatcher.CurrentDispatcher, entryPoint);
            Remoting.Remoting.RegisterService<SafeChildProcessHandle, ISafeChildProcessHandle>(s_SafeChildProcessHandle);
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
