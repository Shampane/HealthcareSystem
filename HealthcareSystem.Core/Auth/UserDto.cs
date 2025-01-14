namespace HealthcareSystem.Core.Auth;

public class UserDto
{
    public UserDto(
        string firstName, string lastName,
        string username, string email
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
    }

    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Username { get; init; }
    public string Email { get; init; }
}