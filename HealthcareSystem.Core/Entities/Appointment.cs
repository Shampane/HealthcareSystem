using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Core.Entities;

public class Appointment(
    Guid doctorId,
    string doctorName,
    Guid scheduleId,
    DateTime scheduleStartTime,
    DateTime scheduleEndTime,
    string userId,
    string userName
) {
    [Key]
    public Guid AppointmentId { get; init; }

    [ForeignKey(nameof(Doctor))]
    public Guid DoctorId { get; init; } = doctorId;

    public Doctor? Doctor { get; init; }
    public string DoctorName { get; init; } = doctorName;

    [ForeignKey(nameof(Schedule))]
    public Guid ScheduleId { get; init; } = scheduleId;

    public DateTime ScheduleStartTime { get; init; } = scheduleStartTime;
    public DateTime ScheduleEndTime { get; init; } = scheduleEndTime;
    public Schedule? Schedule { get; init; }

    [ForeignKey(nameof(User))]
    public string UserId { get; init; } = userId;

    public User? User { get; init; }
    public string UserName { get; init; } = userName;
}