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
    public class IndexModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public IndexModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        public IList<Application> Application { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? StatusFilter { get; set; }

        public List<string> AllStatuses { get; set; } = new();

        public async Task OnGetAsync()
        {
            AllStatuses = Enum.GetNames<ApplicationStatus>().ToList();

            var applications = _context.Applications
                .Include(a => a.Internship)
                .Include(a => a.Student)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchString))
            {
                applications = applications.Where(s => 
                    (s.Internship != null && 
                     ((!string.IsNullOrEmpty(s.Internship.CompanyName) && s.Internship.CompanyName.Contains(SearchString)) ||
                      (!string.IsNullOrEmpty(s.Internship.PositionTitle) && s.Internship.PositionTitle.Contains(SearchString)))) ||
                    (s.Student != null && 
                     ((!string.IsNullOrEmpty(s.Student.FirstName) && s.Student.FirstName.Contains(SearchString)) ||
                      (!string.IsNullOrEmpty(s.Student.LastName) && s.Student.LastName.Contains(SearchString)))));
            }

            if (!string.IsNullOrEmpty(StatusFilter) && Enum.TryParse<ApplicationStatus>(StatusFilter, out var status))
            {
                applications = applications.Where(a => a.Status == status);
            }

            Application = await applications.ToListAsync();
        }
    }
}
