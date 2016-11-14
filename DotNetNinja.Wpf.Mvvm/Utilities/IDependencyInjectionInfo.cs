using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IDependencyInjectionInfo
    {
        Type Abstraction { get; }
        
        Type Implementation { get; }
    }
}
