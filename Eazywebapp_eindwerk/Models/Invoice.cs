using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models
{
    public class Invoice
    {
        //INVOICE ID
        [Key]
        public int InvoiceID { get; set; }

        //INVOICE PRICE
        
        [Display(Name = "Prijs (EUR)")]
        public double InvoicePrice { get; set; }

        //INVOICE DATE
        [Display(Name = "Aanmaak datum")]
        [Required(ErrorMessage = "Aanmaak datum is verplicht.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        //INVOICE Expiry
        [Display(Name = "Verval datum")]
        [Required(ErrorMessage = "Verval datum is verplicht.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceExpiry { get; set; }

        //INVOICE NUMBER
        [Display(Name = "Factuur nummer")]
        [Required(ErrorMessage ="Factuur nummer is verplicht.")]
        public int InvoiceNumber { get; set; }


        public int ProjectID { get; set; }

        public int ClientID { get; set; }


        //FOREIGN KEYS
        [ForeignKey("ProjectID")]
        public virtual Project Project { get; set; }

        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }

    }
}
