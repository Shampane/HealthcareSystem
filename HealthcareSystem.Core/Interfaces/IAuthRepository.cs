using System.Security.Claims;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Records;

namespace HealthcareSystem.Core.Interfaces;

public interface IAuthRepository {
    public Task<User?> GetUserByEmail(string email);
    public Task CreateUserWithPassword(User user, string password);
    public Task AddRolesToUser(User user);
    public Task<Token?> CreateToken(User user, bool populateExp);

    public Task<string> CreateResetToken(User user);
    public Task ResetUserPassword(User user, string token, string password);

    public Task<bool> IsUserPasswordValid(User user, string password);
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}