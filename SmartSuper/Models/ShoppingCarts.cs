using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartSuper.Models
{
    public class ShoppingCarts
    {

        public int ID { get; set; }
        [Display(Name = "האם שולם")]
        [DefaultValue("false")]
        public bool Paid { get; set; }
    }
}