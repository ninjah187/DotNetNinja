using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IPartialViewFactory
    {
        IView Create<TView>(IViewModel viewModel) where TView : IView;
        
        IView Create(Type viewType, IViewModel viewModel);
    }
}
