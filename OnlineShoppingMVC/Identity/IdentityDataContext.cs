using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext() : base("SQL")
        {

        }
        public System.Data.Entity.DbSet<OnlineShoppingMVC.Models.RegisterViewModel> registerViewModels { get; set; }
    }
}