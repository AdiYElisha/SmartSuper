using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class ShoppingCart_Product
    {
        public int ShoppingCart_ID { get; set; }
        public int Product_ID { get; set; }
        public int Amount { get; set; }

    }
}