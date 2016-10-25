using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartSuper.Models
{
    public class ProductsShoppingCarts
    {
        [Key, Column(Order = 0)]
        public int ProductsID { get; set; }
        [Key, Column(Order = 1)]
        public int ShoppingCartsID { get; set; }

        public virtual Products Product { get; set; }
        public virtual ShoppingCarts ShoppingCart { get; set; }

        public int amount { get; set; }
    }
}