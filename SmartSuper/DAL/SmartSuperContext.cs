using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SmartSuper.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SmartSuper.DAL
{
    public class SmartSuperContext : DbContext
    {
        public SmartSuperContext() : base("SmartSuperContext5")
        { }

        public DbSet<FoodCompanies> FoodCompanies { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public DbSet<Products> Products { get; set; }
        //public DbSet<ProductType> ProductTypes { get; set; }
        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Supers> Supers { get; set; }

        public System.Data.Entity.DbSet<SmartSuper.Models.ProductKinds> ProductKinds { get; set; }

        //public System.Data.Entity.DbSet<SmartSuper.Models.Product> Products { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Tables name in RABIM 
        { 
        			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<SmartSuper.Models.ProductTypes> ProductTypes { get; set; }
    }
}