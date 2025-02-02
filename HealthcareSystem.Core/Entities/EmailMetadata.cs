namespace HealthcareSystem.Core.Entities;

public class EmailMetadata(
    string toAddress,
    string subject,
    string? attachmentPath = ""
) {
    public string ToAddress { get; init; } = toAddress;
    public string Subject { get; init; } = subject;
    public string? AttachmentPath { get; init; } = attachmentPath;
}