namespace HealthcareSystem.Application.Requests;

public static class ScheduleRequests {
    public record GetSchedulesRequest(
        Guid? DoctorId, int? PageIndex, int? PageSize,
        DateTimeOffset? StartTime, DateTimeOffset? EndTime
    );

    public record UpdateScheduleRequest(bool IsAvailable);

    public record CreateScheduleRequest(
        Guid DoctorId, DateTimeOffset StartTime, DateTimeOffset EndTime
    );
}