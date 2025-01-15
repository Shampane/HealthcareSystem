using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

    public async Task<string?> CreateTokenAsync(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaimsAsync(user);
        var tokenOptions = CreateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }

    public async Task<User?> FindUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user;
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