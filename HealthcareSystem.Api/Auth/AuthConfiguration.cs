using System.Text;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HealthcareSystem.Api.Auth;

public static class AuthConfiguration
{
    public static IServiceCollection ConfigureAuth(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        var jwtSettings = configuration.GetSection("Jwt");

        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters =
                    new TokenValidationParameters
                    {
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
        services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        services.AddAuthorization(opt =>
        {
            opt.AddPolicy("UserPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes("Bearer");
                policy.RequireRole("User", "Admin");
            });
            opt.AddPolicy("DoctorPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes("Bearer");
                policy.RequireRole("Doctor", "Admin");
            });
            opt.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddAuthenticationSchemes("Bearer");
                policy.RequireRole("Admin");
            });
        });

        return services;
    }
}