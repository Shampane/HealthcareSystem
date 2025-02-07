namespace HealthcareSystem.Core.Entities;

public class EmailMetadata {
    public string ToAddress { get; init; } = string.Empty;
    public string Subject { get; init; } = string.Empty;
    public string? Attachment { get; init; }
}