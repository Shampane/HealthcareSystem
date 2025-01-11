using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Core.Auth;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}