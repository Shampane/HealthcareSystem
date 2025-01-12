using HealthcareSystem.Core.Doctors;

namespace HealthcareSystem.Infrastructure.Doctors;

public interface IDoctorRepository
{
    public Task<Doctor> FindDoctorByIdAsync(Guid doctorId);

    public Task<ICollection<DoctorDto>> GetDoctorsAsync(
        int? pageIndex, int? pageSize,
        string? sortField, string? sortOrder,
        string? searchField, string? searchValue
    );

    public Task<DoctorDto> GetDoctorByIdAsync(Guid id);
    public Task CreateDoctorAsync(Doctor doctor);
    public Task RemoveDoctorAsync(Doctor doctor);
    public Task SaveAsync();
}