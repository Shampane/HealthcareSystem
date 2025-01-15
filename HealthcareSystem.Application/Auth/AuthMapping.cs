using HealthcareSystem.Core.Auth;

namespace HealthcareSystem.Application.Auth;

public static class AuthMapping
{
    public static UserDto ToDto(this User u)
    {
        return new UserDto(
            u.FirstName, u.LastName, u.UserName, u.Email
        );
    }
}