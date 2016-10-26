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
    public class ProductsController : Controller
    {
        private SmartSuperContext23 db = new SmartSuperContext23();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        [HttpGet]
        public ActionResult GetListByID(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;
            //var Current_ShppingCart_Products = db.ProductsShoppingCarts.Where(s => s.ShoppingCartsID == Customer_ShoppingCart_ID);
            //System.Web.HttpContext.Current.Session["Current_ShppingCart_Products"] = Current_ShppingCart_Products;



            // Select Product_ID, ProductType_Name, FoodCompany_Name, LowestPrice from all supers
            var Products_By_TypeID_By_lowest_Price = from products in db.Products
                                                     where products.ProductType_ID == ID
                                                     join producttypes in db.ProductTypes on products.ProductType_ID equals producttypes.ID
                                                     join foodcompanies in db.FoodCompanies on products.FoodCompany_ID equals foodcompanies.Id
                                                     join superbyproducts in db.SupersProducts on products.ID equals superbyproducts.ProductsID
                                                     select new ProductsBySupers { ProductID = products.ID, ProductName = producttypes.Name, FoodCompanyName = foodcompanies.Name, Price = superbyproducts.Price };

            var Products_By_TypeID_By_lowest_Price2 = from products in db.Products
                                                     where products.ProductType_ID == ID
                                                     join producttypes in db.ProductTypes on products.ProductType_ID equals producttypes.ID
                                                     join foodcompanies in db.FoodCompanies on products.FoodCompany_ID equals foodcompanies.Id
                                                     join superbyproducts in db.SupersProducts on products.ID equals superbyproducts.ProductsID
                                                     group superbyproducts
                                                     by products.ID into results
                                                     select new ProductsBySupers {Price = results.Min(z => z.Price )};

            System.Web.HttpContext.Current.Session["Products_By_Prices2"] = Products_By_TypeID_By_lowest_Price2;
            float[] products_by_prices = new float[5000];

            foreach (var item in Products_By_TypeID_By_lowest_Price)
            {
                if (products_by_prices[item.ProductID] == 0)
                {
                    products_by_prices[item.ProductID] = item.Price;
                }
                else
                {
                    if (products_by_prices[item.ProductID] > item.Price)
                    {
                        products_by_prices[item.ProductID] = item.Price;
                    }
                }
            }

            System.Web.HttpContext.Current.Session["Products_By_Prices"] = products_by_prices;



            // Selects the shoppingCart Products I have to make the button active or deactive
            var Current_ShppingCart_Products = from ProductsShoppingCarts in db.ProductsShoppingCarts
                                               where ProductsShoppingCarts.ShoppingCartsID == Customer_ShoppingCart_ID
                                               select new ProductsOfShoppingCartsIDs { ProductsID = ProductsShoppingCarts.ProductsID };

            //Trying
            System.Web.HttpContext.Current.Session["Current_ShppingCart_Products"] = Current_ShppingCart_Products.ToList();

            // Picks the lowest price product

            return View(Products_By_TypeID_By_lowest_Price.ToList());


            // trying different approach
            // Products by the ID of the ProductType ID
            //var ProductsVar = from a in db.Products select a;
            //ProductsVar = ProductsVar.Where(x => x.ProductType_ID == ID);
            //return View(ProductsVar.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductType_ID,FoodCompany_ID,Weight,Price_For_100_Gram,Deal_Bool,Price_For_100_Gram_In_Deal,Condition_For_Deal,Amount_Condition_For_Deal")] Products product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductType_ID,FoodCompany_ID,Weight,Price_For_100_Gram,Deal_Bool,Price_For_100_Gram_In_Deal,Condition_For_Deal,Amount_Condition_For_Deal")] Products product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Id)
        {
            Products product = db.Products.Find(Id);
            db.Products.Remove(product);
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

        [HttpPost]
        public JsonResult addProductToCart(int Id)
        {
            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;
            //var Current_ShppingCart_Products = db.ProductsShoppingCarts.Where(s => s.ShoppingCartsID == Customer_ShoppingCart_ID);
            //System.Web.HttpContext.Current.Session["Current_ShppingCart_Products"] = Current_ShppingCart_Products;

            



            ProductsShoppingCarts productShoppingCarts = new ProductsShoppingCarts();

            productShoppingCarts.ProductsID = Id;
            productShoppingCarts.ShoppingCartsID = Customer_ShoppingCart_ID;
            //productShoppingCarts.amount

            db.ProductsShoppingCarts.Add(productShoppingCarts);
            db.SaveChanges();
               
            return Json(true);    
 
        }

        public JsonResult DeleteProductFromCart(int id = 0)
        {
            List<int> cart = (List<int>)System.Web.HttpContext.Current.Session["shoppingCart"];
            cart.Remove(id);
            return Json(cart.Count);
        }
    }
}
