using HealthcareSystem.Application.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Doctors;

public static class DoctorEndpoints
{
    public static WebApplication MapDoctorEndpoints(
        this WebApplication app
    )
    {
        app.MapPost("/api/doctor", async (
                [FromBody] DoctorRequest request,
                DoctorService service
            ) => await service.CreateAsync(request))
            .RequireAuthorization("AdminPolicy")
            .WithTags("Doctors");

        app.MapGet("/api/doctors", async (
            [FromQuery] int? pageIndex,
            [FromQuery] int? pageSize,
            [FromQuery] string? sortField,
            [FromQuery] string? sortOrder,
            [FromQuery] string? searchField,
            [FromQuery] string? searchValue,
            DoctorService service
        ) => await service.GetAsync(
            pageIndex, pageSize, sortField,
            sortOrder, searchField, searchValue
        )).WithTags("Doctors");

        app.MapGet("/api/doctors/{id:guid}", async (
            Guid id, DoctorService service
        ) => await service.GetByIdAsync(id)).WithTags("Doctors");

        app.MapPut("/api/doctors/{id:guid}", async (
            Guid id, DoctorService service,
            [FromBody] DoctorRequest request
        ) => await service.UpdateAsync(id, request)).WithTags("Doctors");

        app.MapDelete("/api/doctors/{id:guid}", async (
            Guid id, DoctorService service
        ) => await service.RemoveAsync(id)).WithTags("Doctors");

        return app;
    }
}