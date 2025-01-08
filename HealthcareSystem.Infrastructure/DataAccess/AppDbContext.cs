using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HealthcareSystem.Infrastructure.DataAccess;

public class AppDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Doctor>()
            .HasMany(e => e.Schedules)
            .WithOne(e => e.Doctor)
            .HasForeignKey(e => e.DoctorId)
            .IsRequired();
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(
            configuration.GetConnectionString("Database")
        );
    }
}