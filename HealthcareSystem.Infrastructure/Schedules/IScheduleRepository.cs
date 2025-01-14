using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Infrastructure.Schedules;

public interface IScheduleRepository
{
    public Task<ICollection<ScheduleDto>?> GetSchedulesByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    );

    public Task<ScheduleDto?> GetScheduleByIdAsync(Guid scheduleId);
    public Task CreateScheduleAsync(Schedule schedule);
    public Task RemoveScheduleAsync(Schedule schedule);
    public Task ClearOldSchedulesAsync();

    public Task<bool> IsSchedulesTimeAvailable(
        Guid doctorId, DateTime startTime, int durationInMinutes
    );

    public Task<Schedule?> FindScheduleByIdAsync(Guid scheduleId);

    public Task SaveAsync();

    public Task<int> GetSchedulesCount();
}