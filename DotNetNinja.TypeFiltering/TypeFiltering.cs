using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetNinja.TypeFiltering
{
    public class TypeFiltering : ITypeFiltering, ITypeFilteringControl
    {
        protected object Target { get; }

        protected bool AtLeastOneRecognized { get; set; }
        protected bool PreviousRecognized { get; set; }

        protected bool ShouldBreak { get; set; }

        public TypeFiltering(object target)
        {
            Target = target;
        }

        public ITypeFiltering When<T>(Action<T> what)
            where T : class
        {
            // uncomment if only one type in chain should be recognized - only one action in chain could be fired
            // it improves performance
            // for now, every type in the chain will be checked - it means that api could fire more than one action in the chain
            //if (AtLeastOneRecognized)
            //{
            //    return this;
            //}

            if (ShouldBreak)
            {
                return this;
            }

            var item = Target as T;

            if (item != null)
            {
                AtLeastOneRecognized = true;
                PreviousRecognized = true;
                what(item);
            }
            else
            {
                PreviousRecognized = false;
            }

            return this;
        }

        public ITypeFiltering When<T>(Action<T, ITypeFilteringControl> what)
            where T : class
        {
            if (ShouldBreak)
            {
                return this;
            }

            var item = Target as T;

            if (item != null)
            {
                AtLeastOneRecognized = true;
                what(item, this);
            }

            return this;
        }

        public ITypeFiltering BreakIfRecognized()
        {
            if (PreviousRecognized)
            {
                ShouldBreak = true;
            }
            return this;
        }

        public void Break()
        {
            //if (PreviousRecognized)
            //{
            //    ShouldBreak = true;
            //}
            ShouldBreak = true;
        }

        public void Default(Action what)
        {
            if (!AtLeastOneRecognized)
            {
                what();
            }
        }

        public void Default(Action<object> what)
        {
            if (!AtLeastOneRecognized)
            {
                what(Target);
            }
        }

        public void ThrowIfNotRecognized()
            => ThrowIfNotRecognized($"Unknown type: {Target.GetType()}");

        public void ThrowIfNotRecognized(string message)
        {
            if (!AtLeastOneRecognized)
            {
                throw new InvalidOperationException(message);
            }
        }
    }
}
