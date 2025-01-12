namespace HealthcareSystem.Application.Appointments;

public record AppointmentRequest(
    string UserId,
    Guid DoctorId,
    Guid ScheduleId
);