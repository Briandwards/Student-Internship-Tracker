using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Internship_Tracker.Data;
using Student_Internship_Tracker.Models;

namespace Student_Internship_Tracker.Pages_Internships
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
            return Page();
        }

        [BindProperty]
        public Internship Internship { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Internships.Add(Internship);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
