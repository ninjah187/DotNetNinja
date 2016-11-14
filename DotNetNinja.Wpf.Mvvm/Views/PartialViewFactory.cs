using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public class PartialViewFactory : IPartialViewFactory
    {
        public IView Create<TView>(IViewModel viewModel)
            where TView : IView
            => Create(typeof(TView), viewModel);

        public IView Create(Type viewType, IViewModel viewModel)
        {
            if (!viewType.IsAssignableFrom(typeof(IView)))
            {
                throw new InvalidOperationException($"{nameof(viewType)} is not {typeof(IView)}.");
            }

            var view = (IView)Activator.CreateInstance(viewType, viewModel);

            return view;
        }
    }
}
