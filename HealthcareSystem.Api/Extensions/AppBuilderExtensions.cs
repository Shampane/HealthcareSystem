using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.Extensions;

public static class AppBuilderExtensions {
    public static IServiceCollection AddMyRepositories(
        this IServiceCollection services
    ) {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();

        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        return services;
    }
}