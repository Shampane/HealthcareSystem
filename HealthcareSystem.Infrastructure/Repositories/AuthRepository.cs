using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareSystem.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository {
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AuthRepository(
        UserManager<User> userManager, IConfiguration configuration
    ) {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserWithPasswordAsync(
        User user, string password
    ) {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task AddUserRoleAsync(User user) {
        List<string> roles = ["User"];
        await _userManager.AddToRolesAsync(user, roles);
    }

    public async Task<string> GenerateResetToken(User user) {
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }

    public async Task<IdentityResult> ResetPassword(User user, string token,
        string password) {
        return await _userManager.ResetPasswordAsync(user, token, password);
    }

    public async Task<bool> IsUserValidAsync(User user, string password) {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<User?> FindUserByNameAsync(string userName) {
        User? user = await _userManager.FindByNameAsync(userName);
        return user;
    }

    public async Task<User?> FindUserByEmail(string userEmail) {
        User? user = await _userManager.FindByEmailAsync(userEmail);
        return user;
    }

    public async Task<Token?> CreateTokenAsync(
        User user, bool populateExp
    ) {
        SigningCredentials signingCredentials = GetSigningCredentials();
        IList<Claim> claims = await GetClaimsAsync(user);
        JwtSecurityToken tokenOptions =
            CreateTokenOptions(signingCredentials, claims);

        string refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        if (populateExp) {
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(3);
        }

        await _userManager.UpdateAsync(user);
        string? accessToken =
            new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new Token(accessToken, refreshToken);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
        IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
        var tokenValidationParameters = new TokenValidationParameters {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            ),
            ValidateLifetime = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        ClaimsPrincipal? principal = tokenHandler.ValidateToken(
            token, tokenValidationParameters, out securityToken
        );
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken is null ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase
            )) {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }

    private string GenerateRefreshToken() {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private SigningCredentials GetSigningCredentials() {
        IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
        byte[] key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(
            secret, SecurityAlgorithms.HmacSha256
        );
    }

    private async Task<IList<Claim>> GetClaimsAsync(User user) {
        var claims = new List<Claim> { new(ClaimTypes.Name, user.UserName!) };
        IList<string> roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(
            roles
                .Select(r => new Claim(ClaimTypes.Role, r))
        );
        return claims;
    }

    private JwtSecurityToken CreateTokenOptions(
        SigningCredentials signingCredentials, IList<Claim> claims
    ) {
        IConfigurationSection jwtSettings =
            _configuration.GetSection("Jwt");
        var tokenOptions = new JwtSecurityToken(
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(jwtSettings["ExpireInMinutes"]!)
            ),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}