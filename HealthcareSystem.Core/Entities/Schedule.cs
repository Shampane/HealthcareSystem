using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Core.Entities;

public class Schedule {
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [ForeignKey(nameof(Doctor))]
    public Guid DoctorId { get; init; }

    public Doctor? Doctor { get; init; }
    public string DoctorName { get; init; } = string.Empty;

    [DataType(DataType.Date)]
    public required DateTimeOffset StartTime { get; init; }

    [DataType(DataType.Date)]
    public required DateTimeOffset EndTime { get; init; }

    public bool IsAvailable { get; set; } = true;
}