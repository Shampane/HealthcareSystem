using HealthcareSystem.Core.Appointments;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Infrastructure.Appointments;

public interface IAppointmentRepository
{
    public Task<User> FindUserByIdAsync(string userId);

    public Task<Doctor> FindDoctorByIdAsync(Guid doctorId);

    public Task<Schedule> FindScheduleByIdAsync(Guid scheduleId);
    public Task CreateAsync(Appointment appointment);
}