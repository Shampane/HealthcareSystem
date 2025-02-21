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
        string? sortOrder, string? searchName, string? searchSpecialization,
        CancellationToken cancellationToken
    ) {
        IQueryable<Doctor>? query = _dbContext.Doctors.AsNoTracking();

        query = AddGetSearch(query, searchName, searchSpecialization);
        query = AddGetSort(query, sortField, sortOrder);
        query = _getHelper.AddPagination(query, pageSize, pageIndex);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Doctor?> GetDoctorById(
        Guid id, CancellationToken cancellationToken
    ) {
        return await _dbContext.Doctors
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }

    public async Task CreateDoctor(
        Doctor doctor, CancellationToken cancellationToken
    ) {
        await _dbContext.Doctors.AddAsync(doctor, cancellationToken);
        await SaveChanges(cancellationToken);
    }

    public async Task UpdateDoctor(
        Doctor doctor, string? name, string? description,
        string? imageUrl, int? experienceAge, decimal? feeInDollars,
        string? specialization, string? phoneNumber,
        CancellationToken cancellationToken
    ) {
        doctor.Name = name ?? doctor.Name;
        doctor.Description = description ?? doctor.Description;
        doctor.ImageUrl = imageUrl ?? doctor.ImageUrl;
        doctor.ExperienceAge = experienceAge ?? doctor.ExperienceAge;
        doctor.FeeInDollars = feeInDollars ?? doctor.FeeInDollars;
        doctor.Specialization = specialization ?? doctor.Specialization;
        doctor.PhoneNumber = phoneNumber ?? doctor.PhoneNumber;
        await SaveChanges(cancellationToken);
    }

    public async Task RemoveDoctor(
        Doctor doctor, CancellationToken cancellationToken
    ) {
        _dbContext.Doctors.Remove(doctor);
        await SaveChanges(cancellationToken);
    }

    private async Task SaveChanges(CancellationToken cancellationToken) {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static IQueryable<Doctor> AddGetSearch(
        IQueryable<Doctor> query, string? searchName, string? searchSpecialization
    ) {
        if (searchName is null && searchSpecialization is null) {
            return query;
        }

        query = searchName is null
            ? query
            : query.Where(d => d.Name.ToLower().StartsWith(searchName.ToLower()));
        query = searchSpecialization is null
            ? query
            : query.Where(d =>
                d.Specialization.ToLower().StartsWith(searchSpecialization.ToLower())
            );
        return query;
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
                            && sortOrder.Equals("desc",
                                StringComparison.CurrentCultureIgnoreCase);
        return isDescending ? query.OrderByDescending(key) : query.OrderBy(key);
    }
}