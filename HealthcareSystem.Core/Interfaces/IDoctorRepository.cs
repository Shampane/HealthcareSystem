using HealthcareSystem.Core.Models;

namespace HealthcareSystem.Core.Interfaces;

public interface IDoctorRepository
{
    Task<IList<Doctor>> GetAsync();
}