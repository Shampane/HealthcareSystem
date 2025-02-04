using System.Linq.Expressions;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Infrastructure.DataAccess;
using HealthcareSystem.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.Infrastructure.Repositories;

public class DoctorRepository : IDoctorRepository {
    private readonly AppDbContext _dbContext;
    private readonly GetHelper _getHelper;

    public DoctorRepository(AppDbContext dbContext) {
        _dbContext = dbContext;
        _getHelper = new GetHelper();
    }

    public async Task<ICollection<Doctor>?> GetDoctors(
        int? pageIndex, int? pageSize, string? sortField,
        string? sortOrder, string? searchField, string? searchValue
    ) {
        IQueryable<Doctor>? query = _dbContext.Doctors.AsNoTracking();

        query = AddGetSearch(query, searchField, searchValue);
        query = AddGetSort(query, sortField, sortOrder);
        query = _getHelper.AddPagination(query, pageSize, pageIndex);

        return await query.ToListAsync();
    }

    public async Task<Doctor?> GetDoctorById(Guid id) {
        return await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task CreateDoctor(Doctor doctor) {
        await _dbContext.Doctors.AddAsync(doctor);
        await SaveChanges();
    }

    public async Task UpdateDoctor(Doctor doctor) {
        _dbContext.Entry(doctor).State = EntityState.Modified;
        await SaveChanges();
    }

    public async Task RemoveDoctor(Doctor doctor) {
        _dbContext.Doctors.Remove(doctor);
        await SaveChanges();
    }

    public async Task SaveChanges() {
        await _dbContext.SaveChangesAsync();
    }

    private static IQueryable<Doctor> AddGetSearch(
        IQueryable<Doctor> query, string? searchField, string? searchValue
    ) {
        if (searchValue is null) {
            return query;
        }
        Expression<Func<Doctor, bool>> key = searchField?.ToLower() switch {
            "name" => d => d.Name.StartsWith(
                searchValue, StringComparison.CurrentCultureIgnoreCase
            ),
            "specialization" => d => d.Specialization.StartsWith(
                searchValue, StringComparison.CurrentCultureIgnoreCase
            ),
            "phonenumber" => d => d.PhoneNumber.StartsWith(
                searchValue, StringComparison.CurrentCultureIgnoreCase
            ),
            _ => d => false
        };
        return query.Where(key);
    }

    private static IQueryable<Doctor> AddGetSort(
        IQueryable<Doctor> query, string? sortField, string? sortOrder
    ) {
        Expression<Func<Doctor, object>> key = sortField?.ToLower() switch {
            "name" => d => d.Name,
            "experienceage" => d => d.ExperienceAge,
            "feeindollars" => d => d.FeeInDollars,
            "specialization" => d => d.Specialization,
            _ => d => d.Id
        };

        bool isDescending = sortOrder is not null
            && sortOrder.Equals("desc", StringComparison.CurrentCultureIgnoreCase);
        return isDescending ? query.OrderByDescending(key) : query.OrderBy(key);
    }
}