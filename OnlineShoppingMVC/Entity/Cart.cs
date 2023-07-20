using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Entity
{
    public class Cart
    {

        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }
        public void AddToProduct(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item == null)
            {
                Items.Add(new CartItem() { Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }
        public void DeleteProduct(Product product)
        {
            Items.RemoveAll(i => i.Product.Id == product.Id);
        }
        public double Total()
        {
            return Items.Sum(i => i.Product.Price * i.Quantity);
        }
        public void Clear()
        {
            Items.Clear();
        }
    }
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
}