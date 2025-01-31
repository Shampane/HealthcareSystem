namespace HealthcareSystem.Application.Dtos;

public class DoctorDto(
    Guid doctorId,
    string name,
    string description,
    string? imageUrl,
    int experienceAge,
    decimal feeInDollars,
    string specialization,
    string phoneNumber
) {
    public Guid DoctorId { get; } = doctorId;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string? ImageUrl { get; } = imageUrl;
    public int ExperienceAge { get; } = experienceAge;
    public decimal FeeInDollars { get; } = feeInDollars;
    public string Specialization { get; } = specialization;
    public string PhoneNumber { get; } = phoneNumber;
}