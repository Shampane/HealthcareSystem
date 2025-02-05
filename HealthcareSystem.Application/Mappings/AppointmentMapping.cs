using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class AppointmentMapping {
    public static AppointmentDto ToDto(this Appointment a) {
        return new AppointmentDto {
            Id = a.Id, DoctorName = a.DoctorName, StartTime = a.StartTime,
            EndTime = a.EndTime, UserName = a.UserName
        };
    }
}