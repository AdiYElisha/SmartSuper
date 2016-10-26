using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSuper.ViewModel
{
    public class ProductsBySupers
    {
        public int ProductID { get; set; }
        [Display(Name = "שם המוצר")]
        public string ProductName { get; set; }

        [Display(Name = "שם חברת האוכל")]
        public string FoodCompanyName { get; set; }

        [Display(Name = "המחיר הזול ביותר")]
        public float Price { get; set; }
        [Display(Name = "כמות")]
        public int Amount { get; set; }
    }
}