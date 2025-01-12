using HealthcareSystem.Core.Appointments;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Auth;

public class User : IdentityUser
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    public ICollection<Appointment>? Appointments { get; set; } =
        new List<Appointment>();
}