using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Appointments;
using HealthcareSystem.Infrastructure.Appointments;

namespace HealthcareSystem.Application.Appointments;

public class AppointmentService
{
    private const string ErrorStatus = nameof(ResponseStatus.Error);
    private const string SuccessStatus = nameof(ResponseStatus.Success);
    private readonly IAppointmentRepository _repository;

    public AppointmentService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetResponse<AppointmentDto>> GetByDoctorAsync(
        Guid doctorId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    )
    {
        try
        {
            var appointments = await _repository
                .GetAppointmentsByDoctorAsync(
                    doctorId, pageIndex, pageSize,
                    searchStartTime, searchEndTime
                );

            if (appointments == null || appointments.Count == 0)
                return new GetResponse<AppointmentDto>(
                    ErrorStatus,
                    "Appointments weren't found", null
                );

            var listDto = appointments.Select(a => a.ToDto()).ToList();
            return new GetResponse<AppointmentDto>(
                SuccessStatus,
                "Appointments for this Doctor were found", listDto
            );
        }
        catch (Exception ex)
        {
            return new GetResponse<AppointmentDto>(
                ErrorStatus,
                $"{ex.Message}", null
            );
        }
    }

    public async Task<GetResponse<AppointmentDto>> GetByUserAsync(
        string userId, int? pageIndex, int? pageSize,
        DateTime? searchStartTime, DateTime? searchEndTime
    )
    {
        try
        {
            var appointments = await _repository
                .GetAppointmentsByUserAsync(
                    userId, pageIndex, pageSize,
                    searchStartTime, searchEndTime
                );
            if (appointments == null || appointments.Count == 0)
                return new GetResponse<AppointmentDto>(
                    ErrorStatus,
                    "Appointments weren't found", null
                );

            var listDto = appointments.Select(a => a.ToDto()).ToList();
            return new GetResponse<AppointmentDto>(
                SuccessStatus,
                "Appointments for this User were found", listDto
            );
        }
        catch (Exception ex)
        {
            return new GetResponse<AppointmentDto>(
                ErrorStatus,
                $"{ex.Message}", null
            );
        }
    }

    public async Task<GetEntityResponse<AppointmentDto>> GetByIdAsync(
        Guid id)
    {
        try
        {
            var appointment = await _repository
                .GetAppointmentByIdAsync(id);
            if (appointment == null)
                return new GetEntityResponse<AppointmentDto>(
                    ErrorStatus, "The Appointment wasn't found", null
                );

            var dto = appointment.ToDto();
            return new GetEntityResponse<AppointmentDto>(
                SuccessStatus, "The Appointment was found", dto
            );
        }
        catch (Exception ex)
        {
            return new GetEntityResponse<AppointmentDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<CreateResponse<AppointmentDto>> CreateAsync(
        AppointmentRequest request
    )
    {
        try
        {
            var doctor = await _repository
                .FindDoctorByIdAsync(request.DoctorId);
            if (doctor == null)
                return new CreateResponse<AppointmentDto>(
                    ErrorStatus, "The Doctor wasn't found", null
                );
            var user = await _repository
                .FindUserByIdAsync(request.UserId);
            if (user == null)
                return new CreateResponse<AppointmentDto>(
                    ErrorStatus, "The User wasn't found", null
                );
            var schedule = await _repository
                .FindScheduleByIdAsync(request.ScheduleId);
            if (schedule == null)
                return new CreateResponse<AppointmentDto>(
                    ErrorStatus, "The Schedule wasn't found", null
                );

            var appointment = new Appointment(
                request.DoctorId, request.DoctorName, request.ScheduleId,
                request.ScheduleStartTime, request.ScheduleEndTime,
                request.UserId, request.UserName
            );

            await _repository.CreateAppointmentAsync(appointment);

            var appointmentDto = appointment.ToDto();

            return new CreateResponse<AppointmentDto>(
                SuccessStatus, "The Appointment was created",
                appointmentDto
            );
        }
        catch (Exception ex)
        {
            return new CreateResponse<AppointmentDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }

    public async Task<RemoveResponse<AppointmentDto>> RemoveAsync(Guid id)
    {
        try
        {
            var appointment = await _repository
                .FindAppointmentByIdAsync(id);
            if (appointment == null)
                return new RemoveResponse<AppointmentDto>(
                    ErrorStatus,
                    "The Appointment doesn't exist", null
                );

            await _repository.RemoveAppointmentAsync(appointment);

            var appointmentDto = appointment.ToDto();

            return new RemoveResponse<AppointmentDto>(
                SuccessStatus,
                "The Appointment was removed", appointmentDto
            );
        }
        catch (Exception ex)
        {
            return new RemoveResponse<AppointmentDto>(
                ErrorStatus, $"{ex.Message}", null
            );
        }
    }
}