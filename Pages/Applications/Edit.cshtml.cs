using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Internship_Tracker.Data;
using Student_Internship_Tracker.Models;

namespace Student_Internship_Tracker.Pages_Applications
{
    public class EditModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public EditModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application =  await _context.Applications.FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (application == null)
            {
                return NotFound();
            }
            Application = application;
            ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "CompanyName");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email");
            ViewData["StatusList"] = new SelectList(Enum.GetValues<ApplicationStatus>());
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "CompanyName");
                ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email");
                ViewData["StatusList"] = new SelectList(Enum.GetValues<ApplicationStatus>());
                return Page();
            }

            _context.Attach(Application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(Application.ApplicationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ApplicationId == id);
        }
    }
}
