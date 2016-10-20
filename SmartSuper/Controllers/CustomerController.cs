using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartSuper.Models;
using SmartSuper.DAL;


namespace SmartSuper.Controllers
{
    public class CustomerController : Controller
    {
        private SmartSuperContext db = new SmartSuperContext();
        // GET: Customer
        public ActionResult Index()
        {
            //Hello Kobi
            //second hello kobi
            return View(db.Customer.ToList());
        }

     //   public ActionResult Index(string FirstName, string LastName, int Age, string City, string Gender)
       // {
         //   return View();
        //}

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            return View();
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        //public ActionResult Create(Customer customer)
        //{
            //return View();
        //}

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            return View();
        }

        public ActionResult Logoff()
        {
            return View();
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            return View();
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            return View();
        }

        public ActionResult GetFirstName(string term)
        {
            return View();
        }

        public ActionResult GetLastName(string term)
        {
            return View();
        }

        //wtf is this?
        protected override void Dispose(bool disposing)
        {

        }

    }
}