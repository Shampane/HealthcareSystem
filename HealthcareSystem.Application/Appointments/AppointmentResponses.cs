using HealthcareSystem.Core.Appointments;

namespace HealthcareSystem.Application.Appointments;

public record AppointmentResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);

public record AppointmentGetResponse(
    int StatusCode,
    bool IsSuccess,
    string Message,
    ICollection<Appointment>? Appointments
);