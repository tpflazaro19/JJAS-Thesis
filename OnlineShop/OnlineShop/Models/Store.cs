using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Store
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Distance { get; set; }
        public string Phone { get; set; }
    }
}