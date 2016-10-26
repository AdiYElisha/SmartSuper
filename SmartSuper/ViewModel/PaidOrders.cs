using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSuper.ViewModel
{
    public class PaidOrders
    {
        public int customer_ID { get; set; }
        public float Amount_Paid { get; set; }
        public string Customer_Name { get; set; }
    }
}