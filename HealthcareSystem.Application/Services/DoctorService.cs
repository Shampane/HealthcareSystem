using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Interfaces;
using HealthcareSystem.Core.Models;

namespace HealthcareSystem.Application.Services;

public class DoctorService(IDoctorRepository repository)
{
    public async Task<DoctorGetByIdResponse> GetByIdAsync(Guid id)
    {
        try
        {
            var doctor = await repository.GetByIdAsync(id);
            if (doctor == null)
                return new DoctorGetByIdResponse(
                    404, false,
                    "The Doctor wasn't found", null
                );
            return new DoctorGetByIdResponse(
                200, true,
                "The Doctor was found", doctor
            );
        }
        catch
        {
            return new DoctorGetByIdResponse(
                404, false,
                "Error when searching for the Doctor", null
            );
        }
    }

    public async Task<DoctorGetResponse> GetAsync()
    {
        try
        {
            var doctors = await repository.GetAsync();
            if (doctors == null)
                return new DoctorGetResponse(
                    404, false,
                    "Doctors weren't found", null
                );
            return new DoctorGetResponse(
                200, true,
                "Doctors were found", doctors
            );
        }
        catch
        {
            return new DoctorGetResponse(
                404, false,
                "Error when searching for Doctors", null
            );
        }
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
                409, false, "The Doctor already exists"
            );

        await repository.CreateAsync(doctor);
        return new DoctorCreateResponse(
            201, true, "The Doctor was created"
        );
    }

    public async Task<DoctorRemoveResponse> RemoveAsync(
        Guid id
    )
    {
        var doctor = await repository.FindDoctorByIdAsync(id);
        if (doctor == null)
            return new DoctorRemoveResponse(
                404, false, "The Doctor doesn't exist"
            );

        await repository.RemoveAsync(id);
        return new DoctorRemoveResponse(
            201, true, "The Doctor was removed"
        );
    }
}