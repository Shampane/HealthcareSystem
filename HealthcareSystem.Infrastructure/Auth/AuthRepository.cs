using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HealthcareSystem.Core.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareSystem.Infrastructure.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AuthRepository(
        UserManager<User> userManager, IConfiguration configuration
    )
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserWithPasswordAsync(
        User user, string password
    )
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task AddUserRoleAsync(User user)
    {
        List<string> roles = ["User"];
        await _userManager.AddToRolesAsync(user, roles);
    }

    public async Task<bool> IsUserValidAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<User?> FindUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user;
    }

    public async Task<TokenDto?> CreateTokenAsync(User user,
        bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaimsAsync(user);
        var tokenOptions = CreateTokenOptions(signingCredentials, claims);
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        if (populateExp)
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(3);
        await _userManager.UpdateAsync(user);
        var accessToken = new JwtSecurityTokenHandler()
            .WriteToken(tokenOptions);

        return new TokenDto(accessToken, refreshToken);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(
            token, tokenValidationParameters, out securityToken
        );
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase)
           )
            return null;
        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(
            secret, SecurityAlgorithms.HmacSha256
        );
    }

    private async Task<IList<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!)
        };
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles
            .Select(r => new Claim(ClaimTypes.Role, r))
        );
        return claims;
    }

    private JwtSecurityToken CreateTokenOptions(
        SigningCredentials signingCredentials, IList<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var tokenOptions = new JwtSecurityToken(
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}