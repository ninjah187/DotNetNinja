using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DotNetNinja.Wpf.Mvvm
{
    public abstract class WindowBase : Window, IWindow
    {
        /// <summary>
        /// Główny widok, w którym wyświetlana jest treść.
        /// </summary>
        public IView MainView
        {
            get { return (IView)_mainContentControl.Content; }
            set { _mainContentControl.Content = value; }
        }

        public IWindowController Window => _windowController;
        IWindowController _windowController;

        ContentControl _mainContentControl;

        public WindowBase()
        {
            Loaded += delegate
            {
                _mainContentControl = (ContentControl) FindName("MainContentControl");
            };
        }
    }
}
