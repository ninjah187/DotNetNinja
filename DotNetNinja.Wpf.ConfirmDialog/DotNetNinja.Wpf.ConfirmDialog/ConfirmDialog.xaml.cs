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

namespace DotNetNinja.Wpf.ConfirmDialog
{
    /// <summary>
    /// Window dialog which obtains user yes/no input.
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        bool _confirmed;

        ConfirmDialog(string message, string title = "")
        {
            InitializeComponent();

            MessageTextBlock.Text = message;
            Title = title;
        }

        public static Task<bool> ConfirmAsync(string message, string title = "")
        {
            var tcs = new TaskCompletionSource<bool>();

            var win = new ConfirmDialog(message, title);
            win.Closed += delegate
            {
                tcs.SetResult(win._confirmed);
            };

            win.ShowDialog();

            return tcs.Task;
        }

        void YesButton_Click(object sender, RoutedEventArgs e)
        {
            _confirmed = true;
            Close();
        }

        void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
