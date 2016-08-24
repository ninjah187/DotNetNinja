using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.TypeFiltering
{
    public interface ITypeFilteringControl
    {
        /// <summary>
        /// Prevents further checks.
        /// </summary>
        void Break();
    }
}
