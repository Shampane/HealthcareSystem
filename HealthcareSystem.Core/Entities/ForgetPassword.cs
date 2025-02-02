namespace HealthcareSystem.Core.Entities;

public class ForgetPassword(string email, string callbackUrl) {
    public string Email { get; init; } = email;
    public string CallbackUrl { get; init; } = callbackUrl;
}