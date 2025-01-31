using System.Security.Claims;
using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Application.Services;

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

    public async Task<CreateResponse<Token>> Authenticate(
        UserAuthenticateRequest request
    ) {
        try {
            User? user =
                await _repository.FindUserByNameAsync(request.UserName);
            if (user is null) {
                return new CreateResponse<Token>(
                    ErrorStatus, "Invalid UserName", null
                );
            }

            bool isValid = await _repository
                .IsUserValidAsync(user, request.Password);
            if (!isValid) {
                return new CreateResponse<Token>(
                    ErrorStatus, "Invalid Password", null
                );
            }
            Token? token = await _repository.CreateTokenAsync(user, true);
            return new CreateResponse<Token>(
                SuccessStatus, "The JWT Token was created successfully",
                token
            );
        }
        catch (Exception ex) {
            return new CreateResponse<Token>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<Token>> RefreshToken(
        Token token
    ) {
        try {
            ClaimsPrincipal principal =
                _repository.GetPrincipalFromExpiredToken(token.AccessToken);
            User? user = await _repository.FindUserByNameAsync(
                principal.Identity.Name
            );
            if (user is null ||
                user.RefreshToken != token.RefreshToken ||
                user.RefreshTokenExpiry <= DateTime.UtcNow) {
                return new CreateResponse<Token>(
                    ErrorStatus, "Refreshing the token was failed",
                    null
                );
            }

            Token? refreshedToken =
                await _repository.CreateTokenAsync(user, false);
            return new CreateResponse<Token>(
                SuccessStatus, "Refreshing the token was successful",
                refreshedToken
            );
        }
        catch (Exception ex) {
            return new CreateResponse<Token>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }
}