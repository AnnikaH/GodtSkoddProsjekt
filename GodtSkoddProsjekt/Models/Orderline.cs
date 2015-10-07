using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GodtSkoddProsjekt.Models
{
    public class Orderline
    {
        public int id { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}