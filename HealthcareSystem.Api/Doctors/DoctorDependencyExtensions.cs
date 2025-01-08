using HealthcareSystem.Application.Doctors;
using HealthcareSystem.Infrastructure.Doctors;

namespace HealthcareSystem.Api.Doctors;

public static class DoctorDependencyExtensions
{
    public static IServiceCollection AddDoctorServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<DoctorService>();
        return services;
    }
}