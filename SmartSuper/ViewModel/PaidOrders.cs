using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSuper.ViewModel
{
    public class PaidOrders
    {
        [Display(Name = "מספר זיהוי הלקוח")]
        public int customer_ID { get; set; }
        [Display(Name = "סכום הזמנה")]
        public float Amount_Paid { get; set; }
        [Display(Name = "שם הלקוח")]
        public string Customer_Name { get; set; }
    }
}