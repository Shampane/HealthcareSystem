using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Infrastructure.Schedules;

public interface IScheduleRepository
{
    public Task SaveAsync();

    public Task CreateAsync(Schedule schedule);

    public Task<ICollection<Schedule>> GetSchedulesAsync();

    public Task<ICollection<Schedule>> GetSchedulesByDoctorIdAsync(
        Guid doctorId
    );

    public Task<Doctor> GetDoctorByIdAsync(Guid id);

    public Task<bool> IsSchedulesTimeAvailable(
        DateTime startTime, uint duration
    );
}