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
    public class ProductTypesController : Controller
    {
        private SmartSuperContext23 db = new SmartSuperContext23();

        // GET: ProductTypes
        public ActionResult Index()
        {
            return View(db.ProductTypes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string ProductType)
        {
            var ProudctTypeVar = from a in db.ProductTypes select a;

            if (!string.IsNullOrEmpty(ProductType))
            {
                ProudctTypeVar = ProudctTypeVar.Where(x => x.Name == ProductType);
            }

            return View(ProudctTypeVar.ToList());
        }

        [HttpGet]
        public ActionResult GetListByID(int? ID)
        {
            if (ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ProductTypeVar = from a in db.ProductTypes select a;
            ProductTypeVar = ProductTypeVar.Where(x => x.ProductKind_ID == ID);
            return View(ProductTypeVar.ToList());
        }

        [HttpPost]
        public ActionResult GetListByID(string ProductType)
        {
            var ProudctTypeVar = from a in db.ProductTypes select a;

            if (!string.IsNullOrEmpty(ProductType))
            {
                ProudctTypeVar = ProudctTypeVar.Where(x => x.Name == ProductType);
            }

            return View(ProudctTypeVar.ToList());
        }
    
        // GET: ProductTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTypes productTypes = db.ProductTypes.Find(id);
            if (productTypes == null)
            {
                return HttpNotFound();
            }
            return View(productTypes);
        }

        // GET: ProductTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductKind_ID,Name")] ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                db.ProductTypes.Add(productTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productTypes);
        }

        // GET: ProductTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTypes productTypes = db.ProductTypes.Find(id);
            if (productTypes == null)
            {
                return HttpNotFound();
            }
            return View(productTypes);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductKind_ID,Name")] ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productTypes);
        }

        // GET: ProductTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductTypes productTypes = db.ProductTypes.Find(id);
            if (productTypes == null)
            {
                return HttpNotFound();
            }
            return View(productTypes);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTypes productTypes = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(productTypes);
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

        public ActionResult GetProductType(string term)
        {
            var ProductType = (from p in db.ProductTypes where p.Name.Contains(term) select p.Name).Distinct().Take(10);

            return Json(ProductType, JsonRequestBehavior.AllowGet);
        }
    }
}
