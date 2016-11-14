using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DotNetNinja.Wpf.Mvvm
{
    public class WindowController : IWindowController, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Topmost
        {
            get { return _topmost; }
            set { SetProperty(ref _topmost, value); }
        }
        bool _topmost;

        public IAppController App { get; }

        Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> _viewsDictionary;
        IViewFactory _viewFactory;
        IPartialViewFactory _partialViewFactory;

        IWindow _window; // injected via reflections in factory

        public WindowController(Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> viewsDictionary, IAppController appController,
                                IViewFactory viewFactory, IPartialViewFactory partialViewFactory)
        {
            App = appController;

            _viewsDictionary = viewsDictionary;
            _viewFactory = viewFactory;
            _partialViewFactory = partialViewFactory;
        }

        public IView CreateView<TView>(params object[] viewModelArgs)
            where TView : IView
            => CreateView(typeof(TView), viewModelArgs);

        public IView CreateView(Type viewType, params object[] viewModelArgs)
        {
            var viewInfo = _viewsDictionary.Keys.FirstOrDefault(di => di.Abstraction == viewType);

            if (viewInfo == null)
            {
                throw new InvalidOperationException($"View type {viewType} is not registered.");
            }

            var viewModelInfo = _viewsDictionary[viewInfo];

            var view = _viewFactory.Create(viewInfo.Implementation, viewModelInfo.Implementation, CreateViewController(), viewModelArgs);

            return view;
        }

        public IWindowController InjectView<TWindow, TView>(Expression<Func<TWindow, IView>> winContent, params object[] viewModelArgs)
            where TWindow : IWindow
            where TView : IView
            => InjectView(winContent, CreateView<TView>(viewModelArgs));

        public IWindowController InjectView<TWindow>(Expression<Func<TWindow, IView>> winContent, IView view)
            where TWindow : IWindow
        {
            var propertyName = winContent.ExtractPropertyName();

            _window.InjectProperty(propertyName, view);

            return this;
        }

        public void GoToView<TView>(params object[] viewModelArgs)
            where TView : IView
        {
            InjectView<IWindow, TView>(win => win.MainView, viewModelArgs);
        }

        public void GoToWindow<TWindow>()
            where TWindow : IWindow
        {
            App.ShowWindow<TWindow>();
            _window.Close();
        }

        protected IViewController CreateViewController()
            => new ViewController(_viewsDictionary, this, _partialViewFactory);

        protected void OnPropertyChanged(string propName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        protected bool SetProperty<TProperty>(ref TProperty backingField, TProperty value, [CallerMemberName] string propName = null)
        {
            var changed = !EqualityComparer<TProperty>.Default.Equals(backingField, value);

            if (changed)
            {
                backingField = value;
                OnPropertyChanged(propName);
            }

            return changed;
        }
    }
}
