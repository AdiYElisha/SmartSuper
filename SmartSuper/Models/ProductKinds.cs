using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class ProductKinds
    {
        public int ID { get; set; }

        [Display(Name = "שם סוג המוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Name { get; set; }

        [Display(Name = "תצלום של סוג מוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public string PictuerLocation { get; set; }

    }
}