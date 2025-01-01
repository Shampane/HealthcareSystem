using HealthcareSystem.Application.Services;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.DependencyInjection;

public static class DoctorDependencyInjection
{
    public static IServiceCollection AddDoctorServices(
        this IServiceCollection services)
    {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<DoctorService>();
        return services;
    }
}