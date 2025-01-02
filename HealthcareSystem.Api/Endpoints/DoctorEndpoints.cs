using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Endpoints;

public static class DoctorEndpoints
{
    public static WebApplication MapDoctorEndpoints(
        this WebApplication app)
    {
        app.MapPost("/api/doctor", async (
                [FromBody] DoctorCreateRequest request,
                DoctorService service
            ) =>
            await service.CreateAsync(request));
        app.MapGet("/api/doctors",
            async (DoctorService service) => await service.GetAsync());
        app.MapGet("/api/doctors/{id}",
            async ([FromQuery] Guid id, DoctorService service)
                => await service.GetByIdAsync(id));
        app.MapDelete("/api/doctors/{id}",
            async ([FromQuery] Guid id, DoctorService service)
                => await service.RemoveAsync(id));
        return app;
    }
}