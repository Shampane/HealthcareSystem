using FluentValidation;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Validators;

public class EmailMetadataValidator : AbstractValidator<EmailMetadata> {
    public EmailMetadataValidator() {
        RuleFor(e => e.ToAddress)
            .NotEmpty()
            .WithMessage("The email address cannot be empty");
        RuleFor(e => e.Subject)
            .NotEmpty()
            .WithMessage("The email subject cannot be empty");
    }
}