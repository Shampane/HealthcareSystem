using System.ComponentModel.DataAnnotations;
using HealthcareSystem.Core.Doctors;

namespace HealthcareSystem.Core.Appointments;

public class Appointment
{
    [Key] public Guid AppointmentId { get; set; }

    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; }

    public Guid UserId { get; set; }

    [DataType(DataType.Date)] public DateTime AppointmentDate { get; set; }
}