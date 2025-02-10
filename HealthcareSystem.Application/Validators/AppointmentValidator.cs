using FluentValidation;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Validators;

public class AppointmentValidator : AbstractValidator<Appointment> {
    public AppointmentValidator() {
        RuleFor(a => a.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor id cannot be empty");
        RuleFor(a => a.DoctorName)
            .NotEmpty()
            .WithMessage("Doctor name cannot be empty");
        RuleFor(a => a.ScheduleId)
            .NotEmpty()
            .WithMessage("Schedule id cannot be empty");
        RuleFor(a => a.StartTime)
            .NotEmpty()
            .WithMessage("Start time cannot be empty");
        RuleFor(a => a.EndTime)
            .NotEmpty()
            .WithMessage("End time cannot be empty");
        RuleFor(a => a.UserId)
            .NotEmpty()
            .WithMessage("User id cannot be empty");
        RuleFor(a => a.UserName)
            .NotEmpty()
            .WithMessage("User name cannot be empty");
    }
}