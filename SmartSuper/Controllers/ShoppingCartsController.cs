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
using SmartSuper.ViewModel;

namespace SmartSuper.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private SmartSuperContext23 db = new SmartSuperContext23();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;

            // Selecting all the products in our shopping carts

            //var ProductsShoppingCarts = from a in db.ProductsShoppingCarts select a;
            //ProductsShoppingCarts = ProductsShoppingCarts.Where(x => x.ShoppingCartsID == Customer_ShoppingCart_ID);

            // Translating all the products_IDs to Products_names

            //var Products = from a in db.ProductsShoppingCarts join ProductsShoppingCarts select a;

            var ProductsNames = from ProductsShoppingCart in db.ProductsShoppingCarts
                                where ProductsShoppingCart.ShoppingCartsID == Customer_ShoppingCart_ID
                                join Product in db.Products on ProductsShoppingCart.ProductsID equals Product.ID
                                join ProductType in db.ProductTypes on Product.ProductType_ID equals ProductType.ID
                                join FoodCompany in db.FoodCompanies on Product.FoodCompany_ID equals FoodCompany.Id
                                select new ProductsOfShoppingCarts { ProductName = ProductType.Name, FoodCompanyName = FoodCompany.Name };
                                        
            return View(ProductsNames.ToList());
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
