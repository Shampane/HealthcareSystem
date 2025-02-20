using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Records;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HealthcareSystem.Core.Interfaces;

public interface IAuthRepository
{
    public Task<User?> GetUserByName(string name);
    public Task<User?> GetUserByEmail(string email);
    public Task<IdentityResult> CreateUserWithPassword(User user, string password);
    public Task AddRolesToUser(User user);
    public Task<Token?> CreateToken(User user, bool populateExp);

    public Task<string> CreateResetToken(User user);

    public Task<IdentityResult> ResetUserPassword(
        User user, string token, string password
    );

    public Task<string> CreateTwoFactorToken(User user);
    public Task<IList<string>> GetTwoFactorProviders(User user);
    public Task SetEmailTwoFactor(User user, bool enabled);
    public Task<bool> IsUserHasTwoFactor(User user);
    public Task<bool> IsTwoFactorValid(User user, string provider, string token);

    public Task<IList<string>> GetUserRoles(User user);
    public Task<bool> IsUserPasswordValid(User user, string password);
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}