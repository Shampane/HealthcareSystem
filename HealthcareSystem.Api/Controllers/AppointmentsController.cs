using FluentValidation;
using FluentValidation.Results;
using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Application.Mappings;
using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public class AppointmentsController : ControllerBase {
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IValidator<Appointment> _validator;

    public AppointmentsController(
        IAppointmentRepository appointmentRepository,
        IScheduleRepository scheduleRepository,
        IAuthRepository authRepository,
        IValidator<Appointment> validator
    ) {
        _appointmentRepository = appointmentRepository;
        _scheduleRepository = scheduleRepository;
        _authRepository = authRepository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAppointments(
        [FromQuery] AppointmentRequests.GetAppointmentsRequest request,
        CancellationToken ct
    ) {
        ICollection<Appointment>? list =
            await _appointmentRepository.GetAppointments(
                request.DoctorId, request.UserId, request.PageIndex,
                request.PageSize, request.StartTime, request.EndTime, ct
            );
        if (list is null) {
            return NotFound(ResponsesMessages.NotFound("Appointments not found"));
        }
        IEnumerable<AppointmentDto> listDto = list.Select(a => a.ToDto());
        return Ok(listDto);
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetAppointmentById(
        Guid Id, CancellationToken ct
    ) {
        Appointment? appointment =
            await _appointmentRepository.GetAppointmentById(Id, ct);
        if (appointment is null) {
            return NotFound(ResponsesMessages.NotFound("Appointment not found"));
        }
        return Ok(appointment.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment(
        [FromBody] AppointmentRequests.CreateAppointmentRequest request,
        CancellationToken ct
    ) {
        Schedule? schedule =
            await _scheduleRepository.GetScheduleById(request.ScheduleId, ct);
        if (schedule is null) {
            return NotFound(ResponsesMessages.NotFound("Schedule not found"));
        }
        User? user = await _authRepository.GetUserByEmail(request.UserEmail);
        if (user is null) {
            return NotFound(ResponsesMessages.NotFound("User not found"));
        }

        Appointment appointment = new() {
            DoctorId = schedule.DoctorId,
            DoctorName = schedule.DoctorName,
            ScheduleId = schedule.Id,
            StartTime = schedule.StartTime,
            EndTime = schedule.EndTime,
            UserId = user.Id,
            UserName = user.UserName!
        };
        ValidationResult? result = await _validator.ValidateAsync(appointment, ct);
        if (!result.IsValid) {
            IEnumerable<string> errors = result.Errors.Select(e => e.ErrorMessage);
            return BadRequest(errors);
        }
        await _appointmentRepository.CreateAppointment(appointment, ct);
        return Created($"api/appointments/{appointment.Id}", appointment.ToDto());
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> RemoveAppointment(
        Guid Id, CancellationToken ct
    ) {
        Appointment? appointment =
            await _appointmentRepository.GetAppointmentById(Id, ct);
        if (appointment is null) {
            return NotFound(ResponsesMessages.NotFound("Appointment not found"));
        }
        await _appointmentRepository.RemoveAppointment(appointment, ct);
        return NoContent();
    }
}