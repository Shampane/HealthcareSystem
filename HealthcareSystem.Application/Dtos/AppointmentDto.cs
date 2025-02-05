namespace HealthcareSystem.Application.Dtos;

public class AppointmentDto {
    public required Guid Id { get; init; }
    public required Guid DoctorId { get; init; }
    public required string DoctorName { get; init; }
    public required Guid ScheduleId { get; init; }
    public required DateTimeOffset StartTime { get; init; }
    public required DateTimeOffset EndTime { get; init; }
    public required string UserId { get; init; }
    public required string UserName { get; init; }
}