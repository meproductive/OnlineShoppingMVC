using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingMVC.Models
{
    public class OrderViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string AdressTitle { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }
}