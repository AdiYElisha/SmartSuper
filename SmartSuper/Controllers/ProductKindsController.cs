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
    public class ProductKindsController : Controller
    {
        private SmartSuperContext db = new SmartSuperContext();

        // GET: ProductKinds
        public ActionResult Index()
        {
            return View(db.ProductKinds.ToList());
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
