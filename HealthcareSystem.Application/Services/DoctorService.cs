namespace HealthcareSystem.Application.Services;

/*
public class DoctorService {
    private const string ErrorStatus = nameof(ResponseStatus.Error);

    private const string SuccessStatus = nameof(ResponseStatus.Success);

    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository) {
        _repository = repository;
    }

    public async Task<GetEntityResponse<DoctorDto>> GetByIdAsync(Guid id) {
        try {
            DoctorDto? doctor = await _repository.GetDoctorByIdAsync(id);
            if (doctor == null) {
                return new GetEntityResponse<DoctorDto>(
                    ErrorStatus, "The Doctor wasn't found", null
                );
            }

            DoctorDto? dto = doctor.ToDto();
            return new GetEntityResponse<DoctorDto>(
                SuccessStatus,
                "The Doctor was found", dto
            );
        }
        catch (Exception ex) {
            return new GetEntityResponse<DoctorDto>(
                ErrorStatus, ex.Message, null
            );
        }
    }

    public async Task<GetResponse<DoctorDto>> GetAsync(
        int? pageIndex, int? pageSize, string? sortField,
        string? sortOrder, string? searchField, string? searchValue
    ) {
        try {
            ICollection<DoctorDto>? doctors = await _repository.GetDoctorsAsync(
                pageIndex, pageSize, sortField,
                sortOrder, searchField, searchValue
            );
            if (doctors == null) {
                return new GetResponse<DoctorDto>(
                    ErrorStatus,
                    "Doctors weren't found", null
                );
            }

            List<DoctorDto>? listDto = doctors.Select(d => d.ToDto()).ToList();
            return new GetResponse<DoctorDto>(
                SuccessStatus,
                "Doctors were found", listDto
            );
        }
        catch (Exception ex) {
            return new GetResponse<DoctorDto>(
                ErrorStatus, ex.Message, null
            );
        }
    }

    public async Task<CreateResponse<DoctorDto>> CreateAsync(
        DoctorRequest request
    ) {
        try {
            string? errorMessage = BuildDoctorErrors(request);
            if (errorMessage != string.Empty) {
                return new CreateResponse<DoctorDto>(
                    ErrorStatus, errorMessage, null
                );
            }
            var doctor = new DoctorDto(
                request.Name, request.Description, request.ImageUrl,
                request.ExperienceAge, request.FeeInDollars,
                request.Specialization, request.PhoneNumber
            );
            await _repository.CreateDoctorAsync(doctor);

            var doctorDto = new DoctorDto(
                doctor.DoctorId, doctor.Name, doctor.Description,
                doctor.ImageUrl, doctor.ExperienceAge, doctor.FeeInDollars,
                doctor.Specialization, doctor.PhoneNumber
            );
            return new CreateResponse<DoctorDto>(
                SuccessStatus, "The doctor was created", doctorDto
            );
        }
        catch (Exception ex) {
            return new CreateResponse<DoctorDto>(
                ErrorStatus, ex.Message, null
            );
        }
    }

    public async Task<UpdateResponse<DoctorDto>> UpdateAsync(
        Guid id, DoctorRequest request
    ) {
        try {
            string? errorMessage = BuildDoctorErrors(request);
            if (errorMessage != string.Empty) {
                return new UpdateResponse<DoctorDto>(
                    ErrorStatus, errorMessage, null
                );
            }
            DoctorDto? doctor = await _repository.FindDoctorByIdAsync(id);
            if (doctor == null) {
                return new UpdateResponse<DoctorDto>(
                    ErrorStatus, "The Doctor doesn't exist", null
                );
            }

            doctor.Name = request.Name;
            doctor.Description = request.Description;
            doctor.ExperienceAge = request.ExperienceAge;
            doctor.FeeInDollars = request.FeeInDollars;
            doctor.Specialization = request.Specialization;
            doctor.PhoneNumber = request.PhoneNumber;
            doctor.ImageUrl = request.ImageUrl;
            await _repository.SaveAsync();

            var doctorDto = new DoctorDto(
                doctor.DoctorId, doctor.Name, doctor.Description,
                doctor.ImageUrl, doctor.ExperienceAge, doctor.FeeInDollars,
                doctor.Specialization, doctor.PhoneNumber
            );
            return new UpdateResponse<DoctorDto>(
                SuccessStatus,
                "The Doctor was updated", doctorDto
            );
        }
        catch (Exception ex) {
            return new UpdateResponse<DoctorDto>(
                ErrorStatus, ex.Message, null
            );
        }
    }

    public async Task<RemoveResponse<DoctorDto>> RemoveAsync(Guid id) {
        try {
            DoctorDto? doctor = await _repository.FindDoctorByIdAsync(id);
            if (doctor == null) {
                return new RemoveResponse<DoctorDto>(
                    ErrorStatus,
                    "The Doctor doesn't exist", null
                );
            }

            await _repository.RemoveDoctorAsync(doctor);

            var doctorDto = new DoctorDto(
                doctor.DoctorId, doctor.Name, doctor.Description,
                doctor.ImageUrl, doctor.ExperienceAge, doctor.FeeInDollars,
                doctor.Specialization, doctor.PhoneNumber
            );
            return new RemoveResponse<DoctorDto>(
                SuccessStatus,
                "The Doctor was removed", doctorDto
            );
        }
        catch (Exception ex) {
            return new RemoveResponse<DoctorDto>(
                ErrorStatus, ex.Message, null
            );
        }
    }

    private static string BuildDoctorErrors(DoctorRequest request) {
        var errorMessage = new StringBuilder();
        if (request.Name == string.Empty) {
            errorMessage.Append("Name can't be empty, ");
        }
        if (request.Description == string.Empty) {
            errorMessage.Append("Description can't be empty, ");
        }
        if (request.Specialization == string.Empty) {
            errorMessage.Append("Specialization can't be empty, ");
        }
        if (request.PhoneNumber == string.Empty) {
            errorMessage.Append("PhoneNumber can't be empty, ");
        }
        if (request.ExperienceAge < 0 || request.ExperienceAge > 100) {
            errorMessage.Append(
                "Experience age can't be less than 0 or more than 100, "
            );
        }
        if (request.FeeInDollars < 0) {
            errorMessage.Append(
                "Fee in dollars can't be less than 0 dollars, "
            );
        }
        int commaIndex = errorMessage
            .ToString()
            .LastIndexOf(",", StringComparison.Ordinal);
        if (commaIndex == -1) {
            return string.Empty;
        }
        errorMessage[commaIndex] = '.';
        return errorMessage.ToString().TrimEnd();
    }
}
*/