using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models.ViewModels
{
    public class ClientInvoiceProjectDeleteViewModel
    {
        public Client Client { get; set; }
        public int CountInvoices { get; set; }
        public int CountProjects { get; set; }

        public bool deleteable { get; set; }
    }
}
