using System.Net;
using System.Security.Claims;
using HealthcareSystem.Api.Templates;
using HealthcareSystem.Application.Mappings;
using HealthcareSystem.Application.Requests;
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
        if (user is null) {
            return NotFound("User not found");
        }
        bool isValid =
            await _authRepository.IsUserPasswordValid(user, request.Password);
        if (!isValid) {
            return BadRequest("Invalid password");
        }
        Token? token = await _authRepository.CreateToken(user, true);
        return Ok(token);
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken(
        AuthRequests.RefreshTokenRequest request, CancellationToken ct
    ) {
        ClaimsPrincipal principal =
            _authRepository.GetPrincipalFromExpiredToken(request.AccessToken);
        string? email = principal.FindFirstValue(ClaimTypes.Email);
        if (email is null) {
            return BadRequest("Invalid token");
        }
        User? user = await _authRepository.GetUserByEmail(email);
        if (user is null
            || user.RefreshToken != request.RefreshToken
            || user.RefreshTokenExpiry <= DateTimeOffset.UtcNow) {
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
            return NotFound("User not found");
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
        await _emailRepository.SendForgetPassword(
            emailMetadata, _forgetPasswordTemplate, model
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
            return NotFound("User not found");
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