using Microsoft.EntityFrameworkCore;
using Student_Internship_Tracker.Models;

namespace Student_Internship_Tracker.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new InternTrackContext(
            new DbContextOptionsBuilder<InternTrackContext>()
                .UseSqlite("Data Source=InternTrack.db")
                .Options);
            if (context.Students.Any() || context.Internships.Any() || context.Applications.Any())
            {
                return;
            }
            var students = new[]
            {
                new Student
                {
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@university.edu",
                    Major = "Computer Science",
                    EnrollmentDate = DateTime.Parse("2023-08-15")
                },
                new Student
                {
                    FirstName = "Emma",
                    LastName = "Johnson",
                    Email = "emma.j@university.edu",
                    Major = "Business Administration",
                    EnrollmentDate = DateTime.Parse("2024-01-10")
                },
                new Student
                {
                    FirstName = "Michael",
                    LastName = "Davis",
                    Email = "m.davis@university.edu",
                    Major = "Information Systems",
                    EnrollmentDate = DateTime.Parse("2023-08-15")
                }
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var internships = new[]
            {
                new Internship
                {
                    CompanyName = "TechVision Solutions",
                    PositionTitle = "Software Developer Intern",
                    Location = "Austin, TX",
                    ApplicationDeadline = DateTime.Parse("2025-06-15"),
                    ImageUrl = "/images/companies/techvision.png"
                },
                new Internship
                {
                    CompanyName = "Quantum Dynamics",
                    PositionTitle = "AI Research Intern",
                    Location = "Boston, MA",
                    ApplicationDeadline = DateTime.Parse("2025-06-30"),
                    ImageUrl = "/images/companies/quantum.png"
                },
                new Internship
                {
                    CompanyName = "CloudSphere Tech",
                    PositionTitle = "Cloud Infrastructure Intern",
                    Location = "Denver, CO",
                    ApplicationDeadline = DateTime.Parse("2025-07-15"),
                    ImageUrl = "/images/companies/cloudsphere.png"
                },
                new Internship
                {
                    CompanyName = "Digital Frontier Labs",
                    PositionTitle = "Full Stack Developer Intern",
                    Location = "Seattle, WA",
                    ApplicationDeadline = DateTime.Parse("2025-07-30"),
                    ImageUrl = "/images/companies/frontier.png"
                },
                new Internship
                {
                    CompanyName = "InnovateTech Solutions",
                    PositionTitle = "Mobile App Developer Intern",
                    Location = "Chicago, IL",
                    ApplicationDeadline = DateTime.Parse("2025-08-15"),
                    ImageUrl = "/images/companies/innovate.png"
                }
            };

            context.Internships.AddRange(internships);
            context.SaveChanges();

            var applications = new[]
            {
                new Application
                {
                    StudentId = students[0].StudentId,
                    InternshipId = internships[0].InternshipId,
                    ApplicationDate = DateTime.Parse("2025-05-01"),
                    Status = ApplicationStatus.Interviewing
                },
                new Application
                {
                    StudentId = students[1].StudentId,
                    InternshipId = internships[1].InternshipId,
                    ApplicationDate = DateTime.Parse("2025-05-05"),
                    Status = ApplicationStatus.Applied
                },
                new Application
                {
                    StudentId = students[2].StudentId,
                    InternshipId = internships[2].InternshipId,
                    ApplicationDate = DateTime.Parse("2025-05-10"),
                    Status = ApplicationStatus.Offer
                },
                new Application
                {
                    StudentId = students[0].StudentId,
                    InternshipId = internships[3].InternshipId,
                    ApplicationDate = DateTime.Parse("2025-05-12"),
                    Status = ApplicationStatus.Rejected
                },
                new Application
                {
                    StudentId = students[1].StudentId,
                    InternshipId = internships[4].InternshipId,
                    ApplicationDate = DateTime.Parse("2025-05-15"),
                    Status = ApplicationStatus.Applied
                }
            };

            context.Applications.AddRange(applications);
            context.SaveChanges();
    }
}