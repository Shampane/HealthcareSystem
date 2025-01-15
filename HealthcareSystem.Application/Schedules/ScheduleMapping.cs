using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Application.Schedules;

public static class ScheduleMapping
{
    public static ScheduleDto ToDto(this Schedule s)
    {
        return new ScheduleDto(
            s.ScheduleId, s.DoctorId, s.StartTime,
            s.StartTime.AddMinutes(s.DurationInMinutes), s.IsAvailable
        );
    }
}