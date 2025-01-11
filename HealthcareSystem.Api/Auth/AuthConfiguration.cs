using HealthcareSystem.Core.Auth;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Api.Auth;

public static class AuthConfiguration
{
    public static IServiceCollection ConfigureAuth(
        this IServiceCollection services
    )
    {
        services.AddAuthentication();
        services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequiredLength = 8;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthorization();

        return services;
    }
}