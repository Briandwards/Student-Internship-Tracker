using Microsoft.EntityFrameworkCore;
using Student_Internship_Tracker.Models;

namespace Student_Internship_Tracker.Data;

public class InternTrackContext : DbContext
{
    public InternTrackContext(DbContextOptions<InternTrackContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Internship> Internships { get; set; } = null!;
    public DbSet<Application> Applications { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=InternTrack.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
            .HasOne(a => a.Student)
            .WithMany(s => s.Applications)
            .HasForeignKey(a => a.StudentId);

        modelBuilder.Entity<Application>()
            .HasOne(a => a.Internship)
            .WithMany(i => i.Applications)
            .HasForeignKey(a => a.InternshipId);
    }
}