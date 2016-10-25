using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Supers
    {
        public Supers()
        {
            //this.Products = new HashSet<Products>();
        }
        public int ID { get; set; }

        [Display(Name = "שם הרשת")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Name { get; set; }

        [Display(Name = "עיר")]
        [Required(ErrorMessage = "שדה חובה")]
        public string City { get; set; }

        [Display(Name = "רחוב")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Street { get; set; }

        [Display(Name = "מספר הרחוב")]
        [Required(ErrorMessage = "שדה חובה")]
        public int HouseNumber { get; set; }

        [Display(Name = "נקודת רוחב")]
        [Required(ErrorMessage = "שדה חובה")]
        public double CoordX { get; set; }

        [Display(Name = "נקודת אורך")]
        [Required(ErrorMessage = "שדה חובה")]
        public double CoordY { get; set; }

        [Display(Name = "מספר טלפון")]
        public string PhoneNumber { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<SupersProducts> SupersProducts { get; set; }
    }
}