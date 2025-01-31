namespace HealthcareSystem.Application.Requests;

public record AppointmentRequest(
    Guid DoctorId,
    string DoctorName,
    Guid ScheduleId,
    DateTime ScheduleStartTime,
    DateTime ScheduleEndTime,
    string UserId,
    string UserName
);