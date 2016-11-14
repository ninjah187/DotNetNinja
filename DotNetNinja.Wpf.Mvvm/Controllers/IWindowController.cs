using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IWindowController
    {
        IAppController App { get; }

        IView CreateView<TView>(params object[] viewModelArgs) where TView : IView;
        
        IWindowController InjectView<TWindow, TView>(Expression<Func<TWindow, IView>> winContent, params object[] viewModelArgs)
            where TWindow : IWindow
            where TView : IView;
        
        IWindowController InjectView<TWindow>(Expression<Func<TWindow, IView>> winContent, IView view)
            where TWindow : IWindow;
        
        void GoToView<TView>(params object[] viewModelArgs)
            where TView : IView;
        
        void GoToWindow<TWindow>()
            where TWindow : IWindow;
    }
}
