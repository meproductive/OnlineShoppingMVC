using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Adress { get; set; }
        public string BirthDate { get; set; }
        public bool IsActive { get; set; }
    }
}