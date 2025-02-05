namespace HealthcareSystem.Application.Requests;

public static class DoctorRequests {
    public record GetDoctorsRequest(
        int? PageIndex, int? PageSize, string? SortField,
        string? SortOrder, string? SearchField, string? SearchValue
    );

    public record CreateDoctorRequest(
        string Name, string Description, string? ImageUrl, int ExperienceAge,
        decimal FeeInDollars, string Specialization, string PhoneNumber
    );

    public record UpdateDoctorRequest(
        string? Name, string? Description, string? ImageUrl,
        int? ExperienceAge, decimal? FeeInDollars, string? Specialization,
        string? PhoneNumber
    );
}

public record DoctorRequest(
    string Name,
    string Description,
    string? ImageUrl,
    int ExperienceAge,
    decimal FeeInDollars,
    string Specialization,
    string PhoneNumber
);