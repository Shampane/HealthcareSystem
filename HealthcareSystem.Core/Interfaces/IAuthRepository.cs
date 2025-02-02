using System.Security.Claims;
using HealthcareSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Interfaces;

public interface IAuthRepository {
    public Task<IdentityResult> CreateUserWithPasswordAsync(
        User user, string password
    );

    public Task AddUserRoleAsync(User user);
    public Task<string> GenerateResetToken(User user);

    public Task<IdentityResult> ResetPassword(User user, string token,
        string password);

    public Task<User?> FindUserByNameAsync(string userName);
    public Task<User?> FindUserByEmail(string userEmail);
    public Task<bool> IsUserValidAsync(User user, string password);
    public Task<Token?> CreateTokenAsync(User user, bool populateExp);
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}