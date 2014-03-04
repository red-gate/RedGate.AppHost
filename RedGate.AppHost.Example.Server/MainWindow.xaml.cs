using System;
using System.AddIn.Pipeline;
using System.Windows;
using System.Windows.Controls;
using RedGate.AppHost.Server;

namespace RedGate.AppHost.Example.Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var safeAppHostChildHandle = new ChildProcessFactory().Create("RedGate.AppHost.Example.Client.dll");

                var nativeHandleContractWithoutIntPtr = safeAppHostChildHandle.Initialize(new ServiceLocator());

                var contractToViewAdapter = FrameworkElementAdapters.ContractToViewAdapter(nativeHandleContractWithoutIntPtr);

                Content = contractToViewAdapter;
            }
            catch (Exception e)
            {
                Content = new TextBlock()
                          {
                              Text = e.ToString()
                          };
            }
        }
    }
}
