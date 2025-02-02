using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.Extensions;

public static class EmailServicesExtension {
    public static IServiceCollection ConfigureEmail(
        this IServiceCollection services, IConfiguration configuration
    ) {
        IConfigurationSection emailConfiguration =
            configuration.GetSection("Email");
        string? defaultFrom = emailConfiguration["DefaultFrom"];
        string? host = emailConfiguration["Host"];
        int port = int.Parse(emailConfiguration["Port"]!);

        services.AddFluentEmail(defaultFrom)
            .AddSmtpSender(host, port)
            .AddRazorRenderer();

        return services;
    }

    public static IServiceCollection AddEmailServices(
        this IServiceCollection services
    ) {
        services.AddScoped<IEmailRepository, EmailRepository>();
        return services;
    }
}