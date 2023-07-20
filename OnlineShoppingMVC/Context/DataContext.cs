using OnlineShoppingMVC.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Context
{
    public class DataContext:DbContext
    {
        public DataContext():base("SQL")
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}