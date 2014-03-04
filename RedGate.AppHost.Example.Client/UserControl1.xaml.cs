using System.Windows.Controls;

namespace RedGate.AppHost.Example.Client
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1(string textToDisplay)
        {
            InitializeComponent();

            Content = new TextBlock
                      {
                          Text = textToDisplay
                      };
        }
    }
}
