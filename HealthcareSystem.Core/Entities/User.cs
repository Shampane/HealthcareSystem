using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Entities;

public sealed class User : IdentityUser {
    public User() {
        Id = Guid.CreateVersion7().ToString();
    }

    public required string Gender { get; init; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
    public ICollection<Appointment>? Appointments { get; set; }
}