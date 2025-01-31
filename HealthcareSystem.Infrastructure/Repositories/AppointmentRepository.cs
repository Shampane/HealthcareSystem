using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository {
    private readonly AppDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public AppointmentRepository(
        AppDbContext dbContext,
        UserManager<User> userManager
    ) {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<ICollection<Appointment>?>
        GetAppointmentsByDoctorAsync(
            Guid doctorId, int? pageIndex, int? pageSize,
            DateTime? searchStartTime, DateTime? searchEndTime
        ) {
        IQueryable<Appointment>? query = _dbContext.Appointments
            .AsNoTracking()
            .OrderBy(a => a.ScheduleStartTime)
            .Where(a => a.DoctorId == doctorId);

        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = AddGetPagination(query, pageSize, pageIndex);

        return await query.ToListAsync();
    }

    public async Task<ICollection<Appointment>?>
        GetAppointmentsByUserAsync(
            string userId, int? pageIndex, int? pageSize,
            DateTime? searchStartTime, DateTime? searchEndTime
        ) {
        IQueryable<Appointment>? query = _dbContext.Appointments
            .AsNoTracking()
            .OrderBy(a => a.ScheduleStartTime)
            .Where(a => a.UserId == userId);

        query = AddGetSearch(query, searchStartTime, searchEndTime);
        query = AddGetPagination(query, pageSize, pageIndex);

        return await query.ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(Guid id) {
        Appointment? appointment = await _dbContext.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AppointmentId == id);

        return appointment;
    }

    public async Task CreateAppointmentAsync(Appointment appointment) {
        await _dbContext.Appointments.AddAsync(appointment);
        await SaveAsync();
    }

    public async Task RemoveAppointmentAsync(Appointment appointment) {
        _dbContext.Appointments.Remove(appointment);
        await SaveAsync();
    }

    public async Task<Appointment?> FindAppointmentByIdAsync(
        Guid appointmentId
    ) {
        return await _dbContext.Appointments
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
    }

    public async Task<Doctor?> FindDoctorByIdAsync(Guid doctorId) {
        return await _dbContext.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
    }

    public async Task<Schedule?> FindScheduleByIdAsync(Guid scheduleId) {
        return await _dbContext.Schedules
            .FirstOrDefaultAsync(s => s.DoctorId == scheduleId);
    }

    public async Task<User?> FindUserByIdAsync(string userId) {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task SaveAsync() {
        await _dbContext.SaveChangesAsync();
    }

    private static IQueryable<Appointment> AddGetSearch(
        IQueryable<Appointment> query, DateTime? searchStartTime,
        DateTime? searchEndTime
    ) {
        IQueryable<Appointment>? newQuery = searchStartTime.HasValue
            ? query.Where(a => a.ScheduleStartTime >= searchStartTime)
            : query;
        newQuery = searchEndTime.HasValue
            ? newQuery.Where(a => a.ScheduleEndTime <= searchEndTime)
            : newQuery;
        return newQuery;
    }

    private static IQueryable<Appointment> AddGetPagination(
        IQueryable<Appointment> query, int? pageSize, int? pageIndex
    ) {
        IQueryable<Appointment>? newQuery =
            pageSize.HasValue && pageIndex.HasValue
                ? query.Skip((pageIndex.Value - 1) * pageSize.Value)
                : query;
        newQuery = pageSize.HasValue
            ? newQuery.Take(pageSize.Value)
            : newQuery;
        return newQuery;
    }
}