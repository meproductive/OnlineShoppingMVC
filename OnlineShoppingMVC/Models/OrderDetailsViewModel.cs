using OnlineShoppingMVC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Models
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public EnumOrderState OrderState { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public virtual List<OrderItem> Items { get; set; }
    }
    public class OrderItem
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
    }
}
