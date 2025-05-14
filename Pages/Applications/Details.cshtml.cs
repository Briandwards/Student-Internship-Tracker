using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Student_Internship_Tracker.Data;
using Student_Internship_Tracker.Models;

namespace Student_Internship_Tracker.Pages_Applications
{
    public class DetailsModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public DetailsModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        public Application Application { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Student)
                .Include(a => a.Internship)
                .FirstOrDefaultAsync(m => m.ApplicationId == id);

            if (application is not null)
            {
                Application = application;

                return Page();
            }

            return NotFound();
        }
    }
}
