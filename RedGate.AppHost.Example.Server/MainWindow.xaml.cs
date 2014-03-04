using System;
using System.AddIn.Pipeline;
using System.Windows;
using RedGate.AppHost.Interfaces;
using RedGate.AppHost.Server;

namespace RedGate.AppHost.Example.Server
{
    public class ServiceLocator : MarshalByRefObject, IAppHostServices
    {
        public T GetService<T>() where T : MarshalByRefObject
        {
            return null;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var safeAppHostChildHandle = new ChildProcessFactory().Create("RedGate.AppHost.Example.Client.dll", "RedGate.AppHost.Example.Client.UserControl1");

                var nativeHandleContractWithoutIntPtr = safeAppHostChildHandle.Initialize(new ServiceLocator());

                var contractToViewAdapter = FrameworkElementAdapters.ContractToViewAdapter(nativeHandleContractWithoutIntPtr);

                Content = contractToViewAdapter;
            }
            catch (Exception e)
            {
                
            }
            
        }
    }
}
