namespace HealthcareSystem.Core.Entities;

public class ResetPassword(string email, string token) {
    public string Email { get; init; } = email;
    public string Token { get; init; } = token;
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}