using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage ="Vooraam is verplicht")]
        [Display(Name = "Voornaam")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Familienaam is verplicht")]
        [Display(Name = "Familienaam")]
        public string LastName { get; set; }

        [Display(Name = "Bedrijfsnaam")]
        public string CompanyName { get; set; }

        [Display(Name = "BTW-nummer")]
        public string VAT { get; set; }

        [Required(ErrorMessage = "Adres is verplicht")]
        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Stad/gemeente is verplicht")]
        [Display(Name = "Stad/gemeente")]
        public string City { get; set; }

        [Required(ErrorMessage = "Land is verplicht")]
        [Display(Name = "Land")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postcode is verplicht")]
        [Display(Name = "Postcode")]
        public string PostalCode { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }
    }
}
