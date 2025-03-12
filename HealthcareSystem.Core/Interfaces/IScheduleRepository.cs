using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IScheduleRepository {
    public Task<ICollection<Schedule>?> GetSchedules(
        Guid? doctorId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime,
        CancellationToken cancellationToken
    );

    public Task<Schedule?> GetScheduleById(
        Guid id, CancellationToken cancellationToken
    );

    public Task<ICollection<Schedule>?> GetSchedulesByDoctorId(
        Guid doctorId, CancellationToken cancellationToken
    );

    public Task CreateSchedule(
        Schedule schedule, CancellationToken cancellationToken
    );

    public Task UpdateScheduleAvailable(
        Schedule schedule, CancellationToken cancellationToken
    );

    public Task RemoveSchedule(
        Schedule schedule, CancellationToken cancellationToken
    );

    public Task RemoveOldSchedules(CancellationToken cancellationToken);
    public Task<int> GetSchedulesCount(CancellationToken cancellationToken);

    public Task<bool> IsTimeAvailable(
        Schedule schedule, CancellationToken cancellationToken
    );
}