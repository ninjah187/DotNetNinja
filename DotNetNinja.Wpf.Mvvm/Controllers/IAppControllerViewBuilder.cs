using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IAppControllerViewBuilder
    {
        IAppControllerBuilder With<TViewModel, TImplementation>()
            where TViewModel : IViewModel
            where TImplementation : class, IViewModel;

        IAppControllerBuilder AsPartial();
    }
}
