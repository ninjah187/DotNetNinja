using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    static class TypeExtensions
    {
        public static bool Implements<TInterface>(this Type type)
            => type.GetInterfaces().Contains(typeof(TInterface));
    }
}
