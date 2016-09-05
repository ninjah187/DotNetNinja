using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNetNinja.Wpf.Commands
{
    public class CommandParametrized<TParameter> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        protected Action<TParameter> What { get; set; }
        protected Func<TParameter, bool> When { get; set; }

        public CommandParametrized(Action<TParameter> what)
            : this(what, x => true)
        {
        }

        public CommandParametrized(Action<TParameter> what, Func<TParameter, bool> when)
        {
            if (what == null)
            {
                throw new ArgumentNullException(nameof(what));
            }

            if (when == null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            What = what;
            When = when;
        }

        public bool CanExecute(object parameter)
        {
            return When((TParameter) parameter);
        }

        public void Execute(object parameter)
        {
            What((TParameter) parameter);
        }
    }
}
