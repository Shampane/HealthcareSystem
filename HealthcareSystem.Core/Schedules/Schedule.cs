using System.ComponentModel.DataAnnotations;
using HealthcareSystem.Core.Doctors;

namespace HealthcareSystem.Core.Schedules;

public class Schedule
{
    public Schedule(
        Guid doctorId, DateTime startTime,
        int durationInMinutes
    )
    {
        DoctorId = doctorId;
        StartTime = startTime;
        DurationInMinutes = durationInMinutes;
    }

    [Key] public Guid ScheduleId { get; init; }

    public Guid DoctorId { get; init; }

    public Doctor? Doctor { get; init; }

    [DataType(DataType.Date)] public DateTime StartTime { get; init; }

    public int DurationInMinutes { get; init; }

    public bool IsAvailable { get; set; } = true;
}