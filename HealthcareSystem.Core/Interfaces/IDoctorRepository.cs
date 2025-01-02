using HealthcareSystem.Core.Models;

namespace HealthcareSystem.Core.Interfaces;

public interface IDoctorRepository
{
    Task<IList<Doctor>> GetAsync();
    Task CreateAsync(Doctor doctor);
    Task<bool> IsDoctorExistsAsync(Doctor doctor);
}