using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Core.Appointments;

public class Appointment
{
    public Appointment(
        Guid doctorId, string doctorName, Guid scheduleId,
        DateTime scheduleStartTime, DateTime scheduleEndTime,
        string userId, string userName
    )
    {
        DoctorId = doctorId;
        DoctorName = doctorName;
        ScheduleId = scheduleId;
        ScheduleStartTime = scheduleStartTime;
        ScheduleEndTime = scheduleEndTime;
        UserId = userId;
        UserName = userName;
    }

    [Key] public Guid AppointmentId { get; init; }

    [ForeignKey(nameof(Doctor))] public Guid DoctorId { get; init; }

    public Doctor? Doctor { get; init; }
    [MaxLength(128)] public string DoctorName { get; init; }

    [ForeignKey(nameof(Schedule))] public Guid ScheduleId { get; init; }
    public DateTime ScheduleStartTime { get; init; }
    public DateTime ScheduleEndTime { get; init; }

    public Schedule? Schedule { get; init; }

    [MaxLength(256)]
    [ForeignKey(nameof(User))]
    public string UserId { get; init; }

    public User? User { get; init; }
    [MaxLength(128)] public string UserName { get; init; }
}