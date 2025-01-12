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

public record DoctorGetRequest(
    int? PageIndex,
    int? PageSize,
    string? SortField,
    string? SortOrder,
    string? Search
);