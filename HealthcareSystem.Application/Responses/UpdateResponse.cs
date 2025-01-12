namespace HealthcareSystem.Application.Responses;

public record UpdateResponse<T>(
    string Status,
    string Message,
    T? Data
);