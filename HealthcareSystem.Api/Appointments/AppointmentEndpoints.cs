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
        ) => await service.CreateAsync(request)).WithTags("Appointment");

        return app;
    }
}