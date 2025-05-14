using System.ComponentModel.DataAnnotations;

namespace Student_Internship_Tracker.Models;

/// <summary>
/// Represents an internship application submitted by a student.
/// This class manages the relationship between students and internships,
/// tracking the application status throughout the process.
/// </summary>
public class Application
{
    /// <summary>
    /// Gets or sets the unique identifier for the application.
    /// </summary>
    public int ApplicationId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the student who submitted the application.
    /// </summary>
    [Required(ErrorMessage = "Student must be selected")]
    [Display(Name = "Student")]
    public int StudentId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the internship being applied for.
    /// </summary>
    [Required(ErrorMessage = "Internship must be selected")]
    [Display(Name = "Internship")]
    public int InternshipId { get; set; }

    /// <summary>
    /// Gets or sets the date when the application was submitted.
    /// </summary>
    [Required(ErrorMessage = "Application date is required")]
    [Display(Name = "Application Date")]
    [DataType(DataType.Date)]
    public DateTime ApplicationDate { get; set; }

    /// <summary>
    /// Gets or sets the current status of the application.
    /// </summary>
    [Required(ErrorMessage = "Application status must be specified")]
    [Display(Name = "Application Status")]
    [EnumDataType(typeof(ApplicationStatus))]
    public ApplicationStatus Status { get; set; }

    /// <summary>
    /// Navigation property for the associated student.
    /// </summary>
    public Student? Student { get; set; }

    /// <summary>
    /// Navigation property for the associated internship.
    /// </summary>
    public Internship? Internship { get; set; }
}

/// <summary>
/// Represents the possible states of an internship application.
/// </summary>
public enum ApplicationStatus
{
    /// <summary>Initial state when the application is first submitted</summary>
    Applied,

    /// <summary>The student is in the interview process</summary>
    Interviewing,

    /// <summary>The student has received an offer</summary>
    Offer,

    /// <summary>The application was not successful</summary>
    Rejected
}
