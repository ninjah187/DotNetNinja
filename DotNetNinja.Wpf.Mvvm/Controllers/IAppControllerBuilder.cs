using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IAppControllerBuilder
    {
        IAppControllerBuilder UseDefaults();

        IAppControllerViewBuilder UseView<TView, TImplementation>()
            where TView : IView
            where TImplementation : class, IView;

        IAppControllerBuilder UseWindow<TWindow, TImplementation>()
            where TWindow : IWindow
            where TImplementation : class, IWindow;

        IAppControllerBuilder UseViewFactory(IViewFactory viewFactory);
        IAppControllerBuilder UseViewFactory<TViewFactory>(params object[] args) where TViewFactory : class, IViewFactory;

        IAppControllerBuilder UseWindowFactory(IWindowFactory windowFactory);
        IAppControllerBuilder UseWindowFactory<TWindowFactory>(params object[] args);

        IAppControllerBuilder UsePartialViewFactory(IPartialViewFactory partialViewFactory);
        IAppControllerBuilder UsePartialViewFactory<TViewFactory>(params object[] args) where TViewFactory : class, IPartialViewFactory;

        IAppControllerBuilder UseMessageDisplayer<TMessageDisplayer>(params object[] args) where TMessageDisplayer : class, IMessageDisplayer;

        IAppControllerBuilder UseConfirmator<TConfirmator>(params object[] args) where TConfirmator : class, IConfirmator;

        IAppController Build();
    }
}
