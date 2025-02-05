using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IDoctorRepository {
    public Task<ICollection<Doctor>?> GetDoctors(
        int? pageIndex, int? pageSize, string? sortField,
        string? sortOrder, string? searchField, string? searchValue
    );

    public Task<Doctor?> GetDoctorById(Guid id);
    public Task CreateDoctor(Doctor doctor);

    public Task UpdateDoctor(
        Doctor doctor, string? name, string? description,
        string? imageUrl, int? experienceAge, decimal? feeInDollars,
        string? specialization, string? phoneNumber
    );

    public Task RemoveDoctor(Doctor doctor);
}