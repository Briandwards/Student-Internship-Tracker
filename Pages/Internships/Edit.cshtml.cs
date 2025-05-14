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

namespace Student_Internship_Tracker.Pages_Internships
{
    public class EditModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public EditModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Internship Internship { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internship =  await _context.Internships.FirstOrDefaultAsync(m => m.InternshipId == id);
            if (internship == null)
            {
                return NotFound();
            }
            Internship = internship;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Internship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InternshipExists(Internship.InternshipId))
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

        private bool InternshipExists(int id)
        {
            return _context.Internships.Any(e => e.InternshipId == id);
        }
    }
}
