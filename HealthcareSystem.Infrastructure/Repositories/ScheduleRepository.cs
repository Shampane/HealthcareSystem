using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using HealthcareSystem.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository {
    private readonly AppDbContext _dbContext;
    private readonly GetHelper _getHelper;

    public ScheduleRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
        _getHelper = new GetHelper();
    }

    public async Task<ICollection<Schedule>?> GetSchedules(
        Guid? doctorId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime,
        CancellationToken cancellationToken
    ) {
        IQueryable<Schedule>? query = _dbContext.Schedules
            .AsNoTracking()
            .OrderBy(s => s.StartTime);

        query = doctorId is null ? query : query.Where(s => s.DoctorId == doctorId);
        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = _getHelper.AddPagination(query, pageSize, pageIndex);

        return await query.ToListAsync();
    }

    public async Task<Schedule?> GetScheduleById(
        Guid id, CancellationToken cancellationToken
    ) {
        return await _dbContext.Schedules.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task CreateSchedule(
        Schedule schedule, CancellationToken cancellationToken
    ) {
        if (!await IsTimeAvailable(schedule)) {
            throw new Exception("The schedule time is not available");
        }
        await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task UpdateScheduleAvailable(
        Schedule schedule, CancellationToken cancellationToken
    ) {
        _dbContext.Schedules.Update(schedule);
        await SaveChanges(cancellationToken);
    }

    public async Task RemoveSchedule(
        Schedule schedule, CancellationToken cancellationToken
    ) {
        _dbContext.Schedules.Remove(schedule);
        await SaveChanges(cancellationToken);
    }

    public async Task RemoveOldSchedules(CancellationToken cancellationToken) {
        List<Schedule>? oldSchedules = await _dbContext.Schedules.AsNoTracking()
            .Where(s => s.StartTime < DateTimeOffset.UtcNow)
            .ToListAsync(cancellationToken);
        _dbContext.Schedules.RemoveRange(oldSchedules);
        await SaveChanges(cancellationToken);
    }

    public async Task<int> GetSchedulesCount(CancellationToken cancellationToken) {
        return await _dbContext.Schedules.CountAsync(cancellationToken);
    }

    private async Task SaveChanges(CancellationToken cancellationToken) {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<bool> IsTimeAvailable(Schedule schedule) {
        DateTimeOffset sTime = schedule.StartTime;
        DateTimeOffset eTime = schedule.EndTime;
        return !await _dbContext.Schedules.AsNoTracking()
            .Where(s => s.DoctorId == schedule.DoctorId)
            .AnyAsync(s => sTime <= s.EndTime && eTime >= s.StartTime);
    }

    private static IQueryable<Schedule> AddGetSearch(
        IQueryable<Schedule> query, DateTimeOffset? searchStartTime,
        DateTimeOffset? searchEndTime
    ) {
        IQueryable<Schedule>? newQuery = searchStartTime.HasValue
            ? query.Where(s => s.StartTime >= searchStartTime)
            : query;
        return searchEndTime.HasValue
            ? newQuery.Where(s => s.EndTime <= searchEndTime)
            : newQuery;
    }
}