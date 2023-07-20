using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using OnlineShoppingMVC.Context;
using OnlineShoppingMVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using OnlineShoppingMVC.Models;

namespace OnlineShoppingMVC.Controllers
{
    public class AccountController : Controller
    {
        DataContext db = new DataContext();
        private UserManager<ApplicationUser> UserManager;
        private RoleManager<ApplicationRole> RoleManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            UserManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<ApplicationRole>(new IdentityDataContext());
            RoleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            var users = new ApplicationUser();
            users.Name = model.Name;
            users.Surname = model.Surname;
            users.UserName = model.UserName;
            users.Email = model.Email;
            users.Password = model.Password;
            users.RePassword = model.RePassword;
            users.PhoneNumber = model.PhoneNumber;
            users.Adress = model.Adress;
            users.BirthDate = model.BirthDate;
            users.IsActive = model.IsActive;

            var result = UserManager.Create(users, model.Password);

            if (result.Succeeded && users.IsActive == true)
            {
                if (RoleManager.RoleExists("user"))
                {
                    UserManager.AddToRole(users.Id, "user");
                }
                return RedirectToAction("Login");
            }
            else if (users.Password != users.RePassword)
            {
                ModelState.AddModelError("Error", "Şifre uyuşmuyor");
            }
            else if (!ModelState.IsValid)
            {
                ModelState.AddModelError("hata", "Şartlar Sağlanmıyor");
            }


            //else
            // {
            //     ModelState.AddModelError("hata", "Kullanıcı oluşturulamadı");
            // }

            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var users = UserManager.Find(model.UserName, model.Password);
                if (users != null /*&& model.Password == users.Password*/)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = UserManager.CreateIdentity(users, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    authProperties.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProperties, identityclaims);
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("loginerror", "böyle bir kullanıcı yok");
                }
            }

            return View(model);
        }
        public ActionResult logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}