using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public int StoreID { get; set; }
        public int ProductID { get; set; }
        public int NumStock { get; set; }
    }
}