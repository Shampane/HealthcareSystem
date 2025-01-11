using HealthcareSystem.Application.Schedules;
using HealthcareSystem.Infrastructure.Schedules;

namespace HealthcareSystem.Api.Schedules;

public static class ScheduleServicesExtension
{
    public static IServiceCollection AddSchedulesServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ScheduleService>();
        return services;
    }
}