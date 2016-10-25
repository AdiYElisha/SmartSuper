using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Products
    {
        public Products()
        {
            //this.ShoppingCarts = new HashSet<ShoppingCarts>();
            //this.Supers = new HashSet<Supers>();
        }
        public int ID { get; set; }

        [Display(Name = "מספר זיהוי של סוג המוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public int ProductType_ID { get; set; }

        [Display(Name = "מספר זיהוי של חברת האוכל")]
        [Required(ErrorMessage = "שדה חובה")]
        public int FoodCompany_ID { get; set; }

        //public virtual ICollection<ShoppingCarts> ShoppingCarts { get; set; }
        public virtual ICollection<ProductsShoppingCarts> ProductsShoppingCarts { get; set; }
        //public virtual ICollection<Supers> Supers { get; set; }
        public virtual ICollection<SupersProducts> SupersProducts { get; set; }

    }
}