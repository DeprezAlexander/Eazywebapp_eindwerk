using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models.ViewModels
{
    public class InvoiceClientProjectViewModel
    {
        public Invoice Invoice { get; set; }
        public IEnumerable<Project> ProjectList { get; set; }

    }
}
