using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IEmailRepository {
    public Task SendForgetPassword<T>(
        EmailMetadata emailMetadata, string templateFile, T templateModel
    );
}