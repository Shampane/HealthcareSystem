using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Core.Entities;

public class Schedule {
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [ForeignKey(nameof(Doctor))]
    [Required(ErrorMessage = "The doctor id is required")]
    public required Guid DoctorId { get; init; }

    public Doctor? Doctor { get; init; }

    [Required(ErrorMessage = "The doctor name is required")]
    public required string DoctorName { get; init; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "The start time is required")]
    public required DateTimeOffset StartTime { get; init; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "The end time is required")]
    public required DateTimeOffset EndTime { get; init; }

    public bool IsAvailable { get; set; } = true;
}