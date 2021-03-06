﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.UnitOfWork
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
