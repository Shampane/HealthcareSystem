using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Models;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class DoctorRepository(AppDbContext dbContext) : IDoctorRepository
{
    public async Task<IList<Doctor>> GetAsync()
    {
        return await dbContext.Doctors.ToListAsync();
    }

    public async Task CreateAsync(Doctor doctor)
    {
        await dbContext.Doctors.AddAsync(doctor);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsDoctorExistsAsync(Doctor doctor)
    {
        var findDoctor =
            await dbContext.Doctors.FirstOrDefaultAsync(d =>
                d.Name == doctor.Name &&
                d.PhoneNumber == doctor.PhoneNumber
            );
        return findDoctor != null;
    }
}