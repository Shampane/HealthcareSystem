using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Entities;

public class Doctor {
    [Key]
    public Guid Id { get; init; } = Guid.CreateVersion7();

    [Required(ErrorMessage = "The name is required")]
    [StringLength(64, ErrorMessage = "The name is too long")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "The description is required")]
    public required string Description { get; set; }

    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "The experience age is required")]
    [Range(0, 100, ErrorMessage = "The experience age must be between 0 and 100")]
    public required int ExperienceAge { get; set; }

    [DataType(DataType.Currency)]
    [Range(0, int.MaxValue, ErrorMessage = "The fee must be more than 0 dollars")]
    public required decimal FeeInDollars { get; set; }

    [Required(ErrorMessage = "The specialization is required")]
    [StringLength(128, ErrorMessage = "The specialization is too long")]
    public required string Specialization { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Required(ErrorMessage = "The phone number is required")]
    [StringLength(12, ErrorMessage = "The phone number is too long")]
    public required string PhoneNumber { get; set; }

    public ICollection<Schedule>? Schedules { get; init; }
    public ICollection<Appointment>? Appointments { get; init; }
}