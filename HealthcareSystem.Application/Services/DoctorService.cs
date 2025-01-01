using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Models;

namespace HealthcareSystem.Application.Services;

public class DoctorService(IDoctorRepository repository)
{
    public async Task<IList<Doctor>> GetDoctorsAsync()
    {
        return await repository.GetAsync();
    }
}