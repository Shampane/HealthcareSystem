using Microsoft.OpenApi.Models;

namespace HealthcareSystem.Api.Extensions;

public static class SwaggerExtensions {
    public static IServiceCollection ConfigureSwagger(
        this IServiceCollection services
    ) {
        services.AddSwaggerGen(opt => {
            opt.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });

        return services;
    }
}