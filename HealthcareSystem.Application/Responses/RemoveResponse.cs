namespace HealthcareSystem.Application.Responses;

public record RemoveResponse<T>(
    string Status,
    string Message,
    T? Data
);