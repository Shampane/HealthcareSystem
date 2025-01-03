using System.Text;
using HealthcareSystem.Core.Doctors;
using HealthcareSystem.Infrastructure.Doctors;

namespace HealthcareSystem.Application.Doctors;

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
                    "Error: The Doctor wasn't found", null
                );
            return new DoctorGetByIdResponse(
                200, true,
                "The Doctor was found", doctor
            );
        }
        catch (Exception ex)
        {
            return new DoctorGetByIdResponse(
                404, false,
                $"Error: {ex.Message}", null
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
                    "Error: Doctors weren't found", null
                );
            return new DoctorGetResponse(
                200, true,
                "Doctors were found", doctors
            );
        }
        catch (Exception ex)
        {
            return new DoctorGetResponse(
                404, false,
                $"Error: {ex.Message}", null
            );
        }
    }

    private string BuildCreateAsyncError(DoctorCreateRequest request)
    {
        var errorMessage = new StringBuilder("Errors: ");
        if (request.Name == string.Empty)
            errorMessage.Append("Name can't be empty, ");
        if (request.Description == string.Empty)
            errorMessage.Append("Description can't be empty, ");
        if (request.Specialization == string.Empty)
            errorMessage.Append("Specialization can't be empty, ");
        if (request.PhoneNumber == string.Empty)
            errorMessage.Append("PhoneNumber can't be empty, ");
        if (request.ExperienceAge < 0 || request.ExperienceAge > 100)
            errorMessage.Append(
                "Experience age can't be less than 0 or more than 100, "
            );
        if (request.FeeInDollars < 0)
            errorMessage.Append(
                "Fee in dollars can't be less than 0 dollars, "
            );
        var commaIndex = errorMessage
            .ToString()
            .LastIndexOf(",", StringComparison.Ordinal);
        errorMessage[commaIndex] = '.';
        return errorMessage.ToString() == "Errors: "
            ? string.Empty
            : errorMessage.ToString().TrimEnd();
    }

    public async Task<DoctorCreateResponse> CreateAsync(
        DoctorCreateRequest request
    )
    {
        try
        {
            var errorMessage = BuildCreateAsyncError(request);
            if (errorMessage != string.Empty)
                return new DoctorCreateResponse(
                    404, false, errorMessage
                );
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
                    409, false, "Error: The Doctor already exists"
                );

            await repository.CreateAsync(doctor);
            return new DoctorCreateResponse(
                201, true, "The Doctor was created"
            );
        }
        catch (Exception ex)
        {
            return new DoctorCreateResponse(
                404, false,
                $"Error: {ex.Message}"
            );
        }
    }

    public async Task<DoctorRemoveResponse> RemoveAsync(
        Guid id
    )
    {
        var doctor = await repository.FindDoctorByIdAsync(id);
        if (doctor == null)
            return new DoctorRemoveResponse(
                404, false, "Error: The Doctor doesn't exist"
            );

        await repository.RemoveAsync(id);
        return new DoctorRemoveResponse(
            201, true, "The Doctor was removed"
        );
    }
}