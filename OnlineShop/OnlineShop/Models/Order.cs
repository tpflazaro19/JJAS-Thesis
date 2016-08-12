using System;

namespace OnlineShop.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Item { get; set; }
        public string Config { get; set; }
        public decimal Price { get; set; }
        public string OrderType { get; set; }
        public int Quantity { get; set; }
        public string Store { get; set; }
        public DateTime PurchaseDate { get; set; }
        // Status: pending, pickup, reserved, delivery
        public string Status { get; set; }
    }
}