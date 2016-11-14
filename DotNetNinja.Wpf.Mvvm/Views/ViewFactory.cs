using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public class ViewFactory : IViewFactory
    {
        public IView Create<TView, TViewModel>(IViewController viewController, params object[] viewModelArgs)
            where TView : IView
            where TViewModel : IViewModel
            => Create(typeof(TView), typeof(TViewModel), viewController,  viewModelArgs);

        public IView Create(Type viewType, Type viewModelType, IViewController viewController, params object[] viewModelArgs)
        {
            if (!viewType.IsAssignableFrom(typeof(IView)) || !viewModelType.IsAssignableFrom(typeof(IViewModel)))
            {
                throw new InvalidOperationException($"{nameof(viewType)} is not {typeof(IView)} or {nameof(viewModelType)} is not {typeof(IViewModel)}");
            }

            var createViewModel = viewModelArgs.Length == 0
                ? new Func<object>(() => Activator.CreateInstance(viewModelType))
                : new Func<object>(() => Activator.CreateInstance(viewModelType, viewModelArgs));

            var viewModel = createViewModel();

            viewModel
                .InjectPrivateField("_viewController", viewController)
                .InjectPrivateField("_windowController", viewController.Window)
                .InjectPrivateField("_appController", viewController.Window.App);

            viewController
                .InjectPrivateField("_viewModel", viewModel);

            var view = Activator.CreateInstance(viewType, viewModel);

            viewController
                .InjectPrivateField("_view", view);

            view
                .InjectProperty("ViewModel", viewModel)
                .InjectProperty("DataContext", viewModel);

            return (IView)view;
        }
    }
}
