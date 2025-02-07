using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Core.Entities;

public class Appointment {
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [ForeignKey(nameof(Doctor))]
    public Guid DoctorId { get; init; }

    public Doctor? Doctor { get; init; }

    public string DoctorName { get; init; } = string.Empty;

    [ForeignKey(nameof(Schedule))]
    public Guid ScheduleId { get; init; }

    [DataType(DataType.DateTime)]
    public DateTimeOffset StartTime { get; init; }

    [DataType(DataType.DateTime)]
    public DateTimeOffset EndTime { get; init; }

    public Schedule? Schedule { get; init; }

    [ForeignKey(nameof(User))]
    public string UserId { get; init; } = string.Empty;

    public User? User { get; init; }

    public string UserName { get; init; } = string.Empty;
}