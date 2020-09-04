using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models.ViewModels
{
    public class ProjectInvoiceDeleteViewModel
    {
        public Project Project { get; set; }
        public int Count { get; set; }

        public bool deleteable { get; set; }
    }
}
