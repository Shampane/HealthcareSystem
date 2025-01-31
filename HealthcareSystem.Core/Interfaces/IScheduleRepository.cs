using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IScheduleRepository {
    public Task<ICollection<Schedule>?> GetSchedulesByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    );

    public Task<Schedule?> GetScheduleByIdAsync(Guid scheduleId);
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