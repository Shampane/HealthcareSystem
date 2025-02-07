using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IEmailRepository {
    public Task SendEmailWithTemplate<TModel>(
        EmailMetadata emailMetadata, string templateFile,
        TModel templateModel, CancellationToken cancellationToken
    );
}