using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Supers
    {
        public int ID { get; set; }

        [Display(Name = "שם חנות")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Name { get; set; }
        
    }
}