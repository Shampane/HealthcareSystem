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
        ) => await service.CreateAsync(request)).WithTags("Schedules");

        app.MapGet("/api/schedules", async (ScheduleService service)
            => await service.GetSchedulesAsync()).WithTags("Schedules");

        app.MapGet("/api/schedules/{id:guid}", async (
                Guid id,
                ScheduleService service
            ) => await service.GetSchedulesByDoctorIdAsync(id))
            .WithTags("Schedules");

        app.MapPatch("/api/schedules/{id:guid}", async (
                Guid id,
                ScheduleService service
            ) => await service.ChangeAvailableStatusAsync(id))
            .WithTags("Schedules");
        return app;
    }
}