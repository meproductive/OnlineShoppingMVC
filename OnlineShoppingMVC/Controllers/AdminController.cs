using OnlineShoppingMVC.Context;
using OnlineShoppingMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Category categories = new Category();
                categories.Id = category.Id;
                categories.Name = category.Name;
                categories.Description = category.Description;
                db.Categories.Add(categories);
                db.SaveChanges();
            }

            return PartialView(category);
        }
        //public PartialViewResult AddProduct(ProductViewModel product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Product products = new Product();
        //        products.Id= product.Id;
        //        products.Name = product.Name;
        //        products.Stock = product.Stock;
        //        products.Size = product.Size;
        //        products.Price = product.Price;
        //        products.Color = product.Color;
        //        products.Brand= product.Brand;
        //        products.Description = product.Description;
        //        products.ImageUrl = product.ImageUrl;
        //        products.CategoryId = product.CategoryId;
        //        db.Products.Add(products);
        //        db.SaveChanges();
        //    }
        //    return PartialView(product);
        //}
        public ActionResult AddProduct()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
    }
}