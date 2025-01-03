using System.Linq.Expressions;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Doctors;

public class DoctorRepository(AppDbContext dbContext) : IDoctorRepository
{
    public async Task SaveAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<Doctor> GetByIdAsync(Guid id)
    {
        var doctor = await dbContext.Doctors.FirstOrDefaultAsync(
            d => d.DoctorId == id
        );
        return doctor!;
    }

    public async Task CreateAsync(Doctor doctor)
    {
        await dbContext.Doctors.AddAsync(doctor);
        await SaveAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        var doctor = await GetByIdAsync(id);
        dbContext.Doctors.Remove(doctor!);
        await SaveAsync();
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

    public async Task<ICollection<Doctor>> GetAsync(
        int? pageIndex, int? pageSize,
        string? sortField, string? sortOrder,
        string? searchField, string? searchValue
    )
    {
        IQueryable<Doctor> query = dbContext.Doctors;

        Expression<Func<Doctor, bool>> searchKey =
            searchField?.ToLower() switch
            {
                "name" => d =>
                    d.Name.ToLower().StartsWith(searchValue!),
                "specialization" => d =>
                    d.Specialization.ToLower().StartsWith(searchValue!),
                "phonenumber" => d =>
                    d.PhoneNumber.ToLower().StartsWith(searchValue!),
                _ => d => false
            };

        query = searchValue != null
            ? query.Where(searchKey)
            : query;

        Expression<Func<Doctor, object>> sortKey =
            sortField?.ToLower() switch
            {
                "name" => d => d.Name,
                "experienceage" => d => d.ExperienceAge,
                "feeindollars" => d => d.FeeInDollars,
                "specialization" => d => d.Specialization,
                _ => d => d.DoctorId
            };

        var isDescending = sortOrder?.ToLower().Equals("desc") ?? false;

        query = isDescending
            ? query.OrderByDescending(sortKey)
            : query.OrderBy(sortKey);
        query = pageSize.HasValue && pageIndex.HasValue
            ? query.Skip((pageIndex.Value - 1) * pageSize.Value)
            : query;
        query = pageSize.HasValue ? query.Take(pageSize.Value) : query;

        return await query.ToListAsync();
    }
}