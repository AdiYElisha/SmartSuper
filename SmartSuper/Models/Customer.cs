using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Customer
    {
        public int ID { get; set; }

        [Display(Name = "שם פרטי")]
        [Required(ErrorMessage = "שדה חובה")]
        public string FirstName { get; set; }

        [Display(Name = "שם משפחה")]
        [Required(ErrorMessage = "שדה חובה")]
        public string LastName { get; set; }

        [Display(Name = "תאריך לידה")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "שדה חובה")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "מין")]
        public string Gender { get; set; }

        [Display(Name = "אימייל")]
        [Required(ErrorMessage = "שדה חובה")]
        [EmailAddress(ErrorMessage = "הכתובת אינה כתובה אימייל תקינה")]
        public string Email { get; set; }

        [Display(Name = "סיסמה")]
        [Required(ErrorMessage = "שדה חובה")]
        [StringLength(100, ErrorMessage = "הסיסמה צריכה להכיל מינימום 5 תווים", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "גיל")]
        [Required(ErrorMessage = "שדה חובה")]
        [Range(12, 120, ErrorMessage = "גיל בטווח 12-120 בלבד")]
        public int Age { get; set; }

        [Display(Name = "עיר")]
        public string City { get; set; }

        [Display(Name = "רחוב")]
        public string Street { get; set; }

        [Display(Name = "מספר טלפון")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public virtual ICollection<ShoppingCart> Orders { get; set; }

        [Display(Name = "מספר זיהוי של העגלה הנוכחית")]
        [Required(ErrorMessage = "שדה חובה")]
        public string Current_ShoppingCart_ID { get; set; }


    }
}