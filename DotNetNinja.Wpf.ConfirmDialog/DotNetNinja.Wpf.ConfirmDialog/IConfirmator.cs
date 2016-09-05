using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetNinja.Wpf.ConfirmDialog
{
    /// <summary>
    /// Represents object used to confirm or decline some action.
    /// </summary>
    public interface IConfirmator
    {
        /// <summary>
        /// Gets confirmation input. Returns true if accepted and false if declined.
        /// </summary>
        /// <param name="message">Message prompting for user's input.</param>
        /// <returns>True if accepted; false if declined.</returns>
        Task<bool> ConfirmAsync(string message);
    }
}
