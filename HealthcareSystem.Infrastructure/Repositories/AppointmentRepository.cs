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

    public async Task<ICollection<Appointment>?> GetAppointmentsByDoctor(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime
    ) {
        IQueryable<Appointment>? query = _dbContext.Appointments
            .AsNoTracking()
            .OrderBy(a => a.StartTime)
            .Where(a => a.DoctorId == doctorId);

        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = _getHelper.AddPagination(query, pageSize, pageIndex);
        return await query.ToListAsync();
    }

    public async Task<ICollection<Appointment>?> GetAppointmentsByUser(
        string userId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime
    ) {
        IQueryable<Appointment>? query = _dbContext.Appointments
            .AsNoTracking()
            .OrderBy(a => a.StartTime)
            .Where(a => a.UserId == userId);

        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = _getHelper.AddPagination(query, pageIndex, pageSize);

        return await query.ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentById(Guid id) {
        return await _dbContext.Appointments
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task CreateAppointment(Appointment appointment) {
        await _dbContext.Appointments.AddAsync(appointment);
        await SaveChanges();
    }

    public async Task RemoveAppointment(Appointment appointment) {
        _dbContext.Appointments.Remove(appointment);
        await SaveChanges();
    }

    public async Task SaveChanges() {
        await _dbContext.SaveChangesAsync();
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