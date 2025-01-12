using HealthcareSystem.Application.Appointments;
using HealthcareSystem.Infrastructure.Appointments;

namespace HealthcareSystem.Api.Appointments;

public static class AppointmentServicesExtension
{
    public static IServiceCollection AddAppointmentServices(
        this IServiceCollection services)
    {
        services
            .AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<AppointmentService>();
        return services;
    }
}