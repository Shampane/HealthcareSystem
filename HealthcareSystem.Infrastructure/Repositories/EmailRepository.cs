using FluentEmail.Core;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;

namespace HealthcareSystem.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository {
    private readonly IFluentEmail _email;

    public EmailRepository(IFluentEmail email) {
        _email = email;
    }

    public async Task SendForgetPassword<T>(
        EmailMetadata emailMetadata, string templateFile, T templateModel
    ) {
        await _email.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .UsingTemplateFromFile(templateFile, templateModel)
            .SendAsync();
    }
}