using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Infrastructure.Schedules;

public interface IScheduleRepository
{
    public Task SaveAsync();

    public Task CreateAsync(Schedule schedule);

    public Task<Doctor> GetDoctorByIdAsync(Guid id);
}