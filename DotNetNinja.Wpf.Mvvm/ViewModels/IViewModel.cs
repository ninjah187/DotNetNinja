using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IViewModel
    {
        IAppController App { get; }
        IWindowController Window { get; }
        IViewController View { get; }

        bool IsLoading { get; }

        Task LoadAsync();
    }
}
