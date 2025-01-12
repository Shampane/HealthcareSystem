using HealthcareSystem.Core.Appointments;
using HealthcareSystem.Infrastructure.Appointments;

namespace HealthcareSystem.Application.Appointments;

public class AppointmentService(IAppointmentRepository repository)
{
    private readonly IAppointmentRepository _repository = repository;

    public async Task<AppointmentResponse> CreateAsync(
        AppointmentRequest request
    )
    {
        try
        {
            var user = await _repository.FindUserByIdAsync(request.UserId);
            if (user == null)
                return new AppointmentResponse(
                    400, false,
                    "Error: the User doesn't exist"
                );
            var doctor =
                await _repository.FindDoctorByIdAsync(request.DoctorId);
            if (doctor == null)
                return new AppointmentResponse(
                    400, false,
                    "Error: the Doctor doesn't exist"
                );
            var schedule = await _repository.FindScheduleByIdAsync(
                request.ScheduleId
            );
            if (schedule == null)
                return new AppointmentResponse(
                    400, false,
                    "Error: the Schedule doesn't exist"
                );

            var appointment = new Appointment
            {
                DoctorId = request.DoctorId,
                Doctor = doctor,
                ScheduleId = request.ScheduleId,
                Schedule = schedule,
                UserId = request.UserId,
                User = user
            };
            await _repository.CreateAsync(appointment);
            return new AppointmentResponse(
                201, false,
                "The Appointment was created successfully"
            );
        }
        catch (Exception ex)
        {
            return new AppointmentResponse(
                404, false, $"Error: {ex.Message}"
            );
        }
    }
}