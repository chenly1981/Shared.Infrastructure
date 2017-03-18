using Microsoft.AspNetCore.Mvc;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected IUnitOfWorkProvider UnitOfWorkProvider
        {
            get
            {
                return this.HttpContext.RequestServices.GetService<IUnitOfWorkProvider>();
            }
        }

        protected IUnitOfWork CreateUnitOfWork()
        {
            return UnitOfWorkProvider.CreateUnitOfWork(Consts.UnitOfWorkNames.SAMPLE);
        }
    }
}
