using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.TypeFiltering
{
    /// <summary>
    /// Abstraction for executing actions depending on object type.
    /// </summary>
    public interface ITypeFiltering
    {
        /// <summary>
        /// Executes action when object is of specific type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="what"></param>
        /// <returns></returns>
        ITypeFiltering When<T>(Action<T> what) where T : class;

        ITypeFiltering When<T>(Action<T, ITypeFilteringControl> what) where T : class;

        /// <summary>
        /// Prevents further checks if previous type was succesfully recognized.
        /// </summary>
        /// <returns></returns>
        ITypeFiltering BreakIfRecognized();

        /// <summary>
        /// Executes action when not even one type was recognized.
        /// </summary>
        /// <param name="what"></param>
        void Default(Action what);

        /// <summary>
        /// Executes action when not even one type was recognized.
        /// </summary>
        /// <param name="what"></param>
        void Default(Action<object> what);

        /// <summary>
        /// Throws InvalidOperationException if not even one type was recognized.
        /// </summary>
        void ThrowIfNotRecognized();

        /// <summary>
        /// Throws InvalidOperationException if not even one type was recognized.
        /// </summary>
        void ThrowIfNotRecognized(string message);
    }
}
