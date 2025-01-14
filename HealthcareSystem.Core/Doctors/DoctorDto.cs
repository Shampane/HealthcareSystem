namespace HealthcareSystem.Core.Doctors;

public class DoctorDto
{
    public DoctorDto(
        Guid doctorId, string name, string description,
        string? imageUrl, int experienceAge, decimal feeInDollars,
        string specialization, string phoneNumber
    )
    {
        DoctorId = doctorId;
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        ExperienceAge = experienceAge;
        FeeInDollars = feeInDollars;
        Specialization = specialization;
        PhoneNumber = phoneNumber;
    }

    public Guid DoctorId { get; }
    public string Name { get; }
    public string Description { get; }
    public string? ImageUrl { get; }
    public int ExperienceAge { get; }
    public decimal FeeInDollars { get; }
    public string Specialization { get; }
    public string PhoneNumber { get; }
}