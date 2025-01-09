using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Infrastructure.Schedules;

public interface IScheduleRepository
{
    public Task SaveAsync();

    public Task<bool> IsSchedulesTimeAvailable(
        DateTime startTime, uint duration
    );

    public Task<Schedule> GetScheduleByIdAsync(Guid id);

    public Task CreateAsync(Schedule schedule);

    public Task<ICollection<Schedule>> GetSchedulesAsync();

    public Task<ICollection<Schedule>> GetSchedulesByDoctorIdAsync(
        Guid doctorId
    );

    public Task<Doctor> GetDoctorByIdAsync(Guid id);
    public Task ClearOldSchedulesAsync();
    public Task<int> GetSchedulesCount();
}