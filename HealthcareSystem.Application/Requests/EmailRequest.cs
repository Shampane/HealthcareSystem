using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Requests;

public record EmailRequest(
    EmailMetadata Data,
    string UserEmail
);