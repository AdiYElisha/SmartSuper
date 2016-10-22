using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Product_Super
    {
        public int Product_ID { get; set; }
        public int Super_ID { get; set; }
        public int Price { get; set; }
    }
}