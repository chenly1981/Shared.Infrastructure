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
        /// 
        /// </summary>
        /// <param name="action"></param>
        void Invoke(Action action);
    }
}
