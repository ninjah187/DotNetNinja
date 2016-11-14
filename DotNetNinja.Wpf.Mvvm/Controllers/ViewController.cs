using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public class ViewController : IViewController
    {
        public IWindowController Window { get; }

        Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> _viewsDictionary;
        IView _view;            // injected through reflection
        IViewModel _viewModel;  // injected through reflection
        IPartialViewFactory _partialViewFactory;

        public ViewController(Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> viewsDictionary, IWindowController windowController,
                              IPartialViewFactory partialViewFactory)
        {
            Window = windowController;

            _viewsDictionary = viewsDictionary;
            _partialViewFactory = partialViewFactory;
        }

        public IView CreatePartialView<TView>()
            where TView : IView
        {
            var viewInfo = _viewsDictionary.Keys.FirstOrDefault(di => di.Abstraction == typeof(TView));

            if (viewInfo == null)
            {
                throw new InvalidOperationException($"View type {typeof(TView)} is not registered.");
            }

            var view = _partialViewFactory.Create(viewInfo.Implementation, _viewModel);

            return view;
        }

        public IViewController InjectPartialView<TParentView, TView>(Expression<Func<TParentView, IView>> viewContent)
            where TParentView : IView
            where TView : IView
            => InjectPartialView(viewContent, CreatePartialView<TView>());

        public IViewController InjectPartialView<TView>(Expression<Func<TView, IView>> viewContent, IView view)
            where TView : IView
        {
            var propertyName = viewContent.ExtractPropertyName();

            _view.InjectProperty(propertyName, view);

            return this;
        }
    }
}
