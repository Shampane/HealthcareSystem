using HealthcareSystem.Core.Models;

namespace HealthcareSystem.Core.Interfaces;

public interface IDoctorRepository
{
    Task<Doctor> GetByIdAsync(Guid id);
    Task<ICollection<Doctor>> GetAsync();
    Task CreateAsync(Doctor doctor);
    Task RemoveAsync(Guid id);
    Task<bool> IsDoctorExistsAsync(Doctor doctor);
    Task<Doctor> FindDoctorByIdAsync(Guid id);
}