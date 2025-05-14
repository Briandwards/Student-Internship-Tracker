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
    public class CreateModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public CreateModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "CompanyName");
        ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email");
        ViewData["StatusList"] = new SelectList(Enum.GetValues<ApplicationStatus>());
            return Page();
        }

        [BindProperty]
        public Application Application { get; set; } = default!;

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

            // Check for duplicate applications
            var existingApplication = await _context.Applications
                .FirstOrDefaultAsync(a => a.StudentId == Application.StudentId && 
                                         a.InternshipId == Application.InternshipId);

            if (existingApplication != null)
            {
                ModelState.AddModelError(string.Empty, "You have already applied to this internship.");
                ViewData["InternshipId"] = new SelectList(_context.Internships, "InternshipId", "CompanyName");
                ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "Email");
                ViewData["StatusList"] = new SelectList(Enum.GetValues<ApplicationStatus>());
                return Page();
            }

            _context.Applications.Add(Application);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
