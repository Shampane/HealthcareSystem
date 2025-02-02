namespace HealthcareSystem.Application.Dtos;

public class UserDto(
    string? firstName,
    string? lastName,
    string? username,
    string? email
) {
    public string? FirstName { get; init; } = firstName;
    public string? LastName { get; init; } = lastName;
    public string? Username { get; init; } = username;
    public string? Email { get; init; } = email;
}