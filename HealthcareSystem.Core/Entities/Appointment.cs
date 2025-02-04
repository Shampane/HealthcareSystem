using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Core.Entities;

public class Appointment {
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [ForeignKey(nameof(Doctor))]
    public Guid DoctorId { get; init; }

    public Doctor? Doctor { get; init; }

    [Required(ErrorMessage = "The name is required")]
    [StringLength(64, ErrorMessage = "The name is too long")]
    public required string DoctorName { get; init; }

    [ForeignKey(nameof(Schedule))]
    public Guid ScheduleId { get; init; }

    [DataType(DataType.DateTime)]
    [Required(ErrorMessage = "The start time is required")]
    public required DateTimeOffset StartTime { get; init; }

    [DataType(DataType.DateTime)]
    [Required(ErrorMessage = "The end time is required")]
    public required DateTimeOffset EndTime { get; init; }

    public Schedule? Schedule { get; init; }

    [ForeignKey(nameof(User))]
    public string UserId { get; init; }

    public User? User { get; init; }

    [Required(ErrorMessage = "The user name is required")]
    public required string UserName { get; init; }
}