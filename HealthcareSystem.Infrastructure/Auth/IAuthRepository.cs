using HealthcareSystem.Core.Auth;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Infrastructure.Auth;

public interface IAuthRepository
{
    public bool EqualPasswords(string password, string confirmPassword);


    public Task<IdentityResult> CreateUserWithPasswordAsync(
        User user, string password
    );

    public Task AddUserRoleAsync(User user);
}