using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public class DependencyInjectionInfo<TAbstraction, TImplementation> : IDependencyInjectionInfo
        where TImplementation : class
    {
        public Type Abstraction { get; }
        
        public Type Implementation { get; }

        public DependencyInjectionInfo()
        {
            Abstraction = typeof(TAbstraction);
            Implementation = typeof(TImplementation);
        }
    }
}
