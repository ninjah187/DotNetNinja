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
using System.Windows.Shapes;
using DotNetNinja.Wpf.ConfirmDialog;

namespace DotNetNinja.Wpf.ConfirmDialog.Samples.MVVM
{
    /// <summary>
    /// Interaction logic for SampleView.xaml
    /// </summary>
    public partial class SampleView : Window, IView
    {
        public SampleView(IViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
