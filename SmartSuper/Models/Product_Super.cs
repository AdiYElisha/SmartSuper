using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartSuper.Models
{
    public class Product_Super
    {
        [Key]
        [Column(Order = 1)]
        public int Product_ID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Super_ID { get; set; }
        [Display(Name = "מחיר המוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public int Price { get; set; }
    }
}