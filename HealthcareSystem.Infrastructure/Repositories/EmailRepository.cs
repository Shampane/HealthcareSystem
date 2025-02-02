using FluentEmail.Core;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HealthcareSystem.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository {
    private readonly IFluentEmail _email;
    private readonly UserManager<User> _userManager;

    public EmailRepository(IFluentEmail email, UserManager<User> userManager) {
        _email = email;
        _userManager = userManager;
    }

    public async Task SendForgetPassword(
        EmailMetadata emailMetadata, string templateFile, ForgetPassword model
    ) {
        await _email.To(emailMetadata.ToAddress)
            .Subject(emailMetadata.Subject)
            .UsingTemplateFromFile(templateFile, model)
            .SendAsync();
    }
}