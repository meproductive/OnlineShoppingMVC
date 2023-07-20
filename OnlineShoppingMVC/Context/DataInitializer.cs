using OnlineShoppingMVC.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace OnlineShoppingMVC.Context
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var categories = new List<Category>()
            {
                new Category(){Name="Home", Description="Home Products"}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var products = new List<Product>()
            {
                new Product(){Name="T-shirt", Description="Good T-shirt", Price=149.99, Stock=100, CategoryId=1, ImageUrl="t-shirtb.jpg", Color="White", Popular=true, Brand="Nice" }
            };
            context.Products.AddRange(products);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}