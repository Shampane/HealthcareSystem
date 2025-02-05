using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class UserMapping {
    public static UserDto ToDto(this User u) {
        return new UserDto {
            Id = u.Id, Name = u.UserName, Email = u.Email
        };
    }
}