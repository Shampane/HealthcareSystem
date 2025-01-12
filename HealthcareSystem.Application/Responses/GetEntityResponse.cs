namespace HealthcareSystem.Application.Responses;

public record GetEntityResponse<T>(
    string Status,
    string Message,
    T? Data
);