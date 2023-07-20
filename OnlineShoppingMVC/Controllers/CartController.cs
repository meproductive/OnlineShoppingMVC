using OnlineShoppingMVC.Context;
using OnlineShoppingMVC.Entity;
using OnlineShoppingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingMVC.Controllers
{
    public class CartController : Controller
    {
        private DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddToProduct(product, 1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }
        private Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public PartialViewResult Cart()
        {
            return PartialView(GetCart());
        }
        public PartialViewResult Quantity()
        {
            return PartialView(GetCart());
        }
        public ActionResult OrderCheckout()
        {
            return View(new OrderViewModel());
        }
        [HttpPost]
        public ActionResult OrderCheckout(OrderViewModel model)
        {
            var cart = GetCart();

            if (cart.Items.Count == 0)
            {
                ModelState.AddModelError("ZeroQuantity", "Sepette ürün yok");
            }
            if (ModelState.IsValid)
            {
                SaveOrder(cart, model);
                cart.Clear();
                return View("Completed");
            }
            return View(model);
        }
        private void SaveOrder(Cart cart, OrderViewModel model)
        {
            var order = new Order();
            order.OrderNumber = "A" + (new Random()).Next(11111, 99999).ToString();
            order.Total = cart.Total();
            order.OrderState = EnumOrderState.Waiting;
            order.OrderDate = DateTime.Now;
            order.UserName = User.Identity.Name;

            order.Name = model.Name;
            order.Surname = model.Surname;
            order.AdressTitle = model.AdressTitle;
            order.Adress = model.Adress;
            order.City = model.City;
            order.County = model.County;
            order.Street = model.Street;
            order.PostCode = model.PostCode;

            order.Items = new List<Entity.OrderItem>();

            foreach (var item in cart.Items)
            {
                var orderitem = new Entity.OrderItem();
                orderitem.ProductId = item.Product.Id;
                orderitem.Price = item.Quantity * item.Product.Price;
                orderitem.Quantity = item.Quantity;

                order.Items.Add(orderitem);
            }

            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}