using System.Text;
using HealthcareSystem.Core.Schedules;
using HealthcareSystem.Infrastructure.Schedules;

namespace HealthcareSystem.Application.Schedules;

public class ScheduleService(IScheduleRepository repository)
{
    private async Task<string> BuildScheduleErrors(ScheduleRequest request)
    {
        var errorMessage = new StringBuilder("Errors: ");
        if (request.StartTime <= DateTime.UtcNow)
            errorMessage.Append(
                "Start time cannot be earlier than the current time, "
            );
        if (request.DurationInMinutes <= 0)
            errorMessage.Append(
                "Duration in minutes must be greater than 0, "
            );
        if (!await repository.IsSchedulesTimeAvailable(
                request.StartTime, request.DurationInMinutes))
            errorMessage.Append(
                "The schedule time intersect the already exists, "
            );

        var commaIndex = errorMessage
            .ToString()
            .LastIndexOf(",", StringComparison.Ordinal);
        if (commaIndex == -1)
            return string.Empty;
        errorMessage[commaIndex] = '.';
        return errorMessage.ToString() == "Errors: "
            ? string.Empty
            : errorMessage.ToString().TrimEnd();
    }

    public async Task<ScheduleCreateResponse> CreateAsync(
        ScheduleRequest request
    )
    {
        try
        {
            var errorMessage = await BuildScheduleErrors(request);
            if (errorMessage != string.Empty)
                return new ScheduleCreateResponse(
                    404, false, errorMessage
                );
            var doctor =
                await repository.GetDoctorByIdAsync(request.DoctorId);
            if (doctor == null)
                return new ScheduleCreateResponse(
                    409, false,
                    "Error: The Doctor doesn't exist"
                );
            var schedule = new Schedule
            {
                DoctorId = doctor.DoctorId,
                Doctor = doctor,
                StartTime = request.StartTime,
                DurationInMinutes = request.DurationInMinutes
            };

            await repository.CreateAsync(schedule);
            return new ScheduleCreateResponse(
                201, true,
                "The Schedule was created"
            );
        }
        catch (Exception ex)
        {
            return new ScheduleCreateResponse(
                404, false,
                $"Error: {ex.Message}"
            );
        }
    }

    public async Task<ScheduleGetResponse> GetSchedulesAsync()
    {
        try
        {
            Dictionary<Guid, ICollection<Schedule>> responseSchedules = [];
            var schedules = await repository.GetSchedulesAsync();
            foreach (var schedule in schedules)
                if (responseSchedules.ContainsKey(schedule.DoctorId))
                    responseSchedules[schedule.DoctorId].Add(schedule);
                else
                    responseSchedules.Add(
                        schedule.DoctorId, new List<Schedule> { schedule }
                    );

            return new ScheduleGetResponse(
                201, true,
                "Schedules were found", responseSchedules
            );
        }
        catch (Exception ex)
        {
            return new ScheduleGetResponse(
                404, false,
                $"Error: {ex.Message}", null
            );
        }
    }

    public async Task<ScheduleGetResponse> GetSchedulesByDoctorIdAsync(
        Guid doctorId
    )
    {
        try
        {
            Dictionary<Guid, ICollection<Schedule>> responseSchedules = [];
            var schedules =
                await repository.GetSchedulesByDoctorIdAsync(doctorId);
            responseSchedules.Add(doctorId, schedules);

            return new ScheduleGetResponse(
                201, true,
                "Schedules were found", responseSchedules
            );
        }
        catch (Exception ex)
        {
            return new ScheduleGetResponse(
                404, false,
                $"Error: {ex.Message}", null
            );
        }
    }
}