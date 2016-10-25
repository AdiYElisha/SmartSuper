using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartSuper.DAL;
using SmartSuper.Models;

namespace SmartSuper.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private SmartSuperContext5 db = new SmartSuperContext5();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["user"] != null)
            {
                int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;

                var ProductsShoppingCarts = from a in db.ProductsShoppingCarts select a;
                ProductsShoppingCarts = ProductsShoppingCarts.Where(x => x.ShoppingCartsID == Customer_ShoppingCart_ID);
                return View(ProductsShoppingCarts.ToList());
            }
            return View(db.ProductsShoppingCarts.ToList()); ;
        }

        // GET: ShoppingCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Paid")] ShoppingCarts shoppingCarts)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCarts.Add(shoppingCarts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Paid")] ShoppingCarts shoppingCarts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCarts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            db.ShoppingCarts.Remove(shoppingCarts);
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
    }
}
