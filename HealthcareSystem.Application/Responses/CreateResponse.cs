namespace HealthcareSystem.Application.Responses;

public record CreateResponse<T>(
    string Status,
    string Message,
    T? Data
);