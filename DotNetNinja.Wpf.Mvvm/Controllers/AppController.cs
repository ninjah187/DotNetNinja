using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.Mvvm
{
    public class AppController : IAppController
    {
        Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> _viewsDictionary;
        List<IDependencyInjectionInfo> _windowsList;
        IWindowFactory _windowFactory;
        IViewFactory _viewFactory;
        IPartialViewFactory _partialViewFactory;
        IMessageDisplayer _messageDisplayer;
        IConfirmator _confirmator;

        public AppController(Dictionary<IDependencyInjectionInfo, IDependencyInjectionInfo> viewsDictionary,
                             List<IDependencyInjectionInfo> windowsList,
                             IWindowFactory windowFactory, IViewFactory viewFactory,
                             IPartialViewFactory partialViewFactory,
                             IMessageDisplayer messageDisplayer,
                             IConfirmator confirmator)
        {
            _viewsDictionary = viewsDictionary;
            _windowsList = windowsList;
            _windowFactory = windowFactory;
            _viewFactory = viewFactory;
            _partialViewFactory = partialViewFactory;
            _messageDisplayer = messageDisplayer;
            _confirmator = confirmator;
        }

        /// <summary>
        /// Tworzy okno typu <typeparamref name="TWindow"/>.
        /// </summary>
        /// <typeparam name="TWindow">Typ okna.</typeparam>
        /// <returns>Nowo utworzone okno.</returns>
        /// <remarks>
        /// Utworzone okno nie jest wyświetlone (nie wywołano metody .Show()).
        /// </remarks>
        public IWindow GetWindow<TWindow>()
            where TWindow : IWindow
        {
            var windowInfo = _windowsList.FirstOrDefault(di => di.Abstraction == typeof(TWindow));

            if (windowInfo == null)
            {
                throw new InvalidOperationException($"Window type {typeof(TWindow)} is not registered.");
            }

            var window = _windowFactory.Create(windowInfo.Implementation, CreateWinController());

            return window;
        }

        /// <summary>
        /// Tworzy nowe okno typu <typeparamref name="TWindow"/> i wyświetla je.
        /// </summary>
        /// <typeparam name="TWindow">Typ tworzonego i wyświetlanego okna.</typeparam>
        /// <returns>Nowo utworzone i wyświetlone okno.</returns>
        public TWindow ShowWindow<TWindow>()
            where TWindow : IWindow
        {
            var win = GetWindow<TWindow>();
            win.Show();
            return (TWindow)win;
        }

        /// <summary>
        /// Wyświetla wiadomość dla użytkownika.
        /// </summary>
        /// <param name="message">Wiadomość do wyświetlenia.</param>
        /// <remarks>
        /// W standardowej implementacji - wyświetla message boxa z wiadomością.
        /// </remarks>
        public void ShowMessage(string message)
            => _messageDisplayer.ShowMessage(message);

        /// <summary>
        /// Wyświetla wiadomość <paramref name="message"/> i odbiera wejście użytkownika.
        /// </summary>
        /// <param name="message">Wyświetlana wiadomość.</param>
        /// <returns>True jeśli użytkownik potwierdził i false jeśli zaprzeczył.</returns>
        /// <remarks>
        /// W standardowej implementacji - wyświetla okienko z pytaniem oraz przyciskami tak/nie.
        /// </remarks>
        public Task<bool> ConfirmAsync(string message)
            => _confirmator.ConfirmAsync(message);

        /// <summary>
        /// Tworzy nowy kontroler okna dla danej aplikacji.
        /// </summary>
        protected IWindowController CreateWinController()
            => new WindowController(_viewsDictionary, this, _viewFactory, _partialViewFactory);
    }
}
