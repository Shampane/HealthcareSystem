using FluentValidation;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Validators;

public class ScheduleValidator : AbstractValidator<Schedule> {
    public ScheduleValidator() {
        RuleFor(s => s.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor id cannot be empty");
        RuleFor(s => s.DoctorName)
            .NotEmpty()
            .WithMessage("Doctor name cannot be empty");
        RuleFor(s => s.StartTime)
            .NotEmpty()
            .WithMessage("Start time cannot be empty");
        RuleFor(s => s.EndTime)
            .NotEmpty()
            .WithMessage("End time cannot be empty");
    }
}