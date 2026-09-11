using JobTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.Api.Data;

public class JobTrackerDbContext(DbContextOptions<JobTrackerDbContext> options) : DbContext(options)
{
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobApplication>(entity =>
        {
            entity.Property(x => x.Company).HasMaxLength(200).IsRequired();
            entity.Property(x => x.JobTitle).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Status).HasMaxLength(50).IsRequired();
            entity.Property(x => x.JobUrl).HasMaxLength(2048);
            entity.Property(x => x.Notes).HasMaxLength(5000);
            entity.HasIndex(x => x.ApplicationDate);
        });
    }
}
