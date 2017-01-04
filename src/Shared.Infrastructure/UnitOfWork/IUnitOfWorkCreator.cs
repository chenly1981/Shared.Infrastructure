using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    /// <summary>
    /// The creator to create UnitOfWork instance
    /// </summary>
    public interface IUnitOfWorkCreator : IDisposable
    {
        /// <summary>
        /// Create a UnitOfWork instance
        /// </summary>
        /// <returns></returns>
        IUnitOfWork CreateUnitOfWork();
    }
}
