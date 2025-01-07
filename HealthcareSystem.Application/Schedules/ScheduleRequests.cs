namespace HealthcareSystem.Application.Schedules;

public record ScheduleRequest(
    Guid DoctorId,
    DateTime StartTime,
    uint DurationInMinutes
);