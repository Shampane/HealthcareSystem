using HealthcareSystem.Core.Schedules;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Schedules;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _dbContext;

    public ScheduleRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Schedule>?> GetSchedulesByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    )
    {
        var query = _dbContext.Schedules
            .AsNoTracking()
            .OrderBy(s => s.StartTime)
            .Where(s => s.DoctorId == doctorId);

        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = AddGetPagination(query, pageSize, pageIndex);

        return await query.ToListAsync();
    }

    public async Task<Schedule?> GetScheduleByIdAsync(
        Guid scheduleId
    )
    {
        return await _dbContext.Schedules
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);
    }

    public async Task CreateScheduleAsync(Schedule schedule)
    {
        await _dbContext.Schedules.AddAsync(schedule);
        await SaveAsync();
    }


    public async Task RemoveScheduleAsync(Schedule schedule)
    {
        _dbContext.Schedules.Remove(schedule);
        await SaveAsync();
    }

    public async Task ClearOldSchedulesAsync()
    {
        var oldSchedules = await _dbContext.Schedules
            .Where(s => s.StartTime < DateTime.UtcNow)
            .ToListAsync();
        _dbContext.Schedules.RemoveRange(oldSchedules);
        await SaveAsync();
    }

    public async Task<Schedule?> FindScheduleByIdAsync(Guid scheduleId)
    {
        return await _dbContext.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);
    }

    public async Task<int> GetSchedulesCount()
    {
        return await _dbContext.Schedules.CountAsync();
    }

    public async Task<bool> IsSchedulesTimeAvailable(
        Guid doctorId, DateTime startTime, int durationInMinutes
    )
    {
        var endTime = startTime.AddMinutes(durationInMinutes);
        var schedules = await _dbContext.Schedules
            .AsNoTracking()
            .Where(s => s.DoctorId == doctorId)
            .OrderBy(s => s.StartTime)
            .ToListAsync();
        var afterTrigger = false;
        var beforeTrigger = false;
        foreach (var schedule in schedules)
        {
            var sTime = schedule.StartTime;
            var eTime = sTime.AddMinutes(schedule.DurationInMinutes);

            if (eTime <= startTime)
                afterTrigger = true;
            if (sTime >= endTime)
                beforeTrigger = true;
            if (afterTrigger && beforeTrigger)
                return true;

            if (sTime <= startTime && eTime >= endTime)
                return false;
            if (sTime <= startTime &&
                eTime > startTime &&
                eTime < endTime)
                return false;
            if (sTime > startTime &&
                sTime < endTime &&
                eTime >= endTime)
                return false;
        }

        return true;
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    private static IQueryable<Schedule> AddGetSearch(
        IQueryable<Schedule> query, DateTime? searchStartTime,
        DateTime? searchEndTime
    )
    {
        var newQuery = searchStartTime.HasValue
            ? query.Where(s => s.StartTime >= searchStartTime)
            : query;
        newQuery = searchEndTime.HasValue
            ? newQuery.Where(s =>
                s.StartTime.AddMinutes(s.DurationInMinutes) <=
                searchEndTime)
            : newQuery;
        return newQuery;
    }

    private static IQueryable<Schedule> AddGetPagination(
        IQueryable<Schedule> query, int? pageSize, int? pageIndex
    )
    {
        var newQuery = pageSize.HasValue && pageIndex.HasValue
            ? query.Skip((pageIndex.Value - 1) * pageSize.Value)
            : query;
        newQuery = pageSize.HasValue
            ? newQuery.Take(pageSize.Value)
            : newQuery;
        return newQuery;
    }
}