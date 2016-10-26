using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSuper.ViewModel
{
    public class ProductsOfShoppingCartsIDs
    {
        [Display(Name = "מספר זיהוי המוצר")]
        public int ProductsID { get; set; }
    }
}