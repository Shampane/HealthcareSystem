using System.Net;
using System.Security.Claims;
using HealthcareSystem.Api.Templates;
using HealthcareSystem.Application.Mappings;
using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Records;
using InterpolatedParsing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace HealthcareSystem.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase {
    private readonly IAuthRepository _authRepository;
    private readonly IEmailRepository _emailRepository;

    private readonly string _forgetPasswordTemplate =
        $"{Directory.GetCurrentDirectory()}/Templates/ForgetPasswordTemplate.cshtml";

    private readonly ILogger<AuthController> _logger;

    private readonly string _twoFactorTemplate =
        $"{Directory.GetCurrentDirectory()}/Templates/TwoFactorTemplate.cshtml";

    public AuthController(
        IAuthRepository authRepository,
        ILogger<AuthController> logger,
        IEmailRepository emailRepository
    ) {
        _authRepository = authRepository;
        _emailRepository = emailRepository;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        AuthRequests.RegisterRequest request, CancellationToken ct
    ) {
        if (request.Password != request.ConfirmPassword) {
            return BadRequest("Passwords do not match");
        }

        User user = new() {
            UserName = request.Username,
            Email = request.Email,
            Gender = request.Gender
        };
        IdentityResult result = await _authRepository.CreateUserWithPassword(
            user, request.Password
        );
        if (result.Succeeded) {
            await _authRepository.SetEmailTwoFactor(user, request.EnableTwoFactor);
            await _authRepository.AddRolesToUser(user);
            return Created($"auth/register/{user.Id}", user.ToDto());
        }

        IEnumerable<string> errors = result.Errors.Select(e => e.Description);
        return BadRequest(errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        AuthRequests.LoginRequest request, CancellationToken ct
    ) {
        User? user = await _authRepository.GetUserByEmail(request.Email);
        if (
            user is null
            || !await _authRepository.IsUserPasswordValid(user, request.Password)
        ) {
            return NotFound(ResponsesMessages.NotFound(
                "Invalid username or password"
            ));
        }

        bool isUserHasTwoFactor = await _authRepository.IsUserHasTwoFactor(user);
        if (isUserHasTwoFactor) {
            IList<string>? providers = await _authRepository.GetTwoFactorProviders(user);
            if (!providers.Contains("Email")) {
                return Unauthorized("Invalid 2-factor provider");
            }

            EmailMetadata emailMetadata = new() {
                ToAddress = user.Email!,
                Subject = "HealthcareSystem: Two-factor provider"
            };
            TwoFactorModel model = new() {
                CallbackUrl = "https://localhost:4200",
                Email = user.Email!,
                OTP = await _authRepository.CreateTwoFactorToken(user)
            };
            await _emailRepository.SendEmailWithTemplate(
                emailMetadata, _twoFactorTemplate, model, ct
            );
            return Ok("Two factor provider has been sent by email");
        }

        Token? token = await _authRepository.CreateToken(user, true);
        return Ok(token);
    }

    [HttpPost("twoFactor")]
    public async Task<IActionResult> TwoFactor(
        AuthRequests.TwoFactorRequest request, CancellationToken ct
    ) {
        User? user = await _authRepository.GetUserByEmail(request.Email);
        if (user is null) {
            return NotFound(ResponsesMessages.NotFound("User not found"));
        }

        bool isValid = await _authRepository.IsTwoFactorValid(
            user, request.Provider, request.Token
        );
        if (!isValid) {
            return BadRequest("Invalid provider");
        }

        IList<string> roles = await _authRepository.GetUserRoles(user);
        Token? token = await _authRepository.CreateToken(user, true);
        return Ok(token);
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(
        AuthRequests.RefreshTokenRequest request, CancellationToken ct
    ) {
        ClaimsPrincipal principal =
            _authRepository.GetPrincipalFromExpiredToken(request.AccessToken);
        User? user = await _authRepository.GetUserByName(principal.Identity.Name);
        if (user is null
            || user.RefreshToken != request.RefreshToken
            || user.RefreshTokenExpiry <= DateTime.UtcNow) {
            return BadRequest("Invalid refresh token");
        }

        Token? refreshedToken = await _authRepository.CreateToken(user, false);
        return Ok(refreshedToken);
    }

    [HttpPost("forgetPassword")]
    public async Task<IActionResult> ForgetPassword(
        AuthRequests.ForgetPasswordRequest request, CancellationToken ct
    ) {
        User? user = await _authRepository.GetUserByEmail(request.Email);
        if (user is null) {
            return NotFound(ResponsesMessages.NotFound("User not found"));
        }

        string token = await _authRepository.CreateResetToken(user);
        var parameters = new Dictionary<string, string> {
            { "token", token },
            { "email", request.Email }
        };
        string callbackUrl =
            QueryHelpers.AddQueryString(request.ClientUri, parameters!);
        LogResetToken(callbackUrl);
        EmailMetadata emailMetadata = new() {
            ToAddress = user.Email!,
            Subject = "HealthcareSystem: Reset Password"
        };
        ForgetPasswordModel model = new() {
            CallbackUrl = callbackUrl,
            Email = request.Email
        };
        await _emailRepository.SendEmailWithTemplate(
            emailMetadata, _forgetPasswordTemplate, model, ct
        );
        return NoContent();
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(
        AuthRequests.ResetPasswordRequest request, CancellationToken ct
    ) {
        if (request.Password != request.ConfirmPassword) {
            return BadRequest("Passwords do not match");
        }

        User? user = await _authRepository.GetUserByEmail(request.Email);
        if (user is null) {
            return NotFound(ResponsesMessages.NotFound("User not found"));
        }

        IdentityResult result = await _authRepository.ResetUserPassword(
            user, request.Token, request.Password
        );
        if (result.Succeeded) {
            return Ok("Password reset successful");
        }

        IEnumerable<string> errors = result.Errors.Select(e => e.Description);
        return BadRequest(errors);
    }

    private void LogResetToken(string callbackUrl) {
        string decode = WebUtility.UrlDecode(callbackUrl);
        int tokenIndex = decode.IndexOf("token", StringComparison.Ordinal);
        int emailIndex = decode.IndexOf("&email", StringComparison.Ordinal);
        string str = decode.Substring(tokenIndex, emailIndex - tokenIndex);
        string token = string.Empty;
        InterpolatedParser.Parse(str, $"token={token}");
        _logger.LogInformation(token);
    }
}