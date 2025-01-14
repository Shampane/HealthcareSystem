namespace HealthcareSystem.Core.Schedules;

public class ScheduleDto
{
    public ScheduleDto(
        Guid scheduleId, Guid doctorId, DateTime startTime,
        DateTime endTime, bool isAvailable
    )
    {
        ScheduleId = scheduleId;
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
        IsAvailable = isAvailable;
    }

    public Guid ScheduleId { get; init; }
    public Guid DoctorId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public bool IsAvailable { get; init; }
}