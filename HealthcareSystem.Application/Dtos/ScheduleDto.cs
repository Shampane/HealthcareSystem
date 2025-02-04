namespace HealthcareSystem.Application.Dtos;

public class ScheduleDto {
    public required Guid Id { get; init; }
    public required string DoctorName { get; init; }
    public required DateTimeOffset StartTime { get; init; }
    public required DateTimeOffset EndTime { get; init; }
    public required bool IsAvailable { get; init; }
}