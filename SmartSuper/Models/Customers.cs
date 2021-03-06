﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class Customers
    {
        // The shopping carts of the customer
        public virtual ICollection<ShoppingCarts> ShoppingCarts { get; set; }
        public Customers()
        {
            ShoppingCarts = new List<ShoppingCarts>();
        }
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

        [Display(Name = "עיר")]
        public string City { get; set; }

        [Display(Name = "רחוב")]
        public string Street { get; set; }

        [Display(Name = "מספר טלפון")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "סל קניות נוכחי")]
        [DataType(DataType.PhoneNumber)]
        public int Current_Shoppingcart_ID { get; set; }

    }
}