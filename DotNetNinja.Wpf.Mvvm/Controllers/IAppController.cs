using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IAppController
    {
        IWindow GetWindow<TWindow>() where TWindow : IWindow;
        
        TWindow ShowWindow<TWindow>() where TWindow : IWindow;
        
        void ShowMessage(string message);
        
        Task<bool> ConfirmAsync(string message);
    }
}
