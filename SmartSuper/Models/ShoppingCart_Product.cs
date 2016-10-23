using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSuper.Models
{
    public class ShoppingCart_Product
    {
        [Key]
        [Column(Order = 1)]
        public int ShoppingCart_ID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Product_ID { get; set; }
        [Display(Name = "כמות מהמוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public int Amount { get; set; }

    }
}