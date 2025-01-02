namespace HealthcareSystem.Application.Responses;

public record DoctorCreateResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);