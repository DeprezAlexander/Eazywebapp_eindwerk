using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models.ViewModels
{
    public class ClientInvoiceCount
    {
        public Client Client { get; set; }
        public List<Invoice> invoiceList { get; set; }
        public List<Project> projectList { get; set; }
    }
}
