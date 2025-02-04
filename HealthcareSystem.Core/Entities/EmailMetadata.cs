using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Entities;

public class EmailMetadata {
    [Required(ErrorMessage = "The email address is required")]
    public required string ToAddress { get; init; }

    [Required(ErrorMessage = "The email subject is required")]
    public required string Subject { get; init; }

    public string? Attachment { get; init; }
}