namespace HealthcareSystem.Api.Templates;

public class TwoFactorModel {
    public required string Email { get; init; }
    public required string CallbackUrl { get; init; }
    public required string OTP { get; init; }
}