using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GodtSkoddProsjekt.Models
{
    public class User
    {
        // Dette er både en domenemodell og en view-modell
        public int id { get; set; }

        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå]{2,30}", ErrorMessage = "Fornavn kan bare inneholde bokstaver fra A-Å")]
        public String firstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå]{2,30}", ErrorMessage = "Etternavn kan bare inneholde bokstaver fra A-Å.")]
        public String lastName { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adressen må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå0-9\s]{2,30}", ErrorMessage = "Adresse kan bare inneholde bokstaver fra A-Å og tall.")]
        public String address { get; set; }

        [Display(Name = "E-post")]
        [Required(ErrorMessage = "E-post må oppgis")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"+ "@"+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "E-post")]
        public String email { get; set; }

        [Display(Name = "Telefonnr")]
        [Required(ErrorMessage = "Telefonnr må oppgis")]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "Telefonnummer kan bare inneholde 8 tall")]
        public String phoneNumber { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "Postnr må oppgis")]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnr må være 4 siffer")]
        public String postalCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        [RegularExpression(@"[A-ZÆØÅa-zæøå]{2,30}", ErrorMessage = "Poststed kan bare inneholde bokstaver fra A-Å")]
        public String city { get; set; }

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