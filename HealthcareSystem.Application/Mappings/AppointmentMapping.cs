using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class AppointmentMapping {
    public static AppointmentDto ToDto(this Appointment a) {
        return new AppointmentDto {
            Id = a.Id, DoctorId = a.DoctorId, DoctorName = a.DoctorName,
            ScheduleId = a.ScheduleId, StartTime = a.StartTime,
            EndTime = a.EndTime, UserId = a.UserId, UserName = a.UserName
        };
    }
}