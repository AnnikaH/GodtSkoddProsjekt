using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Product
    {
        public int id { get; set; }

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå\s]{2,30}", ErrorMessage = "Navn kan bare inneholde bokstaver fra A-Å")]
        public String name { get; set; }

        [Display(Name = "Pris")]
        [Required(ErrorMessage = "Pris må oppgis")]
        [RegularExpression(@"^[0-9]{1,8}([.][0-9]{1,3})?$", ErrorMessage = "Pris kan bare inneholde siffer og eventuelt desimalpunktum etterfulgt av desimaler")]
        public double price { get; set; }

        [Display(Name = "Størrelse")]
        [Required(ErrorMessage = "Størrelse må oppgis")]
        [RegularExpression(@"[0-9]{1,2}", ErrorMessage = "Størrelse kan bare inneholde 1 eller 2 siffer")]
        public int size { get; set; }

        [Display(Name = "Farge")]
        [Required(ErrorMessage = "Farge må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå\s]{2,30}", ErrorMessage = "Farge kan bare inneholde bokstaver fra A-Å")]
        public String color { get; set; }

        [Display(Name = "Materiale")]
        [Required(ErrorMessage = "Materiale må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå\s]{2,30}", ErrorMessage = "Materiale kan bare inneholde bokstaver fra A-Å")]
        public String material { get; set; }

        [Display(Name = "Merke")]
        [Required(ErrorMessage = "Merke må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå\s]{2,30}", ErrorMessage = "Merke kan bare inneholde bokstaver fra A-Å")]
        public String brand { get; set; }

        [Display(Name = "Bilde-url")]
        //[Required(ErrorMessage = "Bilde-url må oppgis")]
        //[RegularExpression(@"[A-ZÆØÅa-zæøå\s]{2,30}", ErrorMessage = "Bilde-url kan bare inneholde bokstaver fra A-Å")]
        public String url { get; set; }

        [Display(Name = "For hvem")]
        [Required(ErrorMessage = "For hvem må oppgis")]
        [RegularExpression(@"Women|Men|Boys|Girls", ErrorMessage = "For hvem kan bare være Women, Men, Boys eller Girls")]
        public String gender { get; set; }  // Women, Men, Boys, Girls

        [Display(Name = "Type sko")]
        [Required(ErrorMessage = "Type sko må oppgis")]
        [RegularExpression(@"Boots|DressShoes|Sandals|Sneakers|WinterShoes", ErrorMessage = "Type sko kan bare være Boots, DressShoes, Sandals, Sneakers eller WinterShoes")]
        public String type { get; set; }    // Boots, DressShoes, Sandals, Sneakers, WinterShoes
    }
}