using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class FoodCompany
    {
        public int Id { get; set; }

        [Display(Name = "שם חברת אוכל")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Name { get; set; }
    }
}