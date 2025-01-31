using System.Security.Claims;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Application.Auth;

public class AuthService {
    private const string ErrorStatus = nameof(ResponseStatus.Error);
    private const string SuccessStatus = nameof(ResponseStatus.Success);
    private readonly IAuthRepository _repository;

    public AuthService(IAuthRepository repository) {
        _repository = repository;
    }

    public async Task<CreateResponse<UserDto>> Register(
        UserRegisterRequest request
    ) {
        try {
            if (request.Password != request.ConfirmPassword) {
                return new CreateResponse<UserDto>(
                    ErrorStatus, "Passwords do not match", null
                );
            }

            var user = new User {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            IdentityResult result = await _repository
                .CreateUserWithPasswordAsync(user, request.Password);
            if (result.Succeeded) {
                await _repository.AddUserRoleAsync(user);
                var userDto = new UserDto(
                    user.FirstName, user.LastName,
                    user.UserName, user.Email
                );
                return new CreateResponse<UserDto>(
                    SuccessStatus, "The User was created", userDto
                );
            }

            string errors = string.Join(", ",
                result.Errors.Select(e => e.Description)
            ).Replace(".,", ",");
            return new CreateResponse<UserDto>(
                ErrorStatus, errors, null
            );
        }
        catch (Exception ex) {
            return new CreateResponse<UserDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<TokenDto>> Authenticate(
        UserAuthenticateRequest request
    ) {
        try {
            User? user =
                await _repository.FindUserByNameAsync(request.UserName);
            if (user is null) {
                return new CreateResponse<TokenDto>(
                    ErrorStatus, "Invalid UserName", null
                );
            }

            bool isValid = await _repository
                .IsUserValidAsync(user, request.Password);
            if (!isValid) {
                return new CreateResponse<TokenDto>(
                    ErrorStatus, "Invalid Password", null
                );
            }
            TokenDto? token = await _repository.CreateTokenAsync(user, true);
            return new CreateResponse<TokenDto>(
                SuccessStatus, "The JWT Token was created successfully",
                token
            );
        }
        catch (Exception ex) {
            return new CreateResponse<TokenDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<TokenDto>> RefreshToken(
        TokenDto tokenDto
    ) {
        try {
            ClaimsPrincipal principal =
                _repository.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            User? user = await _repository.FindUserByNameAsync(
                principal.Identity.Name
            );
            if (user is null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiry <= DateTime.UtcNow) {
                return new CreateResponse<TokenDto>(
                    ErrorStatus, "Refreshing the token was failed",
                    null
                );
            }

            TokenDto? refreshedToken =
                await _repository.CreateTokenAsync(user, false);
            return new CreateResponse<TokenDto>(
                SuccessStatus, "Refreshing the token was successful",
                refreshedToken
            );
        }
        catch (Exception ex) {
            return new CreateResponse<TokenDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }
}