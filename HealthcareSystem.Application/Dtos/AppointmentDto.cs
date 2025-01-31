namespace HealthcareSystem.Application.Dtos;

public class AppointmentDto(
    Guid appointmentId,
    Guid doctorId,
    string doctorName,
    Guid scheduleId,
    DateTime scheduleStartTime,
    DateTime scheduleEndTime,
    string userId,
    string userName
) {
    public Guid AppointmentId { get; init; } = appointmentId;
    public Guid DoctorId { get; init; } = doctorId;
    public string DoctorName { get; init; } = doctorName;
    public Guid ScheduleId { get; init; } = scheduleId;
    public DateTime ScheduleStartTime { get; init; } = scheduleStartTime;
    public DateTime ScheduleEndTime { get; init; } = scheduleEndTime;
    public string UserId { get; init; } = userId;
    public string UserName { get; init; } = userName;
}