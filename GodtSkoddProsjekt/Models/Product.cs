using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GodtSkoddProsjekt.Models
{
    public class Product
    {
        public int id { get; set; }
        public String name { get; set; }
        public double price { get; set; }
        public int size { get; set; }
        public String color { get; set; }
        public String material { get; set; }
        public String brand { get; set; }
    }
}