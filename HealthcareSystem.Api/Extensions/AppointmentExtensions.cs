using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.Extensions;

public static class AppointmentExtensions {
    public static IServiceCollection AddAppointmentServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        //services.AddScoped<AppointmentService>();
        return services;
    }
}