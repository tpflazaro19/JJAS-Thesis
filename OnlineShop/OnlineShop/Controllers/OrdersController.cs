using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using System.Data.Entity.Migrations;

namespace OnlineShop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Image,Item,Config,Price,OrderType,Quantity,Store,PurchaseDate,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Image,Item,Config,Price,OrderType,Quantity,Store,PurchaseDate,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Orders/Checkout
        public ActionResult Checkout()
        {
            // Select items from ShoppingCarts
            var items_process = db.ShoppingCarts.ToList();
            int quantity = 0;
            decimal subTotal = 0;

            foreach (var item in items_process)
            {
                // Check for duplicate order first
                var qty = from order in db.Orders
                          where order.Item == item.Item && order.OrderType == item.OrderType && order.Config == item.Config && order.Store == item.Store
                          select order;
                if (qty != null)
                {
                    foreach (var x in qty)
                    {
                        quantity = x.Quantity + item.Quantity;
                        subTotal = x.Price + item.Price;
                    }
                }
                else
                {
                    quantity = item.Quantity;
                }

                db.Orders.AddOrUpdate(
                    i => new { i.Item, i.Config, i.Store, i.OrderType },
                    new Order
                    {
                        Item = item.Item,
                        OrderType = item.OrderType,
                        Config = item.Config,
                        Price = subTotal,
                        Quantity = quantity,
                        Store = item.Store,
                        PurchaseDate = DateTime.Now,
                        Status = "pending"
                    }
                    );
                // Delete processed orders from ShoppingCarts
                db.ShoppingCarts.Remove(item);

                db.SaveChanges();
            }
            

            return View(db.Orders.ToList());
        }

        // GET: Orders/Reserve
        public ActionResult Reserve()
        {
            // Select items from ShoppingCarts
            var items_process = db.ShoppingCarts.ToList();
            int quantity = 0;
            decimal subTotal = 0;

            foreach (var item in items_process)
            {
                // Check for duplicate order first
                var qty = from order in db.Orders
                          where order.Item == item.Item && order.OrderType == item.OrderType && order.Config == item.Config && order.Store == item.Store
                          select order;
                if (qty != null)
                {
                    foreach (var x in qty)
                    {
                        quantity = x.Quantity + item.Quantity;
                        subTotal = x.Price + item.Price;
                    }
                }
                else
                {
                    quantity = item.Quantity;
                }

                db.Orders.AddOrUpdate(
                    i => new { i.Item, i.Config, i.Store, i.OrderType },
                    new Order
                    {
                        Item = item.Item,
                        OrderType = item.OrderType,
                        Config = item.Config,
                        Price = subTotal,
                        Quantity = quantity,
                        Store = item.Store,
                        PurchaseDate = DateTime.Now,
                        Status = "pending"
                    }
                    );
                // Delete processed orders from ShoppingCarts
                db.ShoppingCarts.Remove(item);

                db.SaveChanges();
            }


            return View(db.Orders.ToList());
        }

        // GET: Orders/Receipt
        public ActionResult Receipt()
        {
            return View(db.Orders.ToList());
        }
    }
}
