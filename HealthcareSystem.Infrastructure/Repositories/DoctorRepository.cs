using System.Linq.Expressions;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository {
    private readonly AppDbContext _dbContext;

    public DoctorRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Doctor>?> GetDoctorsAsync(
        int? pageIndex, int? pageSize, string? sortField,
        string? sortOrder, string? searchField, string? searchValue
    ) {
        IQueryable<Doctor>? query = _dbContext.Doctors.AsNoTracking();

        query = AddGetSearch(query, searchField, searchValue);
        query = AddGetSort(query, sortField, sortOrder);
        query = AddGetPagination(query, pageSize, pageIndex);

        return await query.ToListAsync();
    }

    public async Task CreateDoctorAsync(Doctor doctor) {
        await _dbContext.Doctors.AddAsync(doctor);
        await SaveAsync();
    }


    public async Task RemoveDoctorAsync(Doctor doctor) {
        _dbContext.Doctors.Remove(doctor);
        await SaveAsync();
    }

    public async Task SaveAsync() {
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Doctor?> FindDoctorByIdAsync(Guid doctorId) {
        return await _dbContext.Doctors
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
    }

    public async Task<Doctor?> GetDoctorByIdAsync(Guid doctorId) {
        return await _dbContext.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);
    }

    private static IQueryable<Doctor> AddGetSearch(
        IQueryable<Doctor> query, string? searchField, string? searchValue
    ) {
        Expression<Func<Doctor, bool>> key =
            searchField?.ToLower() switch {
                "name" => d =>
                    d.Name.ToLower().StartsWith(searchValue!),
                "specialization" => d =>
                    d.Specialization.ToLower().StartsWith(searchValue!),
                "phonenumber" => d =>
                    d.PhoneNumber.ToLower().StartsWith(searchValue!),
                _ => d => false
            };
        IQueryable<Doctor>? newQuery = searchValue != null
            ? query.Where(key)
            : query;

        return newQuery;
    }

    private static IQueryable<Doctor> AddGetSort(
        IQueryable<Doctor> query, string? sortField, string? sortOrder
    ) {
        Expression<Func<Doctor, object>> key =
            sortField?.ToLower() switch {
                "name" => d => d.Name,
                "experienceage" => d => d.ExperienceAge,
                "feeindollars" => d => d.FeeInDollars,
                "specialization" => d => d.Specialization,
                _ => d => d.DoctorId
            };

        bool isDescending = sortOrder?.ToLower().Equals("desc") ?? false;
        IOrderedQueryable<Doctor>? newQuery = isDescending
            ? query.OrderByDescending(key)
            : query.OrderBy(key);
        return newQuery;
    }

    private static IQueryable<Doctor> AddGetPagination(
        IQueryable<Doctor> query, int? pageSize, int? pageIndex
    ) {
        IQueryable<Doctor>? newQuery = pageSize.HasValue && pageIndex.HasValue
            ? query.Skip((pageIndex.Value - 1) * pageSize.Value)
            : query;
        newQuery = pageSize.HasValue
            ? newQuery.Take(pageSize.Value)
            : newQuery;
        return newQuery;
    }
}