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
using System.IO;
using System.Drawing;

namespace SmartSuper.Controllers
{
    public class ProductKindsController : Controller
    {
        private SmartSuperContext5 db = new SmartSuperContext5();

        // GET: ProductKinds
        public ActionResult Index()
        {
            return View(db.ProductKinds.ToList());
        }

        [HttpPost]
        public ActionResult Index(string ProductKind)
        {
            var ProudctKindVar = from a in db.ProductKinds select a;
     
            if (!string.IsNullOrEmpty(ProductKind))
            {
                ProudctKindVar = ProudctKindVar.Where(x => x.Name == ProductKind);
            }

            return View(ProudctKindVar.ToList());
        }

        // GET: ProductKinds/Details/5
        // Should make a search button
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductKinds productKinds = db.ProductKinds.Find(id);
            if (productKinds == null)
            {
                return HttpNotFound();
            }
            return View(productKinds);
        }

        // GET: ProductKinds/Create
        public ActionResult Create()
        {
            ViewBag.ProductKindsID = new SelectList(db.ProductKinds, "ID", "Name");
            return View();
        }

        // POST: ProductKinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductKinds productKinds, HttpPostedFileBase PictuerLocation)
        {
            var path = "";
            var physicalPath = "";

            if (PictuerLocation == null)
            {
                physicalPath = "/Picture/no_picture_to_show.jpg";
            }

            if (PictuerLocation != null && PictuerLocation.ContentLength > 0)
            {
                var FileName = string.Format("{0}.{1}", Guid.NewGuid(), "jpg");
                path = Path.Combine(Server.MapPath("~/Picture"), FileName);

                Size szDimensions = new Size(340, 300);
                Bitmap resizedImg = new Bitmap(Image.FromStream(PictuerLocation.InputStream), szDimensions.Width, szDimensions.Height);

                physicalPath = "/Picture/" + FileName;
                resizedImg.Save(path);
            }

            productKinds.PictuerLocation = physicalPath;

            if (productKinds.PictuerLocation != null)
            {
                db.ProductKinds.Add(productKinds);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(productKinds);                  
        }

        // GET: ProductKinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductKinds productKinds = db.ProductKinds.Find(id);

            if (productKinds == null)
            {
                return HttpNotFound();
            }

            ViewBag.picture = productKinds.PictuerLocation;
            return View(productKinds);
        }

        // POST: ProductKinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductKinds productKinds, HttpPostedFileBase file)
        {
            var physicalPath = "";

            if (file != null && file.ContentLength > 0)
            {
                var FileName = string.Format("{0}.{1}", Guid.NewGuid(), "jpg");
                var path = Path.Combine(Server.MapPath("~/Picture"), FileName);

                Size szDimensions = new Size(340, 300);
                Bitmap resizedImg = new Bitmap(Image.FromStream(file.InputStream), szDimensions.Width, szDimensions.Height);

                physicalPath = "/Picture/" + FileName;
                resizedImg.Save(path);

            }
            if (physicalPath != "")
            {
                productKinds.PictuerLocation = physicalPath;
            }

            if (ModelState.IsValid)
            {
                db.Entry(productKinds).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productKinds);
        }


        // GET: ProductKinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductKinds ProductKind = db.ProductKinds.Find(id);

            if (ProductKind == null)
            {
                return HttpNotFound();
            }

            return View(ProductKind);
        }

        // POST: ProductKinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductKinds ProductKind = db.ProductKinds.Find(id);
            db.ProductKinds.Remove(ProductKind);
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

        public ActionResult GetProductKind(string term)
        {
            var ProductKind = (from p in db.ProductKinds where p.Name.Contains(term) select p.Name).Distinct().Take(10);

            return Json(ProductKind, JsonRequestBehavior.AllowGet);
        }
    }
}
