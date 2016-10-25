using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SmartSuper.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SmartSuper.DAL
{
    public class SmartSuperContext5 : DbContext
    {
        public SmartSuperContext5() : base("SmartSuperContext21")
        { }

        public DbSet<FoodCompanies> FoodCompanies { get; set; }
        public DbSet<Customers> Customer { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Supers> Supers { get; set; }
        public DbSet<ProductsShoppingCarts> ProductsShoppingCarts { get; set; }

        public System.Data.Entity.DbSet<SmartSuper.Models.ProductKinds> ProductKinds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) // Tables name in RABIM 
        { 
        			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<SmartSuper.Models.ProductTypes> ProductTypes { get; set; }

        public System.Data.Entity.DbSet<SmartSuper.Models.ShoppingCarts> ShoppingCarts { get; set; }
    }
}