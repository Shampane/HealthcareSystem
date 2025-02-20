using Scalar.AspNetCore;

namespace HealthcareSystem.Api.Extensions;

public static class AppExtensions {
    public static WebApplication AddScalarApi(
        this WebApplication app
    ) {
        if (!app.Environment.IsDevelopment()) {
            return app;
        }

        app.MapOpenApi();
        app.MapScalarApiReference(options => {
            options.Servers = [
                new ScalarServer("https://localhost:8081")
            ];
        });
        return app;
    }
}