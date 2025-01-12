using HealthcareSystem.Core.Appointments;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;
using HealthcareSystem.Infrastructure.DataAccess;

namespace HealthcareSystem.Infrastructure.Appointments;

public class AppointmentRepository(
    AppDbContext dbContext
) : IAppointmentRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<User> FindUserByIdAsync(string userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
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

    public async Task CreateAsync(Appointment appointment)
    {
        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();
    }
}