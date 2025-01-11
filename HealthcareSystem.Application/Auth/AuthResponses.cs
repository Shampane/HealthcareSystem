namespace HealthcareSystem.Application.Auth;

public record UserRegisterResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);