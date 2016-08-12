using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Item { get; set; }
        public string Config { get; set; }
        public decimal Price { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public string Store { get; set; }
    }
}