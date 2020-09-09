using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eazywebapp_eindwerk.Models;
using Eazywebapp_eindwerk.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Eazywebapp_eindwerk.Utility;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Eazywebapp_eindwerk.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProjectController(AppDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Project
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var appdbcontext = _context.Project.Include(s => s.Client);
            return View(await appdbcontext.ToListAsync());
        }

        // GET: Project/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .Include(s => s.Client)
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }



        // GET: Project/Create
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            ProjectClientViewModel model = new ProjectClientViewModel()
            {
                ClientList = await _context.Client.ToListAsync(),
                Project = new Models.Project()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Project project)
        {
            
            if (ModelState.IsValid)
            {
                DateTime dDate = new DateTime(0001, 01, 01);
                DateTime nDate = DateTime.Today;
                
                if (project.RenewalDate == dDate)
                {
                    if(project.StartDate == dDate)
                    {
                        if(project.EndDate == dDate)
                        {
                            project.EndDate = nDate.AddMonths(2);
                            project.StartDate = nDate;
                            project.RenewalDate = nDate;
                            _context.Add(project);
                            await _context.SaveChangesAsync();
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            var files = HttpContext.Request.Form.Files;
                            var projectFromDb = await _context.Project.FindAsync(project.ProjectID);
                            string extension;
                            if (files.Count > 0)
                            {
                                extension = Path.GetExtension(files[0].FileName);
                                string imageName = project.ProjectID + extension;
                                string path = Path.Combine(webRootPath + SD.DefaultImagePath, imageName);
                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    files[0].CopyTo(fileStream);
                                }
                            }
                            else
                            {
                                extension = SD.DefaultImageExtension;
                                var uploads = Path.Combine(webRootPath + SD.DefaultImagePath + SD.DefaultProjectImage);
                                System.IO.File.Copy(uploads, webRootPath + SD.DefaultImagePath + project.ProjectID.ToString() + extension);
                            }
                            projectFromDb.ProjectImage = SD.DefaultImagePath + project.ProjectID + extension;
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            project.StartDate = nDate;
                            _context.Add(project);
                            await _context.SaveChangesAsync();
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            var files = HttpContext.Request.Form.Files;
                            var projectFromDb = await _context.Project.FindAsync(project.ProjectID);
                            string extension;
                            if (files.Count > 0)
                            {
                                extension = Path.GetExtension(files[0].FileName);
                                string imageName = project.ProjectID + extension;
                                string path = Path.Combine(webRootPath + SD.DefaultImagePath, imageName);
                                using (var fileStream = new FileStream(path, FileMode.Create))
                                {
                                    files[0].CopyTo(fileStream);
                                }
                            }
                            else
                            {
                                extension = SD.DefaultImageExtension;
                                var uploads = Path.Combine(webRootPath + SD.DefaultImagePath + SD.DefaultProjectImage);
                                System.IO.File.Copy(uploads, webRootPath + SD.DefaultImagePath + project.ProjectID.ToString() + extension);
                            }
                            projectFromDb.ProjectImage = SD.DefaultImagePath + project.ProjectID + extension;
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));

                        }

                    }
                    else
                    {
                        project.RenewalDate = nDate;
                        _context.Add(project);
                        await _context.SaveChangesAsync();
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var files = HttpContext.Request.Form.Files;
                        var projectFromDb = await _context.Project.FindAsync(project.ProjectID);
                        string extension;
                        if (files.Count > 0)
                        {
                            extension = Path.GetExtension(files[0].FileName);
                            string imageName = project.ProjectID + extension;
                            string path = Path.Combine(webRootPath + SD.DefaultImagePath, imageName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                files[0].CopyTo(fileStream);
                            }
                        }
                        else
                        {
                            extension = SD.DefaultImageExtension;
                            var uploads = Path.Combine(webRootPath + SD.DefaultImagePath + SD.DefaultProjectImage);
                            System.IO.File.Copy(uploads, webRootPath + SD.DefaultImagePath + project.ProjectID.ToString() + extension);
                        }
                        projectFromDb.ProjectImage = SD.DefaultImagePath + project.ProjectID + extension;
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }

                }
                else
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var projectFromDb = await _context.Project.FindAsync(project.ProjectID);
                    string extension;
                    if (files.Count > 0)
                    {
                        extension = Path.GetExtension(files[0].FileName);
                        string imageName = project.ProjectID + extension;
                        string path = Path.Combine(webRootPath + SD.DefaultImagePath, imageName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                    }
                    else
                    {
                        extension = SD.DefaultImageExtension;
                        var uploads = Path.Combine(webRootPath + SD.DefaultImagePath + SD.DefaultProjectImage);
                        System.IO.File.Copy(uploads, webRootPath + SD.DefaultImagePath + project.ProjectID.ToString() + extension);
                    }
                    projectFromDb.ProjectImage = SD.DefaultImagePath + project.ProjectID + extension;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }

            }
            ProjectClientViewModel model = new ProjectClientViewModel()
            {
                ClientList = await _context.Client.ToListAsync(),
                Project = project
            };

            return View(model);
        }

        // GET: Project/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            ProjectClientViewModel model = new ProjectClientViewModel()
            {
                ClientList = await _context.Client.ToListAsync(),
                Project = project
            };
            return View(model);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, ProjectClientViewModel model)
        {
            if (id != model.Project.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var files = HttpContext.Request.Form.Files;
                    var projectFromDb = await _context.Project.FindAsync(model.Project.ProjectID);
                    if (files.Count > 0)
                    {
                        //Verwijder het oorspronkelijk bestand
                        string imagePath = webRootPath + projectFromDb.ProjectImage;

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        //upload het nieuwe bestand
                        string extension_new = Path.GetExtension(files[0].FileName);
                        string imageName = model.Project.ProjectID + extension_new;
                        string path = Path.Combine(webRootPath + SD.DefaultImagePath, imageName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        projectFromDb.ProjectImage = SD.DefaultImagePath + model.Project.ProjectID + extension_new;
                    }
                    projectFromDb.Name = model.Project.Name;
                    projectFromDb.ProjectDescription = model.Project.ProjectDescription;
                    projectFromDb.StartDate = model.Project.StartDate;
                    projectFromDb.EndDate = model.Project.EndDate;
                    projectFromDb.Hosting = model.Project.Hosting;
                    projectFromDb.HostingPrice = model.Project.HostingPrice;
                    projectFromDb.RenewalDate = model.Project.RenewalDate;
                    projectFromDb.OffertePrice = model.Project.OffertePrice;
                    projectFromDb.TimeSpent = model.Project.TimeSpent;
                    projectFromDb.url = model.Project.url;
                    projectFromDb.IsActive = model.Project.IsActive;
                    projectFromDb.AdditionalFees = model.Project.AdditionalFees;
                    projectFromDb.TypeOfSite = model.Project.TypeOfSite;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Edit));
                
            }

            

            ProjectClientViewModel modelVM = new ProjectClientViewModel()
            {
                ClientList = await _context.Client.ToListAsync(),
                Project = model.Project
            };
            return View(modelVM);
        }

        // GET: Project/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.ProjectID == id);
            if (project == null)
            {
                return NotFound();
            }
            var getInvoices = _context.Invoice.Where(s => s.ProjectID == id).Count();
            bool deleteableid;
            if (getInvoices > 0)
            {
                deleteableid = false;
            }
            else
            {
                deleteableid = true;
            }

            ProjectInvoiceDeleteViewModel model = new ProjectInvoiceDeleteViewModel()
                {
                    Project = project,
                    Count = getInvoices,
                    deleteable = deleteableid
                };
                return View(model);
            
            
            //return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var project = await _context.Project.FindAsync(id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }
    }
}
