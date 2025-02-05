namespace HealthcareSystem.Application.Requests;

public static class AppointmentRequests {
    public record GetAppointmentsRequest(
        Guid? DoctorId, string? UserId, int? PageIndex, int? PageSize,
        DateTimeOffset? StartTime, DateTimeOffset? EndTime
    );

    public record CreateAppointmentRequest(Guid ScheduleId, string UserEmail);
}