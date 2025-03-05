using System.Text;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareSystem.Api.Extensions;

public static class AuthExtensions {
    public static IServiceCollection ConfigureAuth(
        this IServiceCollection services, IConfiguration configuration
    ) {
        IConfigurationSection? jwtSettings = configuration.GetSection("Jwt");

        services.ConfigureAuthentication(jwtSettings);
        services.ConfigureIdentity();
        services.ConfigureRoles();

        return services;
    }

    private static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services, IConfigurationSection jwtSettings
    ) {
        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
                    )
                };
                options.Events = new JwtBearerEvents {
                    OnMessageReceived = context => {
                        context.Request.Cookies.TryGetValue(
                            "accessToken", out string? accessToken
                        );
                        if (!string.IsNullOrEmpty(accessToken)) {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        return services;
    }

    private static IServiceCollection ConfigureIdentity(
        this IServiceCollection services
    ) {
        services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(
            options => options.TokenLifespan = TimeSpan.FromHours(1)
        );
        return services;
    }

    private static IServiceCollection ConfigureRoles(
        this IServiceCollection services
    ) {
        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Admin"))
            .AddPolicy("DoctorPolicy", policy => policy.RequireRole("Doctor", "Admin"))
            .AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));

        return services;
    }
}