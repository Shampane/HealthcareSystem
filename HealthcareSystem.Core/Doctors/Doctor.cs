using System.ComponentModel.DataAnnotations;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Core.Doctors;

public class Doctor
{
    public Doctor(string name, string description, string? imageUrl,
        int experienceAge, decimal feeInDollars, string specialization,
        string phoneNumber)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        ExperienceAge = experienceAge;
        FeeInDollars = feeInDollars;
        Specialization = specialization;
        PhoneNumber = phoneNumber;
    }

    [Key] public Guid DoctorId { get; init; }

    [MaxLength(128)] public string Name { get; set; }

    [MaxLength(512)] public string Description { get; set; }

    [MaxLength(512)] public string? ImageUrl { get; set; }

    public int ExperienceAge { get; set; }

    [DataType(DataType.Currency)] public decimal FeeInDollars { get; set; }

    [MaxLength(128)] public string Specialization { get; set; }

    [DataType(DataType.PhoneNumber)]
    [MaxLength(12)]
    public string PhoneNumber { get; set; }

    public ICollection<Schedule>? Schedules { get; init; }
}