using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartSuper.Models
{
    public class SupersProducts
    {
        [Key, Column(Order = 0)]
        public int SupersID { get; set; }
        [Key, Column(Order = 1)]
        public int ProductsID { get; set; }

        [Display(Name = "מחיר")]
        [Required(ErrorMessage = "שדה חובה")]
        public float Price { get; set; }

        public virtual Supers Super { get; set; }
        public virtual Products Product { get; set; }

    }
}