﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Orderline
    {
        // Legge inn regex m.m.?

        public int id { get; set; }
        public int orderID { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}