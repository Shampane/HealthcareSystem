using HealthcareSystem.Application.Dtos;
using HealthcareSystem.Application.Mappings;
using HealthcareSystem.Application.Requests;
using HealthcareSystem.Core.Entities;
using HealthcareSystem.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Controllers;

[ApiController]
[Route("api/schedules")]
public class SchedulesController : ControllerBase {
    private readonly IDoctorRepository _doctorRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public SchedulesController(
        IDoctorRepository doctorRepository, IScheduleRepository scheduleRepository
    ) {
        _doctorRepository = doctorRepository;
        _scheduleRepository = scheduleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedules(
        [FromQuery] ScheduleRequests.GetSchedulesRequest request,
        CancellationToken ct
    ) {
        ICollection<Schedule>? list = await _scheduleRepository.GetSchedules(
            request.DoctorId, request.PageIndex, request.PageSize,
            request.StartTime, request.EndTime
        );
        if (list is null) {
            return NotFound();
        }
        IEnumerable<ScheduleDto> listDto = list.Select(s => s.ToDto());
        return Ok(listDto);
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetScheduleById(
        Guid Id, CancellationToken ct
    ) {
        Schedule? schedule = await _scheduleRepository.GetScheduleById(Id);
        if (schedule is null) {
            return NotFound();
        }
        return Ok(schedule.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchedule(
        [FromBody] ScheduleRequests.CreateScheduleRequest request,
        CancellationToken ct
    ) {
        Doctor? doctor = await _doctorRepository.GetDoctorById(request.DoctorId);
        if (doctor is null) {
            return NotFound();
        }
        Schedule schedule = new() {
            DoctorId = request.DoctorId,
            DoctorName = doctor.Name,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };
        await _scheduleRepository.CreateSchedule(schedule);
        return Created($"api/schedules/{schedule.Id}", schedule.ToDto());
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> RemoveSchedule(
        Guid Id, CancellationToken ct
    ) {
        Schedule? schedule = await _scheduleRepository.GetScheduleById(Id);
        if (schedule is null) {
            return NotFound();
        }
        await _scheduleRepository.RemoveSchedule(schedule);
        return NoContent();
    }
}