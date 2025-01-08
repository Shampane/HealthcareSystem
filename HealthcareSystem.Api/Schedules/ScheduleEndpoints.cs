using HealthcareSystem.Application.Schedules;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Schedules;

public static class ScheduleEndpoints
{
    public static WebApplication MapScheduleEndpoints(
        this WebApplication app
    )
    {
        app.MapPost("/api/schedule", async (
                [FromBody] ScheduleRequest request,
                ScheduleService service
            ) =>
            await service.CreateAsync(request));

        app.MapGet("/api/schedules", async (ScheduleService service)
            => await service.GetSchedulesAsync());
        return app;
    }
}