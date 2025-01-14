using System.Linq.Expressions;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Doctors;

public class DoctorRepository : IDoctorRepository
{
    private readonly AppDbContext _dbContext;

    public DoctorRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(Guid doctorId)
    {
        return await _dbContext.Doctors
            .AsNoTracking()
            .Select(d => new DoctorDto(
                d.DoctorId, d.Name, d.Description,
                d.ImageUrl, d.ExperienceAge, d.FeeInDollars,
                d.Specialization, d.PhoneNumber
            ))
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
    }

    public async Task<ICollection<DoctorDto>?> GetDoctorsAsync(
        int? pageIndex, int? pageSize, string? sortField,
        string? sortOrder, string? searchField, string? searchValue
    )
    {
        var query = _dbContext.Doctors.AsNoTracking();

        query = AddGetSearch(query, searchField, searchValue);
        query = AddGetSort(query, sortField, sortOrder);
        query = AddGetPagination(query, pageSize, pageIndex);

        return await query.Select(d => new DoctorDto(
            d.DoctorId, d.Name, d.Description,
            d.ImageUrl, d.ExperienceAge, d.FeeInDollars,
            d.Specialization, d.PhoneNumber
        )).ToListAsync();
    }

    public async Task CreateDoctorAsync(Doctor doctor)
    {
        await _dbContext.Doctors.AddAsync(doctor);
        await SaveAsync();
    }


    public async Task RemoveDoctorAsync(Doctor doctor)
    {
        _dbContext.Doctors.Remove(doctor);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Doctor?> FindDoctorByIdAsync(Guid doctorId)
    {
        return await _dbContext.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
    }

    private static IQueryable<Doctor> AddGetSearch(
        IQueryable<Doctor> query, string? searchField, string? searchValue
    )
    {
        Expression<Func<Doctor, bool>> key =
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
        var newQuery = searchValue != null
            ? query.Where(key)
            : query;

        return newQuery;
    }

    private static IQueryable<Doctor> AddGetSort(
        IQueryable<Doctor> query, string? sortField, string? sortOrder
    )
    {
        Expression<Func<Doctor, object>> key =
            sortField?.ToLower() switch
            {
                "name" => d => d.Name,
                "experienceage" => d => d.ExperienceAge,
                "feeindollars" => d => d.FeeInDollars,
                "specialization" => d => d.Specialization,
                _ => d => d.DoctorId
            };

        var isDescending = sortOrder?.ToLower().Equals("desc") ?? false;
        var newQuery = isDescending
            ? query.OrderByDescending(key)
            : query.OrderBy(key);
        return newQuery;
    }

    private static IQueryable<Doctor> AddGetPagination(
        IQueryable<Doctor> query, int? pageSize, int? pageIndex
    )
    {
        var newQuery = pageSize.HasValue && pageIndex.HasValue
            ? query.Skip((pageIndex.Value - 1) * pageSize.Value)
            : query;
        newQuery = pageSize.HasValue
            ? newQuery.Take(pageSize.Value)
            : newQuery;
        return newQuery;
    }
}