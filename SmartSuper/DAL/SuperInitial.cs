using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartSuper.Models;

namespace SmartSuper.DAL
{
    public class SuperInitial : System.Data.Entity.DropCreateDatabaseIfModelChanges<SmartSuperContext5>
    {
        // Recreates the db with initial values
        protected override void Seed(SmartSuperContext5 context)
        {
            var Customers = new List<Customers>
            {

            new Customers { FirstName = "Adis", LastName = "Elisha", Age = 22, City = "KfarSaba", BirthDate = DateTime.Parse("1994-02-02"), Email = "Amflyomfg@gmail.com", Gender = "זכר", Password = "Aa123123", PhoneNumber = "0526630649", Street = "har boker 5" }
            };



            Customers.ForEach(s => context.Customer.Add(s));
            context.SaveChanges();

        }
    }
}