namespace HealthcareSystem.Application.Schedules;

public record ScheduleRequest(
    Guid DoctorId,
    DateTime StartTime,
    int DurationInMinutes
);

public record ScheduleGetRequest(
    Guid DoctorId,
    int? PageIndex,
    int? PageSize,
    DateTime? SearchStartTime,
    DateTime? SearchEndTime
);