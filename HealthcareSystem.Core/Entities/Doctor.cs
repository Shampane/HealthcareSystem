using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Entities;

public class Doctor(
    string name,
    string description,
    string? imageUrl,
    int experienceAge,
    decimal feeInDollars,
    string specialization,
    string phoneNumber
) {
    [Key]
    public Guid DoctorId { get; init; }

    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public string? ImageUrl { get; set; } = imageUrl;
    public int ExperienceAge { get; set; } = experienceAge;

    [DataType(DataType.Currency)]
    public decimal FeeInDollars { get; set; } = feeInDollars;

    public string Specialization { get; set; } = specialization;

    [DataType(DataType.PhoneNumber)]
    [MaxLength(12)]
    public string PhoneNumber { get; set; } = phoneNumber;

    public ICollection<Schedule>? Schedules { get; init; }
}