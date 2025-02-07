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
[Route("api/doctors")]
public class DoctorsController : ControllerBase {
    private readonly IDoctorRepository _doctorRepository;
    private readonly IValidator<Doctor> _validator;

    public DoctorsController(
        IDoctorRepository doctorRepository, IValidator<Doctor> validator
    ) {
        _doctorRepository = doctorRepository;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctors(
        [FromQuery] DoctorRequests.GetDoctorsRequest request,
        CancellationToken ct
    ) {
        ICollection<Doctor>? list = await _doctorRepository.GetDoctors(
            request.PageIndex, request.PageSize, request.SortField,
            request.SortOrder, request.SearchField, request.SearchValue, ct
        );
        if (list is null) {
            return NotFound(ResponsesMessages.NotFound("Doctors not found"));
        }
        IEnumerable<DoctorDto> listDto = list.Select(d => d.ToDto());
        return Ok(listDto);
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetDoctorById(
        Guid Id, CancellationToken ct
    ) {
        Doctor? doctor = await _doctorRepository.GetDoctorById(Id, ct);
        if (doctor is null) {
            return NotFound(ResponsesMessages.NotFound("Doctor not found"));
        }
        return Ok(doctor.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor(
        [FromBody] DoctorRequests.CreateDoctorRequest request,
        CancellationToken ct
    ) {
        Doctor doctor = new() {
            Name = request.Name,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
            ExperienceAge = request.ExperienceAge,
            FeeInDollars = request.FeeInDollars,
            Specialization = request.Specialization,
            PhoneNumber = request.PhoneNumber
        };
        ValidationResult? result = await _validator.ValidateAsync(doctor, ct);
        if (!result.IsValid) {
            IEnumerable<string> errors = result.Errors.Select(e => e.ErrorMessage);
            return BadRequest(errors);
        }

        await _doctorRepository.CreateDoctor(doctor, ct);
        return Created($"api/doctors/{doctor.Id}", doctor.ToDto());
    }

    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdateDoctor(
        Guid Id,
        DoctorRequests.UpdateDoctorRequest request,
        CancellationToken ct
    ) {
        Doctor? doctor = await _doctorRepository.GetDoctorById(Id, ct);
        if (doctor is null) {
            return NotFound(ResponsesMessages.NotFound("Doctor not found"));
        }
        await _doctorRepository.UpdateDoctor(
            doctor, request.Name, request.Description, request.ImageUrl,
            request.ExperienceAge, request.FeeInDollars,
            request.Specialization, request.PhoneNumber, ct
        );

        return Ok(ResponsesMessages.UpdatedIdName(
            "doctor", doctor.Id.ToString(), doctor.Name
        ));
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> RemoveDoctor(
        Guid Id, CancellationToken ct
    ) {
        Doctor? doctor = await _doctorRepository.GetDoctorById(Id, ct);
        if (doctor is null) {
            return NotFound(ResponsesMessages.NotFound("Doctor not found"));
        }
        await _doctorRepository.RemoveDoctor(doctor, ct);

        return NoContent();
    }
}