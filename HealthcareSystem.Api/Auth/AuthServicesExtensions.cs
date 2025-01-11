using HealthcareSystem.Application.Auth;
using HealthcareSystem.Infrastructure.Auth;

namespace HealthcareSystem.Api.Auth;

public static class AuthServicesExtensions
{
    public static IServiceCollection AddAuthServices(
        this IServiceCollection services
    )
    {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<AuthService>();
        return services;
    }
}