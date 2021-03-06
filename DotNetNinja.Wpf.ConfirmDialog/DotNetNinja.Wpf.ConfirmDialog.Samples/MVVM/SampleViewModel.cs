﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DotNetNinja.Wpf.Commands;

namespace DotNetNinja.Wpf.ConfirmDialog.Samples.MVVM
{
    public class SampleViewModel : IViewModel
    {
        public ICommand DoSomethingCommand { get; private set; }

        IConfirmator _confirmator;

        public SampleViewModel(IConfirmator confirmator)
        {
            _confirmator = confirmator;

            DoSomethingCommand = new Command(async () => await DoSomethingAsync());
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
