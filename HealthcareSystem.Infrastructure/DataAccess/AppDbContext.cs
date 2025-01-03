using HealthcareSystem.Core.Doctors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HealthcareSystem.Infrastructure.DataAccess;

public class AppDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }

    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(
            configuration.GetConnectionString("Database")
        );
    }
}