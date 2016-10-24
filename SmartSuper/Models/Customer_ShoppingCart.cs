using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace SmartSuper.Models
{
    public class Customer_ShoppingCart
    {
        [Key]
        [Column(Order = 1)]
        public int Customer_ID { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ShoppingCart_ID { get; set; }
        [Display(Name = "האם שולם")]
        [Required(ErrorMessage = "שדה חובה")]
        [DefaultValue("False")]
        public bool PaidBool { get; set; }
    }
}