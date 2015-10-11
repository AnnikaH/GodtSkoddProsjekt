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
        public String url { get; set; }
        public String gender { get; set; }  // Women, Men, Boys, Girls
        public String type { get; set; }    // Boots, DressShoes, Sandals, Sneakers, WinterShoes
    }
}