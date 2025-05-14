using System.ComponentModel.DataAnnotations;

namespace Student_Internship_Tracker.Models;

public class Student
{
    public int StudentId { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? Major { get; set; }

    [Required]
    [Display(Name = "Enrollment Date")]
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
