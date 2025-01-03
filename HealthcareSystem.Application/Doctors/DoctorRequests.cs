namespace HealthcareSystem.Application.Doctors;

public record DoctorCreateRequest(
    string Name,
    string Description,
    int ExperienceAge,
    decimal FeeInDollars,
    string Specialization,
    string PhoneNumber
);