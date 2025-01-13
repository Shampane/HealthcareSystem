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

        app.MapGet("/api/schedules", async (
            [FromQuery] Guid doctorId,
            [FromQuery] int? pageIndex,
            [FromQuery] int? pageSize,
            [FromQuery] DateTime? searchStartTime,
            [FromQuery] DateTime? searchEndTime,
            ScheduleService service
        ) => await service.GetByDoctorAsync(
            doctorId, pageIndex, pageSize,
            searchStartTime, searchEndTime
        )).WithTags("Schedules");

        app.MapGet("/api/schedules/{id:guid}", async (
            Guid id, ScheduleService service
        ) => await service.GetByIdAsync(id)).WithTags("Schedules");

        app.MapPatch("/api/schedules/{id:guid}", async (
            Guid id, ScheduleService service
        ) => await service.ChangeAvailableAsync(id)).WithTags("Schedules");

        app.MapDelete("/api/schedules/{id:guid}", async (
            Guid id, ScheduleService service
        ) => await service.RemoveAsync(id)).WithTags("Schedules");

        return app;
    }
}