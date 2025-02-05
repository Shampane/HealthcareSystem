using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class ScheduleMapping {
    public static ScheduleDto ToDto(this Schedule s) {
        return new ScheduleDto {
            Id = s.Id, DoctorName = s.DoctorName, StartTime = s.StartTime,
            EndTime = s.EndTime, IsAvailable = s.IsAvailable
        };
    }
}