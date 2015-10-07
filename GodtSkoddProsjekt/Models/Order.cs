using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GodtSkoddProsjekt.Models
{
    public class Order
    {
        public int id { get; set; }
        public int userID { get; set; }
        public DateTime date { get; set; }
        public List<Orderline> orderlines { get; set; }
    }
}