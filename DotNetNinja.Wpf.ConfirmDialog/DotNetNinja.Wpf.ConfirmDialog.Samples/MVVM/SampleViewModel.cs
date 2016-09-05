using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetNinja.Wpf.Commands;

namespace DotNetNinja.Wpf.ConfirmDialog.Samples
{
    public class SampleViewModel
    {
        public ICommand DoSomethingCommand { get; private set; }

        IConfirmator _confirmator;

        public SampleViewModel(IConfirmator confirmator)
        {
            _confirmator = confirmator;    

            DoSomethingCommand = 
        }

        public async Task DoSomethingAsync()
        {
            // ...

            if (await _confirmator.ConfirmAsync("Do you really want to do something?"))
            {
                // accepted
            }
            else
            {
                // declined
            }

            // ...
        }
    }
}
