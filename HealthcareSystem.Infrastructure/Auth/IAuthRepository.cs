using HealthcareSystem.Core.Auth;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Infrastructure.Auth;

public interface IAuthRepository
{
    public Task<IdentityResult> CreateUserWithPasswordAsync(
        User user, string password
    );

    public Task AddUserRoleAsync(User user);
    public Task<User?> FindUserByNameAsync(string userName);
    public Task<bool> IsUserValidAsync(User user, string password);
    public Task<TokenDto?> CreateTokenAsync(User user, bool populateExp);
}