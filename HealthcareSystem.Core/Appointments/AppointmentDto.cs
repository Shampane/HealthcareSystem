namespace HealthcareSystem.Core.Appointments;

public class AppointmentDto
{
    public AppointmentDto(
        Guid appointmentId, Guid doctorId, string doctorName,
        Guid scheduleId, DateTime scheduleStartTime,
        DateTime scheduleEndTime, string userId, string userName
    )
    {
        AppointmentId = appointmentId;
        DoctorId = doctorId;
        DoctorName = doctorName;
        ScheduleId = scheduleId;
        ScheduleStartTime = scheduleStartTime;
        ScheduleEndTime = scheduleEndTime;
        UserId = userId;
        UserName = userName;
    }

    public Guid AppointmentId { get; init; }
    public Guid DoctorId { get; init; }
    public string DoctorName { get; init; }
    public Guid ScheduleId { get; init; }
    public DateTime ScheduleStartTime { get; init; }
    public DateTime ScheduleEndTime { get; init; }
    public string UserId { get; init; }
    public string UserName { get; init; }
}