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
            // this works
            /*int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;

            var ProductsNames = from ProductsShoppingCart in db.ProductsShoppingCarts
                                where ProductsShoppingCart.ShoppingCartsID == Customer_ShoppingCart_ID
                                join Product in db.Products on ProductsShoppingCart.ProductsID equals Product.ID
                                join ProductType in db.ProductTypes on Product.ProductType_ID equals ProductType.ID
                                join FoodCompany in db.FoodCompanies on Product.FoodCompany_ID equals FoodCompany.Id
                            //    join SupersProducts in db.SupersProducts on Product.ID equals SupersProducts.ProductsID
                          //      join SupersProducts
                                select new ProductsOfShoppingCarts { ProductName = ProductType.Name , FoodCompanyName = FoodCompany.Name };
                                        
            return View(ProductsNames.ToList());
            */


            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;


            // Select Product_ID, ProductType_Name, FoodCompany_Name, LowestPrice from all supers
            /*
            var Products_By_TypeID_By_lowest_Price = from ProductsShoppingCart in db.ProductsShoppingCarts
                                                     where ProductsShoppingCart.ShoppingCartsID == Customer_ShoppingCart_ID
                                                     join Product in db.Products on ProductsShoppingCart.ProductsID equals Product.ID
                                                     join ProductType in db.ProductTypes on Product.ProductType_ID equals ProductType.ID
                                                     join FoodCompany in db.FoodCompanies on Product.FoodCompany_ID equals FoodCompany.Id
                                                     join superbyproducts in db.SupersProducts on Product.ID equals superbyproducts.ProductsID
                                                     select new ProductsBySupers { ProductID = ProductsShoppingCart.ProductsID, ProductName = ProductType.Name, FoodCompanyName = FoodCompany.Name, Price = superbyproducts.Price};
            */

            var Products_By_TypeID_By_lowest_Price = from productsshoppingcart in db.ProductsShoppingCarts
                                                     where productsshoppingcart.ShoppingCartsID == Customer_ShoppingCart_ID
                                                     join products in db.Products on productsshoppingcart.ProductsID equals products.ID
                                                     join producttypes in db.ProductTypes on products.ProductType_ID equals producttypes.ID
                                                     join foodcompanies in db.FoodCompanies on products.FoodCompany_ID equals foodcompanies.Id
                                                     join superbyproducts in db.SupersProducts on products.ID equals superbyproducts.ProductsID
                                                     select new ProductsBySupers { ProductID = products.ID, ProductName = producttypes.Name, FoodCompanyName = foodcompanies.Name, Price = superbyproducts.Price, Amount = productsshoppingcart.amount };
            

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


            //don't think I need it
            /*
                // Selects the shoppingCart Products I have to make the button active or deactive
                var Current_ShppingCart_Products = from ProductsShoppingCarts in db.ProductsShoppingCarts
                                                   where ProductsShoppingCarts.ShoppingCartsID == Customer_ShoppingCart_ID
                                                   select new ProductsOfShoppingCartsIDs { ProductsID = ProductsShoppingCarts.ProductsID };

                //Trying
                System.Web.HttpContext.Current.Session["Current_ShppingCart_Products"] = Current_ShppingCart_Products.ToList();
            */
            // Picks the lowest price product

            return View(Products_By_TypeID_By_lowest_Price.ToList());
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

        public ActionResult Orders(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Shows the all the customers who paid for shopping carts, and how much they paid.
            var All_Orders = from shoppingcarts in db.ShoppingCarts
                             where shoppingcarts.Paid == true
                             join customers in db.Customer on shoppingcarts.CustomerID equals customers.ID
                             select new PaidOrders {Customer_Name = customers.FirstName + customers.LastName, Amount_Paid = shoppingcarts.TotalPricePaid, customer_ID = shoppingcarts.CustomerID };

            return View(All_Orders.ToList());
        }

        public ActionResult AmountUp(int id)
        {
            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;
            var Current_product_shoppingcart = db.ProductsShoppingCarts.Where(z => z.ProductsID == id & z.ShoppingCartsID == Customer_ShoppingCart_ID).ToList().LastOrDefault();
            Current_product_shoppingcart.amount++;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult AmountDown(int id)
        {
            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;
            var Current_product_shoppingcart = db.ProductsShoppingCarts.Where(z => z.ProductsID == id & z.ShoppingCartsID == Customer_ShoppingCart_ID).ToList().LastOrDefault();
            Current_product_shoppingcart.amount--;
            if (Current_product_shoppingcart.amount == 0)
            {
                db.ProductsShoppingCarts.Remove(Current_product_shoppingcart);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // the ID is actually total to pay
        public ActionResult BuyCart(int ID)
        {
            int Current_Customer_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).ID;
            int Customer_ShoppingCart_ID = ((SmartSuper.Models.Customers)System.Web.HttpContext.Current.Session["user"]).Current_Shoppingcart_ID;
            var Current_ShoppingCart = db.ShoppingCarts.Find(Customer_ShoppingCart_ID);
            
            // Here, the customers pay money with credit card
            Current_ShoppingCart.Paid = true;
            Current_ShoppingCart.CustomerID = Current_Customer_ID;
            Current_ShoppingCart.TotalPricePaid = ID;
            db.SaveChanges();

            // Creating a new shopping cart
            ShoppingCarts Customer_New_Shopping_Cart = new ShoppingCarts();
            db.ShoppingCarts.Add(Customer_New_Shopping_Cart);
            db.SaveChanges();
            int Current_ShoppingCard_ID = db.ShoppingCarts
                                                 .OrderByDescending(p => p.ID)
                                                 .FirstOrDefault().ID;
            // updating the Customer's shopping cart
            
            var current_customer = db.Customer.Find(Current_Customer_ID);
            current_customer.Current_Shoppingcart_ID = Current_ShoppingCard_ID;
            db.SaveChanges();
            System.Web.HttpContext.Current.Session["user"] = current_customer;
            return RedirectToAction("Index");

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
