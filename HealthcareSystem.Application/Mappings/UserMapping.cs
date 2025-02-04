using HealthcareSystem.Application.Dtos;

namespace HealthcareSystem.Application.Mappings;

public static class UserMapping {
    public static UserDto ToDto(this UserDto u) {
        return new UserDto {
            Id = u.Id, Name = u.Name, Email = u.Email
        };
    }
}