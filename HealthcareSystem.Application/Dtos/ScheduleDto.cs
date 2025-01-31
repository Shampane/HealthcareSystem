namespace HealthcareSystem.Application.Dtos;

public class ScheduleDto(
    Guid scheduleId,
    Guid doctorId,
    DateTime startTime,
    DateTime endTime,
    bool isAvailable
) {
    public Guid ScheduleId { get; init; } = scheduleId;
    public Guid DoctorId { get; init; } = doctorId;
    public DateTime StartTime { get; init; } = startTime;
    public DateTime EndTime { get; init; } = endTime;
    public bool IsAvailable { get; init; } = isAvailable;
}