namespace HealthcareSystem.Core.Schedules;

public class ScheduleDto
{
    public Guid ScheduleId { get; init; }
    public Guid DoctorId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public bool IsAvailable { get; init; } = true;
}