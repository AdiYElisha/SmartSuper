using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSuper.ViewModel
{
    public class Customer_Shoppingcart_Stats
    {
        public int Customer_ID { get; set; }

        [Display(Name = "כמות הסלים שנקנו")]
        public int Count { get; set; }
    }
}