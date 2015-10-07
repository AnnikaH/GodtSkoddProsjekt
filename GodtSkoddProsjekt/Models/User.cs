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

// FÅR FEILMELDING PÅ REGEX'ENE NÅR KJØRER Create.cshtml: ÆØÅ?
        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn må oppgis")]
        //[RegularExpression(@"[A-ZÆØÅa-zæøå]{30}", ErrorMessage = "Fornavn kan bare inneholde bokstaver fra A-Å, og være 30 bokstaver langt.")]
        public String firstName { get; set; }

        [Display(Name = "Etternavn")]
        [Required(ErrorMessage = "Etternavn må oppgis")]
        //[RegularExpression(@"[A-ZÆØÅa-zæøå]{30}", ErrorMessage = "Etternavn kan bare inneholde bokstaver fra A-Å, og være 30 bokstaver langt.")]
        public String lastName { get; set; }

        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Adressen må oppgis")]
        //[RegularExpression(@"[A-ZÆØÅa-zæøå0-9]{30}", ErrorMessage = "Adresse kan bare inneholde bokstaver fra A-Å og tall, og være 30 bokstaver langt.")]
        public String address { get; set; }

        [Display(Name = "E-post")]
        [Required(ErrorMessage = "E-post må oppgis")]
        //[RegularExpression(@"[A-ZÆØÅa-zæøå0-9_-@.]{30}", ErrorMessage = "E-post")]
        public String email { get; set; }

        [Display(Name = "Telefonnr")]
        [Required(ErrorMessage = "Telefonnr må oppgis")]
        //[RegularExpression(@"[0-9]{8}", ErrorMessage = "Telefonnummer kan bare inneholde 8 tall")]
        public String phoneNumber { get; set; }

        [Display(Name = "Postnr")]
        [Required(ErrorMessage = "Postnr må oppgis")]
        //[RegularExpression(@"[0-9]{4}", ErrorMessage = "Postnr må være 4 siffer")]
        public String postalCode { get; set; }

        [Display(Name = "Poststed")]
        [Required(ErrorMessage = "Poststed må oppgis")]
        public String city { get; set; }

        [Display(Name = "Brukernavn")]
        [Required(ErrorMessage = "Brukernavn må oppgis")]
        public String userName { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public String password { get; set; }
    }
}