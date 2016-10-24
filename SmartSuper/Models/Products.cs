using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Products
    {
        public Products()
        {
            this.ShoppingCarts = new HashSet<ShoppingCarts>();
            this.Supers = new HashSet<Supers>();
        }
        public int ID { get; set; }

        [Display(Name = "מספר זיהוי של סוג המוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public int ProductType_ID { get; set; }

        [Display(Name = "מספר זיהוי של חברת האוכל")]
        [Required(ErrorMessage = "שדה חובה")]
        public string FoodCompany_ID { get; set; }

        [Display(Name = "משקל המוצר")]
        [Required(ErrorMessage = "שדה חובה")]
        public int Weight { get; set; }

        [Display(Name = "מחיר ל-100 גרם")]
        [Required(ErrorMessage = "שדה חובה")]
        public float Price_For_100_Gram { get; set; }

        [Display(Name = "במבצע")]
        public bool Deal_Bool { get; set; }

        [Display(Name = "מחיר ל-100 גרם במבצע")]
        public float Price_For_100_Gram_In_Deal { get; set; }

        [Display(Name = "מה הכלל כדי שהמבצע יהיה תקף")]
        public string Condition_For_Deal { get; set; }

        [Display(Name = "כמות בשביל שהמבצע יהיה תקף")]
        public int Amount_Condition_For_Deal { get; set; }
        public virtual ICollection<ShoppingCarts> ShoppingCarts { get; set; }
        public virtual ICollection<Supers> Supers { get; set; }

    }
}