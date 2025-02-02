using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IEmailRepository {
    public Task SendForgetPassword(
        EmailMetadata emailMetadata, string templateFile, ForgetPassword model
    );
}