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

        [Display(Name = "Ordre Id")]
        public int orderID { get; set; }

        [Display(Name = "Produkt Id")]
        [Required(ErrorMessage = "Produkt-Id må oppgis")]
        [RegularExpression(@"[0-9]{1,10}", ErrorMessage = "Produkt-ID kan bare bestå av siffer")]
        public int productId { get; set; }

        [Display(Name = "Antall")]
        [Required(ErrorMessage = "Antall av dette produktet må oppgis")]
        [RegularExpression(@"[0-9]{1,10}", ErrorMessage = "Antall kan bare bestå av siffer")]
        public int quantity { get; set; }
    }
}