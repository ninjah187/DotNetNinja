using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public class AppControllerBuilder : IAppControllerBuilder, IAppControllerViewBuilder
    {
        // key: View info, value: ViewModel info
        Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> _viewsDictionary
            = new Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo>();

        List<IDependencyInjectionInfo> _windowsList = new List<IDependencyInjectionInfo>();

        IDependencyInjectionInfo _lastView;
        IDependencyInjectionInfo _lastViewModel;
        
        IWindowFactory _windowFactory;
        IViewFactory _viewFactory;
        IPartialViewFactory _partialViewFactory;
        IMessageDisplayer _messageDisplayer;
        IConfirmator _confirmator;

        public IAppController Build()
        {
            return new AppController(_viewsDictionary, _windowsList, _windowFactory, _viewFactory, _partialViewFactory, _messageDisplayer, _confirmator);
        }

        public IAppControllerBuilder UseDefaults()
        {
            return this
                .UseWindowFactory<WindowFactory>()
                .UseViewFactory<ViewFactory>()
                .UsePartialViewFactory<PartialViewFactory>()
                .UseMessageDisplayer<MessageBoxDisplayer>()
                .UseConfirmator<MessageBoxConfirmator>();
        }

        public IAppControllerViewBuilder UseView<TView, TImplementation>()
            where TView : IView
            where TImplementation : class, IView
        {
            if (_lastView != null)
            {
                throw new InvalidOperationException();
            }

            _lastView = new DependencyInjectionInfo<TView, TImplementation>();

            return this;
        }

        public IAppControllerBuilder With<TViewModel, TImplementation>()
            where TViewModel : IViewModel
            where TImplementation : class, IViewModel
        {
            if (_lastView == null || _lastViewModel != null)
            {
                throw new InvalidOperationException();
            }

            _lastViewModel = new DependencyInjectionInfo<TViewModel, TImplementation>();

            _viewsDictionary.Add(_lastView, _lastViewModel);
            _lastView = null;
            _lastViewModel = null;

            return this;
        }

        public IAppControllerBuilder AsPartial()
        {
            if (_lastView == null)
            {
                throw new InvalidOperationException();
            }

            _viewsDictionary.Add(_lastView, null);
            _lastView = _lastViewModel = null;

            return this;
        }

        public IAppControllerBuilder UseViewFactory(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            return this;
        }

        public IAppControllerBuilder UseViewFactory<TViewFactory>(params object[] args)
            where TViewFactory : class, IViewFactory
        {
            _viewFactory = (IViewFactory)Activator.CreateInstance(typeof(TViewFactory), args);
            return this;
        }

        public IAppControllerBuilder UseWindow<TWindow, TImplementation>()
            where TWindow : IWindow
            where TImplementation : class, IWindow
        {
            _windowsList.Add(new DependencyInjectionInfo<TWindow, TImplementation>());
            return this;
        }

        public IAppControllerBuilder UseWindowFactory(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            return this;
        }

        public IAppControllerBuilder UseWindowFactory<TWindowFactory>(params object[] args)
        {
            _windowFactory = (IWindowFactory)Activator.CreateInstance(typeof(TWindowFactory), args);
            return this;
        }

        public IAppControllerBuilder UsePartialViewFactory(IPartialViewFactory partialViewFactory)
        {
            _partialViewFactory = partialViewFactory;
            return this;
        }

        public IAppControllerBuilder UsePartialViewFactory<TViewFactory>(params object[] args)
            where TViewFactory : class, IPartialViewFactory
        {
            _partialViewFactory = (IPartialViewFactory)Activator.CreateInstance(typeof(TViewFactory), args);
            return this;
        }

        public IAppControllerBuilder UseMessageDisplayer<TMessageDisplayer>(params object[] args)
            where TMessageDisplayer : class, IMessageDisplayer
        {
            _messageDisplayer = (IMessageDisplayer)Activator.CreateInstance(typeof(TMessageDisplayer), args);
            return this;
        }

        public IAppControllerBuilder UseConfirmator<TConfirmator>(params object[] args)
            where TConfirmator : class, IConfirmator
        {
            _confirmator = (IConfirmator) Activator.CreateInstance(typeof(TConfirmator));
            return this;
        }
    }
}
