namespace HealthcareSystem.Application.Schedules;

public record ScheduleCreateResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);