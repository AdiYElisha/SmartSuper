using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSuper.Controllers
{
    // Adi gay lala
    // Here they will see all the food companies, and if they press one of them, they will see all the products they sell
    public class FoodCompanyController : Controller
    {
        // GET: FoodCompany
        public ActionResult Index()
        {
            return View();
        }
    }
}