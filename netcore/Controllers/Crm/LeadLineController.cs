﻿using System;
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

namespace netcore.Controllers.Crm
{


    [Authorize(Roles = "LeadLine")]
    public class LeadLineController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadLineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeadLine
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeadLine.Include(l => l.activity).Include(l => l.lead);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeadLine/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadLine = await _context.LeadLine
                    .Include(l => l.activity)
                    .Include(l => l.lead)
                        .SingleOrDefaultAsync(m => m.leadLineId == id);
            if (leadLine == null)
            {
                return NotFound();
            }

            return View(leadLine);
        }


        // GET: LeadLine/Create
        public IActionResult Create(string masterid, string id)
        {
            var check = _context.LeadLine.SingleOrDefault(m => m.leadLineId == id);
            var selected = _context.Lead.SingleOrDefault(m => m.leadId == masterid);
            ViewData["activityId"] = new SelectList(_context.Activity, "activityId", "activityName");
            ViewData["leadId"] = new SelectList(_context.Lead, "leadId", "leadName");
            if (check == null)
            {
                LeadLine objline = new LeadLine();
                objline.lead = selected;
                objline.leadId = masterid;
                return View(objline);
            }
            else
            {
                return View(check);
            }
        }




        // POST: LeadLine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("leadLineId,leadId,activityId,startDate,endDate,description,createdAt")] LeadLine leadLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leadLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["activityId"] = new SelectList(_context.Activity, "activityId", "activityName", leadLine.activityId);
            ViewData["leadId"] = new SelectList(_context.Lead, "leadId", "leadName", leadLine.leadId);
            return View(leadLine);
        }

        // GET: LeadLine/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadLine = await _context.LeadLine.SingleOrDefaultAsync(m => m.leadLineId == id);
            if (leadLine == null)
            {
                return NotFound();
            }
            ViewData["activityId"] = new SelectList(_context.Activity, "activityId", "activityName", leadLine.activityId);
            ViewData["leadId"] = new SelectList(_context.Lead, "leadId", "leadName", leadLine.leadId);
            return View(leadLine);
        }

        // POST: LeadLine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("leadLineId,leadId,activityId,startDate,endDate,description,createdAt")] LeadLine leadLine)
        {
            if (id != leadLine.leadLineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leadLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadLineExists(leadLine.leadLineId))
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
            ViewData["activityId"] = new SelectList(_context.Activity, "activityId", "activityName", leadLine.activityId);
            ViewData["leadId"] = new SelectList(_context.Lead, "leadId", "leadName", leadLine.leadId);
            return View(leadLine);
        }

        // GET: LeadLine/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leadLine = await _context.LeadLine
                    .Include(l => l.activity)
                    .Include(l => l.lead)
                    .SingleOrDefaultAsync(m => m.leadLineId == id);
            if (leadLine == null)
            {
                return NotFound();
            }

            return View(leadLine);
        }




        // POST: LeadLine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var leadLine = await _context.LeadLine.SingleOrDefaultAsync(m => m.leadLineId == id);
            _context.LeadLine.Remove(leadLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadLineExists(string id)
        {
            return _context.LeadLine.Any(e => e.leadLineId == id);
        }

    }
}





namespace netcore.MVC
{
    public static partial class Pages
    {
        public static class LeadLine
        {
            public const string Controller = "LeadLine";
            public const string Action = "Index";
            public const string Role = "LeadLine";
            public const string Url = "/LeadLine/Index";
            public const string Name = "LeadLine";
        }
    }
}
namespace netcore.Models
{
    public partial class ApplicationUser
    {
        [Display(Name = "LeadLine")]
        public bool LeadLineRole { get; set; } = false;
    }
}



