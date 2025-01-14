using HealthcareSystem.Core.Appointments;
using HealthcareSystem.Core.Auth;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Core.Schedules;

namespace HealthcareSystem.Infrastructure.Appointments;

public interface IAppointmentRepository
{
    public Task<ICollection<AppointmentDto>?> GetAppointmentsByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    );

    public Task<ICollection<AppointmentDto>?> GetAppointmentsByUserAsync(
        string userId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    );

    public Task<AppointmentDto?> GetAppointmentByIdAsync(Guid id);
    public Task CreateAppointmentAsync(Appointment appointment);
    public Task RemoveAppointmentAsync(Appointment appointment);
    public Task<Appointment?> FindAppointmentByIdAsync(Guid appointmentId);
    public Task<Doctor?> FindDoctorByIdAsync(Guid doctorId);
    public Task<Schedule?> FindScheduleByIdAsync(Guid scheduleId);
    public Task<User?> FindUserByIdAsync(string userId);
    public Task SaveAsync();
}