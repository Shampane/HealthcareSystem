using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Core.Models;

public class Appointment
{
    [Key] public Guid AppointmentId { get; set; }

    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; }

    public Guid UserId { get; set; }

    [DataType(DataType.Date)]
    public DateTime AppointmentDate { get; set; }
}