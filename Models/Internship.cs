using System.ComponentModel.DataAnnotations;

namespace Student_Internship_Tracker.Models;

public class Internship
{
    public int InternshipId { get; set; }

    [Required]
    [Display(Name = "Company Name")]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Position Title")]
    public string PositionTitle { get; set; } = string.Empty;

    public string? Location { get; set; }

    [Required]
    [Display(Name = "Application Deadline")]
    [DataType(DataType.Date)]
    public DateTime ApplicationDeadline { get; set; }

    public string? ImageUrl { get; set; }

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
