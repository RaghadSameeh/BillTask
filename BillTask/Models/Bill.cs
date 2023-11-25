﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillTask.Models
{
    public class Bill
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}