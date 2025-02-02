using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class UserMapping {
    public static UserDto ToDto(this User u) {
        return new UserDto(
            u.FirstName, u.LastName, u.UserName, u.Email
        );
    }
}