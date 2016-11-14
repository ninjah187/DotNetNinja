using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    /// <summary>
    /// Obiekt odpowiedzialny za tworzenie okien.
    /// </summary>
    public class WindowFactory : IWindowFactory
    {
        public IWindow Create<TWindow>(IWindowController winController)
            where TWindow : IWindow
            => Create(typeof(TWindow), winController);
        
        public IWindow Create(Type windowType, IWindowController winController)
        {
            if (!windowType.IsAssignableFrom(typeof(IWindow)))
            {
                throw new InvalidOperationException("windowType is not IWindow");
            }

            var window = Activator.CreateInstance(windowType, winController);

            winController
                .InjectPrivateField("_window", window);

            window
                .InjectPrivateField("_windowController", winController);

            return (IWindow)window;
        }
    }
}
