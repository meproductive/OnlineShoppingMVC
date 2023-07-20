using OnlineShoppingMVC.Context;
using OnlineShoppingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingMVC.Controllers
{
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public PartialViewResult Orders()
        {
            var username = User.Identity.Name;
            var orders = db.Orders.Where(i => i.UserName == username)
                .Select(i => new UserOrderViewModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total
                }).OrderByDescending(i => i.OrderDate).ToList();
            return PartialView(orders);
        }
        [Authorize]
        public ActionResult Details(int? id)
        {
            var entity = db.Orders.Where(i => i.Id == id)
                .Select(i => new OrderDetailsViewModel()
                {
                    Id = i.Id,
                    OrderNumber = i.OrderNumber,
                    OrderDate = i.OrderDate,
                    OrderState = i.OrderState,
                    Total = i.Total,
                    Adress = i.Adress,
                    City = i.City,
                    County = i.County,
                    Street = i.Street,
                    Items = i.Items.Select(e => new OrderItem()
                    {
                        ProductId = e.ProductId,
                        ProductName = e.Product.Name,
                        ImageUrl = e.Product.ImageUrl,
                        Quantity = e.Quantity,
                        Price = e.Price
                    }).ToList()
                }).FirstOrDefault();

            return View(entity);
        }

    }
}