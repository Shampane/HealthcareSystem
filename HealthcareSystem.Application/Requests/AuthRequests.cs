namespace HealthcareSystem.Application.Requests;

public record UserRegisterRequest(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword
);

public record UserAuthenticateRequest(
    string UserName,
    string Password
);