using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DotNetNinja.Wpf.Mvvm
{
    public class MessageBoxDisplayer : IMessageDisplayer
    {
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
