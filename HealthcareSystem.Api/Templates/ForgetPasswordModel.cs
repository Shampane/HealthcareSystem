namespace HealthcareSystem.Api.Templates;

public class ForgetPasswordModel {
    public required string Email { get; init; }
    public required string CallbackUrl { get; init; }
}