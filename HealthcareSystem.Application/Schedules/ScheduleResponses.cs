using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Application.Schedules;

public record ScheduleCreateResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);

public record ScheduleUpdateResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);

public record ScheduleGetResponse(
    int StatusCode,
    bool IsSuccess,
    string Message,
    Dictionary<Guid, ICollection<Schedule>>? Schedules
);