using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Student_Internship_Tracker.Data;
using Student_Internship_Tracker.Models;

namespace Student_Internship_Tracker.Pages_Internships
{
    public class DetailsModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public DetailsModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        public Internship Internship { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internship = await _context.Internships.FirstOrDefaultAsync(m => m.InternshipId == id);

            if (internship is not null)
            {
                Internship = internship;

                return Page();
            }

            return NotFound();
        }
    }
}
