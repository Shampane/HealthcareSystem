using System.ComponentModel.DataAnnotations;
using HealthcareSystem.Core.Doctors;

namespace HealthcareSystem.Core.Schedules;

public class Schedule
{
    [Key] public Guid ScheduleId { get; set; }

    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; }

    [DataType(DataType.Date)] public DateTime StartTime { get; set; }

    public uint DurationInMinutes { get; set; }

    public bool IsAvailable { get; set; } = true;
}