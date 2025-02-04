using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IAppointmentRepository {
    public Task<ICollection<Appointment>?> GetAppointmentsByDoctor(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime
    );

    public Task<ICollection<Appointment>?> GetAppointmentsByUser(
        string userId, int? pageIndex, int? pageSize,
        DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime
    );

    public Task<Appointment?> GetAppointmentById(Guid id);
    public Task CreateAppointment(Appointment appointment);
    public Task RemoveAppointment(Appointment appointment);
    public Task SaveChanges();
}