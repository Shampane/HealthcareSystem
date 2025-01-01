using HealthcareSystem.Application.Services;

namespace HealthcareSystem.Api.Endpoints;

public static class DoctorEndpoints
{
    public static WebApplication MapDoctorEndpoints(
        this WebApplication app)
    {
        app.MapGet("/api/doctors",
            async (DoctorService useCase) =>
                await useCase.GetDoctorsAsync());
        return app;
    }
}