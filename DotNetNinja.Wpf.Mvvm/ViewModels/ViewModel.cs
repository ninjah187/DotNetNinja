using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetNinja.NotifyPropertyChanged;

namespace DotNetNinja.Wpf.Mvvm
{
    public abstract class ViewModel : PropertyChangedNotifier, IViewModel
    {
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }
        private bool _isLoading;

        public IAppController App => _appController;
        IAppController _appController;

        public IWindowController Window => _windowController;
        IWindowController _windowController;

        public IViewController View => _viewController;
        IViewController _viewController;

        public ViewModel()
        {
        }

        public ViewModel(IViewController viewController)
        {
            _viewController = viewController;
            _windowController = View.Window;
            _appController = Window.App;
        }

        public virtual async Task LoadAsync()
        {
        }
    }
}
