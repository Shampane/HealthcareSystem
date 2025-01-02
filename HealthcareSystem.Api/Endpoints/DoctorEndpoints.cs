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
                DoctorService service,
                [FromBody] DoctorCreateRequest request
            ) =>
            await service.CreateAsync(request));
        app.MapGet("/api/doctors",
            async (DoctorService service) => await service.GetAsync());
        return app;
    }
}