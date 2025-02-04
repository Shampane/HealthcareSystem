using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.Extensions;

public static class DoctorExtensions {
    public static IServiceCollection AddDoctorServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        //services.AddScoped<DoctorService>();
        return services;
    }
}