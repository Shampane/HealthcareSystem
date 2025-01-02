using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Models;

namespace HealthcareSystem.Application.Services;

public class DoctorService(IDoctorRepository repository)
{
    public async Task<IList<Doctor>> GetAsync()
    {
        return await repository.GetAsync();
    }

    public async Task<DoctorCreateResponse> CreateAsync(
        DoctorCreateRequest request
    )
    {
        var doctor = new Doctor
        {
            Name = request.Name,
            Description = request.Description,
            ExperienceAge = request.ExperienceAge,
            FeeInDollars = request.FeeInDollars,
            Specialization = request.Specialization,
            PhoneNumber = request.PhoneNumber
        };
        var isDoctorExists =
            await repository.IsDoctorExistsAsync(doctor);
        if (isDoctorExists)
            return new DoctorCreateResponse(
                409, false, "Doctor already exists"
            );

        await repository.CreateAsync(doctor);
        return new DoctorCreateResponse(
            201, true, "Doctor was created"
        );
    }
}