using HealthcareSystem.Core.Auth;
using HealthcareSystem.Infrastructure.Auth;

namespace HealthcareSystem.Application.Auth;

public class AuthService(IAuthRepository repository)
{
    private readonly IAuthRepository _repository = repository;

    public async Task<UserRegisterResponse> UserRegister(
        UserRegisterRequest request
    )
    {
        try
        {
            if (request.Password != request.ConfirmPassword)
                return new UserRegisterResponse(
                    400,
                    false,
                    "Error: Passwords are not equal"
                );
            var firstName = request.FirstName[0].ToString().ToUpper() +
                            request.FirstName[1..].ToLower();
            var lastName = request.LastName[0].ToString().ToUpper() +
                           request.LastName[1..].ToLower();

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = request.Username,
                Email = request.Email
            };
            var result = await _repository.CreateUserWithPasswordAsync(
                user, request.Password!
            );
            if (result.Succeeded)
            {
                await _repository.AddUserRoleAsync(user);
                return new UserRegisterResponse(
                    201,
                    true,
                    "User was successfully registered"
                );
            }

            var errors = string.Join(", ",
                result.Errors.Select(e => e.Description)
            ).Replace(".,", ",");
            return new UserRegisterResponse(
                400,
                false,
                $"Error: {errors}"
            );
        }
        catch (Exception ex)
        {
            return new UserRegisterResponse(
                404,
                false,
                $"Error: {ex.Message}"
            );
        }
    }
}