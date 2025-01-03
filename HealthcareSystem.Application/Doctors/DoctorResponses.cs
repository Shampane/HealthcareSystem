using HealthcareSystem.Core.Doctors;

namespace HealthcareSystem.Application.Doctors;

public record DoctorGetByIdResponse(
    int StatusCode,
    bool IsSuccess,
    string Message,
    Doctor? Doctor
);

public record DoctorGetResponse(
    int StatusCode,
    bool IsSuccess,
    string Message,
    ICollection<Doctor>? Doctors
);

public record DoctorCreateResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);

public record DoctorRemoveResponse(
    int StatusCode,
    bool IsSuccess,
    string Message
);