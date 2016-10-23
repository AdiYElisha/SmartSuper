﻿using System;
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
    public class CustomerController : Controller
    {
        private SmartSuperContext db = new SmartSuperContext();

        // GET: Customer
        public ActionResult Index()
        {
            if (db.Customer.Count() > 0)
            {
                ViewBag.MaxAge = db.Customer.Select(x => x.Age).Max();
            }

            ViewBag.City = new SelectList(db.Customer.Select(x => x.City).Distinct());

            var Customer = db.Customer.Where(x => x.FirstName != "admin");

            return View(Customer.ToList());
        }

        [HttpPost]
        public ActionResult Index(string FirstName, string LastName, int Age, string City, string Gender)
        {
            var Customer = from c in db.Customer select c;
            Customer = Customer.Where(x => x.Age <= Age);

            if (!string.IsNullOrEmpty(FirstName))
            {
                Customer = Customer.Where(x => x.FirstName == FirstName);
            }

            if (!string.IsNullOrEmpty(LastName))
            {
                Customer = Customer.Where(x => x.LastName == LastName);
            }

            if (!string.IsNullOrEmpty(City))
            {
                Customer = Customer.Where(x => x.City == City);
            }

            if (!string.IsNullOrEmpty(Gender))
            {
                Customer = Customer.Where(x => x.Gender == Gender);
            }

            ViewBag.City = new SelectList(db.Customer.Select(x => x.City).Distinct());
            ViewBag.MaxAge = db.Customer.Select(x => x.Age).Max();
            return View(Customer.ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customer.Find(id);
            
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customers customer)
        {
            if (ModelState.IsValid)
            {
                if (db.Customer.Where(c => c.Email == customer.Email).Count() > 0)
                {
                    ViewBag.ErrMsg = "כתובת האימייל שהוזנה כבר קיימת במערכת, אנא נסה כתובת מייל אחרת";
                }
                else
                {
                    db.Customer.Add(customer);
                    db.SaveChanges();
                    System.Web.HttpContext.Current.Session["user"] = customer;
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(customer);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var userInDataBase = db.Customer.Where(s =>
                        s.Email == email &&
                        s.Password == password).SingleOrDefault();
            if (userInDataBase != null)
            {
                System.Web.HttpContext.Current.Session["user"] = userInDataBase;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMsg = "שם משתמש או סיסמה שגויים.";
            return View();
        }

        public ActionResult Logoff()
        {
            System.Web.HttpContext.Current.Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customers customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.MyMessageToUsers = "השינויים נקלטו בהצלחה";
                return RedirectToAction("Details/" + customer.ID);
            }

            return View(customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
/*
        public ActionResult CustomerByBranch(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }

            var query = from c in db.Customer
                        join b in db.Branches on
                        c.Orders.Select(x => x.Album).Where(y => y.BranchID == id).FirstOrDefault().BranchID equals b.BranchId
                        where b.BranchId == id
                        select new BranchCustomerView
                        {
                            branchId = b.BranchId,
                            branchName = b.Name,
                            branchCity = b.City, 
                            firstName = c.FirstName, 
                            lastName = c.LastName, 
                            birthDate = c.BirthDate
                        };

            return View(query.ToList().Distinct());
        }*/

        public ActionResult GetFirstName(string term)
        {
            var firstNames = (from p in db.Customer where p.FirstName.Contains(term) select p.FirstName).Distinct().Take(10);

            return Json(firstNames, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLastName(string term)
        {
            var lastNames = (from p in db.Customer where p.LastName.Contains(term) select p.LastName).Distinct().Take(10);

            return Json(lastNames, JsonRequestBehavior.AllowGet);
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