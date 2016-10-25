using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSuper.ViewModel
{
    public class ProductsOfShoppingCarts
    {
        [Display(Name = "שם המוצר")]
        public string ProductName { get; set; }
        [Display(Name = "שם החברה")]
        public string FoodCompanyName { get; set; }
    }
}