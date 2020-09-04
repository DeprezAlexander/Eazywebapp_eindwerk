using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eazywebapp_eindwerk.Models;
using Microsoft.AspNetCore.Authorization;
using Eazywebapp_eindwerk.Models.ViewModels;

namespace Eazywebapp_eindwerk.Controllers
{
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Client
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
 
            return View(await _context.Client.ToListAsync());
        }

        // GET: Client/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = await _context.Client.FirstOrDefaultAsync(s => s.ClientID == id);
            
            if (client == null)
            {
                return NotFound();
            }
             
                ClientInvoiceCount model = new ClientInvoiceCount()
                {
                    Client = await _context.Client.FirstOrDefaultAsync(m => m.ClientID == id),
                    invoiceList = _context.Invoice.Where(s => s.ClientID == id).Include(c => c.Client).ToList(),
                    projectList = _context.Project.Where(s => s.ClientID == id).Include(c => c.Client).ToList()
                };
                return View(model);
     
        }

        // GET: Client/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("ClientID,Email,Name,LastName,Address,City,PostalCode,Country,Phone,CompanyName,VAT,NumberOfInvoices")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,Email,Name,LastName,Address,City,PostalCode,Country,Phone,CompanyName,VAT,NumberOfInvoices")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }
            var getInvoices = _context.Invoice.Where(s => s.ClientID == id).Count();
            var getProjects = _context.Project.Where(s => s.ClientID == id).Count();
            bool deleteableid;
            if (getInvoices > 0 || getProjects > 0) {
                deleteableid = false;
            }
            else
            {
                deleteableid = true;
            }
            ClientInvoiceProjectDeleteViewModel model = new ClientInvoiceProjectDeleteViewModel()
            {
                Client = client,
                CountInvoices = getInvoices,
                CountProjects = getProjects,
                deleteable = deleteableid
            };
            return View(model);

            //return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientID == id);
        }
    }
}
