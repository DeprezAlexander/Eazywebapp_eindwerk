using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eazywebapp_eindwerk.Models;
using Eazywebapp_eindwerk.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Eazywebapp_eindwerk.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [Authorize(Roles = "User")]
        public IActionResult Index()
        {
            ClientInvoiceProject model = new ClientInvoiceProject()
            {
                invoice = _context.Invoice.Count(),
                project = _context.Project.Count(),
                client = _context.Client.Count()
            };
            return View(model);
        }
        
        [Authorize(Roles = "User")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
