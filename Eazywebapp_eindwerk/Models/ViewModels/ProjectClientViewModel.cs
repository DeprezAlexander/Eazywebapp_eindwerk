using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eazywebapp_eindwerk.Models.ViewModels
{
    public class ProjectClientViewModel
    {
        public Project Project { get; set; }
        public IEnumerable<Client> ClientList { get; set; }

    }
}
