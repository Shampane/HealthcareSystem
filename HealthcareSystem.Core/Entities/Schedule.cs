using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Entities;

public class Schedule(
    Guid doctorId,
    DateTime startTime,
    int durationInMinutes
) {
    [Key]
    public Guid ScheduleId { get; init; }

    public Guid DoctorId { get; init; } = doctorId;
    public Doctor? Doctor { get; init; }

    [DataType(DataType.Date)]
    public DateTime StartTime { get; init; } = startTime;

    public int DurationInMinutes { get; init; } = durationInMinutes;
    public bool IsAvailable { get; set; } = true;
}