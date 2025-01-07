using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Schedules;

public class ScheduleRepository(AppDbContext dbContext)
    : IScheduleRepository
{
    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(Schedule schedule)
    {
        await dbContext.Schedules.AddAsync(schedule);
        await SaveAsync();
    }

    public async Task<Doctor> GetDoctorByIdAsync(Guid id)
    {
        var doctor = await dbContext.Doctors.FirstOrDefaultAsync(d =>
            d.DoctorId == id
        );
        return doctor!;
    }
}