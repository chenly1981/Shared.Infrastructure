using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Background
{
    /// <summary>
    /// A service to support background job.
    /// </summary>
    public interface IBackgroundService
    {
        /// <summary>
        /// Add a action to background service. The action will invoke asynchronously.
        /// </summary>
        /// <param name="action"></param>
        void Invoke(Action action);
    }
}
