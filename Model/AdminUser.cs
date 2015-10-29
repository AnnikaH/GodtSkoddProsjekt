using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class AdminUser
    {
        public int id { get; set; }

        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå0-9]{2,30}", ErrorMessage = "Brukernavn kan bare inneholde bokstaver fra A-Å og siffer")]
        public String userName { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        [DataType(DataType.Password)]
        [RegularExpression(@"[A-ZÆØÅa-zæøå0-9!#$%&'*+\-/=?\^_`{|}~+(\.]{8,30}", ErrorMessage = "Passord må inneholde minst 8 tegn")]
        public String password { get; set; }
    }
}