using HealthcareSystem.Application.Services;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.Extensions;

public static class ScheduleExtensions {
    public static IServiceCollection AddScheduleServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        //services.AddScoped<ScheduleService>();
        return services;
    }
}