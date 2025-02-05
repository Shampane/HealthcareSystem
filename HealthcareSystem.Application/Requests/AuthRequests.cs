namespace HealthcareSystem.Application.Requests;

public static class AuthRequests {
    public record RegisterRequest(
        string Username, string Email, string Gender,
        string Password, string ConfirmPassword
    );

    public record LoginRequest(string Email, string Password);

    public record RefreshTokenRequest(string AccessToken, string RefreshToken);

    public record ForgetPasswordRequest(string Email, string ClientUri);

    public record ResetPasswordRequest(
        string Email, string Token, string Password, string ConfirmPassword
    );
}