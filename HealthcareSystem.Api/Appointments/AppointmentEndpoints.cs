using HealthcareSystem.Application.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Appointments;

public static class AppointmentEndpoints
{
    public static WebApplication MapAppointmentEndpoints(
        this WebApplication app
    )
    {
        app.MapPost("/api/appointment", async (
            [FromBody] AppointmentRequest request,
            AppointmentService service
        ) => await service.CreateAsync(request)).WithTags("Appointments");

        app.MapGet("/api/appointments/{doctorId:guid}", async (
            Guid doctorId,
            [FromQuery] int? pageIndex,
            [FromQuery] int? pageSize,
            [FromQuery] DateTime? searchStartTime,
            [FromQuery] DateTime? searchEndTime,
            AppointmentService service
        ) => await service.GetByDoctorAsync(
            doctorId, pageIndex, pageSize,
            searchStartTime, searchEndTime
        )).WithTags("Appointments");

        app.MapGet("/api/appointments/{userId}", async (
            string userId,
            [FromQuery] int? pageIndex,
            [FromQuery] int? pageSize,
            [FromQuery] DateTime? searchStartTime,
            [FromQuery] DateTime? searchEndTime,
            AppointmentService service
        ) => await service.GetByUserAsync(
            userId, pageIndex, pageSize,
            searchStartTime, searchEndTime
        )).WithTags("Appointments");

        app.MapGet("/api/appointment/{id:guid}", async (
            Guid id, AppointmentService service
        ) => await service.GetByIdAsync(id)).WithTags("Appointments");

        app.MapDelete("/api/appointment/{id:guid}", async (
            Guid id, AppointmentService service
        ) => await service.RemoveAsync(id)).WithTags("Appointments");

        return app;
    }
}