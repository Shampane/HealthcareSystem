using HealthcareSystem.Core.Entities;

namespace HealthcareSystem.Core.Interfaces;

public interface IDoctorRepository {
    public Task<ICollection<Doctor>?> GetDoctors(
        int? pageIndex, int? pageSize, string? sortField,
        string? sortOrder, string? searchName, string? searchSpecialization,
        CancellationToken cancellationToken
    );

    public Task<Doctor?> GetDoctorById(Guid id, CancellationToken cancellationToken);
    public Task CreateDoctor(Doctor doctor, CancellationToken cancellationToken);

    public Task UpdateDoctor(
        Doctor doctor, string? name, string? description,
        string? imageUrl, int? experienceAge, decimal? feeInDollars,
        string? specialization, string? phoneNumber,
        CancellationToken cancellationToken
    );

    public Task RemoveDoctor(Doctor doctor, CancellationToken cancellationToken);
}