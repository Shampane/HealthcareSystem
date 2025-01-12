using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Core.Appointments;

public class Appointment
{
    [Key] public Guid AppointmentId { get; set; }

    [ForeignKey(nameof(Doctor))] public Guid DoctorId { get; set; }

    [Required] public Doctor? Doctor { get; set; }

    [ForeignKey(nameof(Schedule))] public Guid ScheduleId { get; set; }

    [Required] public Schedule? Schedule { get; set; }

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    [Required] public User? User { get; set; }
}