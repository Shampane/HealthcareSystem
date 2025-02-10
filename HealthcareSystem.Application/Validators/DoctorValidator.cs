using FluentValidation;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Validators;

public class DoctorValidator : AbstractValidator<Doctor> {
    public DoctorValidator() {
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("Doctor name cannot be empty")
            .MaximumLength(64)
            .WithMessage("Doctor name cannot be longer than 64 characters");
        RuleFor(d => d.Description)
            .NotEmpty()
            .WithMessage("Doctor description cannot be empty");
        RuleFor(d => d.ExperienceAge)
            .NotEmpty()
            .WithMessage("Doctor experience age cannot be empty")
            .GreaterThan(0)
            .WithMessage("Doctor experience age must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Doctor experience age must be less or equal to 100");
        RuleFor(d => d.FeeInDollars)
            .NotEmpty()
            .WithMessage("Doctor fee cannot be empty")
            .GreaterThan(0)
            .WithMessage("Doctor fee must be greater than 0 dollars");
        RuleFor(d => d.Specialization)
            .NotEmpty()
            .WithMessage("Doctor specialization cannot be empty")
            .MaximumLength(128)
            .WithMessage(
                "Doctor specialization cannot be longer than 128 characters"
            );
        RuleFor(d => d.PhoneNumber)
            .NotEmpty()
            .WithMessage("Doctor phone number cannot be empty")
            .MaximumLength(12)
            .WithMessage(
                "Doctor phone number cannot be longer than 12 characters"
            );
    }
}