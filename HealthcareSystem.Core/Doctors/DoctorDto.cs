namespace HealthcareSystem.Core.Doctors;

public class DoctorDto
{
    public Guid DoctorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public int ExperienceAge { get; set; }
    public decimal FeeInDollars { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}