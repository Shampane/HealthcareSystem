using HealthcareSystem.Application.Doctors;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Doctors;

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
        app.MapGet("/api/doctors/{id:guid}",
            async (Guid id, DoctorService service)
                => await service.GetByIdAsync(id));
        app.MapDelete("/api/doctors/{id:guid}",
            async (Guid id, DoctorService service)
                => await service.RemoveAsync(id));
        return app;
    }
}