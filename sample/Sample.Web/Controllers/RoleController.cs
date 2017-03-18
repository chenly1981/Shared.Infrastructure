using Microsoft.AspNetCore.Mvc;
using Sample.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Infrastructure.UnitOfWork;

namespace Sample.Web.Controllers
{
    [Route("role")]
    public class RoleController : ControllerBase
    {
        public RoleController()
        {

        }

        [HttpGet]
        public List<Role> Query()
        {
            using (var uw = this.CreateUnitOfWork())
            {
                return uw.All<Role>();
            }
        }

        [HttpGet("{id:int}")]
        public Role Get(int id)
        {
            using (var uw = this.CreateUnitOfWork())
            {
                var role = uw.Get<Role>(t => t.ID == id);

                return role;
            }
        }
    }
}
