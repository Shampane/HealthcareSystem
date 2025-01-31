using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class AuthMapping {
    public static UserDto ToDto(this User u) {
        return new UserDto(
            u.FirstName, u.LastName, u.UserName, u.Email
        );
    }
}