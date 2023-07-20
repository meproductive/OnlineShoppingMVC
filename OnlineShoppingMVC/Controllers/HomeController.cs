using OnlineShoppingMVC.Context;
using OnlineShoppingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingMVC.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        // GET: Home
        public ActionResult Index()
        {
            var products = db.Products
                 .Where(i => i.Popular)
                 .Select(i => new ProductViewModel()
                 {
                     Id = i.Id,
                     Name = i.Name,
                     Description = i.Description.Length > 50 ? i.Description.Substring(0, 100) : i.Description,
                     Price = i.Price,
                     ImageUrl = i.ImageUrl,
                     CategoryId = i.CategoryId
                 }).ToList();

            return View(products);
        }
        public ActionResult Detail(int id)
        {
            return View(db.Products.Where(i => i.Id == id).FirstOrDefault());
        }
        public ActionResult Shop(int? id)
        {
            var products = db.Products
             .Where(i => i.Popular)
             .Select(i => new ProductViewModel()
             {
                 Id = i.Id,
                 Name = i.Name,
                 Description = i.Description.Length > 50 ? i.Description.Substring(0, 100) : i.Description,
                 Price = i.Price,
                 ImageUrl = i.ImageUrl,
                 CategoryId = i.CategoryId
             }).AsQueryable();

            if (id != null)
            {
                products = products.Where(i => i.CategoryId == id);
            }

            return View(products.ToList());
        }
        public PartialViewResult Categories()
        {
            return PartialView(db.Categories.ToList());
        }
    }
}