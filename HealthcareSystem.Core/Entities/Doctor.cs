using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Entities;

public class Doctor {
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [Url]
    public string? ImageUrl { get; set; }

    public int ExperienceAge { get; set; }
    public decimal FeeInDollars { get; set; }
    public string Specialization { get; set; } = string.Empty;

    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Schedule>? Schedules { get; init; }
    public ICollection<Appointment>? Appointments { get; init; }
}