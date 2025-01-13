namespace HealthcareSystem.Core.Appointments;

public class AppointmentDto
{
    public Guid AppointmentId { get; init; }
    public Guid DoctorId { get; init; }
    public string DoctorName { get; init; } = string.Empty;
    public Guid ScheduleId { get; init; }
    public DateTime ScheduleStartTime { get; init; }
    public DateTime ScheduleEndTime { get; init; }
    public string UserId { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
}