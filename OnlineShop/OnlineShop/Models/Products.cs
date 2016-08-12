using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Products
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MaxStock { get; set; }
        public string Availability { get; set; }
        public string Store { get; set; }
        public string Image { get; set; }
        public string Config { get; set; }
    }
}