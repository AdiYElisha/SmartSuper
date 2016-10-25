using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartSuper.Models
{
    public class ShoppingCarts
    {
        public ShoppingCarts()
        {
            //this.Products = new HashSet<Products>();
        }

        public int ID { get; set; }
        [Display(Name = "האם שולם")]
        [DefaultValue("false")]
        public bool Paid { get; set; }

        public virtual Customers Customer { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<ProductsShoppingCarts> ProductsShoppingCarts { get; set; }

    }
}