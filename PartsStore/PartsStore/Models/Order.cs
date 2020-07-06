﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartsStore.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }   
        public string Line3 { get; set; }
        public string City { get; set; }
        public bool GiftWrap { get; set; }
        public bool Dispatcher { get; set; }
        public virtual List<OrderLine> OrderLines { get; set; }

       
    }
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public Order Order { get; set; }
        public Details Details { get; set; }
        public int Quantity { get; set; }

    }
    
}