using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotNetNinja.Wpf.ConfirmDialog;

namespace DotNetNinja.Wpf.ConfirmDialog.Samples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += async delegate
            {
                if (await ConfirmDialog.ConfirmAsync("Do you really want to confirm your action?"))
                {
                    MessageBox.Show("Action confirmed.");
                }
                else
                {
                    MessageBox.Show("Action declined.");
                }
            };
        }
    }
}
