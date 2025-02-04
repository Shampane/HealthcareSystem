using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Entities;

public sealed class User : IdentityUser {
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiry { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}