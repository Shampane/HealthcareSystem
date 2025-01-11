namespace HealthcareSystem.Application.Auth;

public record UserRegisterRequest(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password,
    string ConfirmPassword
);