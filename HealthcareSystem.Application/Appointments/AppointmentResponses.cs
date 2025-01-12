namespace HealthcareSystem.Application.Appointments;

public record AppointmentResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);