using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Doctors;

public class DoctorRepository(AppDbContext dbContext) : IDoctorRepository
{
    public async Task RemoveAsync(Guid id)
    {
        var doctor = await FindDoctorByIdAsync(id);
        dbContext.Doctors.Remove(doctor!);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Doctor> GetByIdAsync(Guid id)
    {
        var doctor = await dbContext.Doctors.FirstOrDefaultAsync(
            d => d.DoctorId == id
        );
        return doctor!;
    }

    public async Task<ICollection<Doctor>> GetAsync()
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

    public async Task<Doctor> FindDoctorByIdAsync(Guid id)
    {
        var findDoctor =
            await dbContext.Doctors.FirstOrDefaultAsync(
                d => d.DoctorId == id
            );
        return findDoctor!;
    }
}