using Shared.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Web.Entity.Views
{
    public class UserData : IEntity
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }
    }
}
