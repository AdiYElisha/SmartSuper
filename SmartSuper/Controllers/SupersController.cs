using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartSuper.Models;
using SmartSuper.DAL;

namespace SmartSuper.Controllers
{
    public class SupersController : Controller
    {
        private SmartSuperContext5 db = new SmartSuperContext5();

        // GET: /super/
        public ActionResult Index()
        {
            return View(db.Supers.ToList());
        }

        [HttpPost]
        public ActionResult Index(string superName, string City)
        {
            var superes = from b in db.Supers select b;

            if (!string.IsNullOrEmpty(superName))
            {
                superes = superes.Where(x => x.Name == superName);
            }

            if (!string.IsNullOrEmpty(City))
            {
                superes = superes.Where(x => x.City == City);
            }

            return View(superes.ToList());
        }

        // GET: /super/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supers super = db.Supers.Find(id);
            if (super == null)
            {
                return HttpNotFound();
            }
            return View(super);
        }

        // GET: /super/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /super/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supers super)
        {
            if (ModelState.IsValid)
            {
                db.Supers.Add(super);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(super);
        }

        // GET: /super/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supers super = db.Supers.Find(id);
            if (super == null)
            {
                return HttpNotFound();
            }
            return View(super);
        }

        // POST: /super/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supers super)
        {
            if (ModelState.IsValid)
            {
                db.Entry(super).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(super);
        }

        // GET: /super/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supers super = db.Supers.Find(id);
            if (super == null)
            {
                return HttpNotFound();
            }
            return View(super);
        }

        // POST: /super/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supers super = db.Supers.Find(id);
            db.Supers.Remove(super);
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

        public ActionResult GetSuperName(string term)
        {
            var SuperName = (from p in db.Supers where p.Name.Contains(term) select p.Name).Distinct().Take(10);

            return Json(SuperName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCityName(string term)
        {
            var CityName = (from p in db.Supers where p.City.Contains(term) select p.City).Distinct().Take(10);

            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
    }
}
