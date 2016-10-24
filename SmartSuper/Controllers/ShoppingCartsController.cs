using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            return View(await db.ShoppingCarts.ToListAsync());
        }

        // GET: ShoppingCarts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = await db.ShoppingCarts.FindAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "ID,Paid")] ShoppingCarts shoppingCarts)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCarts.Add(shoppingCarts);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = await db.ShoppingCarts.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "ID,Paid")] ShoppingCarts shoppingCarts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCarts).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = await db.ShoppingCarts.FindAsync(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ShoppingCarts shoppingCarts = await db.ShoppingCarts.FindAsync(id);
            db.ShoppingCarts.Remove(shoppingCarts);
            await db.SaveChangesAsync();
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
