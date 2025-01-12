using HealthcareSystem.Core.Appointments;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Appointments;

public class AppointmentRepository(
    AppDbContext dbContext,
    UserManager<User> userManager
) : IAppointmentRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<User> FindUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user!;
    }

    public async Task<Doctor> FindDoctorByIdAsync(Guid doctorId)
    {
        var doctor = await _dbContext.Doctors.FindAsync(doctorId);
        return doctor!;
    }

    public async Task<Schedule> FindScheduleByIdAsync(Guid scheduleId)
    {
        var schedule = await _dbContext.Schedules.FindAsync(scheduleId);
        return schedule!;
    }

    public async Task CreateAppointmentAsync(Appointment appointment)
    {
        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Appointment>> GetAppointmentsAsync()
    {
        return await _dbContext.Appointments.AsNoTracking().ToListAsync();
    }
}