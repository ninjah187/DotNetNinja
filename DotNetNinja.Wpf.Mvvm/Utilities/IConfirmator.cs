using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IConfirmator
    {
        Task<bool> ConfirmAsync(string message);
    }
}
