using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DotNetNinja.Wpf.ConfirmDialog.Samples.MVVM;

namespace DotNetNinja.Wpf.ConfirmDialog.Samples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //StartVanillaSample();
            StartMvvmSample();
        }

        void StartVanillaSample()
        {
            var window = new MainWindow();
            window.Show();
        }

        void StartMvvmSample()
        {
            var viewModel = new SampleViewModel(new ConfirmDialogConfirmator());
            var view = new SampleView(viewModel);

            view.Show();
        }
    }
}
