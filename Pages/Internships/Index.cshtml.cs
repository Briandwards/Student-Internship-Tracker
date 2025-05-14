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
    public class IndexModel : PageModel
    {
        private readonly Student_Internship_Tracker.Data.InternTrackContext _context;

        public IndexModel(Student_Internship_Tracker.Data.InternTrackContext context)
        {
            _context = context;
        }

        public IList<Internship> Internship { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? LocationFilter { get; set; }

        public List<string> AllLocations { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowActiveOnly { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortDirection { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 6; // Number of items per page

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int TotalPages { get; set; }

        public async Task OnGetAsync()
        {
            var internships = _context.Internships
                .Include(i => i.Applications)
                    .ThenInclude(a => a.Student)
                .AsQueryable();

            if (!string.IsNullOrEmpty(CurrentFilter))
            {
                internships = internships.Where(s => 
                    (!string.IsNullOrEmpty(s.CompanyName) && s.CompanyName.Contains(CurrentFilter)) ||
                    (!string.IsNullOrEmpty(s.PositionTitle) && s.PositionTitle.Contains(CurrentFilter)) ||
                    (!string.IsNullOrEmpty(s.Location) && s.Location.Contains(CurrentFilter)));
            }

            internships = SortField?.ToLower() switch
            {
                "company" => SortDirection == "desc" ? 
                    internships.OrderByDescending(i => i.CompanyName) :
                    internships.OrderBy(i => i.CompanyName),
                "position" => SortDirection == "desc" ? 
                    internships.OrderByDescending(i => i.PositionTitle) :
                    internships.OrderBy(i => i.PositionTitle),
                "location" => SortDirection == "desc" ? 
                    internships.OrderByDescending(i => i.Location) :
                    internships.OrderBy(i => i.Location),
                "deadline" => SortDirection == "desc" ? 
                    internships.OrderByDescending(i => i.ApplicationDeadline) :
                    internships.OrderBy(i => i.ApplicationDeadline),
                _ => internships.OrderBy(i => i.CompanyName)
            };

            var totalItems = await internships.CountAsync();
            TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            PageIndex = Math.Max(1, Math.Min(PageIndex, TotalPages));

            Internship = await internships
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }
    }
}
