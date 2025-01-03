using HealthcareSystem.Core.Doctors;

namespace HealthcareSystem.Infrastructure.Doctors;

public interface IDoctorRepository
{
    public Task SaveAsync();
    public Task<Doctor> GetByIdAsync(Guid id);

    public Task<ICollection<Doctor>> GetAsync(
        int? pageIndex, int? pageSize,
        string? sortField, string? sortOrder,
        string? searchField, string? searchValue
    );

    public Task CreateAsync(Doctor doctor);

    public Task RemoveAsync(Guid id);

    public Task<bool> IsDoctorExistsAsync(Doctor doctor);
}