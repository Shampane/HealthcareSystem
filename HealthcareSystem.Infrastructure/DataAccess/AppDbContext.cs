using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace HealthcareSystem.Infrastructure.DataAccess;

public class AppDbContext(
    DbContextOptions options,
    IConfiguration configuration
) : IdentityDbContext<User, IdentityRole, string>(options)
{
    private readonly IdentityRole[] _roles =
    [
        new()
        {
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new()
        {
            Name = "User",
            NormalizedName = "USER"
        },
        new()
        {
            Name = "Doctor",
            NormalizedName = "DOCTOR"
        }
    ];

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
        modelBuilder.Entity<IdentityRole>().HasData(_roles);
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(
            configuration.GetConnectionString("Database")
        );
        builder.ConfigureWarnings(w => w.Ignore(
            RelationalEventId.PendingModelChangesWarning
        ));
    }
}