namespace HealthcareSystem.Application.Requests;

public record DoctorGetRequest;

public record DoctorCreateRequest(
    string Name,
    string Description,
    int ExperienceAge,
    decimal FeeInDollars,
    string Specialization,
    string PhoneNumber
);