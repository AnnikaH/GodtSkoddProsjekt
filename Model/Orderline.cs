using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Orderline
    {
        // Legge inn regex m.m.?

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Ordre-Id")]
        public int orderID { get; set; }

        [Display(Name = "Produkt-Id")]
        public int productId { get; set; }

        [Display(Name = "Antall")]
        public int quantity { get; set; }
    }
}