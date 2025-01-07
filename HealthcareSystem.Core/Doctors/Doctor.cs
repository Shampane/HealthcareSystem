using System.ComponentModel.DataAnnotations;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Core.Doctors;

public class Doctor
{
    [Key] public Guid DoctorId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int ExperienceAge { get; set; }

    [DataType(DataType.Currency)] public decimal FeeInDollars { get; set; }

    public string Specialization { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Schedule> Schedules { get; set; } = [];
}