using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GodtSkoddProsjekt.Models
{
    public class LoginUser
    {
        /*[Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]*/
        public String userName { get; set; }

        /*[Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]*/
        public String password { get; set; }
    }
}