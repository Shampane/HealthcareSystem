using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Infrastructure.Auth;

namespace HealthcareSystem.Application.Auth;

public class AuthService
{
    private const string ErrorStatus = nameof(ResponseStatus.Error);
    private const string SuccessStatus = nameof(ResponseStatus.Success);
    private readonly IAuthRepository _repository;

    public AuthService(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResponse<UserDto>> Register(
        UserRegisterRequest request
    )
    {
        try
        {
            if (request.Password != request.ConfirmPassword)
                return new CreateResponse<UserDto>(
                    ErrorStatus, "Passwords do not match", null
                );

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _repository
                .CreateUserWithPasswordAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _repository.AddUserRoleAsync(user);
                var userDto = new UserDto(
                    user.FirstName, user.LastName,
                    user.UserName, user.Email
                );
                return new CreateResponse<UserDto>(
                    SuccessStatus, "The User was created", userDto
                );
            }

            var errors = string.Join(", ",
                result.Errors.Select(e => e.Description)
            ).Replace(".,", ",");
            return new CreateResponse<UserDto>(
                ErrorStatus, errors, null
            );
        }
        catch (Exception ex)
        {
            return new CreateResponse<UserDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<string>> Authenticate(
        UserAuthenticateRequest request
    )
    {
        try
        {
            var user =
                await _repository.FindUserByNameAsync(request.UserName);
            if (user == null)
                return new CreateResponse<string>(
                    ErrorStatus, "Invalid UserName", null
                );

            var isValid = await _repository
                .IsUserValidAsync(user, request.Password);
            if (!isValid)
                return new CreateResponse<string>(
                    ErrorStatus, "Invalid Password", null
                );
            var token = await _repository.CreateTokenAsync(user);
            return new CreateResponse<string>(
                SuccessStatus, "The JWT Token was created successfully",
                token
            );
        }
        catch (Exception ex)
        {
            return new CreateResponse<string>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }
}