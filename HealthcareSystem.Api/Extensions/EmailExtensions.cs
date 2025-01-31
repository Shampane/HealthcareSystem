namespace HealthcareSystem.Api.Email;

public static class EmailServicesExtension {
    public static IServiceCollection AddEmailServices(
        this IServiceCollection services, IConfiguration configuration
    ) {
        IConfigurationSection emailConfiguration =
            configuration.GetSection("Email");
        string? defaultFrom = emailConfiguration["DefaultFrom"];
        string? host = emailConfiguration["Host"];
        int port = int.Parse(emailConfiguration["Port"]!);

        services.AddFluentEmail(defaultFrom)
            .AddSmtpSender(host, port);

        return services;
    }
}