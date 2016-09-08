using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.TypeFiltering
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Executes action if object is of specific type and starts ITypeFiltering methods chain.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="what"></param>
        /// <returns></returns>
        public static ITypeFiltering When<T>(this object o, Action<T> what)
            where T : class
        {
            var api = new TypeFiltering(o);
            return api.When(what);
        }

        /// <summary>
        /// Executes action if object is of specific type and starts ITypeFiltering methods chain.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="what"></param>
        /// <returns></returns>
        public static ITypeFiltering When<T>(this object o, Action<T, ITypeFilteringControl> what)
            where T : class
        {
            var api = new TypeFiltering(o);
            return api.When(what);
        }
    }
}
