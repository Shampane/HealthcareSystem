using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Auth;

public sealed class User : IdentityUser
{
    [MaxLength(128)] public string? FirstName { get; init; }
    [MaxLength(128)] public string? LastName { get; init; }
}