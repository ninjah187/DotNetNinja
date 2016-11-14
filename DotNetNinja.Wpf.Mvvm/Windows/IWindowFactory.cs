using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IWindowFactory
    {
        IWindow Create<TWindow>(IWindowController winController) where TWindow : IWindow;
        
        IWindow Create(Type windowType, IWindowController winController);
    }
}
