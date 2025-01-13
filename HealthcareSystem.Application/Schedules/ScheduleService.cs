using System.Text;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Schedules;
using HealthcareSystem.Infrastructure.Schedules;

namespace HealthcareSystem.Application.Schedules;

public class ScheduleService(IScheduleRepository repository)
{
    private const string ErrorStatus = nameof(ResponseStatus.Error);

    private const string SuccessStatus = nameof(ResponseStatus.Success);

    private readonly IScheduleRepository _repository = repository;

    public async Task<GetResponse<ScheduleDto>> GetByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    )
    {
        try
        {
            var schedules = await _repository
                .GetSchedulesByDoctorAsync(
                    doctorId, pageIndex, pageSize,
                    searchStartTime, searchEndTime
                );
            if (schedules == null)
                return new GetResponse<ScheduleDto>(
                    ErrorStatus,
                    "Schedules weren't found", null
                );
            return new GetResponse<ScheduleDto>(
                SuccessStatus,
                "Schedules for this Doctor were found", schedules
            );
        }
        catch (Exception ex)
        {
            return new GetResponse<ScheduleDto>(
                ErrorStatus,
                $"{ex.Message}", null
            );
        }
    }

    public async Task<GetEntityResponse<ScheduleDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var schedule = await _repository.GetScheduleByIdAsync(id);
            if (schedule == null)
                return new GetEntityResponse<ScheduleDto>(
                    ErrorStatus, "The Schedule wasn't found", null
                );

            return new GetEntityResponse<ScheduleDto>(
                SuccessStatus, "The Schedule was found", schedule
            );
        }
        catch (Exception ex)
        {
            return new GetEntityResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<ScheduleDto>> CreateAsync(
        ScheduleRequest request
    )
    {
        try
        {
            var errorMessage = await BuildScheduleErrors(request);
            if (errorMessage != string.Empty)
                return new CreateResponse<ScheduleDto>(
                    ErrorStatus, errorMessage, null
                );
            var schedule = new Schedule
            {
                DoctorId = request.DoctorId,
                StartTime = request.StartTime,
                DurationInMinutes = request.DurationInMinutes
            };
            await _repository.CreateScheduleAsync(schedule);

            var scheduleDto = new ScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                DoctorId = schedule.DoctorId,
                StartTime = schedule.StartTime,
                EndTime = schedule.StartTime
                    .AddMinutes(schedule.DurationInMinutes),
                IsAvailable = schedule.IsAvailable
            };
            return new CreateResponse<ScheduleDto>(
                SuccessStatus, "The Schedule was created", scheduleDto
            );
        }
        catch (Exception ex)
        {
            return new CreateResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<UpdateResponse<ScheduleDto>> ChangeAvailableAsync(
        Guid id
    )
    {
        try
        {
            var schedule = await _repository.FindScheduleByIdAsync(id);
            if (schedule == null)
                return new UpdateResponse<ScheduleDto>(
                    ErrorStatus,
                    "The Schedule doesn't exist", null
                );

            var isAvailable = schedule.IsAvailable;
            schedule.IsAvailable = !isAvailable;
            await _repository.SaveAsync();

            var scheduleDto = new ScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                DoctorId = schedule.DoctorId,
                StartTime = schedule.StartTime,
                EndTime = schedule.StartTime
                    .AddMinutes(schedule.DurationInMinutes),
                IsAvailable = schedule.IsAvailable
            };
            return new UpdateResponse<ScheduleDto>(
                SuccessStatus,
                "The Schedule's available was changed", scheduleDto
            );
        }
        catch (Exception ex)
        {
            return new UpdateResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<RemoveResponse<ScheduleDto>> RemoveAsync(Guid id)
    {
        try
        {
            var schedule = await _repository.FindScheduleByIdAsync(id);
            if (schedule == null)
                return new RemoveResponse<ScheduleDto>(
                    ErrorStatus,
                    "The Schedule doesn't exist", null
                );

            await _repository.RemoveScheduleAsync(schedule);

            var scheduleDto = new ScheduleDto
            {
                ScheduleId = schedule.ScheduleId,
                DoctorId = schedule.DoctorId,
                StartTime = schedule.StartTime,
                EndTime = schedule.StartTime
                    .AddMinutes(schedule.DurationInMinutes),
                IsAvailable = schedule.IsAvailable
            };
            return new RemoveResponse<ScheduleDto>(
                SuccessStatus,
                "The Schedule was removed", scheduleDto
            );
        }
        catch (Exception ex)
        {
            return new RemoveResponse<ScheduleDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<MessageResponse> ClearOldAsync()
    {
        try
        {
            await _repository.ClearOldSchedulesAsync();
            return new MessageResponse(
                SuccessStatus, "The old Schedules were removed"
            );
        }
        catch (Exception ex)
        {
            return new MessageResponse(
                ErrorStatus, $"{ex.Message}"
            );
        }
    }

    private async Task<string> BuildScheduleErrors(ScheduleRequest request)
    {
        var errorMessage = new StringBuilder();
        if (request.StartTime <= DateTime.UtcNow)
            errorMessage.Append(
                "Start time cannot be earlier than the current time, "
            );
        if (request.DurationInMinutes <= 0)
            errorMessage.Append(
                "Duration in minutes must be greater than 0, "
            );
        if (!await _repository.IsSchedulesTimeAvailable(
                request.DoctorId, request.StartTime,
                request.DurationInMinutes)
           )
            errorMessage.Append(
                "The schedule time intersect the already exists, "
            );

        var commaIndex = errorMessage
            .ToString()
            .LastIndexOf(",", StringComparison.Ordinal);
        if (commaIndex == -1)
            return string.Empty;
        errorMessage[commaIndex] = '.';
        return errorMessage.ToString().TrimEnd();
    }
}