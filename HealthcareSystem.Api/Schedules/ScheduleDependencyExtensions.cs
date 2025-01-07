using HealthcareSystem.Application.Schedules;
using HealthcareSystem.Infrastructure.Schedules;

namespace HealthcareSystem.Api.Schedules;

public static class ScheduleDependencyExtensions
{
    public static IServiceCollection AddSchedulesServices(
        this IServiceCollection services
    )
    {
        services.AddTransient<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ScheduleService>();
        return services;
    }
}