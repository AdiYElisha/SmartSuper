﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SmartSuper.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }

        public bool Paid { get; set; }
    }
}