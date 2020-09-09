using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eazywebapp_eindwerk.Models;
using Eazywebapp_eindwerk.Models.ViewModels;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;

namespace Eazywebapp_eindwerk.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var appdbcontext = _context.Invoice.Include(s => s.Project).Include(x => x.Client);
            return View(await appdbcontext.ToListAsync());
        }

        // GET: Invoice/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .Include(s => s.Project)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoice/Create
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            InvoiceClientProjectViewModel model = new InvoiceClientProjectViewModel()
            {
                ProjectList = await _context.Project.ToListAsync(),
                Invoice = new Models.Invoice()
            };

            return View(model);
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                DateTime dDate = new DateTime(0001, 01, 01);
                DateTime nDate = DateTime.Today;
                
                if(invoice.InvoiceDate == dDate)
                {
                    if(invoice.InvoiceExpiry == dDate)
                    {
                        var clientID = _context.Project.Where(s => s.ProjectID == invoice.ProjectID).Select(s => s.ClientID).FirstOrDefault();
                        var invoicePrice = _context.Project.Where(s => s.ProjectID == invoice.ProjectID).Select(s => s.OffertePrice).FirstOrDefault();

                        invoice.InvoicePrice = invoicePrice;
                        invoice.ClientID = clientID;

                        invoice.InvoiceExpiry = nDate.AddMonths(1);
                        invoice.InvoiceDate = nDate;

                        _context.Add(invoice);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {

                        var clientID = _context.Project.Where(s => s.ProjectID == invoice.ProjectID).Select(s => s.ClientID).FirstOrDefault();
                        var invoicePrice = _context.Project.Where(s => s.ProjectID == invoice.ProjectID).Select(s => s.OffertePrice).FirstOrDefault();

                        invoice.InvoiceDate = nDate;

                        invoice.InvoicePrice = invoicePrice;
                        invoice.ClientID = clientID;
                        _context.Add(invoice);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    var clientID = _context.Project.Where(s => s.ProjectID == invoice.ProjectID).Select(s => s.ClientID).FirstOrDefault();
                    var invoicePrice = _context.Project.Where(s => s.ProjectID == invoice.ProjectID).Select(s => s.OffertePrice).FirstOrDefault();

                    invoice.InvoicePrice = invoicePrice;
                    invoice.ClientID = clientID;
                    _context.Add(invoice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            InvoiceClientProjectViewModel model = new InvoiceClientProjectViewModel()
            {
                ProjectList = await _context.Project.ToListAsync(),
                Invoice = invoice
            };
            return View(model);
        }

        // GET: Invoice/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.SingleOrDefaultAsync(m => m.InvoiceID == id);

            if (invoice == null)
            {
                return NotFound();
            }

            InvoiceClientProjectViewModel model = new InvoiceClientProjectViewModel()
            {
                ProjectList = await _context.Project.ToListAsync(),
                Invoice = invoice
            };
            return View(model);*/
            return RedirectToAction(nameof(Index));
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, InvoiceClientProjectViewModel model)
        {
            if (id != model.Invoice.InvoiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var invoiceFromDB = await _context.Invoice.FindAsync(model.Invoice.InvoiceID);

                invoiceFromDB.InvoicePrice = model.Invoice.InvoicePrice;
                invoiceFromDB.InvoiceDate = model.Invoice.InvoiceDate;
                invoiceFromDB.InvoiceExpiry = model.Invoice.InvoiceExpiry;
                invoiceFromDB.InvoiceNumber = model.Invoice.InvoiceNumber;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            InvoiceClientProjectViewModel modelVM = new InvoiceClientProjectViewModel()
            {
                ProjectList = await _context.Project.ToListAsync(),
                Invoice = model.Invoice
            };
            return View(modelVM);
        }

        // GET: Invoice/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice
                .FirstOrDefaultAsync(m => m.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.InvoiceID == id);
        }

       

    }
}
