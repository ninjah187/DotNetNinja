using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.ConfirmDialog
{
    /// <summary>
    /// <see cref="IConfirmator"/> which displays <see cref="ConfirmDialog"/> and obtains user's input confirmation.
    /// </summary>
    public class ConfirmDialogConfirmator : IConfirmator
    {
        public Task<bool> ConfirmAsync(string message)
            => ConfirmDialog.ConfirmAsync(message);
    }
}
