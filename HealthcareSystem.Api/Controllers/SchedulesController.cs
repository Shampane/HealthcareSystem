using FluentValidation;
using FluentValidation.Results;
using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Application.Mappings;
using HealthcareSystem.Application.Requests;
using HealthcareSystem.Application.Responses;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class SchedulesController : ControllerBase {
    private readonly IDoctorRepository _doctorRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IValidator<Schedule> _validator;

    public SchedulesController(
        IDoctorRepository doctorRepository,
        IScheduleRepository scheduleRepository,
        IValidator<Schedule> validator
    ) {
        _doctorRepository = doctorRepository;
        _scheduleRepository = scheduleRepository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedules(
        [FromQuery] ScheduleRequests.GetSchedulesRequest request,
        CancellationToken ct
    ) {
        ICollection<Schedule>? list = await _scheduleRepository.GetSchedules(
            request.DoctorId, request.PageIndex, request.PageSize,
            request.StartTime, request.EndTime, ct
        );
        if (list is null) {
            return NotFound(ResponsesMessages.NotFound("Schedules not found"));
        }

        IEnumerable<ScheduleDto> listDto = list.Select(s => s.ToDto());
        return Ok(listDto);
    }

    [HttpGet("{DoctorId:guid}")]
    public async Task<IActionResult> GetSchedulesByDoctorId(
        Guid DoctorId, CancellationToken ct
    ) {
        ICollection<Schedule>? list =
            await _scheduleRepository.GetSchedulesByDoctorId(DoctorId, ct);
        if (list is null) {
            return NotFound(ResponsesMessages.NotFound("Schedules not found"));
        }

        IEnumerable<ScheduleDto> listDto = list.Select(s => s.ToDto());
        return Ok(listDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(
        [FromBody] ScheduleRequests.CreateScheduleRequest request,
        CancellationToken ct
    ) {
        Doctor? doctor = await _doctorRepository.GetDoctorById(request.DoctorId, ct);
        if (doctor is null) {
            return NotFound(ResponsesMessages.NotFound("Doctor not found"));
        }

        Schedule schedule = new() {
            DoctorId = request.DoctorId,
            DoctorName = doctor.Name,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };
        ValidationResult? result = await _validator.ValidateAsync(schedule, ct);
        if (!result.IsValid) {
            IEnumerable<string> errors = result.Errors.Select(e => e.ErrorMessage);
            return BadRequest(errors);
        }

        if (!await _scheduleRepository.IsTimeAvailable(schedule, ct)) {
            return BadRequest("Schedule time is not available");
        }

        await _scheduleRepository.CreateSchedule(schedule, ct);
        return Created($"api/schedules/{schedule.Id}", schedule.ToDto());
    }

    [HttpPatch("{Id:guid}/toggle")]
    public async Task<IActionResult> UpdateScheduleAvailable(
        Guid Id,
        [FromBody] JsonPatchDocument<Schedule> patchDoc,
        CancellationToken ct
    ) {
        Schedule? schedule = await _scheduleRepository.GetScheduleById(Id, ct);
        if (schedule is null) {
            return NotFound(ResponsesMessages.NotFound("Schedule not found"));
        }

        patchDoc.ApplyTo(schedule, ModelState);
        if (!TryValidateModel(schedule)) {
            return BadRequest(ModelState);
        }

        await _scheduleRepository.UpdateScheduleAvailable(schedule, ct);
        if (schedule.IsAvailable == false) {
            return Ok(ResponsesMessages.UpdatedIdTime(
                "schedule", schedule.Id.ToString(), schedule.StartTime,
                schedule.EndTime
            ));
        }

        return BadRequest("Update available was not successful");
    }


    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> RemoveSchedule(
        Guid Id, CancellationToken ct
    ) {
        Schedule? schedule = await _scheduleRepository.GetScheduleById(Id, ct);
        if (schedule is null) {
            return NotFound(ResponsesMessages.NotFound("Schedule not found"));
        }

        await _scheduleRepository.RemoveSchedule(schedule, ct);
        return NoContent();
    }
}