using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class AppointmentMapping {
    public static AppointmentDto ToDto(this Appointment a) {
        return new AppointmentDto(
            a.AppointmentId, a.DoctorId, a.DoctorName,
            a.ScheduleId, a.ScheduleStartTime,
            a.ScheduleEndTime, a.UserId, a.UserName
        );
    }
}