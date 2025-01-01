using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Models;

public class Schedule
{
    [Key] public Guid ScheduleId { get; set; }

    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndTime { get; set; }

    public bool IsAvailable { get; set; }
}