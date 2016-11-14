using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DotNetNinja.Wpf.Mvvm
{
    public class MessageBoxConfirmator : IConfirmator
    {
        public Task<bool> ConfirmAsync(string message)
            => MessageBox.Show(message, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes
                ? Task.FromResult(true)
                : Task.FromResult(false);
    }
}
