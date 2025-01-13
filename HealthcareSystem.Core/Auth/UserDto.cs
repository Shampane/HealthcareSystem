using HealthcareSystem.Core.Appointments;

namespace HealthcareSystem.Core.Auth;

public class UserDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

    public ICollection<Appointment> Appointments { get; init; } =
        new List<Appointment>();
}