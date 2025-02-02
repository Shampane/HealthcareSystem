using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Endpoints;

public static class ScheduleEndpoints {
    public static WebApplication MapScheduleEndpoints(
        this WebApplication app
    ) {
        app.MapPost("/api/schedule", async (
                [FromBody] ScheduleRequest request,
                ScheduleService service
            ) => await service.CreateAsync(request))
            .RequireAuthorization("DoctorPolicy")
            .WithTags("Schedules");

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
            ) => await service.ChangeAvailableAsync(id))
            .RequireAuthorization("UserPolicy")
            .WithTags("Schedules");

        app.MapDelete("/api/schedules/{id:guid}", async (
                Guid id, ScheduleService service
            ) => await service.RemoveAsync(id))
            .RequireAuthorization("DoctorPolicy")
            .WithTags("Schedules");

        return app;
    }
}