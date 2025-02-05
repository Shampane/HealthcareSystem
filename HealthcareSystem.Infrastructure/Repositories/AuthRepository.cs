using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Records;
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
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<User?> GetUserByEmail(string userEmail) {
        return await _userManager.FindByEmailAsync(userEmail);
    }

    public async Task<IdentityResult> CreateUserWithPassword(
        User user, string password
    ) {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task AddRolesToUser(User user) {
        List<string> roles = ["User"];
        await _userManager.AddToRolesAsync(user, roles);
    }

    public async Task<Token?> CreateToken(User user, bool populateExp) {
        SigningCredentials signingCredentials = GetSigningCredentials();
        List<Claim> claims = await GetClaims(user);
        JwtSecurityToken tokenOptions =
            CreateTokenOptions(signingCredentials, claims);
        string refreshToken = CreateRefreshToken();

        user.RefreshToken = refreshToken;
        if (populateExp) {
            user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddDays(3);
        }

        await _userManager.UpdateAsync(user);
        string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new Token(accessToken, refreshToken);
    }

    public async Task<string> CreateResetToken(User user) {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetUserPassword(
        User user, string token, string password
    ) {
        return await _userManager.ResetPasswordAsync(user, token, password);
    }

    public async Task<bool> IsUserPasswordValid(User user, string password) {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token) {
        IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
        TokenValidationParameters tokenValidationParameters = new() {
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
        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(
            SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase
        )) {
            throw new SecurityTokenException("Invalid token");
        }
        return principal;
    }


    public async Task<User?> FindUserByNameAsync(string userName) {
        User? user = await _userManager.FindByNameAsync(userName);
        return user;
    }


    private string CreateRefreshToken() {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private SigningCredentials GetSigningCredentials() {
        IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
        byte[] key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
        SymmetricSecurityKey secret = new(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims(User user) {
        List<Claim> claims = [
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        ];
        IList<string> roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(
            roles.Select(r => new Claim(ClaimTypes.Role, r))
        );
        return claims;
    }

    private JwtSecurityToken CreateTokenOptions(
        SigningCredentials signingCredentials, IList<Claim> claims
    ) {
        IConfigurationSection jwtSettings = _configuration.GetSection("Jwt");
        DateTime expires = DateTime.UtcNow.AddMinutes(
            double.Parse(jwtSettings["ExpireInMinutes"]!)
        );
        return new JwtSecurityToken(
            jwtSettings["Issuer"], jwtSettings["Audience"],
            claims, expires, signingCredentials: signingCredentials
        );
    }
}