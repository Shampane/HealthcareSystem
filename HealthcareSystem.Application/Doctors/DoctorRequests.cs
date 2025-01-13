namespace HealthcareSystem.Application.Doctors;

public record DoctorRequest(
    string Name,
    string Description,
    string? ImageUrl,
    int ExperienceAge,
    decimal FeeInDollars,
    string Specialization,
    string PhoneNumber
);