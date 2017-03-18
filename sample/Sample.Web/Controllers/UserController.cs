using Microsoft.AspNetCore.Mvc;
using Sample.Web.Entity.Views;
using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Web.Controllers
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<UserData> Query()
        {
            using (var uw = this.CreateUnitOfWork())
            {
                var list = uw.All<UserData>();

                return list;
            }
        }

        [HttpGet("{id:int}")]
        public UserData Get(int id)
        {
            using (var uw = this.CreateUnitOfWork())
            {
                var user = uw.Get<UserData>(t => t.ID == id);

                return user;
            }
        }
    }
}
