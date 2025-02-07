using FluentValidation;
using HealthcareSystem.Api.Middlewares;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Validators;
using HealthcareSystem.Infrastructure.Repositories;

namespace HealthcareSystem.Api.Extensions;

public static class AppBuilderExtensions {
    public static IServiceCollection AddMyRepositories(
        this IServiceCollection services
    ) {
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();

        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        return services;
    }

    public static IServiceCollection AddMyValidators(
        this IServiceCollection services
    ) {
        services.AddScoped<IValidator<Doctor>, DoctorValidator>();
        services.AddScoped<IValidator<Schedule>, ScheduleValidator>();
        services.AddScoped<IValidator<Appointment>, AppointmentValidator>();
        services.AddScoped<IValidator<EmailMetadata>, EmailMetadataValidator>();
        return services;
    }

    public static IServiceCollection AddMyExceptionHandlers(
        this IServiceCollection services
    ) {
        services.AddExceptionHandler<AppExceptionHandler>();
        services.AddProblemDetails();
        return services;
    }
}