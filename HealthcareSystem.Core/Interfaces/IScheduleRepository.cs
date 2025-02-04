using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IScheduleRepository {
    public Task<ICollection<Schedule>?> GetSchedulesByDoctor(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime
    );

    public Task<Schedule?> GetScheduleById(Guid id);
    public Task CreateSchedule(Schedule schedule);
    public Task RemoveSchedule(Schedule schedule);
    public Task RemoveOldSchedules();
    public Task SaveChanges();
    public Task<int> GetSchedulesCount();
}