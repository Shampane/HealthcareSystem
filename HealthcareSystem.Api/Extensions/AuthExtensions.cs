using System.Text;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using HealthcareSystem.Infrastructure.Repositories;
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
            });

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
            opt => { opt.TokenLifespan = TimeSpan.FromHours(1); }
        );

        services.AddAuthorizationBuilder()
            .SetDefaultPolicy(
                new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole("User", "Admin")
                    .Build()
            )
            .AddPolicy(
                "DoctorPolicy", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireRole("Doctor", "Admin");
                }
            )
            .AddPolicy(
                "AdminPolicy", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireRole("Admin");
                }
            );

        return services;
    }

    public static IServiceCollection AddAuthServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IAuthRepository, AuthRepository>();
        //services.AddScoped<AuthService>();
        return services;
    }
}