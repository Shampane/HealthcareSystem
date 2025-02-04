namespace HealthcareSystem.Application.Services;

/*
public class ScheduleService {
    private const string ErrorStatus = nameof(ResponseStatus.Error);
    private const string SuccessStatus = nameof(ResponseStatus.Success);
    private readonly IScheduleRepository _repository;

    public ScheduleService(IScheduleRepository repository) {
        _repository = repository;
    }

    public async Task<GetResponse<ScheduleDto>> GetByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    ) {
        try {
            ICollection<ScheduleDto>? schedules = await _repository
                .GetSchedulesByDoctorAsync(
                    doctorId, pageIndex, pageSize,
                    searchStartTime, searchEndTime
                );
            if (schedules == null || schedules.Count == 0) {
                return new GetResponse<ScheduleDto>(
                    ErrorStatus,
                    "Schedules weren't found", null
                );
            }

            List<ScheduleDto>? listDto =
                schedules.Select(s => s.ToDto()).ToList();
            return new GetResponse<ScheduleDto>(
                SuccessStatus,
                "Schedules for this Doctor were found", listDto
            );
        }
        catch (Exception ex) {
            return new GetResponse<ScheduleDto>(
                ErrorStatus,
                $"{ex.Message}", null
            );
        }
    }

    public async Task<GetEntityResponse<ScheduleDto>> GetByIdAsync(Guid id) {
        try {
            ScheduleDto? schedule = await _repository.GetScheduleByIdAsync(id);
            if (schedule == null) {
                return new GetEntityResponse<ScheduleDto>(
                    ErrorStatus, "The Schedule wasn't found", null
                );
            }

            ScheduleDto? dto = schedule.ToDto();
            return new GetEntityResponse<ScheduleDto>(
                SuccessStatus, "The Schedule was found", dto
            );
        }
        catch (Exception ex) {
            return new GetEntityResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<ScheduleDto>> CreateAsync(
        ScheduleRequest request
    ) {
        try {
            string? errorMessage = await BuildScheduleErrors(request);
            if (errorMessage != string.Empty) {
                return new CreateResponse<ScheduleDto>(
                    ErrorStatus, errorMessage, null
                );
            }
            var schedule = new ScheduleDto(
                request.DoctorId, request.StartTime,
                request.DurationInMinutes
            );

            await _repository.CreateScheduleAsync(schedule);

            DateTime scheduleEndTime = schedule.StartTime
                .AddMinutes(schedule.DurationInMinutes);
            var scheduleDto = new ScheduleDto(
                schedule.ScheduleId, schedule.DoctorId, schedule.StartTime,
                scheduleEndTime, schedule.IsAvailable
            );

            return new CreateResponse<ScheduleDto>(
                SuccessStatus, "The Schedule was created", scheduleDto
            );
        }
        catch (Exception ex) {
            return new CreateResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<UpdateResponse<ScheduleDto>> ChangeAvailableAsync(
        Guid id
    ) {
        try {
            ScheduleDto? schedule = await _repository.FindScheduleByIdAsync(id);
            if (schedule == null) {
                return new UpdateResponse<ScheduleDto>(
                    ErrorStatus,
                    "The Schedule doesn't exist", null
                );
            }

            bool isAvailable = schedule.IsAvailable;
            schedule.IsAvailable = !isAvailable;
            await _repository.SaveAsync();

            DateTime scheduleEndTime = schedule.StartTime
                .AddMinutes(schedule.DurationInMinutes);
            var scheduleDto = new ScheduleDto(
                schedule.ScheduleId, schedule.DoctorId, schedule.StartTime,
                scheduleEndTime, schedule.IsAvailable
            );

            return new UpdateResponse<ScheduleDto>(
                SuccessStatus,
                "The Schedule's available was changed", scheduleDto
            );
        }
        catch (Exception ex) {
            return new UpdateResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<RemoveResponse<ScheduleDto>> RemoveAsync(Guid id) {
        try {
            ScheduleDto? schedule = await _repository.FindScheduleByIdAsync(id);
            if (schedule == null) {
                return new RemoveResponse<ScheduleDto>(
                    ErrorStatus,
                    "The Schedule doesn't exist", null
                );
            }

            await _repository.RemoveScheduleAsync(schedule);

            DateTime scheduleEndTime = schedule.StartTime
                .AddMinutes(schedule.DurationInMinutes);
            var scheduleDto = new ScheduleDto(
                schedule.ScheduleId, schedule.DoctorId, schedule.StartTime,
                scheduleEndTime, schedule.IsAvailable
            );

            return new RemoveResponse<ScheduleDto>(
                SuccessStatus,
                "The Schedule was removed", scheduleDto
            );
        }
        catch (Exception ex) {
            return new RemoveResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<MessageResponse> ClearOldAsync() {
        try {
            await _repository.ClearOldSchedulesAsync();
            return new MessageResponse(
                SuccessStatus, "The old Schedules were removed"
            );
        }
        catch (Exception ex) {
            return new MessageResponse(
                ErrorStatus, $"{ex.Message}"
            );
        }
    }

    private async Task<string> BuildScheduleErrors(ScheduleRequest request) {
        var errorMessage = new StringBuilder();
        if (request.StartTime <= DateTime.UtcNow) {
            errorMessage.Append(
                "Start time cannot be earlier than the current time, "
            );
        }
        if (request.DurationInMinutes <= 0) {
            errorMessage.Append(
                "Duration in minutes must be greater than 0, "
            );
        }
        if (!await _repository.IsSchedulesTimeAvailable(
            request.DoctorId, request.StartTime,
            request.DurationInMinutes)
        ) {
            errorMessage.Append(
                "The schedule time intersect the already exists, "
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