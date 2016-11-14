using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IViewFactory
    {
        IView Create<TView, TViewModel>(IViewController viewController, params object[] viewModelArgs)
            where TView : IView
            where TViewModel : IViewModel;

        IView Create(Type viewType, Type viewModelType, IViewController viewController, params object[] viewModelArgs);
    }
}
