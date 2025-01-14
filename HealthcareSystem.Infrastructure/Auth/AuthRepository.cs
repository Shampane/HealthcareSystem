using HealthcareSystem.Core.Auth;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Infrastructure.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<User> _userManager;

    public AuthRepository(UserManager<User> userManager)
    {
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
        var roles = new List<string> { "User" };
        await _userManager.AddToRolesAsync(user, roles);
    }
}