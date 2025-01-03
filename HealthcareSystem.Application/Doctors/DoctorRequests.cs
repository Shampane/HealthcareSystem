namespace HealthcareSystem.Application.Doctors;

public record DoctorRequest(
    string Name,
    string Description,
    int ExperienceAge,
    decimal FeeInDollars,
    string Specialization,
    string PhoneNumber
);