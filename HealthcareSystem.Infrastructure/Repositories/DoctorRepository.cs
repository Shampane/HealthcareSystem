using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Models;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class DoctorRepository(AppDbContext dbContext)
    : IDoctorRepository
{
    public async Task<IList<Doctor>> GetAsync()
    {
        return await dbContext.Doctors.ToListAsync();
    }
}