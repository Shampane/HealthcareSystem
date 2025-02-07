using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using HealthcareSystem.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository {
    private readonly AppDbContext _dbContext;
    private readonly GetHelper _getHelper;

    public AppointmentRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
        _getHelper = new GetHelper();
    }

    public async Task<ICollection<Appointment>?> GetAppointments(
        Guid? doctorId, string? userId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime,
        CancellationToken cancellationToken
    ) {
        IQueryable<Appointment>? query = _dbContext.Appointments
            .AsNoTracking()
            .OrderBy(a => a.StartTime);

        query = doctorId is null ? query : query.Where(a => a.DoctorId == doctorId);
        query = userId is null ? query : query.Where(a => a.UserId == userId);
        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = _getHelper.AddPagination(query, pageSize, pageIndex);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Appointment?> GetAppointmentById(
        Guid id, CancellationToken cancellationToken
    ) {
        return await _dbContext.Appointments
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task CreateAppointment(
        Appointment appointment, CancellationToken cancellationToken
    ) {
        await _dbContext.Appointments.AddAsync(appointment, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task RemoveAppointment(
        Appointment appointment, CancellationToken cancellationToken
    ) {
        _dbContext.Appointments.Remove(appointment);
        await SaveChanges(cancellationToken);
    }

    private async Task SaveChanges(CancellationToken cancellationToken) {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static IQueryable<Appointment> AddGetSearch(
        IQueryable<Appointment> query, DateTimeOffset? searchStartTime,
        DateTimeOffset? searchEndTime
    ) {
        IQueryable<Appointment>? newQuery = searchStartTime.HasValue
            ? query.Where(a => a.StartTime >= searchStartTime)
            : query;
        return searchEndTime.HasValue
            ? newQuery.Where(a => a.EndTime <= searchEndTime)
            : newQuery;
    }
}