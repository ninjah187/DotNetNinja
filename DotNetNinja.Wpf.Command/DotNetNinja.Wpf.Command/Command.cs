using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNetNinja.Wpf.Command
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        protected Action What { get; set; }
        protected Func<bool> When { get; set; }

        public Command(Action what)
            : this(what, () => true)
        {
        }

        public Command(Action what, Func<bool> when)
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
            return When();
        }

        public void Execute(object parameter)
        {
            What();
        }
    }
}
