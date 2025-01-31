using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Entities;

public sealed class User : IdentityUser {
    [MaxLength(128)]
    public string? FirstName { get; init; }

    [MaxLength(128)]
    public string? LastName { get; init; }

    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}