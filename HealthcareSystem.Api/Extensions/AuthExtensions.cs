using System.Text;
using HealthcareSystem.Application.Services;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using HealthcareSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareSystem.Api.Extensions;

public static class AuthExtensions {
    public static IServiceCollection ConfigureAuth(
        this IServiceCollection services, IConfiguration configuration
    ) {
        IConfigurationSection? jwtSettings = configuration.GetSection("Jwt");

        services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt => {
                opt.TokenValidationParameters =
                    new TokenValidationParameters {
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
        services.AddIdentity<User, IdentityRole>(opt => {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.Configure<DataProtectionTokenProviderOptions>(opt => {
            opt.TokenLifespan = TimeSpan.FromHours(1);
        });
        services.AddAuthorizationBuilder()
            .AddPolicy("UserPolicy", policy => {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes("Bearer");
                policy.RequireRole("User", "Admin");
            })
            .AddPolicy("DoctorPolicy", policy => {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes("Bearer");
                policy.RequireRole("Doctor", "Admin");
            })
            .AddPolicy("AdminPolicy", policy => {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes("Bearer");
                policy.RequireRole("Admin");
            });

        return services;
    }

    public static IServiceCollection AddAuthServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<AuthService>();
        return services;
    }
}