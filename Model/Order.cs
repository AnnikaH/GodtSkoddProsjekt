using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Order
    {
        // Legge inn regex m.m.?

        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Kunde-Id")]
        public int userID { get; set; }

        [Display(Name = "Tidspunkt")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }
        
        public List<Orderline> orderlines { get; set; }
    }
}