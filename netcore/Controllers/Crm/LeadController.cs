using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

using netcore.Data;
using netcore.Models.Crm;
using netcore.Models.Invent;
using System.IO;
using OfficeOpenXml;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace netcore.Controllers.Crm
{


    [Authorize(Roles = "Lead")]
    public class LeadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lead
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lead.Include(l => l.accountExecutive).Include(l => l.channel);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            // var filePath = Path.GetTempFileName();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {


                    var fileName = Path.GetFileName(formFile.FileName);
                    var filePath = Path.GetFullPath(formFile.FileName);
                    FileInfo file = new FileInfo(filePath);
                    List<Lead> groups = new List<Lead>();
                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    await formFile.CopyToAsync(stream);
                    //}
                    //var stream = new FileStream(filePath, FileMode.Open);
                    using (ExcelPackage package = new ExcelPackage(file))
                    {

                        StringBuilder sb = new StringBuilder();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;
                        for (int i = 2; i <= 20; i++)
                        {
                            Lead obj = new Lead();
                            obj.PersonName = Convert.ToString(worksheet.Cells[i, 1].Value).Trim();
                            obj.birthDate = Convert.ToDateTime(worksheet.Cells[i, 2].Value);
                            // obj.age = Convert.ToInt32(worksheet.Cells[i, 3].Value);
                            obj.FuneralHome = Convert.ToString(worksheet.Cells[i, 3].Value).Trim();
                            obj.DirectorName = Convert.ToString(worksheet.Cells[i, 4].Value).Trim();
                            obj.TypeOfService = Convert.ToString(worksheet.Cells[i, 5].Value).Trim();
                            obj.Veteran = Convert.ToString(worksheet.Cells[i, 6].Value).Trim();
                            obj.Address = Convert.ToString(worksheet.Cells[i, 7].Value).Trim();
                            obj.leadName = Convert.ToString(worksheet.Cells[i, 8].Value).Trim();
                            obj.description = Convert.ToString(worksheet.Cells[i, 9].Value).Trim();
                            obj.street1 = Convert.ToString(worksheet.Cells[i, 10].Value).Trim();
                            obj.street2 = Convert.ToString(worksheet.Cells[i, 11].Value).Trim();
                            obj.city = Convert.ToString(worksheet.Cells[i, 12].Value).Trim();
                            obj.country = Convert.ToString(worksheet.Cells[i, 13].Value).Trim();
                            groups.Add(obj);
                        }
                        _context.AddRange(groups);
                        await _context.SaveChangesAsync();
                       
                    }
                }
            }
                      return RedirectToAction("Index");

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            // return Ok(new { count = files.Count, size, filePath });
        }

        // GET: Lead/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead
                    .Include(l => l.accountExecutive)
                    .Include(l => l.channel)
                        .SingleOrDefaultAsync(m => m.leadId == id);
            if (lead == null)
            {
                return NotFound();
            }

            return View(lead);
        }


        // GET: Lead/Create
        public IActionResult Create()
        {
            ViewData["accountExecutiveId"] = new SelectList(_context.AccountExecutive, "accountExecutiveId", "accountExecutiveName");
            ViewData["channelId"] = new SelectList(_context.Channel, "channelId", "channelName");
            Lead lead = new Lead();
            lead.isQualified = false;
            lead.isConverted = false;
            return View(lead);
        }


        // POST: Lead/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("leadId,leadName,description,street1,street2,city,country,isQualified,isConverted,channelId,customerId,accountExecutiveId,zipCode,birthDate,state,HasChild,createdAt")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["accountExecutiveId"] = new SelectList(_context.AccountExecutive, "accountExecutiveId", "accountExecutiveName", lead.accountExecutiveId);
            ViewData["channelId"] = new SelectList(_context.Channel, "channelId", "channelName", lead.channelId);
            return View(lead);
        }

        // GET: Lead/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead.SingleOrDefaultAsync(m => m.leadId == id);
            lead.birthDate = DateTime.Now;
            if (lead == null)
            {
                return NotFound();
            }
            ViewData["accountExecutiveId"] = new SelectList(_context.AccountExecutive, "accountExecutiveId", "accountExecutiveName", lead.accountExecutiveId);
            ViewData["channelId"] = new SelectList(_context.Channel, "channelId", "channelName", lead.channelId);
            return View(lead);
        }

        // POST: Lead/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("leadId,leadName,description,street1,street2,city,province,isQualified,isConverted,channelId,customerId,accountExecutiveId,zipCode,,birthDate,state,HasChild,createdAt")] Lead lead)
        {
            if (id != lead.leadId)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {
                try
                {
                    if (!lead.isQualified)
                    {
                        lead.isConverted = false;
                    }
                    if (!String.IsNullOrEmpty(lead.customerId))
                    {
                        lead.isConverted = true;
                        lead.isQualified = true;
                    }
                    _context.Update(lead);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadExists(lead.leadId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                //convert to customer, only if customerId is still empty and already qualified
                if (lead.isQualified && lead.isConverted && String.IsNullOrEmpty(lead.customerId))
                {
                    Customer cust = new Customer();
                    cust.city = lead.city;
                    cust.country = lead.country;
                    cust.customerName = lead.leadName;
                    cust.description = lead.description;
                    cust.street1 = lead.street1;
                    cust.street2 = lead.street2;

                    _context.Customer.Add(cust);
                    _context.SaveChanges();

                    lead.customerId = cust.customerId;
                    _context.Update(lead);
                    _context.SaveChanges();
                }


                return RedirectToAction(nameof(Index));
            }

            
            ViewData["accountExecutiveId"] = new SelectList(_context.AccountExecutive, "accountExecutiveId", "accountExecutiveName", lead.accountExecutiveId);
            ViewData["channelId"] = new SelectList(_context.Channel, "channelId", "channelName", lead.channelId);
            return View(lead);
        }

        // GET: Lead/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lead = await _context.Lead
                    .Include(l => l.accountExecutive)
                    .Include(l => l.channel)
                    .Include(x => x.leadLine)
                    .SingleOrDefaultAsync(m => m.leadId == id);

            if (lead == null)
            {
                return NotFound();
            }
            
            ViewData["StatusMessage"] = TempData["StatusMessage"];

            return View(lead);
        }




        // POST: Lead/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lead = await _context.Lead
                .Include(x => x.leadLine)
                .SingleOrDefaultAsync(m => m.leadId == id);

            if (lead.isConverted)
            {
                TempData["StatusMessage"] = "Error. Converted lead can not be deleted";
                return RedirectToAction(nameof(Delete), new { id = lead.leadId });
            }

            try
            {
                _context.LeadLine.RemoveRange(lead.leadLine);
                _context.Lead.Remove(lead);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData["StatusMessage"] = "Error. Calm Down ^_^ and please contact your SysAdmin with this message: " + ex;
                return RedirectToAction(nameof(Delete), new { id = lead.leadId });
            }

            
        }

        private bool LeadExists(string id)
        {
            return _context.Lead.Any(e => e.leadId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class Lead
        {
            public const string Controller = "Lead";
            public const string Action = "Index";
            public const string Role = "Lead";
            public const string Url = "/Lead/Index";
            public const string Name = "Lead";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "Lead")]
        public bool LeadRole { get; set; } = false;
    }
}



