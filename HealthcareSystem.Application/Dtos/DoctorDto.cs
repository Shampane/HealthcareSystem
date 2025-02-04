namespace HealthcareSystem.Application.Dtos;

public class DoctorDto {
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public string? ImageUrl { get; init; }
    public required int ExperienceAge { get; init; }
    public required decimal FeeInDollars { get; init; }
    public required string Specialization { get; init; }
    public required string PhoneNumber { get; init; }
}