using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public interface IViewController
    {
        IWindowController Window { get; }
        
        IView CreatePartialView<TView>() where TView : IView;

        IViewController InjectPartialView<TParentView, TView>(Expression<Func<TParentView, IView>> viewContent)
            where TParentView : IView
            where TView : IView;

        IViewController InjectPartialView<TView>(Expression<Func<TView, IView>> viewContent, IView view)
            where TView : IView;
    }
}
