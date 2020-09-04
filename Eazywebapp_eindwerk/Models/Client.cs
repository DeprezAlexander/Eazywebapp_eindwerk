using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models
{
    public class Client
    {
        [Key]
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Email is verplicht.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Voornaam is verplicht.")]
        [Display(Name = "Naam")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht.")]
        [Display(Name = "Achternaam")]
        public string LastName { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Stad")]
        public string City { get; set; }

        [Display(Name = "Postcode")]
        public string PostalCode { get; set; }

        [Display(Name = "Land")]
        public string Country { get; set; }

        [Display(Name = "GSM")]
        public string Phone { get; set; }

        [Display(Name = "Bedrijfsnaam")]
        public string CompanyName { get; set; }

        [Display(Name = "BTW nummer")]
        public string VAT { get; set; }

        [Display(Name = "Aantal facturen")]
        public int NumberOfInvoices { get; set; }
    }
}
