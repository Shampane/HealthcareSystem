using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Application.Mappings;

public static class ScheduleMapping {
    public static ScheduleDto ToDto(this Schedule s) {
        return new ScheduleDto(
            s.ScheduleId, s.DoctorId, s.StartTime,
            s.StartTime.AddMinutes(s.DurationInMinutes), s.IsAvailable
        );
    }
}