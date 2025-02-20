using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IAppointmentRepository
{
    public Task<ICollection<Appointment>?> GetAppointments(
        Guid? doctorId, string? userId, int? pageIndex, int? pageSize,
    DateTimeOffset? searchStartTime, DateTimeOffset? searchEndTime,
    CancellationToken cancellationToken
    );

    public Task<Appointment?> GetAppointmentById(
    Guid id, CancellationToken cancellationToken
    );

    public Task CreateAppointment(
    Appointment appointment, CancellationToken cancellationToken
    );

    public Task RemoveAppointment(
    Appointment appointment, CancellationToken cancellationToken
    );
}