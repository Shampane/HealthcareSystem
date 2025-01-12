namespace HealthcareSystem.Application.Responses;

public record GetResponse<T>(
    string Status,
    string Message,
    ICollection<T>? Data
);