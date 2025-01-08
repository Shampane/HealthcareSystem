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

    public async Task<ICollection<Schedule>> GetSchedulesByDoctorIdAsync(
        Guid doctorId
    )
    {
        var doctors = await dbContext.Schedules
            .Where(s => s.DoctorId == doctorId)
            .ToListAsync();
        return doctors;
    }

    public async Task<Doctor> GetDoctorByIdAsync(Guid id)
    {
        var doctor = await dbContext.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == id);
        return doctor!;
    }

    public async Task<bool> IsSchedulesTimeAvailable(
        DateTime startTime, uint duration
    )
    {
        var sortedSchedules = await GetSchedulesAsync();
        foreach (var schedule in sortedSchedules)
        {
            var scheduleStartTime = schedule.StartTime;
            var scheduleEndTime =
                scheduleStartTime.AddMinutes(schedule.DurationInMinutes);
            if (startTime.AddMinutes(duration) <= scheduleStartTime ||
                startTime >= scheduleEndTime)
                return true;

            var isSchedulesIntersect = IsSchedulesIntersect(
                schedule.StartTime, startTime,
                schedule.DurationInMinutes, duration);
            if (isSchedulesIntersect)
                return false;
        }

        return true;
    }

    public async Task<ICollection<Schedule>> GetSchedulesAsync()
    {
        return await dbContext.Schedules
            .OrderBy(s => s.StartTime)
            .ToListAsync();
    }

    private bool IsSchedulesIntersect(
        DateTime aDate, DateTime bDate,
        uint aDuration, uint bDuration
    )
    {
        var isAllOneInAnother =
            bDate >= aDate &&
            bDate.AddMinutes(bDuration) <= aDate.AddMinutes(aDuration);
        var isStartTimeIntersect =
            bDate >= aDate &&
            bDate < aDate.AddMinutes(aDuration);
        var isEndTimeIntersect =
            bDate.AddMinutes(bDuration) > aDate &&
            bDate.AddMinutes(bDuration) <= aDate.AddMinutes(aDuration);
        return isAllOneInAnother ||
               isStartTimeIntersect ||
               isEndTimeIntersect;
    }
}