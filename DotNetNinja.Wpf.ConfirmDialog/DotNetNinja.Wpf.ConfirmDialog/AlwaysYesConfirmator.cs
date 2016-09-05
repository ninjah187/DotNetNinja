﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.ConfirmDialog
{
    /// <summary>
    /// <see cref="IConfirmator"/> which always returns true without displaying any dialogs.
    /// </summary>
    public class AlwaysYesConfirmator : IConfirmator
    {
        public async Task<bool> ConfirmAsync(string message) => true;
    }
}
