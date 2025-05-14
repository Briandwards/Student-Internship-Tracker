using System.ComponentModel.DataAnnotations;

namespace Student_Internship_Tracker.Models;

public class Application
{
    public int ApplicationId { get; set; }

    [Required(ErrorMessage = "Student must be selected")]
    [Display(Name = "Student")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Internship must be selected")]
    [Display(Name = "Internship")]
    public int InternshipId { get; set; }

    [Required(ErrorMessage = "Application date is required")]
    [Display(Name = "Application Date")]
    [DataType(DataType.Date)]
    public DateTime ApplicationDate { get; set; }

    [Required(ErrorMessage = "Application status must be specified")]
    [Display(Name = "Application Status")]
    [EnumDataType(typeof(ApplicationStatus))]
    public ApplicationStatus Status { get; set; }

    public Student? Student { get; set; }

    public Internship? Internship { get; set; }
}

public enum ApplicationStatus
{
    Applied,

    Interviewing,

    Offer,
    
    Rejected
}
